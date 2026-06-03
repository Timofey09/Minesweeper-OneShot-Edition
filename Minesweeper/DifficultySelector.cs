using AxWMPLib;
using Minesweeper.Properties;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class DifficultySelector : Form
    {
        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        private const int DWMWA_CAPTION_COLOR = 35;
        private bool suppressScrollSound = false;

        public DifficultySelector()
        {
            InitializeComponent();
            this.Shown += DifficultySelector_Shown;
        }

        private void DifficultySelector_Shown(object sender, EventArgs e)
        {
            label1.Font = new Font(Program.pfc.Families[0], 22, FontStyle.Regular);
            label2.Font = new Font(Program.pfc.Families[0], 20, FontStyle.Regular);
            label3.Font = new Font(Program.pfc.Families[0], 12, FontStyle.Regular);
            label4.Font = new Font(Program.pfc.Families[0], 19, FontStyle.Regular);
            label5.Font = new Font(Program.pfc.Families[0], 19, FontStyle.Regular);
            LaunchButton.Font = new Font(Program.pfc.Families[0], 15, FontStyle.Regular);

            suppressScrollSound = true; 

            if (Program.MapWidth == 9 && Program.MapHeight == 9) difficultyTrackBar.Value = 0;
            else if (Program.MapWidth == 16 && Program.MapHeight == 16) difficultyTrackBar.Value = 1;
            else if (Program.MapWidth == 30 && Program.MapHeight == 16) difficultyTrackBar.Value = 2;
            else if (Program.MapWidth == 35 && Program.MapHeight == 25) difficultyTrackBar.Value = 3;
            else difficultyTrackBar.Value = 0;

            suppressScrollSound = false; 
            UpdateDifficultyUI(); 
        }

        private void difficultyTrackBar_Scroll(object sender, EventArgs e)
        {
            UpdateDifficultyUI();
        }

        private void UpdateDifficultyUI()
        {
            int lastValue = -1;
            if (difficultyTrackBar.Value == lastValue) return;
            lastValue = difficultyTrackBar.Value;

            if (!suppressScrollSound)
            {
                Program.PlayTickSound();
            }

            switch (difficultyTrackBar.Value)
            {
                case 0:
                    SetDifficulty(9, 9, 10, Properties.Resources.Difficulty0Name, Properties.Resources.Difficulty0Description, Color.MediumVioletRed);
                    break;
                case 1:
                    SetDifficulty(16, 16, 40, Properties.Resources.Difficulty1Name, Properties.Resources.Difficulty1Description, Color.MediumVioletRed);
                    break;
                case 2:
                    SetDifficulty(30, 16, 99, Properties.Resources.Difficulty2Name, Properties.Resources.Difficulty2Description, Color.MediumVioletRed);
                    break;
                case 3:
                    Program.MapWidth = 35;
                    Program.MapHeight = 25;
                    Program.MinesCount = 300;
                    difficultyTrackBar.BackColor = Color.Red;
                    label2.ForeColor = Color.Red;
                    label2.Text = Properties.Resources.Difficulty3Name;
                    label3.ForeColor = Color.Red;
                    label3.Text = Properties.Resources.Difficulty3Description;
                    label4.ForeColor = Color.Red;
                    label4.Text = Properties.Resources.Field + ": 3̸̤̇̌5̷͕̅͒͐x̴͖͇͋͘̚2̴̧̬̈́̇͗5̷͚̂̃";
                    label5.ForeColor = Color.Red;
                    label5.Text = Properties.Resources.Mines + ": 3̷̫̟̈́̀0̷̧̯̫͝0̴̥̙̪̐";
                    break;
            }
        }

        private void SetDifficulty(int w, int h, int m, string title, string desc, Color color)
        {
            Program.MapWidth = w;
            Program.MapHeight = h;
            Program.MinesCount = m;
            difficultyTrackBar.BackColor = color;
            label2.ForeColor = Color.White;
            label2.Text = title;
            label3.ForeColor = Color.White;
            label3.Text = desc;
            label4.ForeColor = Color.White;
            label4.Text = Properties.Resources.Field + ": " + w + "x" + h;
            label5.ForeColor = Color.White;
            label5.Text = Properties.Resources.Mines + ": " + m;
        }

        private void LaunchButton_Click(object sender, EventArgs e)
        {
            Program.StopBGM();
            Program.PlayStartSound();
            new GameForm().Show();
            this.Hide();
        }

        private void DifficultySelector_FormClosed(object sender, FormClosedEventArgs e)
        {
            new MainMenu().Show();
            Program.PlayNoSound();
        }

        private void DifficultySelector_Load(object sender, EventArgs e)
        {

        }
    }
}