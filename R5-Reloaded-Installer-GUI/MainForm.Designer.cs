﻿
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
            this.InformationTabPage = new System.Windows.Forms.TabPage();
            this.PlaceOfInstallationTabPage = new System.Windows.Forms.TabPage();
            this.OptionTabPage = new System.Windows.Forms.TabPage();
            this.ProcessTabPage = new System.Windows.Forms.TabPage();
            this.CompletionTabPage = new System.Windows.Forms.TabPage();
            this.MainTabControl.SuspendLayout();
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
            // 
            // InstallButton
            // 
            this.InstallButton.Location = new System.Drawing.Point(293, 326);
            this.InstallButton.Name = "InstallButton";
            this.InstallButton.Size = new System.Drawing.Size(75, 23);
            this.InstallButton.TabIndex = 0;
            this.InstallButton.Text = "Install";
            this.InstallButton.UseVisualStyleBackColor = true;
            // 
            // NextButton
            // 
            this.NextButton.Location = new System.Drawing.Point(212, 326);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(75, 23);
            this.NextButton.TabIndex = 0;
            this.NextButton.Text = "Next >";
            this.NextButton.UseVisualStyleBackColor = true;
            // 
            // BackButton
            // 
            this.BackButton.Location = new System.Drawing.Point(131, 326);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(75, 23);
            this.BackButton.TabIndex = 0;
            this.BackButton.Text = "< Back";
            this.BackButton.UseVisualStyleBackColor = true;
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
            // 
            // IntroductionTabPage
            // 
            this.IntroductionTabPage.Location = new System.Drawing.Point(4, 24);
            this.IntroductionTabPage.Name = "IntroductionTabPage";
            this.IntroductionTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.IntroductionTabPage.Size = new System.Drawing.Size(452, 280);
            this.IntroductionTabPage.TabIndex = 0;
            this.IntroductionTabPage.Text = "Introduction";
            this.IntroductionTabPage.UseVisualStyleBackColor = true;
            // 
            // InformationTabPage
            // 
            this.InformationTabPage.Location = new System.Drawing.Point(4, 24);
            this.InformationTabPage.Name = "InformationTabPage";
            this.InformationTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.InformationTabPage.Size = new System.Drawing.Size(452, 280);
            this.InformationTabPage.TabIndex = 1;
            this.InformationTabPage.Text = "Information";
            this.InformationTabPage.UseVisualStyleBackColor = true;
            // 
            // PlaceOfInstallationTabPage
            // 
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
            this.MainTabControl.ResumeLayout(false);
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
    }
}

