﻿using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace R5_Reloaded_Installer_GUI
{
    public class StartInstallEventArgs
    {
        public string InstallationPath;
        public string ApexClientURL;
        public string Detours_R5URL;
        public string Scripts_R5URL;
        public bool CreateDesktopShortcut;
        public bool AddShortcutToStartMenu;
    }

    public delegate void StartInstallEventHandler(object sender, StartInstallEventArgs e);

    public class ButtonOperation
    {
        private MainForm mainForm;
        private bool ButtonSelectFlug = false;
        private StartInstallEventHandler startInstallEventHandler;

        public ButtonOperation(MainForm form, StartInstallEventHandler installEventHandler)
        {
            mainForm = form;
            startInstallEventHandler = installEventHandler;
            mainForm.InstallLinkTextBox.Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), MainForm.FinalDirectoryName);
            mainForm.MainTabControl.Selecting += new TabControlCancelEventHandler(MainTabControl_Selecting);
            mainForm.MainTabControl.Selected += new TabControlEventHandler(MainTabControl_Selected);
            mainForm.AgreeCheckBox.CheckedChanged += new EventHandler(AgreeCheckBox_CheckedChanged);
            mainForm.NextButton.Click += new EventHandler((sender, e) => { Button_Click(sender, e); NextButton_Click(sender, e); });
            mainForm.BackButton.Click += new EventHandler((sender, e) => { Button_Click(sender, e); BackButton_Click(sender, e); });
            mainForm.InstallButton.Click += new EventHandler((sender, e) => { Button_Click(sender, e); InstallButton_Click(sender, e); });
            mainForm.CancelButton.Click += new EventHandler(CancelButton_Click);
            MainTabControl_Selected(new object(), new TabControlEventArgs(
                    mainForm.MainTabControl.SelectedTab,
                    mainForm.MainTabControl.SelectedIndex,
                    new TabControlAction { }));
        }

        private void MainTabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!ButtonSelectFlug)
            {
                e.Cancel = true;
                return;
            }
            else
            {
                ButtonSelectFlug = false;
            }
        }

        private void MainTabControl_Selected(object sender, TabControlEventArgs e)
        {
            ButtonForEachTab(e.TabPage.Name);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            ButtonSelectFlug = true;
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            ButtonToTabNext(1);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            ButtonToTabNext(-1);
        }

        private void InstallButton_Click(object sender, EventArgs e)
        {
            if (!IsValueCorrect()) return;

            if (!ContinueCheck())
            {
                var dr = MessageBox.Show("Do you want to start the installation?\n\n" +
                    "Since it is downloaded from Torrent, it may take several hours depending on the time of day. " +
                    "The download does not start for a few seconds after File Alloc finishes, but it is normal. " +
                    "However, if it does not progress by 1% after a few minutes, try again after a few minutes.", "Installer",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (dr != DialogResult.OK) return;
            }
            else
            {
                var dr = MessageBox.Show("Found a file that is in the process of being executed.\n" +
                    "Would you like to continue?", "Installer",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (dr != DialogResult.OK) return;
            }

            ButtonToTabNext(1);

            startInstallEventHandler(new object(), new StartInstallEventArgs()
            {
                InstallationPath = mainForm.InstallLinkTextBox.Text,
                CreateDesktopShortcut = mainForm.CreateDesktopShortcutCheckBox.Checked,
                AddShortcutToStartMenu = mainForm.AddToStartMenuCheckBox.Checked,
                ApexClientURL = GetSizeAndPath.ApexClientURL,
                Detours_R5URL = GetSizeAndPath.Detours_R5URL,
                Scripts_R5URL = GetSizeAndPath.Scripts_R5URL
            });
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (!MainForm.IsRunning && mainForm.LaunchCheckBox.Checked)
                Process.Start(Path.Combine(mainForm.InstallLinkTextBox.Text, MainForm.ExecutableFileName));
            Application.Exit();
        }

        private void AgreeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            mainForm.NextButton.Enabled = mainForm.AgreeCheckBox.Checked;
        }

        private void ButtonToTabNext(int i)
        {
            ButtonSelectFlug = true;
            if (0 < i) if (mainForm.MainTabControl.SelectedIndex < mainForm.MainTabControl.TabCount - 1)
                    mainForm.MainTabControl.SelectedIndex += 1;
            if (i < 0) if (0 < mainForm.MainTabControl.SelectedIndex)
                    mainForm.MainTabControl.SelectedIndex -= 1;
        }

        private void ButtonForEachTab(string nowTab)
        {
            if (nowTab == mainForm.IntroductionTabPage.Name)
                SetButtonEnabled(false, true, false);
            if (nowTab == mainForm.InformationTabPage.Name)
                SetButtonEnabled(true, mainForm.AgreeCheckBox.Checked, false);
            if (nowTab == mainForm.PlaceOfInstallationTabPage.Name)
                SetButtonEnabled(true, true, false);
            if (nowTab == mainForm.OptionTabPage.Name)
                SetButtonEnabled(true, false, true);
            if (nowTab == mainForm.ProcessTabPage.Name)
                SetButtonEnabled(false, false, false);
            if (nowTab == mainForm.CompletionTabPage.Name)
            {
                SetButtonEnabled(false, false, false);
                mainForm.CancelButton.Text = "Exit";
                MainForm.IsRunning = false;
            }
        }

        private void SetButtonEnabled(bool back, bool next, bool install)
        {
            mainForm.BackButton.Enabled = back;
            mainForm.NextButton.Enabled = next;
            mainForm.InstallButton.Enabled = install;
        }

        private bool IsValueCorrect()
        {
            if (!mainForm.AgreeCheckBox.Checked)
            {
                MessageBox.Show("Select the check box on the Information tab before continuing.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if ((GetSizeAndPath.TargetDirectoryRoot == -1) || (GetSizeAndPath.TargetAllFiles == -1))
            {
                MessageBox.Show("The file size or drive size cannot be obtained. \nPlease wait for a while.",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (GetSizeAndPath.TargetDirectoryRoot < GetSizeAndPath.TargetAllFiles)
            {
                MessageBox.Show("There is no free space on the optical disc.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (Directory.Exists(mainForm.InstallLinkTextBox.Text) & !ContinueCheck())
            {
                MessageBox.Show("The specified directory already exists.\n" +
                    "Please move or delete the file and try again.\n" +
                    "Explorer opens.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Process.Start("EXPLORER.EXE", mainForm.InstallLinkTextBox.Text);
                return false;
            }
            return true;
        }

        private bool ContinueCheck()
        {
            if (!Directory.Exists(mainForm.InstallLinkTextBox.Text)) return false;
            var files = Directory.GetFiles(mainForm.InstallLinkTextBox.Text, "*.aria2", SearchOption.TopDirectoryOnly);
            return files.Length != 0;
        }
    }
}