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
            // F_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.B_freeAPISkype);
            this.Controls.Add(this.B_attachToSkype);
            this.Name = "F_Main";
            this.Text = "Program Interface v.1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button B_attachToSkype;
        private System.Windows.Forms.Button B_freeAPISkype;
    }
}

