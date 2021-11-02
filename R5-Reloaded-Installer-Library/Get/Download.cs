using R5_Reloaded_Installer_Library.IO;
using R5_Reloaded_Installer_Library.JobObjectSharp;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text.RegularExpressions;

namespace R5_Reloaded_Installer_Library.Get
{
    public delegate void WebClientProcessEventHandler(object sender, DownloadProgressChangedEventArgs e);
    public delegate void TransmissionProcessEventHandler(object sender, DataReceivedEventArgs outLine);
    //public delegate void Aria2ProcessEventHandler(object sender, DataReceivedEventArgs outLine);

    public class Download : IDisposable
    {
        public string WorkingDirectoryPath { get; private set; }
        public string SaveingDirectoryPath { get; private set; }

        public event WebClientProcessEventHandler WebClientReceives = null;
        public event TransmissionProcessEventHandler TransmissionProcessReceives = null;
        //public event Aria2ProcessEventHandler Aria2ProcessReceives = null;

        //private static readonly string Aria2Argument = "--seed-time=0 --allow-overwrite=true";
        //private static readonly string Aria2ExecutableFileName = "aria2c.exe";

        private static readonly string TransmissionArgument = "-u 0";
        private static readonly string TransmissionExecutableFileName = "transmission-cli.exe";

        private static readonly string WorkingDirectoryName = "R5-Reloaded-Installer";

        //private static string Aria2Path;
        private static string TransmissionPath;

        private Process Transmission = null;
        //private Process Aria2c = null;

        private bool IsRunning = true;

        public Download(string saveingDirectoryPath = null)
        {
            WorkingDirectoryPath = Path.Combine(DirectoryExpansion.AppDataDirectoryPath, WorkingDirectoryName);
            if (saveingDirectoryPath != null) SaveingDirectoryPath = saveingDirectoryPath;
            else SaveingDirectoryPath = DirectoryExpansion.RunningDirectoryPath;

            if (!Directory.Exists(SaveingDirectoryPath)) Directory.CreateDirectory(SaveingDirectoryPath);
            if (Directory.Exists(WorkingDirectoryPath)) DirectoryExpansion.DeleteAll(WorkingDirectoryPath);
            Directory.CreateDirectory(WorkingDirectoryPath);

            // Aria2Path = Path.Combine(RunZip(GetAria2Link(), "aria2", WorkingDirectoryPath), Aria2ExecutableFileName);

            TransmissionPath = Path.Combine(RunZip(GetTransmissionLink(), "transmission", WorkingDirectoryPath),
                    TransmissionExecutableFileName);
        }

        public void Dispose()
        {
            DirectoryExpansion.DeleteAll(WorkingDirectoryPath);
        }

        //private static string GetAria2Link()
        //{
        //    foreach (var link in GitHub.GetLatestRelease("aria2", "aria2"))
        //        if (Path.GetFileName(link).Contains("win-64bit"))
        //            return link;
        //    return null;
        //}

        private static string GetTransmissionLink()
        {
            foreach (var link in GitHub.GetLatestRelease("Limitex", "transmission-vs"))
                if (Path.GetFileName(link).Contains("cli"))
                    return link;
            return null;
        }

        private static bool IsUrl(string address) => Regex.IsMatch(address, @"^s?https?://[-_.!~*'()a-zA-Z0-9;/?:@&=+$,%#]+$");

        public string Run(string address, string filePath)
        {
            using (var wc = new WebClient())
            {
                if (WebClientReceives != null)
                    wc.DownloadProgressChanged +=
                        new DownloadProgressChangedEventHandler((sender, e) => WebClientReceives(new string[] { address, filePath }, e));
                wc.DownloadFileTaskAsync(new Uri(address), filePath).Wait();
            }
            return filePath;
        }

        public string RunZip(string address, string name = null, string SavePath = null)
        {
            if (!IsRunning) return null;
            if (!IsUrl(address) || FileExpansion.GetExtension(address) != "zip")
                throw new Exception("The specified string does not download the ZIP file.");
            if (name == null) name = Path.GetFileName(address);
            else name += Path.GetExtension(address);
            if (SavePath == null) SavePath = SaveingDirectoryPath;

            var filePath = Run(address, Path.Combine(SavePath, name));

            var directoryPath = filePath.Remove(filePath.IndexOf(Path.GetExtension(filePath)));
            if (Directory.Exists(directoryPath)) DirectoryExpansion.DeleteAll(directoryPath);
            Directory.CreateDirectory(directoryPath);
            ZipFile.ExtractToDirectory(filePath, directoryPath);
            File.Delete(filePath);
            var files = Directory.GetFiles(directoryPath);
            var dirs = Directory.GetDirectories(directoryPath);
            if (files.Length == 0 && dirs.Length == 1)
            {
                Directory.Move(dirs[0], directoryPath + "_buffer");
                Directory.Delete(directoryPath);
                Directory.Move(directoryPath + "_buffer", directoryPath);
            }
            return directoryPath;
        }

