using AxWMPLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 

namespace Minesweeper
{
    public partial class MainMenu : Form
    {
        public string resourcesPath;
        private HowToPlay guide; 
        private Settings settings;
        
        public MainMenu()
        {
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US"); // Временная строка для проверки англ. локализации
            InitializeComponent();
            resourcesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");
            Version v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            label3.Text = "v" + v.Major + "." + v.Minor + "." + v.Build;
            LoadCustomFont();
        }

        public void LoadCustomFont()
        {
            try
            {
                string fontPath = Path.Combine(resourcesPath, "Terminus (TTF) Bold.ttf");

                if (File.Exists(fontPath))
                {
                    Program.pfc.AddFontFile(fontPath);
                    Font customFont = new Font(Program.pfc.Families[0], 14, FontStyle.Regular);
                    label1.Font = new Font(Program.pfc.Families[0], 30, FontStyle.Regular);
                    label2.Font = new Font(Program.pfc.Families[0], 12, FontStyle.Regular);
                    label3.Font = new Font(Program.pfc.Families[0], 10, FontStyle.Regular);
                    NewGameButton.Font = new Font(Program.pfc.Families[0], 11, FontStyle.Regular);
                    HowToPlayButton.Font = new Font(Program.pfc.Families[0], 11, FontStyle.Regular);
                    SettingsButton.Font = new Font(Program.pfc.Families[0], 11, FontStyle.Regular);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Properties.Resources.ErrorMsgBox + ex.Message, "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HowToPlay_Click(object sender, EventArgs e)
        {
            if (guide == null || guide.IsDisposed) 
            {
                guide = new HowToPlay();
                guide.Show();
                Program.PlayYesSound();
            }
            else
            {
                guide.Show();
                guide.Focus();
            }
        }

        private void NewGame_Click(object sender, EventArgs e)
        {
            Program.PlayYesSound();
            Program.StopBGM();
            DifficultySelector difficultySelector = new DifficultySelector();
            if (guide != null && !guide.IsDisposed)
            {
                guide.Hide();
            }
            if (settings != null && !settings.IsDisposed)
            {
                settings.Hide();
            }
            difficultySelector.Show();
            Hide();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            if (settings == null || settings.IsDisposed)
            {
                settings = new Settings();
                settings.Show();
                Program.PlayYesSound();
            }
            else
            {
                settings.Show();
                settings.Focus();
            }
        }

        public void MainMenu_Load(object sender, EventArgs e)
        {
            Program.PlayMenuBGM();
        }

        private void MainMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
