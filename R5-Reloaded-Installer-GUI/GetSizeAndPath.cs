using R5_Reloaded_Installer_Library.Exclusive;
using R5_Reloaded_Installer_Library.IO;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace R5_Reloaded_Installer_GUI
{
    public class GetSizeAndPath
    {
        public static long TargetDirectoryRoot = -1;
        public static long TargetAllFiles = -1;

        public static string ApexClientURL;
        public static string Detours_R5URL;
        public static string Scripts_R5URL;

        private MainForm mainForm;

        public GetSizeAndPath(MainForm form)
        {
            mainForm = form;
            mainForm.DiscordLinkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(DiscordLinkLabel_LinkClicked);
            mainForm.WebsiteLinkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(WebsiteLinkLabel_LinkClicked);
            mainForm.ReloadDriveSizeLinkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(ReloadDriveSizeLinkLabel_LinkClicked);
            mainForm.BrowseButton.Click += new EventHandler(this.BrowseButton_Click);
            mainForm.Load += new EventHandler(MainForm_Load);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
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
                mainForm.InstallLinkTextBox.Text = Path.Combine(fbd.SelectedPath, MainForm.FinalDirectoryName);
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
                long size = 0;
                if (mainForm.Visible) ApexClientURL = WebGetLink.ApexClient();
                if (mainForm.Visible) Detours_R5URL = WebGetLink.DetoursR5();
                if (mainForm.Visible) Scripts_R5URL = WebGetLink.ScriptsR5();
                if (mainForm.Visible) size += FileExpansion.GetTorrentFileSize(ApexClientURL);
                if (mainForm.Visible) size += FileExpansion.GetZipFileSize(Detours_R5URL);
                if (mainForm.Visible) size += FileExpansion.GetZipFileSize(Scripts_R5URL);
                if (mainForm.Visible) mainForm.Invoke(new Delegate(() =>
                {
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
