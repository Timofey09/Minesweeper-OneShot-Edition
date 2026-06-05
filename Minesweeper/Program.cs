using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace Minesweeper
{
    internal static class Program
    {
        public static string UserName = Environment.UserName;
        public static int Theme = 0;
        public static int MapWidth = 9;
        public static int MapHeight = 9;
        public static int MinesCount = 10;
        public static int CellSize = 30;
        public static bool NikoRiding = false;
        public static bool MusicEnabled = true;
        public static bool SFXEnabled = true;
        public static bool NikoJumpscareDone = false;
        public static bool Scare1 = false;
        public static bool NightMsgBox = false;
        public static WindowsMediaPlayer bgmPlayer = new WindowsMediaPlayer();
        public static PrivateFontCollection pfc = new PrivateFontCollection();

        private static string currentTempMp3Path = "";

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            LoadFonts();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string path = GetNikoDataPath();
            if (File.Exists(path))
            {
                File.Delete(path);

                DialogResult result = MessageBox.Show(Properties.Resources.ForceCloseMsgBox1 + UserName + "..." + Properties.Resources.ForceCloseMsgBox2 + "\n\n" + Properties.Resources.ForceCloseMsgBox3, Properties.Resources.Niko, MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    MessageBox.Show(Properties.Resources.ForceCloseMsgBoxYes, Properties.Resources.Niko);
                }
                else
                {
                    MessageBox.Show(Properties.Resources.ForceCloseMsgBoxNo, Properties.Resources.Niko);
                    System.Diagnostics.Process.Start("shutdown", "/s /t 10");
                }
            }
            Application.Run(new MainMenu());

            CleanUpTempFiles();
        }

        private static void PlaySoundFromResource(string fileName)
        {
            if (!SFXEnabled) return;
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                string namespaceName = typeof(Program).Namespace;
                string resourcePath = $"{namespaceName}.Resources.{fileName}";

                using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
                {
                    if (stream != null)
                    {
                        using (SoundPlayer player = new SoundPlayer(stream))
                        {
                            player.Play();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Properties.Resources.ErrorMsgBox + ex.Message, "Program.cs PlaySound: Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void PlayMusic(string fileName, bool loop)
        {
            if (!MusicEnabled) return;

            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                string namespaceName = typeof(Program).Namespace;
                string resourcePath = $"{namespaceName}.Resources.{fileName}";

                string tempPath = Path.Combine(Path.GetTempPath(), fileName);

                if (!File.Exists(tempPath))
                {
                    using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
                    using (FileStream fileStream = new FileStream(tempPath, FileMode.Create, FileAccess.Write))
                    {
                        stream?.CopyTo(fileStream);
                    }
                }

                if (bgmPlayer.URL == tempPath && bgmPlayer.playState == WMPPlayState.wmppsPlaying) return;

                currentTempMp3Path = tempPath;
                bgmPlayer.URL = tempPath;
                bgmPlayer.settings.setMode("loop", loop);
                bgmPlayer.settings.volume = 70;
                bgmPlayer.controls.play();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Properties.Resources.ErrorMsgBox + ex.Message, "Program.cs PlayMusic Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void LoadFonts()
        {
            try
            {
                string fontPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Terminus (TTF) Bold.ttf");

                if (!File.Exists(fontPath))
                {
                    MessageBox.Show($"Критическая ошибка: Файл шрифта не найден по пути:\n{fontPath}\n\nУбедись, что папка Resources скопирована вместе с игрой.", "Fonts Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                pfc.AddFontFile(fontPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Properties.Resources.ErrorMsgBox + ex.Message, "Fonts: Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void StopBGM()
        {
            try
            {
                bgmPlayer.controls.stop();
                bgmPlayer.URL = "";
            }
            catch { }
        }

        public static void CleanUpTempFiles()
        {
            try
            {
                bgmPlayer.close(); 

                if (!string.IsNullOrEmpty(currentTempMp3Path) && File.Exists(currentTempMp3Path))
                {
                    File.Delete(currentTempMp3Path);
                }
            }
            catch { }
        }

        public static string GetNikoDataPath()
        {
            string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MinesweeperOneShot");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
            return Path.Combine(folder, "niko_state.txt");
        }

        public static void PlayNoSound()
        {
            PlaySoundFromResource("menu_cancel.wav");
        }

        public static void PlayYesSound()
        {
            PlaySoundFromResource("menu_decision.wav");
        }

        public static void PlayLoseSound()
        {
            PlaySoundFromResource("shatter.wav");
        }

        public static void PlayOpenSound()
        {
            PlaySoundFromResource("tock.wav");
        }

        public static void PlayWinSound()
        {
            PlaySoundFromResource("puzzle_solved.wav");
        }

        public static void PlayStartSound()
        {
            PlaySoundFromResource("item_get.wav");
        }

        public static void PlayTickSound()
        {
            PlaySoundFromResource("menu_cursor.wav");
        }

        public static void PlayMenuBGM()
        {
            PlayMusic("On Little Cat Feet.mp3", true);
        }

        public static void PlayGameBGM()
        {
            PlayMusic("Abandoned Factory.mp3", true);
        }
    }
}