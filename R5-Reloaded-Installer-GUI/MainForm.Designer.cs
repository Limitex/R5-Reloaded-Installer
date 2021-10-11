
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
            this.CancelButton = new System.Windows.Forms.Button();
            this.InstallButton = new System.Windows.Forms.Button();
            this.NextButton = new System.Windows.Forms.Button();
            this.BackButton = new System.Windows.Forms.Button();
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.IntroductionTabPage = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TopIcon = new System.Windows.Forms.PictureBox();
            this.InformationTabPage = new System.Windows.Forms.TabPage();
            this.label15 = new System.Windows.Forms.Label();
            this.WebsiteLinkLabel = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.DiscordLinkLabel = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.AgreeCheckBox = new System.Windows.Forms.CheckBox();
            this.PlaceOfInstallationTabPage = new System.Windows.Forms.TabPage();
            this.ReloadDriveSizeLinkLabel = new System.Windows.Forms.LinkLabel();
            this.SizeLabel = new System.Windows.Forms.Label();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.InstallLinkTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.OptionTabPage = new System.Windows.Forms.TabPage();
            this.DhtListenPortCheckBox = new System.Windows.Forms.CheckBox();
            this.ListenPortCheckBox = new System.Windows.Forms.CheckBox();
            this.DhtListenPortNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.ListenPortNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.AddToStartMenuCheckBox = new System.Windows.Forms.CheckBox();
            this.CreateDesktopShortcutCheckBox = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ProcessTabPage = new System.Windows.Forms.TabPage();
            this.DetailedInfoLinkLabe = new System.Windows.Forms.LinkLabel();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.OverallStatusLabel = new System.Windows.Forms.Label();
            this.TimeLeftLabel = new System.Windows.Forms.Label();
            this.DownloadStatusLabel = new System.Windows.Forms.Label();
            this.OverallProgressBar = new System.Windows.Forms.ProgressBar();
            this.DownloadProgressBar = new System.Windows.Forms.ProgressBar();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.CompletionTabPage = new System.Windows.Forms.TabPage();
            this.LaunchCheckBox = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.MainTabControl.SuspendLayout();
            this.IntroductionTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TopIcon)).BeginInit();
            this.InformationTabPage.SuspendLayout();
            this.PlaceOfInstallationTabPage.SuspendLayout();
            this.OptionTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DhtListenPortNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListenPortNumericUpDown)).BeginInit();
            this.ProcessTabPage.SuspendLayout();
            this.CompletionTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(397, 326);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 0;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // InstallButton
            // 
            this.InstallButton.Location = new System.Drawing.Point(293, 326);
            this.InstallButton.Name = "InstallButton";
            this.InstallButton.Size = new System.Drawing.Size(75, 23);
            this.InstallButton.TabIndex = 0;
            this.InstallButton.Text = "Install";
            this.InstallButton.UseVisualStyleBackColor = true;
            this.InstallButton.Click += new System.EventHandler(this.InstallButton_Click);
            // 
            // NextButton
            // 
            this.NextButton.Location = new System.Drawing.Point(212, 326);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(75, 23);
            this.NextButton.TabIndex = 0;
            this.NextButton.Text = "Next >";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // BackButton
            // 
            this.BackButton.Location = new System.Drawing.Point(131, 326);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(75, 23);
            this.BackButton.TabIndex = 0;
            this.BackButton.Text = "< Back";
            this.BackButton.UseVisualStyleBackColor = true;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
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
            this.MainTabControl.TabIndex = 1;
            this.MainTabControl.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.MainTabControl_Selecting);
            // 
            // IntroductionTabPage
            // 
            this.IntroductionTabPage.Controls.Add(this.label2);
            this.IntroductionTabPage.Controls.Add(this.label1);
            this.IntroductionTabPage.Controls.Add(this.TopIcon);
            this.IntroductionTabPage.Location = new System.Drawing.Point(4, 24);
            this.IntroductionTabPage.Name = "IntroductionTabPage";
            this.IntroductionTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.IntroductionTabPage.Size = new System.Drawing.Size(452, 280);
            this.IntroductionTabPage.TabIndex = 0;
            this.IntroductionTabPage.Text = "Introduction";
            this.IntroductionTabPage.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(134, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(296, 116);
            this.label2.TabIndex = 2;
            this.label2.Text = "Welcome to the installer for Your R5-Reloaded.\r\n\r\nThe Installer will install R5-R" +
    "eloaded on your computer.\r\n\r\nPress the Cancel button at the bottom right, if you" +
    " want to cancel.";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(134, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 34);
            this.label1.TabIndex = 1;
            this.label1.Text = "Welcome!";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TopIcon
            // 
            this.TopIcon.Image = global::R5_Reloaded_Installer_GUI.Properties.Resources.r5_icon;
            this.TopIcon.Location = new System.Drawing.Point(16, 87);
            this.TopIcon.Name = "TopIcon";
            this.TopIcon.Size = new System.Drawing.Size(100, 100);
            this.TopIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.TopIcon.TabIndex = 0;
            this.TopIcon.TabStop = false;
            // 
            // InformationTabPage
            // 
            this.InformationTabPage.Controls.Add(this.label15);
            this.InformationTabPage.Controls.Add(this.WebsiteLinkLabel);
            this.InformationTabPage.Controls.Add(this.label4);
            this.InformationTabPage.Controls.Add(this.DiscordLinkLabel);
            this.InformationTabPage.Controls.Add(this.label3);
            this.InformationTabPage.Controls.Add(this.AgreeCheckBox);
            this.InformationTabPage.Location = new System.Drawing.Point(4, 24);
            this.InformationTabPage.Name = "InformationTabPage";
            this.InformationTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.InformationTabPage.Size = new System.Drawing.Size(452, 280);
            this.InformationTabPage.TabIndex = 1;
            this.InformationTabPage.Text = "Information";
            this.InformationTabPage.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label15.Location = new System.Drawing.Point(19, 17);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(390, 25);
            this.label15.TabIndex = 5;
            this.label15.Text = "Please read and confirm the checkbox below.\r\n";
            // 
            // WebsiteLinkLabel
            // 
            this.WebsiteLinkLabel.AutoSize = true;
            this.WebsiteLinkLabel.Location = new System.Drawing.Point(28, 151);
            this.WebsiteLinkLabel.Name = "WebsiteLinkLabel";
            this.WebsiteLinkLabel.Size = new System.Drawing.Size(165, 15);
            this.WebsiteLinkLabel.TabIndex = 4;
            this.WebsiteLinkLabel.TabStop = true;
            this.WebsiteLinkLabel.Text = "https://r5reloaded.gitbook.io/";
            this.WebsiteLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.WebsiteLinkLabel_LinkClicked);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(255, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Click here for a reference to the official website.";
            // 
            // DiscordLinkLabel
            // 
            this.DiscordLinkLabel.AutoSize = true;
            this.DiscordLinkLabel.Location = new System.Drawing.Point(28, 81);
            this.DiscordLinkLabel.Name = "DiscordLinkLabel";
            this.DiscordLinkLabel.Size = new System.Drawing.Size(215, 15);
            this.DiscordLinkLabel.TabIndex = 2;
            this.DiscordLinkLabel.TabStop = true;
            this.DiscordLinkLabel.Text = "https://discord.com/invite/jqMkUdXrBr";
            this.DiscordLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.DiscordLinkLabel_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(225, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Join us for the official Discord server here.";
            // 
            // AgreeCheckBox
            // 
            this.AgreeCheckBox.AutoSize = true;
            this.AgreeCheckBox.Location = new System.Drawing.Point(28, 223);
            this.AgreeCheckBox.Name = "AgreeCheckBox";
            this.AgreeCheckBox.Size = new System.Drawing.Size(318, 19);
            this.AgreeCheckBox.TabIndex = 0;
            this.AgreeCheckBox.Text = "I won\'t use cosmetics such as heirlooms from the game.\r\n";
            this.AgreeCheckBox.UseVisualStyleBackColor = true;
            this.AgreeCheckBox.CheckedChanged += new System.EventHandler(this.AgreeCheckBox_CheckedChanged);
            // 
            // PlaceOfInstallationTabPage
            // 
            this.PlaceOfInstallationTabPage.Controls.Add(this.ReloadDriveSizeLinkLabel);
            this.PlaceOfInstallationTabPage.Controls.Add(this.SizeLabel);
            this.PlaceOfInstallationTabPage.Controls.Add(this.BrowseButton);
            this.PlaceOfInstallationTabPage.Controls.Add(this.InstallLinkTextBox);
            this.PlaceOfInstallationTabPage.Controls.Add(this.label5);
            this.PlaceOfInstallationTabPage.Location = new System.Drawing.Point(4, 24);
            this.PlaceOfInstallationTabPage.Name = "PlaceOfInstallationTabPage";
            this.PlaceOfInstallationTabPage.Size = new System.Drawing.Size(452, 280);
            this.PlaceOfInstallationTabPage.TabIndex = 2;
            this.PlaceOfInstallationTabPage.Text = "Place of installation";
            this.PlaceOfInstallationTabPage.UseVisualStyleBackColor = true;
            // 
            // ReloadDriveSizeLinkLabel
            // 
            this.ReloadDriveSizeLinkLabel.AutoSize = true;
            this.ReloadDriveSizeLinkLabel.LinkColor = System.Drawing.Color.Black;
            this.ReloadDriveSizeLinkLabel.Location = new System.Drawing.Point(36, 243);
            this.ReloadDriveSizeLinkLabel.Name = "ReloadDriveSizeLinkLabel";
            this.ReloadDriveSizeLinkLabel.Size = new System.Drawing.Size(96, 15);
            this.ReloadDriveSizeLinkLabel.TabIndex = 4;
            this.ReloadDriveSizeLinkLabel.TabStop = true;
            this.ReloadDriveSizeLinkLabel.Text = "Reload Drive Size";
            this.ReloadDriveSizeLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ReloadDriveSizeLinkLabel_LinkClicked);
            // 
            // SizeLabel
            // 
            this.SizeLabel.AutoSize = true;
            this.SizeLabel.Location = new System.Drawing.Point(36, 181);
            this.SizeLabel.Name = "SizeLabel";
            this.SizeLabel.Size = new System.Drawing.Size(101, 45);
            this.SizeLabel.TabIndex = 3;
            this.SizeLabel.Text = "File size : 0.00GB\r\n\r\nDrive size : 0.00GB";
            // 
            // BrowseButton
            // 
            this.BrowseButton.Location = new System.Drawing.Point(352, 118);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(75, 23);
            this.BrowseButton.TabIndex = 2;
            this.BrowseButton.Text = "Browse";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // InstallLinkTextBox
            // 
            this.InstallLinkTextBox.Location = new System.Drawing.Point(24, 118);
            this.InstallLinkTextBox.Name = "InstallLinkTextBox";
            this.InstallLinkTextBox.Size = new System.Drawing.Size(322, 23);
            this.InstallLinkTextBox.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(24, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(403, 40);
            this.label5.TabIndex = 0;
            this.label5.Text = "Setup will install Everything in the following folder. To install in a different " +
    "folder, click Browse and select another folder, Click Next to continue.";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OptionTabPage
            // 
            this.OptionTabPage.Controls.Add(this.DhtListenPortCheckBox);
            this.OptionTabPage.Controls.Add(this.ListenPortCheckBox);
            this.OptionTabPage.Controls.Add(this.DhtListenPortNumericUpDown);
            this.OptionTabPage.Controls.Add(this.ListenPortNumericUpDown);
            this.OptionTabPage.Controls.Add(this.label16);
            this.OptionTabPage.Controls.Add(this.label14);
            this.OptionTabPage.Controls.Add(this.AddToStartMenuCheckBox);
            this.OptionTabPage.Controls.Add(this.CreateDesktopShortcutCheckBox);
            this.OptionTabPage.Controls.Add(this.label6);
            this.OptionTabPage.Location = new System.Drawing.Point(4, 24);
            this.OptionTabPage.Name = "OptionTabPage";
            this.OptionTabPage.Size = new System.Drawing.Size(452, 280);
            this.OptionTabPage.TabIndex = 3;
            this.OptionTabPage.Text = "Option";
            this.OptionTabPage.UseVisualStyleBackColor = true;
            // 
            // DhtListenPortCheckBox
            // 
            this.DhtListenPortCheckBox.AutoSize = true;
            this.DhtListenPortCheckBox.Location = new System.Drawing.Point(52, 213);
            this.DhtListenPortCheckBox.Name = "DhtListenPortCheckBox";
            this.DhtListenPortCheckBox.Size = new System.Drawing.Size(122, 19);
            this.DhtListenPortCheckBox.TabIndex = 4;
            this.DhtListenPortCheckBox.Text = "--dht-listen-port=";
            this.DhtListenPortCheckBox.UseVisualStyleBackColor = true;
            this.DhtListenPortCheckBox.CheckedChanged += new System.EventHandler(this.DhtListenPortCheckBox_CheckedChanged);
            // 
            // ListenPortCheckBox
            // 
            this.ListenPortCheckBox.AutoSize = true;
            this.ListenPortCheckBox.Location = new System.Drawing.Point(52, 188);
            this.ListenPortCheckBox.Name = "ListenPortCheckBox";
            this.ListenPortCheckBox.Size = new System.Drawing.Size(99, 19);
            this.ListenPortCheckBox.TabIndex = 4;
            this.ListenPortCheckBox.Text = "--listen-port=";
            this.ListenPortCheckBox.UseVisualStyleBackColor = true;
            this.ListenPortCheckBox.CheckedChanged += new System.EventHandler(this.ListenPortCheckBox_CheckedChanged);
            // 
            // DhtListenPortNumericUpDown
            // 
            this.DhtListenPortNumericUpDown.Enabled = false;
            this.DhtListenPortNumericUpDown.Location = new System.Drawing.Point(180, 212);
            this.DhtListenPortNumericUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.DhtListenPortNumericUpDown.Name = "DhtListenPortNumericUpDown";
            this.DhtListenPortNumericUpDown.Size = new System.Drawing.Size(105, 23);
            this.DhtListenPortNumericUpDown.TabIndex = 3;
            // 
            // ListenPortNumericUpDown
            // 
            this.ListenPortNumericUpDown.Enabled = false;
            this.ListenPortNumericUpDown.Location = new System.Drawing.Point(180, 187);
            this.ListenPortNumericUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.ListenPortNumericUpDown.Name = "ListenPortNumericUpDown";
            this.ListenPortNumericUpDown.Size = new System.Drawing.Size(105, 23);
            this.ListenPortNumericUpDown.TabIndex = 3;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(34, 160);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(91, 15);
            this.label16.TabIndex = 2;
            this.label16.Text = "aria2 arguments";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(24, 136);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(103, 15);
            this.label14.TabIndex = 2;
            this.label14.Text = "Advanced options";
            // 
            // AddToStartMenuCheckBox
            // 
            this.AddToStartMenuCheckBox.AutoSize = true;
            this.AddToStartMenuCheckBox.Checked = true;
            this.AddToStartMenuCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AddToStartMenuCheckBox.Location = new System.Drawing.Point(40, 98);
            this.AddToStartMenuCheckBox.Name = "AddToStartMenuCheckBox";
            this.AddToStartMenuCheckBox.Size = new System.Drawing.Size(121, 19);
            this.AddToStartMenuCheckBox.TabIndex = 1;
            this.AddToStartMenuCheckBox.Text = "Add to start menu";
            this.AddToStartMenuCheckBox.UseVisualStyleBackColor = true;
            // 
            // CreateDesktopShortcutCheckBox
            // 
            this.CreateDesktopShortcutCheckBox.AutoSize = true;
            this.CreateDesktopShortcutCheckBox.Checked = true;
            this.CreateDesktopShortcutCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CreateDesktopShortcutCheckBox.Location = new System.Drawing.Point(40, 73);
            this.CreateDesktopShortcutCheckBox.Name = "CreateDesktopShortcutCheckBox";
            this.CreateDesktopShortcutCheckBox.Size = new System.Drawing.Size(160, 19);
            this.CreateDesktopShortcutCheckBox.TabIndex = 1;
            this.CreateDesktopShortcutCheckBox.Text = "Create a desktop shortcut";
            this.CreateDesktopShortcutCheckBox.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(24, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(389, 42);
            this.label6.TabIndex = 0;
            this.label6.Text = "Select the additional tasks you would like Setup to perform while installing R5-R" +
    "eloaded, then click next.";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ProcessTabPage
            // 
            this.ProcessTabPage.Controls.Add(this.DetailedInfoLinkLabe);
            this.ProcessTabPage.Controls.Add(this.label10);
            this.ProcessTabPage.Controls.Add(this.label9);
            this.ProcessTabPage.Controls.Add(this.OverallStatusLabel);
            this.ProcessTabPage.Controls.Add(this.TimeLeftLabel);
            this.ProcessTabPage.Controls.Add(this.DownloadStatusLabel);
            this.ProcessTabPage.Controls.Add(this.OverallProgressBar);
            this.ProcessTabPage.Controls.Add(this.DownloadProgressBar);
            this.ProcessTabPage.Controls.Add(this.label8);
            this.ProcessTabPage.Controls.Add(this.label7);
            this.ProcessTabPage.Location = new System.Drawing.Point(4, 24);
            this.ProcessTabPage.Name = "ProcessTabPage";
            this.ProcessTabPage.Size = new System.Drawing.Size(452, 280);
            this.ProcessTabPage.TabIndex = 4;
            this.ProcessTabPage.Text = "Process";
            this.ProcessTabPage.UseVisualStyleBackColor = true;
            // 
            // DetailedInfoLinkLabe
            // 
            this.DetailedInfoLinkLabe.AutoSize = true;
            this.DetailedInfoLinkLabe.LinkColor = System.Drawing.Color.Black;
            this.DetailedInfoLinkLabe.Location = new System.Drawing.Point(306, 251);
            this.DetailedInfoLinkLabe.Name = "DetailedInfoLinkLabe";
            this.DetailedInfoLinkLabe.Size = new System.Drawing.Size(115, 15);
            this.DetailedInfoLinkLabe.TabIndex = 7;
            this.DetailedInfoLinkLabe.TabStop = true;
            this.DetailedInfoLinkLabe.Text = "Detailed information";
            this.DetailedInfoLinkLabe.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.DetailedInfoLinkLabe_LinkClicked);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 174);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(102, 15);
            this.label10.TabIndex = 6;
            this.label10.Text = "Installer progress :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 86);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 15);
            this.label9.TabIndex = 5;
            this.label9.Text = "Download status :";
            // 
            // OverallStatusLabel
            // 
            this.OverallStatusLabel.AutoSize = true;
            this.OverallStatusLabel.Location = new System.Drawing.Point(15, 218);
            this.OverallStatusLabel.Name = "OverallStatusLabel";
            this.OverallStatusLabel.Size = new System.Drawing.Size(39, 15);
            this.OverallStatusLabel.TabIndex = 4;
            this.OverallStatusLabel.Text = "Status";
            // 
            // TimeLeftLabel
            // 
            this.TimeLeftLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.TimeLeftLabel.Location = new System.Drawing.Point(122, 86);
            this.TimeLeftLabel.Name = "TimeLeftLabel";
            this.TimeLeftLabel.Size = new System.Drawing.Size(313, 15);
            this.TimeLeftLabel.TabIndex = 3;
            this.TimeLeftLabel.Text = "in preparation : TimeLeft";
            this.TimeLeftLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DownloadStatusLabel
            // 
            this.DownloadStatusLabel.AutoSize = true;
            this.DownloadStatusLabel.Location = new System.Drawing.Point(19, 130);
            this.DownloadStatusLabel.Name = "DownloadStatusLabel";
            this.DownloadStatusLabel.Size = new System.Drawing.Size(39, 15);
            this.DownloadStatusLabel.TabIndex = 3;
            this.DownloadStatusLabel.Text = "Status";
            // 
            // OverallProgressBar
            // 
            this.OverallProgressBar.Location = new System.Drawing.Point(19, 192);
            this.OverallProgressBar.Name = "OverallProgressBar";
            this.OverallProgressBar.Size = new System.Drawing.Size(416, 23);
            this.OverallProgressBar.TabIndex = 2;
            // 
            // DownloadProgressBar
            // 
            this.DownloadProgressBar.Location = new System.Drawing.Point(19, 104);
            this.DownloadProgressBar.Name = "DownloadProgressBar";
            this.DownloadProgressBar.Size = new System.Drawing.Size(416, 23);
            this.DownloadProgressBar.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(149, 15);
            this.label8.TabIndex = 1;
            this.label8.Text = "Do not shut down your PC.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(19, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 25);
            this.label7.TabIndex = 0;
            this.label7.Text = "Installing...";
            // 
            // CompletionTabPage
            // 
            this.CompletionTabPage.Controls.Add(this.LaunchCheckBox);
            this.CompletionTabPage.Controls.Add(this.label13);
            this.CompletionTabPage.Controls.Add(this.label12);
            this.CompletionTabPage.Controls.Add(this.label11);
            this.CompletionTabPage.Location = new System.Drawing.Point(4, 24);
            this.CompletionTabPage.Name = "CompletionTabPage";
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
            this.LaunchCheckBox.Location = new System.Drawing.Point(35, 170);
            this.LaunchCheckBox.Name = "LaunchCheckBox";
            this.LaunchCheckBox.Size = new System.Drawing.Size(155, 19);
            this.LaunchCheckBox.TabIndex = 2;
            this.LaunchCheckBox.Text = "Launch the R5-Reloaded";
            this.LaunchCheckBox.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(35, 105);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(372, 39);
            this.label13.TabIndex = 1;
            this.label13.Text = "Also, since the game client torrent file is generated in the installed directory," +
    " please seed it using the existing torrent client software.";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(35, 66);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(372, 39);
            this.label12.TabIndex = 1;
            this.label12.Text = "The download and installation of R5-Reloaded has been completed successfully.";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label11.Location = new System.Drawing.Point(24, 28);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(213, 25);
            this.label11.TabIndex = 0;
            this.label11.Text = "Installation is Complete.";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Controls.Add(this.MainTabControl);
            this.Controls.Add(this.BackButton);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.InstallButton);
            this.Controls.Add(this.CancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(500, 400);
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "MainForm";
            this.Text = "R5-Reloaded Installer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainTabControl.ResumeLayout(false);
            this.IntroductionTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TopIcon)).EndInit();
            this.InformationTabPage.ResumeLayout(false);
            this.InformationTabPage.PerformLayout();
            this.PlaceOfInstallationTabPage.ResumeLayout(false);
            this.PlaceOfInstallationTabPage.PerformLayout();
            this.OptionTabPage.ResumeLayout(false);
            this.OptionTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DhtListenPortNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListenPortNumericUpDown)).EndInit();
            this.ProcessTabPage.ResumeLayout(false);
            this.ProcessTabPage.PerformLayout();
            this.CompletionTabPage.ResumeLayout(false);
            this.CompletionTabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button InstallButton;
        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.Button BackButton;
        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TabPage IntroductionTabPage;
        private System.Windows.Forms.TabPage InformationTabPage;
        private System.Windows.Forms.TabPage PlaceOfInstallationTabPage;
        private System.Windows.Forms.TabPage OptionTabPage;
        private System.Windows.Forms.TabPage ProcessTabPage;
        private System.Windows.Forms.TabPage CompletionTabPage;
        private System.Windows.Forms.PictureBox TopIcon;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel WebsiteLinkLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel DiscordLinkLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox AgreeCheckBox;
        private System.Windows.Forms.Label SizeLabel;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.TextBox InstallLinkTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox AddToStartMenuCheckBox;
        private System.Windows.Forms.CheckBox CreateDesktopShortcutCheckBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label OverallStatusLabel;
        private System.Windows.Forms.Label DownloadStatusLabel;
        private System.Windows.Forms.ProgressBar OverallProgressBar;
        private System.Windows.Forms.ProgressBar DownloadProgressBar;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label TimeLeftLabel;
        private System.Windows.Forms.CheckBox LaunchCheckBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox DhtListenPortCheckBox;
        private System.Windows.Forms.CheckBox ListenPortCheckBox;
        private System.Windows.Forms.NumericUpDown DhtListenPortNumericUpDown;
        private System.Windows.Forms.NumericUpDown ListenPortNumericUpDown;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.LinkLabel DetailedInfoLinkLabe;
        private System.Windows.Forms.LinkLabel ReloadDriveSizeLinkLabel;
    }
}

