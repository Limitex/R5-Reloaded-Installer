using R5_Reloaded_Installer_Library.IO;
using R5_Reloaded_Installer_Library.Other.JobObjectSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace R5_Reloaded_Installer_Library.External
{
    public delegate void ResourceProcessEventHandler(object sender, DataReceivedEventArgs outLine);

    public class ResourceProcess : IDisposable
    {
        public event ResourceProcessEventHandler? ResourceProcessReceives = null;

        private string WorkingDirectoryPath;
        private string applicationPath;

        private Process? process = null;

        public ResourceProcess(string workingDirectory, string resource)
        {
            var applicationName = resource + ".exe";
            WorkingDirectoryPath = Path.Combine(workingDirectory, Path.GetFileName(resource));
            DirectoryExpansion.CreateOverwrite(WorkingDirectoryPath);
            applicationPath = ExportingFile(Path.Combine(WorkingDirectoryPath, applicationName), applicationName);
        }

        public void Dispose()
        {
            DirectoryExpansion.DirectoryDelete(WorkingDirectoryPath);
        }

        public void Run(string arguments, string workingDirectory)
        {
            using (var job = JobObject.CreateAsKillOnJobClose())
            {
                process = new Process();
                process.StartInfo = new ProcessStartInfo()
                {
                    FileName = applicationPath,
                    Arguments = arguments,
                    WorkingDirectory = workingDirectory,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };
                if (ResourceProcessReceives != null)
                {
                    process.EnableRaisingEvents = true;
                    process.ErrorDataReceived += new DataReceivedEventHandler(ResourceProcessReceives);
                    process.OutputDataReceived += new DataReceivedEventHandler(ResourceProcessReceives);
                }
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                job.AssignProcess(process);
                process.WaitForExit();
                process.Close();
                process = null;
            }
        }

        public void Kill()
        {
            if (process != null)
            {
                process.Kill();
                process.Close();
            }
        }

        private static string ExportingFile(string path, string resource)
        {
            var name = "R5_Reloaded_Installer_Library.External.Resources." + resource;
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
            if (stream == null) throw new Exception("The assembly does not have the specified file.");
            using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            File.WriteAllBytes(path, memoryStream.ToArray());
            return path;
        }
    }
}
