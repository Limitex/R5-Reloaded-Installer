using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace R5_Reloaded_Installer_GUI
{
    public partial class MainForm : Form
    {

        private static bool ButtonSelectFlug = false;
        public MainForm()
        {
            InitializeComponent();
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

        private void NextButton_Click(object sender, EventArgs e)
        {
            ButtonSelectFlug = true;
            if(MainTabControl.SelectedIndex < MainTabControl.TabCount - 1)
                MainTabControl.SelectedIndex += 1;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            ButtonSelectFlug = true;
            if(0 < MainTabControl.SelectedIndex)
                MainTabControl.SelectedIndex -= 1;
        }
    }
}
