using System;
using System.IO;

namespace R5_Reloaded_Installer
{
    class Program
    {
        private static string FinalDirectoryName = "R5-Reloaded";
        private static string ScriptsDirectoryPath = Path.Combine(FinalDirectoryName, "platform", "scripts");
        static void Main(string[] args)
        {
            Console.WriteLine("\n" +
                "  -----------------------------------\n" +
                "  ||                               ||\n" +
                "  ||     R5-Reloaded Installer     ||\n" +
                "  ||                               ||\n" +
                "  -----------------------------------\n\n" +
                "  This program was created by Limitex.\n" +
                "  Please refer to the link below for the latest version of this program.\n\n" +
                "  https://github.com/Limitex/R5-Reloaded-Installer/releases \n\n" +
                "Welcome!\n");

            ConsoleExpansion.LogWrite("Do you want to continue the installation ?");
            ConsoleExpansion.LogWrite("Installation takes about an hour.");
            if (ConsoleExpansion.ConsentInput())
            {
                string detoursR5FileName, scriptsR5FileName;
                using (new Download())
                {
                    detoursR5FileName = Download.RunZip(WebGetLink.GetDetoursR5Link(), "detours_r5");
                    scriptsR5FileName = Download.RunZip(WebGetLink.GetScriptsR5Link(), "scripts_r5");
                    Download.RunTorrent(WebGetLink.GetApexClientLink(), FinalDirectoryName);
                }
                ConsoleExpansion.LogWrite("The detours_r5 file is being moved.");
                DirectoryExpansion.MoveOverwrite(detoursR5FileName, FinalDirectoryName);
                ConsoleExpansion.LogWrite("The scripts_r5 file is being moved.");
                Directory.Move(scriptsR5FileName, ScriptsDirectoryPath);
                ConsoleExpansion.LogWrite("The entire process has been completed!");
                ConsoleExpansion.LogWrite("Done.");
            }
            ConsoleExpansion.Exit();
        }

    }
}
