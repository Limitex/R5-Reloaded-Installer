using System;
using System.Collections.Generic;
using System.Text;

namespace R5_Reloaded_Installer_Library.Text
{
    public class StringProcessing
    {
        public static string ByteToStringWithUnits(float data)
        {
            var count = 0;
            var text = new string[] { "Byte", "KB", "MB", "GB", "TB" };
            while (data >= 1024f) { data /= 1024f; count++; }
            return data.ToString(".000") + " " + text[count];
        }
    }
}
