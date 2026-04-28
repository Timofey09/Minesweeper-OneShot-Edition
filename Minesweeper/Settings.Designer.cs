namespace Minesweeper
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.MusicSwitch = new System.Windows.Forms.CheckBox();
            this.SFXSwitch = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MusicSwitch
            // 
            resources.ApplyResources(this.MusicSwitch, "MusicSwitch");
            this.MusicSwitch.BackColor = System.Drawing.Color.Transparent;
            this.MusicSwitch.Checked = true;
            this.MusicSwitch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MusicSwitch.ForeColor = System.Drawing.Color.White;
            this.MusicSwitch.Name = "MusicSwitch";
            this.MusicSwitch.UseVisualStyleBackColor = false;
            this.MusicSwitch.CheckedChanged += new System.EventHandler(this.MusicSwitch_CheckedChanged);
            // 
            // SFXSwitch
            // 
            resources.ApplyResources(this.SFXSwitch, "SFXSwitch");
            this.SFXSwitch.BackColor = System.Drawing.Color.Transparent;
            this.SFXSwitch.Checked = true;
            this.SFXSwitch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SFXSwitch.ForeColor = System.Drawing.Color.White;
            this.SFXSwitch.Name = "SFXSwitch";
            this.SFXSwitch.UseVisualStyleBackColor = false;
            this.SFXSwitch.CheckedChanged += new System.EventHandler(this.SFXSwitch_CheckedChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Name = "label1";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(12)))), ((int)(((byte)(30)))));
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Gold;
            this.button1.FlatAppearance.BorderSize = 2;
            resources.ApplyResources(this.button1, "button1");
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Settings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SFXSwitch);
            this.Controls.Add(this.MusicSwitch);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Settings_FormClosed);
            this.Load += new System.EventHandler(this.Settings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox MusicSwitch;
        private System.Windows.Forms.CheckBox SFXSwitch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}