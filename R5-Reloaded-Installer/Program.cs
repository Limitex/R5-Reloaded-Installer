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

            ConsoleExpansion.LogWriteLine("Moving " + FileOperations.FlagName_detours + " to APEX Client.");
            DirectoryExpansion.AllMove(FileOperations.FlagName_detours, FileOperations.FlagName_apex);
            ConsoleExpansion.LogWriteLine("Creating and moving the script directory.");
            Directory.CreateDirectory(FileOperations.R5_ScriptsPath);
            DirectoryExpansion.AllMove(FileOperations.FlagName_scripts, FileOperations.R5_ScriptsPath);

            ConsoleExpansion.LogWriteLine("The end process is in progress.");
            Directory.Move(FileOperations.FlagName_apex, FinalDirectoryName);
 
            ConsoleExpansion.LogWriteLine("Exists in the " + FinalDirectoryName + " directory.");
            ConsoleExpansion.LogWriteLine("Done.");
        }
    }
}
