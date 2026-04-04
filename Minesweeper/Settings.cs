using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Settings : Form
    {
        private GameForm gameForm;
        public Settings()
        {
            InitializeComponent();
            MusicSwitch.Checked = Program.MusicEnabled;
            SFXSwitch.Checked = Program.SFXEnabled;
        }

        private void SFXSwitch_CheckedChanged(object sender, EventArgs e)
        {
            Program.SFXEnabled = SFXSwitch.Checked;
            if (!MusicSwitch.Checked)
            {
                Program.PlayOpenSound();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.PlayYesSound();
            About about = new About();
            about.Show();
            Hide();
        }

        private void Settings_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.PlayNoSound();
        }

        private void MusicSwitch_CheckedChanged(object sender, EventArgs e)
        {
            Program.MusicEnabled = MusicSwitch.Checked;
            Program.PlayOpenSound();
            if (Program.MusicEnabled)
            {
                Program.PlayMenuBGM();
            }
            else
            {
                Program.StopBGM();
            }
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            label1.Font = new Font(Program.pfc.Families[0], 20, FontStyle.Regular);
            MusicSwitch.Font = new Font(Program.pfc.Families[0], 18, FontStyle.Regular);
            SFXSwitch.Font = new Font(Program.pfc.Families[0], 18, FontStyle.Regular);
            button1.Font = new Font(Program.pfc.Families[0], 12, FontStyle.Regular);
        }
    }
}
