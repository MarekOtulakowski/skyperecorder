namespace skyperecorder.GUI
{
    partial class F_Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.B_attachToSkype = new System.Windows.Forms.Button();
            this.B_freeAPISkype = new System.Windows.Forms.Button();
            this.B_openChatFolder = new System.Windows.Forms.Button();
            this.B_openVoiceFolder = new System.Windows.Forms.Button();
            this.B_openVideoFolder = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // B_attachToSkype
            // 
            this.B_attachToSkype.Location = new System.Drawing.Point(36, 25);
            this.B_attachToSkype.Name = "B_attachToSkype";
            this.B_attachToSkype.Size = new System.Drawing.Size(97, 23);
            this.B_attachToSkype.TabIndex = 0;
            this.B_attachToSkype.Text = "Attach to Skype";
            this.B_attachToSkype.UseVisualStyleBackColor = true;
            this.B_attachToSkype.Click += new System.EventHandler(this.B_attachToSkype_Click);
            // 
            // B_freeAPISkype
            // 
            this.B_freeAPISkype.Location = new System.Drawing.Point(152, 25);
            this.B_freeAPISkype.Name = "B_freeAPISkype";
            this.B_freeAPISkype.Size = new System.Drawing.Size(97, 23);
            this.B_freeAPISkype.TabIndex = 1;
            this.B_freeAPISkype.Text = "Free API Skype";
            this.B_freeAPISkype.UseVisualStyleBackColor = true;
            this.B_freeAPISkype.Click += new System.EventHandler(this.B_freeAPISkype_Click);
            // 
            // B_openChatFolder
            // 
            this.B_openChatFolder.Location = new System.Drawing.Point(36, 79);
            this.B_openChatFolder.Name = "B_openChatFolder";
            this.B_openChatFolder.Size = new System.Drawing.Size(115, 23);
            this.B_openChatFolder.TabIndex = 2;
            this.B_openChatFolder.Text = "Open chat folder";
            this.B_openChatFolder.UseVisualStyleBackColor = true;
            this.B_openChatFolder.Click += new System.EventHandler(this.B_openChatFolder_Click);
            // 
            // B_openVoiceFolder
            // 
            this.B_openVoiceFolder.Location = new System.Drawing.Point(36, 108);
            this.B_openVoiceFolder.Name = "B_openVoiceFolder";
            this.B_openVoiceFolder.Size = new System.Drawing.Size(115, 23);
            this.B_openVoiceFolder.TabIndex = 3;
            this.B_openVoiceFolder.Text = "Open voice folder";
            this.B_openVoiceFolder.UseVisualStyleBackColor = true;
            this.B_openVoiceFolder.Click += new System.EventHandler(this.B_openVoiceFolder_Click);
            // 
            // B_openVideoFolder
            // 
            this.B_openVideoFolder.Location = new System.Drawing.Point(36, 137);
            this.B_openVideoFolder.Name = "B_openVideoFolder";
            this.B_openVideoFolder.Size = new System.Drawing.Size(115, 23);
            this.B_openVideoFolder.TabIndex = 4;
            this.B_openVideoFolder.Text = "Open video folder";
            this.B_openVideoFolder.UseVisualStyleBackColor = true;
            this.B_openVideoFolder.Click += new System.EventHandler(this.B_openVideoFolder_Click);
            // 
            // F_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.B_openVideoFolder);
            this.Controls.Add(this.B_openVoiceFolder);
            this.Controls.Add(this.B_openChatFolder);
            this.Controls.Add(this.B_freeAPISkype);
            this.Controls.Add(this.B_attachToSkype);
            this.Name = "F_Main";
            this.Text = "Program Interface v.1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button B_attachToSkype;
        private System.Windows.Forms.Button B_freeAPISkype;
        private System.Windows.Forms.Button B_openChatFolder;
        private System.Windows.Forms.Button B_openVoiceFolder;
        private System.Windows.Forms.Button B_openVideoFolder;
    }
}

