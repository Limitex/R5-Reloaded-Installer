using BencodeNET.Parsing;
using BencodeNET.Torrents;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;

namespace R5_Reloaded_Installer.SharedClass
{
    public class Download : IDisposable
    {
        private static string Aria2Path;
        private static string Aria2ExecutableFileName = "aria2c.exe";
        private static string Argument = "--seed-time=0";

        public Download(string directoryPath = "")
        {
            ConsoleExpansion.LogWrite("Preparing for download.");
            Aria2Path = Path.Combine(RunZip(WebGetLink.GetAria2Link(), directoryPath, "aria2" , false), Aria2ExecutableFileName);
            ConsoleExpansion.LogWrite("Success.");
            ConsoleExpansion.LogWrite("Start downloading the file.");
        }
        public void Dispose()
        {
            ConsoleExpansion.LogWrite("The download end process is in progress.");
            DirectoryExpansion.AllDelete(Path.GetDirectoryName(Aria2Path));
            ConsoleExpansion.LogWrite("All files have been downloaded.");
        }

        public static string Run(string url, string directoryPath = "", string fileName = null, bool log = true)
        {
            var FileName = fileName != null ? fileName + Path.GetExtension(url) : Path.GetFileName(url);
            var FilePath = Path.Combine(directoryPath, FileName);
            if (File.Exists(FilePath)) File.Delete(FilePath);
            if(log) ConsoleExpansion.LogWrite("Downloading " + Path.GetFileName(url) + " file.");
            try
            {
                new WebClient().DownloadFile(url, FilePath);
            }
            catch
            {
                ConsoleExpansion.LogError("Failed to download the file.");
                ConsoleExpansion.Exit();
            }
            return FilePath;
        }
        public static string RunZip(string url, string directoryPath = "", string directoryName = null, bool log = true)
        {
            if (GetExtension(url) != "zip")
            {
                ConsoleExpansion.LogDebug("The url specified in the ExtractZip function is not a zip file.");
                ConsoleExpansion.Exit();
                return null;
            }

            var DirectoryName = directoryName != null ? directoryName : Path.GetFileName(url).Replace(Path.GetExtension(url), "");
            var DirectoryPath = Path.Combine(directoryPath, DirectoryName);

            var FileName = Run(url, directoryPath, DirectoryName, log);
            if (log) ConsoleExpansion.LogWrite("Unzip the zip file.");

            if (Directory.Exists(DirectoryPath)) DirectoryExpansion.AllDelete(DirectoryPath);
            Directory.CreateDirectory(DirectoryPath);

            try
            {
                ZipFile.ExtractToDirectory(FileName, DirectoryPath);
                File.Delete(FileName);
            }
            catch
            {
                ConsoleExpansion.LogError("Failed to extract Zip file.");
                ConsoleExpansion.Exit();
            }

            var files = Directory.GetFiles(DirectoryPath);
            var dirs = Directory.GetDirectories(DirectoryPath);
            if (files.Length == 0 && dirs.Length == 1)
            {
                Directory.Move(dirs[0], DirectoryName + "_Buffer");
                Directory.Delete(DirectoryPath);
                Directory.Move(DirectoryPath + "_Buffer", DirectoryPath);
            }
            if (log) ConsoleExpansion.LogWrite("Success.");
            return DirectoryPath;
        }
        public static string RunTorrent(string url, string directoryPath = "", string directoryName = null)
        {
            if (GetExtension(url) != "torrent")
            {
                ConsoleExpansion.LogDebug("The url specified in the ExtractZip function is not a torrent file.");
                ConsoleExpansion.Exit();
                return null;
            }

            var FilePath = Run(url, directoryPath);

            var DriveInfo = new DriveInfo(Path.GetPathRoot(Process.GetCurrentProcess().MainModule.FileName));
            var TorerntByteSize = new BencodeParser().Parse<Torrent>(FilePath).TotalSize;
            var DriveByteSize = DriveInfo.AvailableFreeSpace;
            ConsoleExpansion.LogWrite("Torrent Download Size : " + GetFileSize.ByteToGByte(TorerntByteSize) + " GByte");
            ConsoleExpansion.LogWrite(DriveInfo.Name + " Drive Free Space  : " + GetFileSize.ByteToGByte(DriveByteSize) + " GByte");
            if (TorerntByteSize > DriveByteSize)
            {
                ConsoleExpansion.LogError("There is not enough disk space.");
                ConsoleExpansion.LogError("Do you want to force the installation?");
                ConsoleExpansion.LogError("An error may occur.");
                if (!ConsoleExpansion.ConsentInput()) ConsoleExpansion.Exit();
            }
            ConsoleExpansion.LogWrite("Success.");

            ConsoleExpansion.LogWrite("Continue downloading the torrent.");
            ConsoleExpansion.LogNotes("It may take a few moments depending on the status of the destination.");
            ConsoleExpansion.WriteWidth('=', "Download with aria2");
            Process aria2Process = new Process();
            aria2Process.StartInfo.FileName = Aria2Path;
            aria2Process.StartInfo.Arguments = FilePath + " " + Argument;
            aria2Process.StartInfo.WorkingDirectory = directoryPath;
            aria2Process.Start();
            aria2Process.WaitForExit();
            aria2Process.Close();
            ConsoleExpansion.WriteWidth('=');
            ConsoleExpansion.LogWrite("Success.");

            ConsoleExpansion.LogNotes("This program stops seeding, but If possible, Please use torrent software to seed.");
            ConsoleExpansion.LogNotes("No one may be able to download it from torrents.");
            ConsoleExpansion.LogNotes("The torrent file exists in the directory.");

            var rawPath = FilePath.Replace(Path.GetExtension(FilePath), "");
            var DirectoryPath = Path.Combine(directoryPath, directoryName);
            if (directoryName != null)
            {
                Directory.Move(rawPath, DirectoryPath);
                return directoryName;
            }
            else
            {
                return rawPath;
            }
        }
        private static string GetExtension(string url) => Path.GetExtension(url).Replace(".", "").ToLower();
    }
}
