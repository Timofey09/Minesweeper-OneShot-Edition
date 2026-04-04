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
            this.MusicSwitch.AutoSize = true;
            this.MusicSwitch.BackColor = System.Drawing.Color.Transparent;
            this.MusicSwitch.Checked = true;
            this.MusicSwitch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MusicSwitch.Font = new System.Drawing.Font("Comic Sans MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MusicSwitch.ForeColor = System.Drawing.Color.White;
            this.MusicSwitch.Location = new System.Drawing.Point(87, 78);
            this.MusicSwitch.Name = "MusicSwitch";
            this.MusicSwitch.Size = new System.Drawing.Size(122, 37);
            this.MusicSwitch.TabIndex = 0;
            this.MusicSwitch.Text = "Музыка";
            this.MusicSwitch.UseVisualStyleBackColor = false;
            this.MusicSwitch.CheckedChanged += new System.EventHandler(this.MusicSwitch_CheckedChanged);
            // 
            // SFXSwitch
            // 
            this.SFXSwitch.AutoSize = true;
            this.SFXSwitch.BackColor = System.Drawing.Color.Transparent;
            this.SFXSwitch.Checked = true;
            this.SFXSwitch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SFXSwitch.Font = new System.Drawing.Font("Comic Sans MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SFXSwitch.ForeColor = System.Drawing.Color.White;
            this.SFXSwitch.Location = new System.Drawing.Point(87, 125);
            this.SFXSwitch.Name = "SFXSwitch";
            this.SFXSwitch.Size = new System.Drawing.Size(100, 37);
            this.SFXSwitch.TabIndex = 1;
            this.SFXSwitch.Text = "Звуки";
            this.SFXSwitch.UseVisualStyleBackColor = false;
            this.SFXSwitch.CheckedChanged += new System.EventHandler(this.SFXSwitch_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(72, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 33);
            this.label1.TabIndex = 2;
            this.label1.Text = "Настройки";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.MidnightBlue;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Gold;
            this.button1.FlatAppearance.BorderSize = 2;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(98, 183);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 36);
            this.button1.TabIndex = 3;
            this.button1.Text = "Об игре";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(284, 234);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SFXSwitch);
            this.Controls.Add(this.MusicSwitch);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки";
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