﻿#region UsingDirectives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using skyperecorder.Library;
using System.IO; 
#endregion

namespace skyperecorder.GUI
{
    /// <summary>
    /// Class Interface manage to Skype Manager Library
    /// </summary>
    public partial class F_Main : Form
    {
        /// <summary>
        /// manager Skype API
        /// </summary>
        Manager managerSkype = null; 

        /// <summary>
        /// Default constructor
        /// </summary>
        public F_Main()
        {
            InitializeComponent();
            AccessToSkype(false);
        }

        /// <summary>
        /// method switch access to button and etc. (in the future)
        /// when API Skype is accesible or isn't
        /// </summary>
        /// <param name="isSkypeAccess">true is connect successfull</param>
        private void AccessToSkype(bool isSkypeAccess)
        {
            if (isSkypeAccess)
            {
                B_attachToSkype.Enabled = false;
                B_freeAPISkype.Enabled = true;
            }
            else
            {
                B_attachToSkype.Enabled = true;
                B_freeAPISkype.Enabled = false;
            }
        }

        /// <summary>
        /// Event when press button "Attach to Skype"
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e">Button events</param>
        private void B_attachToSkype_Click(object sender, EventArgs e)
        {
            //create manager instance
            managerSkype = new Manager();

            //inform user when manager success access to Skype
            AccessToSkype(true);
        }

        /// <summary>
        /// Event when press button "Free API Skype"
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e">Button events</param>
        private void B_freeAPISkype_Click(object sender, EventArgs e)
        {
            //free manager instance
            managerSkype = null;

            //inform user wher manager disconnect from API Skype
            AccessToSkype(false);
        }

        /// <summary>
        /// Event when press button "Open Chat Folder"
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e">Button events</param>
        private void B_openChatFolder_Click(object sender, EventArgs e)
        {
            if (managerSkype != null)
            {
                OpenFolderInExplorer(managerSkype.tempChatDirectory); 
            }
        }

        /// <summary>
        /// Event when press button "Open Voice folder"
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e">Button events</param>
        private void B_openVoiceFolder_Click(object sender, EventArgs e)
        {
            if (managerSkype != null)
            {
                OpenFolderInExplorer(managerSkype.tempVoiceDirectory); 
            }
        }

        /// <summary>
        /// Event when press button "Open Video folder"
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e">Button events</param>
        private void B_openVideoFolder_Click(object sender, EventArgs e)
        {
            if (managerSkype != null)
            {
                OpenFolderInExplorer(managerSkype.tempVideoDirectory); 
            }
        }

        private void OpenFolderInExplorer(string path)
        {
            if (Directory.Exists(path))
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.FileName = @"explorer";
                process.StartInfo.Arguments = path;
                process.Start();
            }
        }
    }
}
