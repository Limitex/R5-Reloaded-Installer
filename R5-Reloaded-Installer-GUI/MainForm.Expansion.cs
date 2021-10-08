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
        private bool CheckSize() => (DriveSize > FileSize) && (DriveSize != -1) && (FileSize != -1);

        private void SetDriveAndFileSize()
        {
            DriveSize = GetFileSize.DriveFreeSpace(InstallPath);
            new Thread(() =>
            {
                Invoke(new Delegate(() => SetSizesText(0, DriveSize)));
                long size = 0;
                if (!ExitFlug) size = GetFileSize.Torrent(WebGetLink.GetApexClientLink());
                if (!ExitFlug) size += GetFileSize.Zip(WebGetLink.GetDetoursR5Link());
                if (!ExitFlug) size += GetFileSize.Zip(WebGetLink.GetScriptsR5Link());
                if (!ExitFlug) size += GetFileSize.Zip(WebGetLink.GetAria2Link());
                if (!ExitFlug) FileSize = size;
                if (!ExitFlug) Invoke(new Delegate(() => {
                    SetSizesText(FileSize, DriveSize);
                    NextButton.Enabled = CheckSize();
                }));
            }).Start();
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
                NextButton.Enabled = CheckSize();
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
                GetFileSize.ByteToGByte(fileSize).ToString("0.00") + " GB" +
                "\n\nDrive size : " +
                GetFileSize.ByteToGByte(driveSize).ToString("0.00") + " GB";
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
                    "Please move or delete the file and try again.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
