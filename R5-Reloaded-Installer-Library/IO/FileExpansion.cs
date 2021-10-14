using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using BencodeNET.Parsing;
using BencodeNET.Torrents;
using IWshRuntimeLibrary;

namespace R5_Reloaded_Installer_Library.IO
{
    /// <summary>
    /// "class System.IO.File" expansion and other
    /// </summary>
    public static class FileExpansion
    {
        public static float ByteToGByte(long value) => value / 1024f / 1024f / 1024f;

        public static string GetExtension(string address) => Path.GetExtension(address).Replace(".", "").ToLower();

        public static long GetDriveFreeSpace(string path)
        {
            return new DriveInfo(Path.GetPathRoot(path)).AvailableFreeSpace;
        }

        public static long GetTorrentFileSize(string url)
        {
            using (var wc = new WebClient())
            {
                return new BencodeParser().Parse<Torrent>(wc.OpenRead(url)).TotalSize;
            }
        }

        public static long GetZipFileSize(string url)
        {
            using (var wc = new WebClient())
            {
                wc.OpenRead(url);
                return Convert.ToInt64(wc.ResponseHeaders["Content-Length"]);
            }
        }

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