        //public string RunTorrentOfAria2(string address, string name = null, string SavePath = null)
        //{
        //    if (!IsRunning) return null;
        //    if (!IsUrl(address) || FileExpansion.GetExtension(address) != "torrent")
        //        throw new Exception("The specified string does not download the Torrent file.");

        //    if (SavePath == null) SavePath = SaveingDirectoryPath;

        //    var filePath = Run(address, Path.Combine(SavePath, Path.GetFileName(address)));

        //    var rawDirectoryPath = filePath.Replace(Path.GetExtension(filePath), string.Empty);
        //    var directoryPath = rawDirectoryPath;
        //    if (name != null) directoryPath = Path.Combine(Path.GetDirectoryName(filePath), name);

        //    using (var job = JobObject.CreateAsKillOnJobClose())
        //    {
        //        Aria2c = new Process();
        //        Aria2c.StartInfo = new ProcessStartInfo()
        //        {
        //            FileName = Aria2Path,
        //            Arguments = filePath + " " + Aria2Argument,
        //            WorkingDirectory = SavePath,
        //            CreateNoWindow = true,
        //            UseShellExecute = false,
        //            RedirectStandardInput = true,
        //            RedirectStandardOutput = true,
        //            RedirectStandardError = true
        //        };
        //        if (Aria2ProcessReceives != null)
        //        {
        //            Aria2c.EnableRaisingEvents = true;
        //            Aria2c.ErrorDataReceived += new DataReceivedEventHandler(Aria2ProcessReceives);
        //            Aria2c.OutputDataReceived += new DataReceivedEventHandler(Aria2ProcessReceives);
        //        }
        //        Aria2c.Start();
        //        Aria2c.BeginOutputReadLine();
        //        Aria2c.BeginErrorReadLine();
        //        job.AssignProcess(Aria2c);
        //        Aria2c.WaitForExit();
        //        Aria2c.Close();
        //    }

        //    if (IsRunning && (name != null))
        //    {
        //        Directory.Move(rawDirectoryPath, directoryPath);
        //        return directoryPath;
        //    }
        //    else return rawDirectoryPath;
        //}

        public string RunTorrentOfTransmission(string address, string name = null)
        {
            if (!IsRunning) return null;
            if (!IsUrl(address) || FileExpansion.GetExtension(address) != "torrent")
                throw new Exception("The specified string does not download the Torrent file.");

            var filePath = Run(address, Path.Combine(SaveingDirectoryPath, Path.GetFileName(address)));
            var rawDirectoryPath = filePath.Replace(Path.GetExtension(filePath), string.Empty);
            var directoryName = Path.GetFileName(rawDirectoryPath);
            var directoryPath = rawDirectoryPath;
            var TorrentFileSize = FileExpansion.ByteToMByte(FileExpansion.GetTorrentFileSize(address));

            if (name != null) directoryPath = Path.Combine(Path.GetDirectoryName(filePath), name);

            using (var job = JobObject.CreateAsKillOnJobClose())
            {
                Transmission = new Process
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        FileName = TransmissionPath,
                        Arguments = address + " -w \"" + SaveingDirectoryPath + "\" -g \"" + WorkingDirectoryPath + "\" " + TransmissionArgument,
                        WorkingDirectory = SaveingDirectoryPath,
                        CreateNoWindow = true,
                        UseShellExecute = false,
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    }
                };
                if (TransmissionProcessReceives != null)
                {
                    Transmission.EnableRaisingEvents = true;
                    Transmission.ErrorDataReceived += new DataReceivedEventHandler((sender, outLine) => TransmissionProcess_EventHandler(new string[] { address, filePath, directoryName, TorrentFileSize.ToString() }, outLine));
                    Transmission.OutputDataReceived += new DataReceivedEventHandler((sender, outLine) => TransmissionProcess_EventHandler(new string[] { address, filePath, directoryName, TorrentFileSize.ToString() }, outLine));
                }
                Transmission.Start();
                Transmission.BeginOutputReadLine();
                Transmission.BeginErrorReadLine();
                job.AssignProcess(Transmission);
                Transmission.WaitForExit();
                Transmission.Close();
            }

            if (IsRunning && name != null)
            {
                Directory.Move(rawDirectoryPath, directoryPath);
                return directoryPath;
            }
            else return rawDirectoryPath;
        }

        private void TransmissionProcess_EventHandler(object sender, DataReceivedEventArgs outLine)
        {
            if (!string.IsNullOrEmpty(outLine.Data) && outLine.Data.Contains("Seeding"))
            {
                ProcessKill();
                return;
            }
            TransmissionProcessReceives(sender, outLine);
        }

        public void ProcessKill()
        {
            IsRunning = false;
            //if (Aria2c != null && !Aria2c.HasExited)
            //{
            //    Aria2c.Kill();
            //    Aria2c.Close();
            //}
            if (Transmission != null && !Transmission.HasExited)
            {
                Transmission.Kill();
                Transmission.Close();
            }
        }
    }
}
