
using System;
using System.IO;

namespace R5_Reloaded_Installer_GUI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.IntroductionTabPage = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.InformationTabPage = new System.Windows.Forms.TabPage();
            this.AgreeCheckBox = new System.Windows.Forms.CheckBox();
            this.WebsiteLinkLabel = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.DiscordLinkLabel = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.PlaceOfInstallationTabPage = new System.Windows.Forms.TabPage();
            this.ReloadDriveSizeLinkLabel = new System.Windows.Forms.LinkLabel();
            this.FileSizeLabel = new System.Windows.Forms.Label();
            this.DriveSizeLabel = new System.Windows.Forms.Label();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.InstallLinkTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.OptionTabPage = new System.Windows.Forms.TabPage();
            this.AddToStartMenuCheckBox = new System.Windows.Forms.CheckBox();
            this.CreateDesktopShortcutCheckBox = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ProcessTabPage = new System.Windows.Forms.TabPage();
            this.DetailedInfoLinkLabe = new System.Windows.Forms.LinkLabel();
            this.TimeLeftLabel = new System.Windows.Forms.Label();
            this.OverallLogLabel = new System.Windows.Forms.Label();
            this.DownloadLogLabel = new System.Windows.Forms.Label();
            this.OverallProgressBar = new System.Windows.Forms.ProgressBar();
            this.DownloadProgressBar = new System.Windows.Forms.ProgressBar();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.CompletionTabPage = new System.Windows.Forms.TabPage();
            this.LaunchCheckBox = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.InstallButton = new System.Windows.Forms.Button();
            this.NextButton = new System.Windows.Forms.Button();
            this.BackButton = new System.Windows.Forms.Button();
            this.MainTabControl.SuspendLayout();
            this.IntroductionTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.InformationTabPage.SuspendLayout();
            this.PlaceOfInstallationTabPage.SuspendLayout();
            this.OptionTabPage.SuspendLayout();
            this.ProcessTabPage.SuspendLayout();
            this.CompletionTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainTabControl
            // 
            this.MainTabControl.Controls.Add(this.IntroductionTabPage);
            this.MainTabControl.Controls.Add(this.InformationTabPage);
            this.MainTabControl.Controls.Add(this.PlaceOfInstallationTabPage);
            this.MainTabControl.Controls.Add(this.OptionTabPage);
            this.MainTabControl.Controls.Add(this.ProcessTabPage);
            this.MainTabControl.Controls.Add(this.CompletionTabPage);
            this.MainTabControl.Location = new System.Drawing.Point(12, 12);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(460, 308);
            this.MainTabControl.TabIndex = 0;
            // 
            // IntroductionTabPage
            // 
            this.IntroductionTabPage.Controls.Add(this.label2);
            this.IntroductionTabPage.Controls.Add(this.label1);
            this.IntroductionTabPage.Controls.Add(this.pictureBox1);
            this.IntroductionTabPage.Location = new System.Drawing.Point(4, 24);
            this.IntroductionTabPage.Name = "IntroductionTabPage";
            this.IntroductionTabPage.Padding = new System.Windows.Forms.Padding(25);
            this.IntroductionTabPage.Size = new System.Drawing.Size(452, 280);
            this.IntroductionTabPage.TabIndex = 0;
            this.IntroductionTabPage.Text = "Introduction";
            this.IntroductionTabPage.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(172, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(252, 155);
            this.label2.TabIndex = 2;
            this.label2.Text = "Welcome to the installer for Your R5-Reloaded.\r\n\r\nThe Installer will install R5-R" +
    "eloaded on your computer.\r\n\r\nPress the Cancel button at the bottom right, if you" +
    " want to cancel.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(172, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Welcome !";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::R5_Reloaded_Installer_GUI.Properties.Resources.r5_reloaded_border_radius;
            this.pictureBox1.Location = new System.Drawing.Point(28, 80);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 120);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // InformationTabPage
            // 
            this.InformationTabPage.Controls.Add(this.AgreeCheckBox);
            this.InformationTabPage.Controls.Add(this.WebsiteLinkLabel);
            this.InformationTabPage.Controls.Add(this.label5);
            this.InformationTabPage.Controls.Add(this.DiscordLinkLabel);
            this.InformationTabPage.Controls.Add(this.label4);
            this.InformationTabPage.Controls.Add(this.label3);
            this.InformationTabPage.Location = new System.Drawing.Point(4, 24);
            this.InformationTabPage.Name = "InformationTabPage";
            this.InformationTabPage.Padding = new System.Windows.Forms.Padding(25);
            this.InformationTabPage.Size = new System.Drawing.Size(452, 280);
            this.InformationTabPage.TabIndex = 1;
            this.InformationTabPage.Text = "Information";
            this.InformationTabPage.UseVisualStyleBackColor = true;
            // 
            // AgreeCheckBox
            // 
            this.AgreeCheckBox.AutoSize = true;
            this.AgreeCheckBox.Location = new System.Drawing.Point(28, 233);
            this.AgreeCheckBox.Name = "AgreeCheckBox";
            this.AgreeCheckBox.Size = new System.Drawing.Size(318, 19);
            this.AgreeCheckBox.TabIndex = 5;
            this.AgreeCheckBox.Text = "I won\'t use cosmetics such as heirlooms from the game.";
            this.AgreeCheckBox.UseVisualStyleBackColor = true;
            // 
            // WebsiteLinkLabel
            // 
            this.WebsiteLinkLabel.AutoSize = true;
            this.WebsiteLinkLabel.Location = new System.Drawing.Point(46, 165);
            this.WebsiteLinkLabel.Name = "WebsiteLinkLabel";
            this.WebsiteLinkLabel.Size = new System.Drawing.Size(165, 15);
            this.WebsiteLinkLabel.TabIndex = 4;
            this.WebsiteLinkLabel.TabStop = true;
            this.WebsiteLinkLabel.Text = "https://r5reloaded.gitbook.io/";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(46, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(255, 15);
            this.label5.TabIndex = 3;
            this.label5.Text = "Click here for a reference to the official website.";
            // 
            // DiscordLinkLabel
            // 
            this.DiscordLinkLabel.AutoSize = true;
            this.DiscordLinkLabel.Location = new System.Drawing.Point(46, 95);
            this.DiscordLinkLabel.Name = "DiscordLinkLabel";
            this.DiscordLinkLabel.Size = new System.Drawing.Size(215, 15);
            this.DiscordLinkLabel.TabIndex = 2;
            this.DiscordLinkLabel.TabStop = true;
            this.DiscordLinkLabel.Text = "https://discord.com/invite/jqMkUdXrBr";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(46, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(225, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "Join us for the official Discord server here.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(28, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(390, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "Please read and confirm the checkbox below.";
            // 
            // PlaceOfInstallationTabPage
            // 
            this.PlaceOfInstallationTabPage.Controls.Add(this.ReloadDriveSizeLinkLabel);
            this.PlaceOfInstallationTabPage.Controls.Add(this.FileSizeLabel);
            this.PlaceOfInstallationTabPage.Controls.Add(this.DriveSizeLabel);
            this.PlaceOfInstallationTabPage.Controls.Add(this.BrowseButton);
            this.PlaceOfInstallationTabPage.Controls.Add(this.InstallLinkTextBox);
            this.PlaceOfInstallationTabPage.Controls.Add(this.label6);
            this.PlaceOfInstallationTabPage.Location = new System.Drawing.Point(4, 24);
            this.PlaceOfInstallationTabPage.Name = "PlaceOfInstallationTabPage";
            this.PlaceOfInstallationTabPage.Padding = new System.Windows.Forms.Padding(25);
            this.PlaceOfInstallationTabPage.Size = new System.Drawing.Size(452, 280);
            this.PlaceOfInstallationTabPage.TabIndex = 2;
            this.PlaceOfInstallationTabPage.Text = "Place of installation";
            this.PlaceOfInstallationTabPage.UseVisualStyleBackColor = true;
            // 
            // ReloadDriveSizeLinkLabel
            // 
            this.ReloadDriveSizeLinkLabel.ActiveLinkColor = System.Drawing.Color.Gray;
            this.ReloadDriveSizeLinkLabel.AutoSize = true;
            this.ReloadDriveSizeLinkLabel.LinkColor = System.Drawing.Color.Black;
            this.ReloadDriveSizeLinkLabel.Location = new System.Drawing.Point(28, 240);
            this.ReloadDriveSizeLinkLabel.Name = "ReloadDriveSizeLinkLabel";
            this.ReloadDriveSizeLinkLabel.Size = new System.Drawing.Size(96, 15);
            this.ReloadDriveSizeLinkLabel.TabIndex = 4;
            this.ReloadDriveSizeLinkLabel.TabStop = true;
            this.ReloadDriveSizeLinkLabel.Text = "Reload Drive Size";
            this.ReloadDriveSizeLinkLabel.VisitedLinkColor = System.Drawing.Color.Black;
            // 
            // FileSizeLabel
            // 
            this.FileSizeLabel.AutoSize = true;
            this.FileSizeLabel.Location = new System.Drawing.Point(28, 160);
            this.FileSizeLabel.Name = "FileSizeLabel";
            this.FileSizeLabel.Size = new System.Drawing.Size(92, 15);
            this.FileSizeLabel.TabIndex = 3;
            this.FileSizeLabel.Text = "File size : 0.00GB";
            // 
            // DriveSizeLabel
            // 
            this.DriveSizeLabel.AutoSize = true;
            this.DriveSizeLabel.Location = new System.Drawing.Point(28, 190);
            this.DriveSizeLabel.Name = "DriveSizeLabel";
            this.DriveSizeLabel.Size = new System.Drawing.Size(101, 15);
            this.DriveSizeLabel.TabIndex = 3;
            this.DriveSizeLabel.Text = "Drive size : 0.00GB\r\n";
            // 
            // BrowseButton
            // 
            this.BrowseButton.Location = new System.Drawing.Point(349, 105);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(75, 23);
            this.BrowseButton.TabIndex = 2;
            this.BrowseButton.Text = "Browse";
            this.BrowseButton.UseVisualStyleBackColor = true;
            // 
            // InstallLinkTextBox
            // 
            this.InstallLinkTextBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.InstallLinkTextBox.Location = new System.Drawing.Point(28, 105);
            this.InstallLinkTextBox.Name = "InstallLinkTextBox";
            this.InstallLinkTextBox.ReadOnly = true;
            this.InstallLinkTextBox.Size = new System.Drawing.Size(315, 23);
            this.InstallLinkTextBox.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(28, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(396, 34);
            this.label6.TabIndex = 0;
            this.label6.Text = "Setup will install Everything in the following folder. To install in a different " +
    "folder, click Browse and select another folder, Click Next to continue.";
            // 
            // OptionTabPage
            // 
            this.OptionTabPage.Controls.Add(this.AddToStartMenuCheckBox);
            this.OptionTabPage.Controls.Add(this.CreateDesktopShortcutCheckBox);
            this.OptionTabPage.Controls.Add(this.label7);
            this.OptionTabPage.Location = new System.Drawing.Point(4, 24);
            this.OptionTabPage.Name = "OptionTabPage";
            this.OptionTabPage.Padding = new System.Windows.Forms.Padding(25);
            this.OptionTabPage.Size = new System.Drawing.Size(452, 280);
            this.OptionTabPage.TabIndex = 3;
            this.OptionTabPage.Text = "Option";
            this.OptionTabPage.UseVisualStyleBackColor = true;
            // 
            // AddToStartMenuCheckBox
            // 
            this.AddToStartMenuCheckBox.AutoSize = true;
            this.AddToStartMenuCheckBox.Checked = true;
            this.AddToStartMenuCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AddToStartMenuCheckBox.Location = new System.Drawing.Point(50, 113);
            this.AddToStartMenuCheckBox.Name = "AddToStartMenuCheckBox";
            this.AddToStartMenuCheckBox.Size = new System.Drawing.Size(121, 19);
            this.AddToStartMenuCheckBox.TabIndex = 2;
            this.AddToStartMenuCheckBox.Text = "Add to start menu";
            this.AddToStartMenuCheckBox.UseVisualStyleBackColor = true;
            // 
            // CreateDesktopShortcutCheckBox
            // 
            this.CreateDesktopShortcutCheckBox.AutoSize = true;
            this.CreateDesktopShortcutCheckBox.Checked = true;
            this.CreateDesktopShortcutCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CreateDesktopShortcutCheckBox.Location = new System.Drawing.Point(50, 88);
            this.CreateDesktopShortcutCheckBox.Name = "CreateDesktopShortcutCheckBox";
            this.CreateDesktopShortcutCheckBox.Size = new System.Drawing.Size(160, 19);
            this.CreateDesktopShortcutCheckBox.TabIndex = 1;
            this.CreateDesktopShortcutCheckBox.Text = "Create a desktop shortcut";
            this.CreateDesktopShortcutCheckBox.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(28, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(396, 34);
            this.label7.TabIndex = 0;
            this.label7.Text = "Select the additional tasks you would like Setup to perform while installing R5-R" +
    "eloaded, then click next.";
            // 
            // ProcessTabPage
            // 
            this.ProcessTabPage.Controls.Add(this.DetailedInfoLinkLabe);
            this.ProcessTabPage.Controls.Add(this.TimeLeftLabel);
            this.ProcessTabPage.Controls.Add(this.OverallLogLabel);
            this.ProcessTabPage.Controls.Add(this.DownloadLogLabel);
            this.ProcessTabPage.Controls.Add(this.OverallProgressBar);
            this.ProcessTabPage.Controls.Add(this.DownloadProgressBar);
            this.ProcessTabPage.Controls.Add(this.label11);
            this.ProcessTabPage.Controls.Add(this.label10);
            this.ProcessTabPage.Controls.Add(this.label9);
            this.ProcessTabPage.Controls.Add(this.label8);
            this.ProcessTabPage.Location = new System.Drawing.Point(4, 24);
            this.ProcessTabPage.Name = "ProcessTabPage";
            this.ProcessTabPage.Padding = new System.Windows.Forms.Padding(25);
            this.ProcessTabPage.Size = new System.Drawing.Size(452, 280);
            this.ProcessTabPage.TabIndex = 4;
            this.ProcessTabPage.Text = "Process";
            this.ProcessTabPage.UseVisualStyleBackColor = true;
            // 
            // DetailedInfoLinkLabe
            // 
            this.DetailedInfoLinkLabe.ActiveLinkColor = System.Drawing.Color.Gray;
            this.DetailedInfoLinkLabe.AutoSize = true;
            this.DetailedInfoLinkLabe.LinkColor = System.Drawing.Color.Black;
            this.DetailedInfoLinkLabe.Location = new System.Drawing.Point(309, 240);
            this.DetailedInfoLinkLabe.Name = "DetailedInfoLinkLabe";
            this.DetailedInfoLinkLabe.Size = new System.Drawing.Size(115, 15);
            this.DetailedInfoLinkLabe.TabIndex = 7;
            this.DetailedInfoLinkLabe.TabStop = true;
            this.DetailedInfoLinkLabe.Text = "Detailed information";
            this.DetailedInfoLinkLabe.VisitedLinkColor = System.Drawing.Color.Black;
            // 
            // TimeLeftLabel
            // 
            this.TimeLeftLabel.Location = new System.Drawing.Point(135, 84);
            this.TimeLeftLabel.Name = "TimeLeftLabel";
            this.TimeLeftLabel.Size = new System.Drawing.Size(289, 15);
            this.TimeLeftLabel.TabIndex = 6;
            this.TimeLeftLabel.Text = "in preparation : TimeLeft";
            this.TimeLeftLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // OverallLogLabel
            // 
            this.OverallLogLabel.AutoSize = true;
            this.OverallLogLabel.Location = new System.Drawing.Point(28, 205);
            this.OverallLogLabel.Name = "OverallLogLabel";
            this.OverallLogLabel.Size = new System.Drawing.Size(191, 15);
            this.OverallLogLabel.TabIndex = 5;
            this.OverallLogLabel.Text = "Waiting for download to complete.";
            // 
            // DownloadLogLabel
            // 
            this.DownloadLogLabel.Location = new System.Drawing.Point(28, 128);
            this.DownloadLogLabel.Name = "DownloadLogLabel";
            this.DownloadLogLabel.Size = new System.Drawing.Size(396, 33);
            this.DownloadLogLabel.TabIndex = 5;
            this.DownloadLogLabel.Text = "in preparation.";
            // 
            // OverallProgressBar
            // 
            this.OverallProgressBar.Location = new System.Drawing.Point(28, 179);
            this.OverallProgressBar.Name = "OverallProgressBar";
            this.OverallProgressBar.Size = new System.Drawing.Size(396, 23);
            this.OverallProgressBar.TabIndex = 4;
            // 
            // DownloadProgressBar
            // 
            this.DownloadProgressBar.Location = new System.Drawing.Point(28, 102);
            this.DownloadProgressBar.Name = "DownloadProgressBar";
            this.DownloadProgressBar.Size = new System.Drawing.Size(396, 23);
            this.DownloadProgressBar.TabIndex = 4;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(28, 161);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(102, 15);
            this.label11.TabIndex = 3;
            this.label11.Text = "Installer progress :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(28, 84);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 15);
            this.label10.TabIndex = 2;
            this.label10.Text = "Download status :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(28, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(149, 15);
            this.label9.TabIndex = 1;
            this.label9.Text = "Do not shut down your PC.";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(28, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 25);
            this.label8.TabIndex = 0;
            this.label8.Text = "Installing...";
            // 
            // CompletionTabPage
            // 
            this.CompletionTabPage.Controls.Add(this.LaunchCheckBox);
            this.CompletionTabPage.Controls.Add(this.label14);
            this.CompletionTabPage.Controls.Add(this.label13);
            this.CompletionTabPage.Controls.Add(this.label12);
            this.CompletionTabPage.Location = new System.Drawing.Point(4, 24);
            this.CompletionTabPage.Name = "CompletionTabPage";
            this.CompletionTabPage.Padding = new System.Windows.Forms.Padding(25);
            this.CompletionTabPage.Size = new System.Drawing.Size(452, 280);
            this.CompletionTabPage.TabIndex = 5;
            this.CompletionTabPage.Text = "Completion";
            this.CompletionTabPage.UseVisualStyleBackColor = true;
            // 
            // LaunchCheckBox
            // 
            this.LaunchCheckBox.AutoSize = true;
            this.LaunchCheckBox.Checked = true;
            this.LaunchCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.LaunchCheckBox.Location = new System.Drawing.Point(45, 180);
            this.LaunchCheckBox.Name = "LaunchCheckBox";
            this.LaunchCheckBox.Size = new System.Drawing.Size(155, 19);
            this.LaunchCheckBox.TabIndex = 2;
            this.LaunchCheckBox.Text = "Launch the R5-Reloaded";
            this.LaunchCheckBox.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(45, 115);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(361, 43);
            this.label14.TabIndex = 1;
            this.label14.Text = "Also, since the game client torrent file is generated in the installed directory," +
    " please seed it using the existing torrent client software.";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(45, 72);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(361, 43);
            this.label13.TabIndex = 1;
            this.label13.Text = "The download and installation of R5-Reloaded has been completed successfully.";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label12.Location = new System.Drawing.Point(28, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(213, 25);
            this.label12.TabIndex = 0;
            this.label12.Text = "Installation is Complete.";
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(397, 326);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 1;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // InstallButton
            // 
            this.InstallButton.Location = new System.Drawing.Point(293, 326);
            this.InstallButton.Name = "InstallButton";
            this.InstallButton.Size = new System.Drawing.Size(75, 23);
            this.InstallButton.TabIndex = 1;
            this.InstallButton.Text = "Install";
            this.InstallButton.UseVisualStyleBackColor = true;
            // 
            // NextButton
            // 
            this.NextButton.Location = new System.Drawing.Point(212, 326);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(75, 23);
            this.NextButton.TabIndex = 1;
            this.NextButton.Text = "Next >";
            this.NextButton.UseVisualStyleBackColor = true;
            // 
            // BackButton
            // 
            this.BackButton.Location = new System.Drawing.Point(131, 326);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(75, 23);
            this.BackButton.TabIndex = 1;
            this.BackButton.Text = "< Back";
            this.BackButton.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Controls.Add(this.BackButton);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.InstallButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.MainTabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "R5 Reloaded Installer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainTabControl.ResumeLayout(false);
            this.IntroductionTabPage.ResumeLayout(false);
            this.IntroductionTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.InformationTabPage.ResumeLayout(false);
            this.InformationTabPage.PerformLayout();
            this.PlaceOfInstallationTabPage.ResumeLayout(false);
            this.PlaceOfInstallationTabPage.PerformLayout();
            this.OptionTabPage.ResumeLayout(false);
            this.OptionTabPage.PerformLayout();
            this.ProcessTabPage.ResumeLayout(false);
            this.ProcessTabPage.PerformLayout();
            this.CompletionTabPage.ResumeLayout(false);
            this.CompletionTabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TabControl MainTabControl;
        public System.Windows.Forms.TabPage IntroductionTabPage;
        public System.Windows.Forms.TabPage InformationTabPage;
        public System.Windows.Forms.Button CancelButton;
        public System.Windows.Forms.Button InstallButton;
        public System.Windows.Forms.Button NextButton;
        public System.Windows.Forms.Button BackButton;
        public System.Windows.Forms.TabPage PlaceOfInstallationTabPage;
        public System.Windows.Forms.TabPage OptionTabPage;
        public System.Windows.Forms.TabPage ProcessTabPage;
        public System.Windows.Forms.TabPage CompletionTabPage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.CheckBox AgreeCheckBox;
        public System.Windows.Forms.LinkLabel WebsiteLinkLabel;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.LinkLabel DiscordLinkLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.LinkLabel ReloadDriveSizeLinkLabel;
        public System.Windows.Forms.Label FileSizeLabel;
        public System.Windows.Forms.Label DriveSizeLabel;
        public System.Windows.Forms.Button BrowseButton;
        public System.Windows.Forms.TextBox InstallLinkTextBox;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.CheckBox AddToStartMenuCheckBox;
        public System.Windows.Forms.CheckBox CreateDesktopShortcutCheckBox;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.LinkLabel DetailedInfoLinkLabe;
        private System.Windows.Forms.Label TimeLeftLabel;
        private System.Windows.Forms.Label OverallLogLabel;
        private System.Windows.Forms.Label DownloadLogLabel;
        private System.Windows.Forms.ProgressBar OverallProgressBar;
        private System.Windows.Forms.ProgressBar DownloadProgressBar;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.CheckBox LaunchCheckBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
    }
}

