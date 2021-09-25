using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using BencodeNET.Objects;
using BencodeNET.Parsing;
using BencodeNET.Torrents;

namespace R5_Reloaded_Installer
{
    static class FileOperations
    {
        private static string ZipExtension = ".zip";
        private static string Aria2ExecutableFileName = "aria2c.exe";
        public static string FlagName_Aria2 { get; private set; } = "aria2";
        public static string FlagName_detours { get; private set; } = "detours_r5";
        public static string FlagName_scripts { get; private set; } = "scripts_r5";
        public static string FlagName_apex { get; private set; } = "apex_client";
        public static string R5_ScriptsPath { get; private set; } = FlagName_apex + "\\platform\\scripts";

        private static string[] SettingFlags = { FlagName_Aria2, FlagName_detours, FlagName_scripts, FlagName_apex };

        private static Hashtable SettingData = new Hashtable();

        public static void ReadSettingFile(string path)
        {
            ConsoleExpansion.LogWriteLine("Loading config file.");
            var DownloadLinks = new Hashtable();
            try
            {
                using (var reader = new StreamReader(path))
                {
                    var rawData = Regex.Replace(reader.ReadToEnd(), @"( |　|\t)", "").Split("\r\n");
                    ConsoleExpansion.LogWriteLine("Checking the integrity of the configuration file.");
                    for (int i = 0; i < rawData.Length; i++)
                    {
                        var data = rawData[i].Split('>');
                        if (SettingFlags.Contains(data[0]))
                            DownloadLinks[data[0]] = data[1];
                        if (!Regex.IsMatch(data[1], @"^s?https?://[-_.!~*'()a-zA-Z0-9;/?:@&=+$,%#]+$"))
                        {
                            ConsoleExpansion.LogWriteLine("Contains a string that is not a URI.");
                            ConsoleExpansion.ExitConsole();
                        }
                    }
                }
            }
            catch
            {
                ConsoleExpansion.LogWriteLine("There is something wrong with the \'" + path + "\' file.");
                ConsoleExpansion.ExitConsole();
            }

            foreach (var flag in SettingFlags)
            {
                if (!DownloadLinks.ContainsKey(flag))
                {
                    ConsoleExpansion.LogWriteLine("The\'" + path + "\' file is incorrect.");
                    ConsoleExpansion.ExitConsole();
                    break;
                }
            }

            SettingData = DownloadLinks;
            ConsoleExpansion.LogWriteLine("Success.");
        }

        public static void DownloadFiles()
        {
            DownloadZipFile(FlagName_Aria2);
            DownloadZipFile(FlagName_detours);
            DownloadZipFile(FlagName_scripts);
            ExtractZipFile(FlagName_Aria2);
            ExtractZipFile(FlagName_detours);
            ExtractZipFile(FlagName_scripts);
            DownloadTorrentFile(FlagName_apex);

            ConsoleExpansion.LogWriteLine("Removing " + FlagName_Aria2 + "file.");
            DirectoryExpansion.AllDelete(FlagName_Aria2);
        }

        private static void DownloadZipFile(string flag)
        {
            ConsoleExpansion.LogWriteLine("Downloading " + flag + " file.");
            new WebClient().DownloadFile(SettingData[flag].ToString(), flag + ZipExtension);
            ConsoleExpansion.LogWriteLine("Success.");
        }

        private static void DownloadTorrentFile(string flag)
        {
            var link = SettingData[flag].ToString();
            var FileName = Path.GetFileName(link);
            var DirName = FileName.Replace(Path.GetExtension(FileName), "");
            var driveInfo = new DriveInfo(Path.GetPathRoot(Assembly.GetExecutingAssembly().Location));

            ConsoleExpansion.LogWriteLine("Checking disk capacity and download capacity.");
            new WebClient().DownloadFile(link, FileName);
            var TorerntByteSize = new BencodeParser().Parse<Torrent>(FileName).TotalSize;
            var DriveByteSize = driveInfo.AvailableFreeSpace;
            ConsoleExpansion.LogWriteLine("Torrent Download Size : " + ByteToGByte(TorerntByteSize) + " GByte");
            // if(driveInfo.IsReady)
            ConsoleExpansion.LogWriteLine("Drive Free Space: " + ByteToGByte(DriveByteSize) + " GByte");
            if (TorerntByteSize > DriveByteSize)
            {
                ConsoleExpansion.LogWriteLine("There is not enough disk space.");
                ConsoleExpansion.ExitConsole();
            }
            ConsoleExpansion.LogWriteLine("Success.");

            ConsoleExpansion.LogWriteLine("Start downloading torrents with " + FlagName_Aria2 + ".");
            ConsoleExpansion.LogWriteLine("It takes about 40GB to download, So it will take some time.\n");
            Console.WriteLine("================= Download the APEX client with aria2 =================");
            Process proc = new Process();
            proc.StartInfo.FileName = FlagName_Aria2 + "\\" + Aria2ExecutableFileName;
            proc.StartInfo.Arguments = link + " --seed-time=0";
            proc.Start();
            proc.WaitForExit();
            proc.Close();
            Console.WriteLine("=======================================================================");
            Directory.Move(DirName, flag);
            ConsoleExpansion.LogWriteLine("Success.");
            ConsoleExpansion.LogWriteLine("This program stops seeding, but If possible, Please use torrent software to seed.");
            ConsoleExpansion.LogWriteLine("No one may be able to download it from torrents.");
            ConsoleExpansion.LogWriteLine("The torrent file exists in the directory.");
        }

        private static void ExtractZipFile(string flag)
        {
            ConsoleExpansion.LogWriteLine("Extracting " + flag + " file.");

            if (Directory.Exists(flag)) DirectoryExpansion.AllDelete(flag);
            Directory.CreateDirectory(flag);

            ZipFile.ExtractToDirectory(flag + ZipExtension, flag);
            File.Delete(flag + ZipExtension);

            var files = Directory.GetFiles(flag);
            var dirs = Directory.GetDirectories(flag);

            if (files.Length == 0 && dirs.Length == 1)
            {
                Directory.Move(dirs[0], flag + "_Buffer");
                Directory.Delete(flag);
                Directory.Move(flag + "_Buffer", flag);
            }
            ConsoleExpansion.LogWriteLine("Success.");
        }

        private static float ByteToGByte(long value) => value / 1024f / 1024f / 1024f;
    }
}
