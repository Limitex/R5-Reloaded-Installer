using System;
using System.IO;

namespace R5_Reloaded_Installer
{
    class Program
    {
        private static string SettingFileName = "info.links";

        private static string FinalDirectoryName = "R5-Reloaded";
        static void Main(string[] args)
        {
            FileOperations.ReadSettingFile(SettingFileName);
            FileOperations.DownloadFiles();

            DirectoryExpansion.AllMove(FileOperations.FlagName_detours, FileOperations.FlagName_apex);

            Directory.CreateDirectory(FileOperations.R5_ScriptsPath);
            DirectoryExpansion.AllMove(FileOperations.FlagName_scripts, FileOperations.R5_ScriptsPath);

            Directory.Move(FileOperations.FlagName_apex, FinalDirectoryName);

            Console.WriteLine("Succees.\n" + FinalDirectoryName + " directory.");
        }
    }
}
