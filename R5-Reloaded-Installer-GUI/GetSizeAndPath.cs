using R5_Reloaded_Installer_Library.Exclusive;
using R5_Reloaded_Installer_Library.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace R5_Reloaded_Installer_GUI
{
    public delegate void Delegate();
    public class GetSizeAndPath
    {
        public static long TargetDirectoryRoot = -1;
        public static long TargetAllFiles = -1;

        private MainForm mainForm;
        
        public GetSizeAndPath(MainForm form)
        {
            mainForm = form;
            mainForm.DiscordLinkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(DiscordLinkLabel_LinkClicked);
            mainForm.WebsiteLinkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(WebsiteLinkLabel_LinkClicked);
            mainForm.ReloadDriveSizeLinkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(ReloadDriveSizeLinkLabel_LinkClicked);
            mainForm.BrowseButton.Click += new EventHandler(this.BrowseButton_Click);

            GetDirectoryRootSize();
            GetAllFilesSize();
        }

        private void DiscordLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenSite(mainForm.DiscordLinkLabel.Text);
        }

        private void WebsiteLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenSite(mainForm.WebsiteLinkLabel.Text);
        }

        private void ReloadDriveSizeLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GetDirectoryRootSize();
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog(mainForm) == DialogResult.OK)
            {
                mainForm.InstallLinkTextBox.Text = Path.Combine(fbd.SelectedPath, MainForm.LatestDirectoryName);
                GetDirectoryRootSize();
            }
        }

        public void GetDirectoryRootSize()
        {
            TargetDirectoryRoot = FileExpansion.GetDriveFreeSpace(mainForm.InstallLinkTextBox.Text);
            mainForm.DriveSizeLabel.Text = "Drive size: " + FileExpansion.ByteToGByte(TargetDirectoryRoot).ToString("0.00") + "GB";
        }

        public void GetAllFilesSize()
        {
            new Thread(() =>
            {
                long size = FileExpansion.GetTorrentFileSize(WebGetLink.ApexClient()) +
                FileExpansion.GetZipFileSize(WebGetLink.DetoursR5()) +
                FileExpansion.GetZipFileSize(WebGetLink.ScriptsR5());
                mainForm.Invoke(new Delegate(() => {
                    TargetAllFiles = size;
                    mainForm.FileSizeLabel.Text = "File size: " + FileExpansion.ByteToGByte(TargetAllFiles).ToString("0.00") + "GB";
                }));
            }).Start();
        }

        private void OpenSite(string url)
        {
            Process.Start(new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = url,
            });
        }
    }
}
