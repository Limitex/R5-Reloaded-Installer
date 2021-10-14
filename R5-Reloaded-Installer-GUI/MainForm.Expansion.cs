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
        public static string DirName = "R5-Reloaded";
        public static string InstallPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DirName);
        private static string ExecutableFileName = "r5reloaded.exe";
        private static string ScriptsDirectoryPath = Path.Combine("platform", "scripts");
        private static int FirstPort = 6900;

        private static bool ExitFlug = false;
        private static bool ButtonSelectFlug = false;
        private static long FileSize = -1;
        private static long DriveSize = -1;
        delegate void Delegate();

        private static Form LogForm;
        private static RichTextBox LogFormRichTexBox;

        private void LogFormInitialize()
        {
            LogForm = new Form() { Text = "Log Window", ShowIcon = false };
            LogFormRichTexBox = new RichTextBox()
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                Name = "logRichTextBox",
                Location = new Point(0, 0),
                Size = new Size(LogForm.Width - 16, LogForm.Height - 39),
                BackColor = Color.White,
                ReadOnly = true,
                HideSelection = false
            };
            LogForm.Controls.Add(LogFormRichTexBox);
        }

        private bool CheckSize() => (DriveSize > FileSize) && (DriveSize != -1) && (FileSize != -1);

        private void SetDriveAndFileSize()
        {
            new Thread(() =>
            {
                DriveSize = GetFileSize.DriveFreeSpace(InstallPath);
                Invoke(new Delegate(() => SetSizesText(-1, DriveSize)));
                long size = 0;
                if (!ExitFlug) size = GetFileSize.Torrent(WebGetLink.GetApexClientLink());
                if (!ExitFlug) size += GetFileSize.Zip(WebGetLink.GetDetoursR5Link());
                if (!ExitFlug) size += GetFileSize.Zip(WebGetLink.GetScriptsR5Link());
                if (!ExitFlug) size += GetFileSize.Zip(WebGetLink.GetAria2Link());
                if (!ExitFlug) FileSize = size;
                if (!ExitFlug) Invoke(new Delegate(() => SetDriveSize()));
            }).Start();
        }

        private void SetDriveSize()
        {
            DriveSize = GetFileSize.DriveFreeSpace(InstallPath);
            SetSizesText(FileSize, DriveSize);
        }

        private void ButtonToTabNext(int i)
        {
            ButtonSelectFlug = true;
            if (0 < i) if (MainTabControl.SelectedIndex < MainTabControl.TabCount - 1)
                    MainTabControl.SelectedIndex += 1;
            if (i < 0) if (0 < MainTabControl.SelectedIndex)
                    MainTabControl.SelectedIndex -= 1;

        }

        private void SetButtonEnebled()
        {
            var nowTab = MainTabControl.SelectedTab.Name;

            if (nowTab == IntroductionTabPage.Name)
            {
                BackButton.Enabled = false;
                NextButton.Enabled = true;
                InstallButton.Enabled = false;
            }
            if (nowTab == InformationTabPage.Name)
            {
                BackButton.Enabled = true;
                NextButton.Enabled = AgreeCheckBox.Checked;
                InstallButton.Enabled = false;
            }
            if (nowTab == PlaceOfInstallationTabPage.Name)
            {
                BackButton.Enabled = true;
                NextButton.Enabled = true;
                InstallButton.Enabled = false;
            }
            if (nowTab == OptionTabPage.Name)
            {
                BackButton.Enabled = true;
                NextButton.Enabled = false;
                InstallButton.Enabled = true;
            }
            if (nowTab == ProcessTabPage.Name)
            {
                BackButton.Enabled = false;
                NextButton.Enabled = false;
                InstallButton.Enabled = false;
            }
            if (nowTab == CompletionTabPage.Name)
            {
                BackButton.Enabled = false;
                NextButton.Enabled = false;
                InstallButton.Enabled = false;
                CancelButton.Text = "Exit";
                ExitFlug = true;
            }
        }

        private void OpenSite(string url)
        {
            var info = new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = url,
            };
            Process.Start(info);
        }

        private void SetSizesText(long fileSize, long driveSize)
        {
            SizeLabel.Text = "File size : " +
                (fileSize != -1 ? GetFileSize.ByteToGByte(fileSize).ToString("0.00") + " GB" : "Loading...") +
                "\n\nDrive size : " +
                (driveSize != -1 ? GetFileSize.ByteToGByte(driveSize).ToString("0.00") + " GB" : "Loading...");
        }

        private bool CheckValue()
        {
            if (!AgreeCheckBox.Checked)
            {
                MessageBox.Show("Select the check box on the Information tab before continuing.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!(DriveSize > FileSize))
            {
                MessageBox.Show("There is no free space on the optical disc.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (Directory.Exists(InstallPath))
            {
                MessageBox.Show("The specified directory already exists.\n" +
                    "Please move or delete the file and try again.\n" +
                    "Explorer opens.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Process.Start("EXPLORER.EXE", InstallPath);
                return false;
            }
            return true;
        }

        private void CompleteProcess()
        {
            DownloadProgressBar.Value = 100;
            OverallProgressBar.Value = 100;
            DownloadStatusLabel.Text = "Complete!";
            OverallStatusLabel.Text = "Complete!";
            NextButton.Enabled = true;
        }

        private bool CheckApplication()
        {
            var applicationList = GetInstalledApps.AllList();
            return applicationList.Contains("Origin") && applicationList.Contains("Apex Legends");
        }
    }
}
