using System;
using System.Collections.Generic;
using System.Text;

namespace R5_Reloaded_Installer
{
    static class ConsoleExpansion
    {
        public static void LogWriteLine(string value)
        {
            Console.WriteLine("[" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "] : " + value);
        }

        public static void ExitConsole()
        {
            Console.WriteLine("Press the key to exit");
            Console.ReadKey();
            Environment.Exit(0x8020);
        }
    }
}
