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
            DownloadProgressBar.Value = 50;
            OverallProgressBar.Value = 50;
            DownloadStatusLabel.Text = "Download Status";
            OverallStatusLabel.Text = "Overall Status";


            CompleteProcess();
        }
    }
}
