using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using R5_Reloaded_Installer.SharedClass;

namespace R5_Reloaded_Installer_GUI
{
    partial class MainForm
    {
        private static string TargetDirectory;
        private static bool CreateShortcutFlug;
        private static bool AddStartMenuFlug;
        private void StartProcessInitialize()
        {
            TargetDirectory = InstallPath;
            CreateShortcutFlug = CreateDesktopShortcutCheckBox.Checked;
            AddStartMenuFlug = AddToStartMenuCheckBox.Checked;
        }

        private void StartProcess()
        {
            new Thread(() => {
                Invoke(new SetStatusDelgete(SetStatus), -1, -1, "Preparing...", "Waiting for download process");
                string detoursR5FileName, scriptsR5FileName;
                using (new Download(TargetDirectory))
                {
                    Invoke(new SetStatusDelgete(SetStatus), 1, -1, "Downloading detours_r5", null);
                    detoursR5FileName = Download.RunZip(WebGetLink.GetDetoursR5Link(), TargetDirectory, "detours_r5");
                    Invoke(new SetStatusDelgete(SetStatus), 2, -1, "Downloading scripts_r5", null);
                    scriptsR5FileName = Download.RunZip(WebGetLink.GetScriptsR5Link(), TargetDirectory, "scripts_r5");
                    Invoke(new SetStatusDelgete(SetStatus), 3, -1, "Downloading APEX Client", null);
                }

                Invoke(new Delegate(() => CompleteProcess()));
            }).Start();
        }

        private delegate void SetStatusDelgete(int dpValue, int opValue, string dsText, string osText);
        private void SetStatus(int dpValue = -1, int opValue = -1, string dsText = null, string osText = null)
        {
            if (dpValue != -1) DownloadProgressBar.Value = dpValue;
            if (opValue != -1) OverallProgressBar.Value = opValue;
            if (dsText != null) DownloadStatusLabel.Text = dsText;
            if (osText != null) OverallStatusLabel.Text = osText;
        }
    }
}
