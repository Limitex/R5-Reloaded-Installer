using R5_Reloaded_Installer_Library.IO;
using R5_Reloaded_Installer_Library.JobObjectSharp;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text.RegularExpressions;

namespace R5_Reloaded_Installer_Library.Get
{
    public class Download : IDisposable
    {
        public delegate void ProgressEventHandler(string outline);

        public string WorkingDirectoryPath { get; private set; }
        public string SaveingDirectoryPath { get; private set; }
        public static float WebClientLogUpdateInterval { get; set; } = 1f;

        private static readonly string Aria2Argument = "--seed-time=0 --allow-overwrite=true";
        private static readonly string Aria2ExecutableFileName = "aria2c.exe";
        private static readonly string WorkingDirectoryName = "R5-Reloaded-Installer";

        private static ProgressEventHandler progressEventHandler = null;
        private static string Aria2Path;
        private static float LogTimer = 0;

        public Download(ProgressEventHandler peh, string saveingDirectoryPath = null)
        {
            progressEventHandler = peh;
            
            WorkingDirectoryPath = Path.Combine(DirectoryExpansion.AppDataDirectoryPath, WorkingDirectoryName);
            if (saveingDirectoryPath != null) SaveingDirectoryPath = saveingDirectoryPath;
            else SaveingDirectoryPath = DirectoryExpansion.RunningDirectoryPath;

            if (!Directory.Exists(SaveingDirectoryPath)) Directory.CreateDirectory(SaveingDirectoryPath);
            if (Directory.Exists(WorkingDirectoryPath)) DirectoryExpansion.DeleteAll(WorkingDirectoryPath);
            Directory.CreateDirectory(WorkingDirectoryPath);

            Aria2Path = Path.Combine(RunZip(GetAria2Link(), "aria2", WorkingDirectoryPath), Aria2ExecutableFileName);
        }

        public void Dispose()
        {
            DirectoryExpansion.DeleteAll(WorkingDirectoryPath);
        }

        private static string GetAria2Link()
        {
            foreach (var link in GitHub.GetLatestRelease("aria2", "aria2"))
                if (Path.GetFileName(link).Contains("win-64bit"))
                    return link;
            return null;
        }

        public string Run(string address, string filePath)
        {
            using (var wc = new WebClient())
            {
                CallBack("info", "Start WebClient", "Address:" + address + "|To:" + filePath);
                wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(WebClient_EventHandler);
                wc.DownloadFileCompleted += new AsyncCompletedEventHandler((sender, e) => CallBack("info", "Completed WebClient"));
                wc.DownloadFileTaskAsync(new Uri(address), filePath).Wait();
            }
            return filePath;
        }

        public string RunZip(string address, string name = null, string SavePath = null)
        {
            if (!IsUrl(address) || GetExtension(address) != "zip")
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

        public string RunTorrent(string address, string name = null, string SavePath = null)
        {
            if (!IsUrl(address) || GetExtension(address) != "torrent")
                throw new Exception("The specified string does not download the Torrent file.");

            if (SavePath == null) SavePath = SaveingDirectoryPath;

            var filePath = Run(address, Path.Combine(SavePath, Path.GetFileName(address)));

            var rawDirectoryPath = filePath.Replace(Path.GetExtension(filePath), string.Empty);
            var directoryPath = rawDirectoryPath;
            if (name != null) directoryPath = Path.Combine(Path.GetDirectoryName(filePath), name);

            using (var job = JobObject.CreateAsKillOnJobClose())
            {
                var aria2c = new Process();
                aria2c.StartInfo = new ProcessStartInfo()
                {
                    FileName = Aria2Path,
                    Arguments = filePath + " " + Aria2Argument,
                    WorkingDirectory = SavePath,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };
                aria2c.EnableRaisingEvents = true;
                aria2c.Exited += new EventHandler((EHsender, EHe) => CallBack("info", "Completed Aria2Process"));
                aria2c.ErrorDataReceived += new DataReceivedEventHandler(Aria2Process_EventHandler);
                aria2c.OutputDataReceived += new DataReceivedEventHandler(Aria2Process_EventHandler);
                CallBack("info", "Start Aria2Process", "Address:" + address + "|To:" + directoryPath + "|from:" + rawDirectoryPath);
                aria2c.Start();
                aria2c.BeginOutputReadLine();
                aria2c.BeginErrorReadLine();
                job.AssignProcess(aria2c);
                aria2c.WaitForExit();
                aria2c.Close();
            }

            if (name != null)
            {
                Directory.Move(rawDirectoryPath, directoryPath);
                return directoryPath;
            }
            else return rawDirectoryPath;
        }

        private static void CallBack(string info, string status, string data = "")
        {
            if (progressEventHandler != null) progressEventHandler("[" + info + "](" + status + ")<" + data + ">");
        }

        private static void WebClient_EventHandler(object sender, DownloadProgressChangedEventArgs e)
        {
            LogTimer += Time.deltaTime;
            if (LogTimer > WebClientLogUpdateInterval || e.ProgressPercentage == 100)
            {
                LogTimer = 0;
                CallBack("log", "WebClient Download",
                    "ProgressPercentage:" + e.ProgressPercentage.ToString() +
                    "|BytesReceived:" + e.BytesReceived +
                    "|TotalBytesToReceive:" + e.TotalBytesToReceive);
            }
        }

        private static void Aria2Process_EventHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (string.IsNullOrEmpty(outLine.Data)) return;

            var rawLine = Regex.Replace(outLine.Data, @"(\r|\n|(  )|\t)", string.Empty);

            if (rawLine[0] == '[')
            {
                var nakedLine = Regex.Replace(rawLine, @"((#.{6}( ))|\[|\])", "");
                if (rawLine.Contains("FileAlloc"))
                {
                    CallBack("log", "Aria2Process Download", nakedLine.Substring(nakedLine.IndexOf("FileAlloc")));
                }
                else
                {
                    CallBack("log", "Aria2Process Download", nakedLine);
                }
            }
            else if (rawLine[0] == '(')
            {
                CallBack("log", "Aria2Process Download", rawLine);
            }
            else if (rawLine.Contains("NOTICE"))
            {
                var nakedLine = Regex.Replace(rawLine, @"([0-9]{2}/[0-9]{2})( )([0-9]{2}:[0-9]{2}:[0-9]{2})( )", string.Empty);
                CallBack("log", "Aria2Process Download", nakedLine);
            }
        }

        private static string GetExtension(string address) => Path.GetExtension(address).Replace(".", "").ToLower();

        private static bool IsUrl(string address) => Regex.IsMatch(address, @"^s?https?://[-_.!~*'()a-zA-Z0-9;/?:@&=+$,%#]+$");
        
        private static class Time
        {
            private static Stopwatch stopWatch = Stopwatch.StartNew();
            private static float timeBuffer = nowTime;
            private static float nowTime => stopWatch.ElapsedTicks * 0.0001f * 0.001f;
            private static float getDelta()
            {
                var oldTime = timeBuffer;
                timeBuffer = nowTime;
                return timeBuffer - oldTime;
            }
            public static float deltaTime => getDelta();
        }
    }
}
