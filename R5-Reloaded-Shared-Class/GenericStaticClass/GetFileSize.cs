using BencodeNET.Parsing;
using BencodeNET.Torrents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace R5_Reloaded_Installer.SharedClass
{
    public static class GetFileSize
    {
        public static long DriveFreeSpace(string path)
        {
            return new DriveInfo(Path.GetPathRoot(path)).AvailableFreeSpace;
        }
        public static long Torrent(string url)
        {
            var file = Download.Run(url);
            var size = new BencodeParser().Parse<Torrent>(file).TotalSize;
            File.Delete(file);
            return size;
        }
        public static long Zip(string url)
        {
            var client = new WebClient();
            client.OpenRead(url);
            return Convert.ToInt64(client.ResponseHeaders["Content-Length"]);
        }
        public static float ByteToGByte(long value) => value / 1024f / 1024f / 1024f;
    }
}
