
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
            this.WebsiteLinkLabel = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.DiscordLinkLabel = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.AgreeCheckBox = new System.Windows.Forms.CheckBox();
            this.PlaceOfInstallationTabPage = new System.Windows.Forms.TabPage();
            this.OptionTabPage = new System.Windows.Forms.TabPage();
            this.ProcessTabPage = new System.Windows.Forms.TabPage();
            this.CompletionTabPage = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.InstallLinkTextBox = new System.Windows.Forms.TextBox();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.SizeLabel = new System.Windows.Forms.Label();
            this.MainTabControl.SuspendLayout();
            this.IntroductionTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TopIcon)).BeginInit();
            this.InformationTabPage.SuspendLayout();
            this.PlaceOfInstallationTabPage.SuspendLayout();
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
            // WebsiteLinkLabel
            // 
            this.WebsiteLinkLabel.AutoSize = true;
            this.WebsiteLinkLabel.Location = new System.Drawing.Point(28, 119);
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
            this.label4.Location = new System.Drawing.Point(28, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(255, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Click here for a reference to the official website.";
            // 
            // DiscordLinkLabel
            // 
            this.DiscordLinkLabel.AutoSize = true;
            this.DiscordLinkLabel.Location = new System.Drawing.Point(28, 49);
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
            this.label3.Location = new System.Drawing.Point(28, 25);
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
            this.AgreeCheckBox.Size = new System.Drawing.Size(232, 19);
            this.AgreeCheckBox.TabIndex = 0;
            this.AgreeCheckBox.Text = " I don\'t allow cosmetics from the game.";
            this.AgreeCheckBox.UseVisualStyleBackColor = true;
            this.AgreeCheckBox.CheckedChanged += new System.EventHandler(this.AgreeCheckBox_CheckedChanged);
            // 
            // PlaceOfInstallationTabPage
            // 
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
            // OptionTabPage
            // 
            this.OptionTabPage.Location = new System.Drawing.Point(4, 24);
            this.OptionTabPage.Name = "OptionTabPage";
            this.OptionTabPage.Size = new System.Drawing.Size(452, 280);
            this.OptionTabPage.TabIndex = 3;
            this.OptionTabPage.Text = "Option";
            this.OptionTabPage.UseVisualStyleBackColor = true;
            // 
            // ProcessTabPage
            // 
            this.ProcessTabPage.Location = new System.Drawing.Point(4, 24);
            this.ProcessTabPage.Name = "ProcessTabPage";
            this.ProcessTabPage.Size = new System.Drawing.Size(452, 280);
            this.ProcessTabPage.TabIndex = 4;
            this.ProcessTabPage.Text = "Process";
            this.ProcessTabPage.UseVisualStyleBackColor = true;
            // 
            // CompletionTabPage
            // 
            this.CompletionTabPage.Location = new System.Drawing.Point(4, 24);
            this.CompletionTabPage.Name = "CompletionTabPage";
            this.CompletionTabPage.Size = new System.Drawing.Size(452, 280);
            this.CompletionTabPage.TabIndex = 5;
            this.CompletionTabPage.Text = "Completion";
            this.CompletionTabPage.UseVisualStyleBackColor = true;
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
            // InstallLinkTextBox
            // 
            this.InstallLinkTextBox.Location = new System.Drawing.Point(24, 118);
            this.InstallLinkTextBox.Name = "InstallLinkTextBox";
            this.InstallLinkTextBox.Size = new System.Drawing.Size(322, 23);
            this.InstallLinkTextBox.TabIndex = 1;
            // 
            // BrowseButton
            // 
            this.BrowseButton.Location = new System.Drawing.Point(352, 118);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(75, 23);
            this.BrowseButton.TabIndex = 2;
            this.BrowseButton.Text = "Browse";
            this.BrowseButton.UseVisualStyleBackColor = true;
            // 
            // SizeLabel
            // 
            this.SizeLabel.AutoSize = true;
            this.SizeLabel.Location = new System.Drawing.Point(36, 194);
            this.SizeLabel.Name = "SizeLabel";
            this.SizeLabel.Size = new System.Drawing.Size(101, 45);
            this.SizeLabel.TabIndex = 3;
            this.SizeLabel.Text = "File size : 0.00GB\r\n\r\nDrive size : 0.00GB";
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
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainTabControl.ResumeLayout(false);
            this.IntroductionTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TopIcon)).EndInit();
            this.InformationTabPage.ResumeLayout(false);
            this.InformationTabPage.PerformLayout();
            this.PlaceOfInstallationTabPage.ResumeLayout(false);
            this.PlaceOfInstallationTabPage.PerformLayout();
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
    }
}

