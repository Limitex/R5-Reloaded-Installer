using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace R5_Reloaded_Installer_Library.External
{
    public static class ResourceProcessing
    {
        public static string ExportingFile(string path, string resource)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream("R5_Reloaded_Installer_Library.Resources." + resource);
            if (stream == null) throw new Exception("The assembly does not have the specified file.");
            File.WriteAllBytes(path, GetByteArrayFromStream(stream));
            return path;
        }

        private static byte[] GetByteArrayFromStream(Stream stream)
        {
            using var ms = new MemoryStream();
            stream.CopyTo(ms);
            return ms.ToArray();
        }
    }
}
