using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using IWshRuntimeLibrary;
using System.Diagnostics.CodeAnalysis;

namespace R5_Reloaded_Installer_Library
{
    public static class FileExpansion
    {
        [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public static void CreateShortcut(string path, string name, string LinkDestination, string arguments)
        {
            var shell = new WshShell();
            var shortcut = (IWshShortcut)shell.CreateShortcut(Path.Combine(path, name + @".lnk"));
            shortcut.TargetPath = LinkDestination;
            shortcut.Arguments = arguments;
            shortcut.WorkingDirectory = Path.GetDirectoryName(LinkDestination);
            shortcut.WindowStyle = 1;
            shortcut.IconLocation = LinkDestination + ",0";
            shortcut.Save();
            Marshal.FinalReleaseComObject(shortcut);
            Marshal.FinalReleaseComObject(shell);
        }
    }
}
