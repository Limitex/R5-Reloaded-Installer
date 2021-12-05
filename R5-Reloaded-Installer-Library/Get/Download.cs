using R5_Reloaded_Installer_Library.External;
using R5_Reloaded_Installer_Library.IO;
using R5_Reloaded_Installer_Library.Other;
using R5_Reloaded_Installer_Library.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace R5_Reloaded_Installer_Library.Get
{
    public enum ApplicationType
    {
        Aria2c,
        Transmission,
        SevenZip,
        HttpClient
    }

    public delegate void ProcessEventHandler(ApplicationType appType, string outLine);

    public class Download : IDisposable
    {
        public event ProcessEventHandler? ProcessReceives = null;

        private static string WorkingDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "R5-Reloaded-Installer");
        private string SaveingDirectoryPath;

        private ResourceProcess aria2c;
        private ResourceProcess sevenZip;
        private ResourceProcess transmission;
        private HttpClientProgress? httpClient = null;

        public Download(string saveingDirectoryPath)
        {
            SaveingDirectoryPath = saveingDirectoryPath;
            DirectoryExpansion.CreateOverwrite(WorkingDirectoryPath);

            aria2c = new ResourceProcess(WorkingDirectoryPath, "aria2c");
            sevenZip = new ResourceProcess(WorkingDirectoryPath, "seven-za");
            transmission = new ResourceProcess(WorkingDirectoryPath, "transmission");

            aria2c.ResourceProcessReceives += new ResourceProcessEventHandler(Aria2cProcess_EventHandler);
            sevenZip.ResourceProcessReceives += new ResourceProcessEventHandler(SevenZipProcess_EventHandler);
            transmission.ResourceProcessReceives += new ResourceProcessEventHandler(TransmissionProcess_EventHandler);
        }

        public void Dispose()
        {
            aria2c.Dispose();
            sevenZip.Dispose();
            transmission.Dispose();
            httpClient?.Dispose();
            DirectoryExpansion.DirectoryDelete(WorkingDirectoryPath);
        }

        public void Kill()
        {
            aria2c.Kill();
            aria2c.Dispose();
            sevenZip.Kill();
            sevenZip.Dispose();
            transmission.Kill();
            transmission.Dispose();
            httpClient?.Dispose();
        }

        public string Run(string address, string? name = null, string? path = null, ApplicationType? appType = null)
        {
            switch (Path.GetExtension(address).ToLower())
            {
                case ".zip":
                case ".7z":
                    return NormalDownload(address, name, path, appType);
                case ".torrent":
                    return TorrentDownload(address, name, path, appType);
                default:
                    throw new Exception("The specified address cannot be downloaded with.");
            }
        }

        public void DirectoryFix(string sourceDirName)
        {
            var files = Directory.GetFiles(sourceDirName);
            var dirs = Directory.GetDirectories(sourceDirName);
            if (files.Length == 0 && dirs.Length == 1)
            {
                Directory.Move(dirs[0], sourceDirName + "_buffer");
                Directory.Delete(sourceDirName);
                Directory.Move(sourceDirName + "_buffer", sourceDirName);
            }
        }

        private string NormalDownload(string address, string? name = null, string? path = null, ApplicationType? appType = null)
        {
            string filedirPath;
            switch (appType)
            {
                case ApplicationType.Aria2c:
                case null:
                    filedirPath = Aria2c(address, name, path);
                    break;
                case ApplicationType.HttpClient:
                    filedirPath = HttpClientDownload(address, name, path);
                    break;
                default:
                    throw new Exception("Specify \"Aria2c\" or \"HttpClient\" for the app type.");
            }
            var dirPath = SevenZip(filedirPath, name, path);
            DirectoryFix(dirPath);
            return dirPath;
        }

        private string TorrentDownload(string address, string? name = null, string? path = null, ApplicationType? appType = null)
        {
            string torrentdirPath;
            switch (appType)
            {
                case ApplicationType.Aria2c:
                case null:
                    torrentdirPath = Aria2c(address, name, path);
                    break;
                case ApplicationType.Transmission:
                    torrentdirPath = Transmission(address, name, path);
                    break;
                default:
                    throw new Exception("Specify \"Aria2c\" or \"Transmission\" for the app type.");
            }
            DirectoryFix(torrentdirPath);
            return torrentdirPath;
        }

        private string Aria2c(string address, string? name = null, string? path = null)
        {
            var extension = Path.GetExtension(address);
            var fileName = name == null ? Path.GetFileName(address) : name + extension;
            var dirPath = path ?? SaveingDirectoryPath;
            var dirName = Path.GetFileNameWithoutExtension(fileName);
            var resurtPath = Path.Combine(dirPath, dirName);
            var argument = " --dir=\"" + resurtPath + "\" --out=\"" + fileName + "\" --seed-time=0 --allow-overwrite=true --follow-torrent=mem";
            DirectoryExpansion.CreateOverwrite(resurtPath);
            aria2c.Run(address + argument, resurtPath);
            return extension != ".torrent" ? Path.Combine(resurtPath, fileName) : resurtPath;
        }

        private string Transmission(string address, string? name = null, string? path = null)
        {
            var dirPath = path ?? SaveingDirectoryPath;
            var dirName = name ?? Path.GetFileNameWithoutExtension(address);
            var resurtPath = Path.Combine(dirPath, dirName);
            var argument = " --download-dir \"" + resurtPath + "\" --config-dir \"" + WorkingDirectoryPath + "\" -u 0";
            DirectoryExpansion.CreateOverwrite(resurtPath);
            transmission.Run(address + argument, dirPath);
            return resurtPath;
        }

        private string SevenZip(string address, string? name = null, string? path = null)
        {
            var dirPath = path ?? SaveingDirectoryPath;
            var dirName = name ?? Path.GetFileNameWithoutExtension(address);
            var resurtPath = Path.Combine(dirPath, dirName);
            var argument = "x -y \"" + address + "\" -o\"" + resurtPath + "\"";
            DirectoryExpansion.CreateIfNotFound(resurtPath);
            sevenZip.Run(argument, resurtPath);
            File.Delete(address);
            return resurtPath;
        }

        private string HttpClientDownload(string address, string? name = null, string? path = null)
        {
            var fileName = name == null ? Path.GetFileName(address) : name + Path.GetExtension(address);
            var dirPath = path ?? SaveingDirectoryPath;
            var dirName = Path.GetFileNameWithoutExtension(fileName);
            var resurtPath = Path.Combine(dirPath, dirName);
            var filePath = Path.Combine(resurtPath, fileName);
            DirectoryExpansion.CreateOverwrite(resurtPath);
            httpClient = new HttpClientProgress(address, filePath);
            httpClient.ProgressChanged += new ProgressChangedHandler(HttpClientProcess_EventHandler);
            httpClient.StartDownload().Wait();
            return filePath;
        }

        private string FormattingLine(string str) => Regex.Replace(str, @"(\r|\n|(  )|\t|\x1b\[.*?m)", string.Empty);

        private void Aria2cProcess_EventHandler(object sender, DataReceivedEventArgs outLine)
        {
            if (ProcessReceives == null) return;
            if (string.IsNullOrEmpty(outLine.Data)) return;
            var rawLine = FormattingLine(outLine.Data);

            if (rawLine[0] == '[')
            {
                var nakedLine = Regex.Replace(rawLine, @"((#.{6}( ))|\[|\])", "");
                if (rawLine.Contains("FileAlloc"))
                    ProcessReceives(ApplicationType.Aria2c ,nakedLine.Substring(nakedLine.IndexOf("FileAlloc")));
                else
                    ProcessReceives(ApplicationType.Aria2c, nakedLine);
            }
            else if (rawLine[0] == '(')
            {
                ProcessReceives(ApplicationType.Aria2c, rawLine);
            }
            else if (rawLine.Contains("NOTICE"))
            {
                var nakedLine = Regex.Replace(rawLine, @"([0-9]{2}/[0-9]{2})( )([0-9]{2}:[0-9]{2}:[0-9]{2})( )\[NOTICE\]( )", string.Empty);
                ProcessReceives(ApplicationType.Aria2c, nakedLine);
            }
        }

        private void SevenZipProcess_EventHandler(object sender, DataReceivedEventArgs outLine)
        {
            if (ProcessReceives == null) return;
            if (string.IsNullOrEmpty(outLine.Data)) return;
            var rawLine = FormattingLine(outLine.Data);

            ProcessReceives(ApplicationType.SevenZip, rawLine);
        }

        private void TransmissionProcess_EventHandler(object sender, DataReceivedEventArgs outLine)
        {
            if (ProcessReceives == null) return;
            if (string.IsNullOrEmpty(outLine.Data)) return;
            var rawLine = FormattingLine(outLine.Data);

            if (rawLine.Contains("Seeding"))
            {
                transmission.Kill();
                return;
            }

            var nakedLine = Regex.Replace(rawLine, @"(\[([0-9]{4})-([0-9]{2})-([0-9]{2})( )([0-9]{2}):([0-9]{2}):([0-9]{2})\.(.*?)\])( )", string.Empty);
            
            var dirName = Path.GetFileNameWithoutExtension(Regex.Match(((string[])sender)[0], "http.*?(?=( ))").ToString());
            if (!Regex.Match(nakedLine, dirName + ":").Success)
            {
                ProcessReceives(ApplicationType.Transmission, Regex.Replace(nakedLine, @", ul to 0 \(0 kB/s\) \[(0\.00|None)\]", string.Empty));
                Thread.Sleep(200);
            }
        }

        private void HttpClientProcess_EventHandler(long? totalFileSize, long totalBytesDownloaded, double? progressPercentage)
        {
            if (ProcessReceives == null) return;
            var downloadedByteSize = StringProcessing.ByteToStringWithUnits(totalBytesDownloaded);
            var totalByteSize = StringProcessing.ByteToStringWithUnits(totalFileSize ?? 0);
            var progressPercent = ((int?)progressPercentage ?? 0).ToString().PadLeft(3);
            ProcessReceives(ApplicationType.HttpClient, $"{downloadedByteSize} / {totalByteSize} ({progressPercent}%)");
        }
    }
}
