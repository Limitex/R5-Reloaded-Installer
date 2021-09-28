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

        public static void LogDebug(string value)
        {
            LogInfo("Debug", ConsoleColor.Yellow);
            Console.WriteLine(value);
        }

        public static void LogWriteError(string value)
        {
            LogInfo("Error", ConsoleColor.Red);
            Console.Write(value);
        }

        public static bool ConsentInput(string CanselMassage = null)
        {
            var Trials = 5;
            var TrialsCount = 0;
            while (TrialsCount < Trials)
            {
                LogWrite("Yes No (y/n) : ");
                var key = Console.ReadKey().Key;
                Console.WriteLine();
                switch (key)
                {
                    case ConsoleKey.Y:
                        return true;
                    case ConsoleKey.N:
                        if (CanselMassage == null)
                            LogWriteLine("The operation was canceled by the user.");
                        else
                            LogWriteLine(CanselMassage);
                        return false;
                    default:
                        LogWriteLineError("Enter either Y or N. Type it again.");
                        TrialsCount++;
                        break;
                }
            }
            LogWriteLineError("The trial limit has been reached.");
            ExitConsole();
            return false;
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
