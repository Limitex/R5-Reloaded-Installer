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
            var DownloadLinks = new Hashtable();
            try
            {
                using (var reader = new StreamReader(path))
                {
                    var rawData = Regex.Replace(reader.ReadToEnd(), @"( |　|\t)", "").Split("\r\n");

                    for (int i = 0; i < rawData.Length; i++)
                    {
                        var data = rawData[i].Split('>');
                        if (SettingFlags.Contains(data[0]))
                            DownloadLinks[data[0]] = data[1];
                        if (!Regex.IsMatch(data[1], @"^s?https?://[-_.!~*'()a-zA-Z0-9;/?:@&=+$,%#]+$"))
                        {
                            Console.WriteLine("Contains a string that is not a URI.");
                            Environment.Exit(0x8020);
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("There is something wrong with the \'" + path + "\' file.");
                Environment.Exit(0x8020);
            }

            foreach (var flag in SettingFlags)
            {
                if (!DownloadLinks.ContainsKey(flag))
                {
                    Console.WriteLine("The\'" + path + "\' file is incorrect.");
                    Environment.Exit(0x8020);
                    break;
                }
            }

            SettingData = DownloadLinks;
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

            Console.WriteLine("Removing " + FlagName_Aria2 + "...");
            DirectoryExpansion.AllDelete(FlagName_Aria2);
        }

        private static void DownloadZipFile(string flag)
        {
            Console.WriteLine("Downloading " + flag + "...");
            new WebClient().DownloadFile(SettingData[flag].ToString(), flag + ZipExtension);
        }

        private static void DownloadTorrentFile(string flag)
        {
            var link = SettingData[flag].ToString();
            var FileName = Path.GetFileName(link);
            var DirName = FileName.Replace(Path.GetExtension(FileName), "");

            Console.WriteLine("Since it is a torrent file, it may take some time to download.\n");
            Console.WriteLine("============= Download the APEX client with aria2 =============");
            Process proc = new Process();
            proc.StartInfo.FileName = FlagName_Aria2 + "\\" + Aria2ExecutableFileName;
            proc.StartInfo.Arguments = link + " --seed-time=0";
            proc.Start();
            proc.WaitForExit();
            proc.Close();
            Console.WriteLine("===============================================================");
            Console.WriteLine("This program stops seeding, but If possible, Please use torrent software to seed. No one may be able to download it from torrents.\nThe torrent file exists in the directory.");
            
            new WebClient().DownloadFile(link, FileName);

            Directory.Move(DirName, flag);
        }

        private static void ExtractZipFile(string flag)
        {
            Console.WriteLine("Extracting " + flag + "...");

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
        }
    }
}
