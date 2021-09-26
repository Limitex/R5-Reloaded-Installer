using System;
using System.Collections.Generic;
using System.Text;

namespace R5_Reloaded_Installer
{
    static class ConsoleExpansion
    {
        public static void LogWriteLine(string value)
        {
            LogWrite(value + "\n");
        }

        public static void LogWriteLineError(string value)
        {
            LogWriteError(value + "\n");
        }

        public static void LogWrite(string value)
        {
            LogInfo("Info", ConsoleColor.Green);
            Console.Write(value);
        }

        public static void LogWriteError(string value)
        {
            LogInfo("Error", ConsoleColor.Red);
            Console.Write(value);
        }

        private static void LogInfo(string str, ConsoleColor color)
        {
            Console.Write("[");
            Console.ForegroundColor = color;
            Console.Write(str);
            Console.ResetColor();
            Console.Write("][" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "] : ");
        }

        public static void ExitConsole()
        {
            Console.WriteLine("Press the key to exit.");
            Console.ReadKey();
            Environment.Exit(0x8020);
        }
    }
}
