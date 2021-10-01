using BencodeNET.Parsing;
using BencodeNET.Torrents;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;

namespace R5_Reloaded_Installer
{
    public class Download : IDisposable
    {
        private static string Aria2Path;
        private static string Aria2ExecutableFileName = "aria2c.exe";
        private static string Argument = "--seed-time=0";

        public Download()
        {
            ConsoleExpansion.LogWrite("Preparing for download.");
            Aria2Path = Path.Combine(RunZip(WebGetLink.GetAria2Link(), "aria2", false), Aria2ExecutableFileName);
            ConsoleExpansion.LogWrite("Success.");
            ConsoleExpansion.LogWrite("Start downloading the file.");
        }
        public void Dispose()
        {
            ConsoleExpansion.LogWrite("The download end process is in progress.");
            DirectoryExpansion.AllDelete(Path.GetDirectoryName(Aria2Path));
            ConsoleExpansion.LogWrite("All files have been downloaded.");
        }

        private static string Run(string url, string fileName = null, bool log = true)
        {
            var FileName = fileName != null ? fileName + Path.GetExtension(url) : Path.GetFileName(url);
            if (File.Exists(FileName)) File.Delete(FileName);
            if(log) ConsoleExpansion.LogWrite("Downloading " + Path.GetFileName(url) + " file.");
            try
            {
                new WebClient().DownloadFile(url, FileName);
            }
            catch
            {
                ConsoleExpansion.LogError("Failed to download the file.");
                ConsoleExpansion.Exit();
            }
            return FileName;
        }
        public static string RunZip(string url, string directoryName = null, bool log = true)
        {
            if (GetExtension(url) != "zip")
            {
                ConsoleExpansion.LogDebug("The url specified in the ExtractZip function is not a zip file.");
                ConsoleExpansion.Exit();
                return null;
            }

            var DirectoryName = directoryName != null ? directoryName : Path.GetFileName(url).Replace(Path.GetExtension(url), "");

            var FileName = Run(url, DirectoryName, log);
            if (log) ConsoleExpansion.LogWrite("Unzip the zip file.");

            if (Directory.Exists(DirectoryName)) DirectoryExpansion.AllDelete(DirectoryName);
            Directory.CreateDirectory(DirectoryName);

            try
            {
                ZipFile.ExtractToDirectory(FileName, DirectoryName);
                File.Delete(FileName);
            }
            catch
            {
                ConsoleExpansion.LogError("Failed to extract Zip file.");
                ConsoleExpansion.Exit();
            }

            var files = Directory.GetFiles(DirectoryName);
            var dirs = Directory.GetDirectories(DirectoryName);
            if (files.Length == 0 && dirs.Length == 1)
            {
                Directory.Move(dirs[0], DirectoryName + "_Buffer");
                Directory.Delete(DirectoryName);
                Directory.Move(DirectoryName + "_Buffer", DirectoryName);
            }
            if (log) ConsoleExpansion.LogWrite("Success.");
            return DirectoryName;
        }
        public static string RunTorrent(string url, string directoryName = null)
        {
            if (GetExtension(url) != "torrent")
            {
                ConsoleExpansion.LogDebug("The url specified in the ExtractZip function is not a torrent file.");
                ConsoleExpansion.Exit();
                return null;
            }

            var FileName = Run(url);

            var DriveInfo = new DriveInfo(Path.GetPathRoot(Process.GetCurrentProcess().MainModule.FileName));
            var TorerntByteSize = new BencodeParser().Parse<Torrent>(FileName).TotalSize;
            var DriveByteSize = DriveInfo.AvailableFreeSpace;
            ConsoleExpansion.LogWrite("Torrent Download Size : " + ByteToGByte(TorerntByteSize) + " GByte");
            ConsoleExpansion.LogWrite(DriveInfo.Name + " Drive Free Space  : " + ByteToGByte(DriveByteSize) + " GByte");
            if (TorerntByteSize > DriveByteSize)
            {
                ConsoleExpansion.LogError("There is not enough disk space.");
                ConsoleExpansion.LogError("Do you want to force the installation?");
                ConsoleExpansion.LogError("An error may occur.");
                if (!ConsoleExpansion.ConsentInput()) ConsoleExpansion.Exit();
            }
            ConsoleExpansion.LogWrite("Success.");

            ConsoleExpansion.LogWrite("Continue downloading the torrent.");
            ConsoleExpansion.LogWrite("It may take a few moments depending on the status of the destination.");
            Console.WriteLine();
            ConsoleExpansion.WriteWidth('=', "Download with aria2");
            Process aria2Process = new Process();
            aria2Process.StartInfo.FileName = Aria2Path;
            aria2Process.StartInfo.Arguments = FileName + " " + Argument;
            aria2Process.Start();
            aria2Process.WaitForExit();
            aria2Process.Close();
            ConsoleExpansion.WriteWidth('=');
            ConsoleExpansion.LogWrite("Success.");
            Console.WriteLine();

            var rawName = FileName.Replace(Path.GetExtension(FileName), "");
            if (directoryName != null)
            {
                Directory.Move(rawName, directoryName);
                return directoryName;
            }
            else
            {
                return rawName;
            }
        }
        private static string GetExtension(string url) => Path.GetExtension(url).Replace(".", "").ToLower();
        private static float ByteToGByte(long value) => value / 1024f / 1024f / 1024f;
    }
}
