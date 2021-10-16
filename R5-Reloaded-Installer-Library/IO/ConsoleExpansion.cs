using System;
using System.Runtime.InteropServices;

namespace R5_Reloaded_Installer_Library.IO
{
    /// <summary>
    /// "class System.Console" expansion and other
    /// </summary>
    public static class ConsoleExpansion
    {
        private readonly static int InformationMaxWidth = 5;
        private readonly static int ConsentMaxAttempts = 5;

        [DllImport("kernel32.dll", SetLastError = true)] static extern IntPtr GetStdHandle(int nStdHandle);
        [DllImport("kernel32.dll")] static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);
        [DllImport("kernel32.dll")] static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        public static void DisableEasyEditMode()
        {
            const int STD_INPUT_HANDLE = -10;
            const uint ENABLE_QUICK_EDIT = 0x0040;

            var consoleHandle = GetStdHandle(STD_INPUT_HANDLE);
            GetConsoleMode(consoleHandle, out uint consoleMode);
            SetConsoleMode(consoleHandle, consoleMode & ~ENABLE_QUICK_EDIT);
        }

        public static void WriteWidth(char c, string text = null)
        {
            Console.WriteLine();
            if (text != null)
            {
                var size = (Console.WindowWidth / 2f) - (text.Length / 2f) - 2f;
                text = ' ' + text + ' ';
                for (int i = 0; i <= size; i++) Console.Write(c);
                Console.Write(text);
                for (int i = 0; i <= size; i++) Console.Write(c);
            }
            else
            {
                for (int i = 0; i < Console.WindowWidth; i++) Console.Write(c);
            }
            Console.WriteLine("\n");
        }

        public static void LogWrite(string value)
        {
            LogInfo("Info", ConsoleColor.DarkGreen, value);
        }
        public static void LogNotes(string value)
        {
            LogInfo("Notes", ConsoleColor.Yellow, value);
        }
        public static void LogError(string value)
        {
            LogInfo("Error", ConsoleColor.DarkRed, value);
        }
        public static void LogDebug(string value)
        {
            LogInfo("Debug", ConsoleColor.DarkYellow, value);
        }
        public static void LogInput(string value, bool NewLine = false)
        {
            LogInfo("Input", ConsoleColor.DarkCyan, value, NewLine);
        }

        public static bool ConsentInput(string CanselMassage = null)
        {
            var ConsentAttempts = 0;
            while (ConsentAttempts < ConsentMaxAttempts)
            {
                LogInput("Yes No (y/n) : ");
                ConsoleKey key;
                do key = Console.ReadKey().Key;
                while (key == ConsoleKey.Escape);
                Console.WriteLine();
                switch (key)
                {
                    case ConsoleKey.Y:
                        return true;
                    case ConsoleKey.N:
                        if (CanselMassage == null)
                            LogWrite("The operation was canceled by the user.");
                        else
                            LogWrite(CanselMassage);
                        return false;
                    default:
                        ConsentAttempts++;
                        if (ConsentAttempts < ConsentMaxAttempts)
                            LogError("Enter either Y or N. Type it again.");
                        break;
                }
            }
            LogError("Max attempts has been reached.");
            Exit();
            return false;
        }

        public static void Exit()
        {
            Console.WriteLine("Press the key to exit.");
            Console.ReadKey();
            Environment.Exit(0x8020);
        }

        private static void LogInfo(string info, ConsoleColor color, string str, bool NewLine = true)
        {
            ColorWrite("[ ", ConsoleColor.DarkMagenta);
            ColorWrite(info.PadRight(InformationMaxWidth), color);
            ColorWrite(" ][ ", ConsoleColor.DarkMagenta);
            ColorWrite(DateTime.Now.ToString("yyyy/MM/dd"), ConsoleColor.DarkGray);
            Console.Write(' ' + DateTime.Now.ToString("HH:mm:ss"));
            ColorWrite(" ] ", ConsoleColor.DarkMagenta);
            ColorWrite(": ", ConsoleColor.DarkGray);
            Console.Write(str);
            if (NewLine) Console.WriteLine();
        }

        private static void ColorWrite(string value, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(value);
            Console.ResetColor();
        }
    }
}
