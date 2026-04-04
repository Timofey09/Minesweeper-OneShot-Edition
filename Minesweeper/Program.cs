using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace Minesweeper
{
    internal static class Program
    {
        public static string userName = Environment.UserName;
        public static int MapWidth = 9;
        public static int MapHeight = 9;
        public static int MinesCount = 10;
        public static int CellSize = 30;
        public static bool MusicEnabled = true;
        public static bool SFXEnabled = true;
        public static bool NikoJumpscareDone = false;
        public static WindowsMediaPlayer bgmPlayer = new WindowsMediaPlayer();
        public static PrivateFontCollection pfc = new PrivateFontCollection();
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainMenu());
        }
        public static void PlayNoSound()
        {
            if (SFXEnabled)
            {
                try
                {
                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "menu_cancel.wav");
                    if (File.Exists(path))
                    {
                        using (SoundPlayer player = new SoundPlayer(path))
                        {
                            player.Play();
                        }
                    }
                }
                catch {}
            }
        }
        public static void PlayYesSound()
        {
            if (SFXEnabled)
            {
                try
                {
                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "menu_decision.wav");
                    if (File.Exists(path))
                    {
                        using (SoundPlayer player = new SoundPlayer(path))
                        {
                            player.Play();
                        }
                    }
                }
                catch {}
            }
        }
        public static void PlayLoseSound()
        {
            if (SFXEnabled)
            {
                try
                {
                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "shatter.wav");
                    if (File.Exists(path))
                    {
                        using (SoundPlayer player = new SoundPlayer(path))
                        {
                            player.Play();
                        }
                    }
                }
                catch {}
            }
        }
        public static void PlayOpenSound()
        {
            if (SFXEnabled)
            {
                try
                {
                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "tock.wav");
                    if (File.Exists(path))
                    {
                        using (SoundPlayer player = new SoundPlayer(path))
                        {
                            player.Play();
                        }
                    }
                }
                catch {}
            }
        }
        public static void PlayWinSound()
        {
            if (SFXEnabled)
            {
                try
                {
                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "puzzle_solved.wav");
                    if (File.Exists(path))
                    {
                        using (SoundPlayer player = new SoundPlayer(path))
                        {
                            player.Play();
                        }
                    }
                }
                catch {}
            }
        }
        public static void PlayStartSound()
        {
            if (SFXEnabled)
            {
                try
                {
                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "item_get.wav");
                    if (File.Exists(path))
                    {
                        using (SoundPlayer player = new SoundPlayer(path))
                        {
                            player.Play();
                        }
                    }
                }
                catch { }
            }
        }
        public static void PlayTickSound()
        {
            if (SFXEnabled)
            {
                try
                {
                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "menu_cursor.wav");
                    if (File.Exists(path))
                    {
                        using (SoundPlayer player = new SoundPlayer(path))
                        {
                            player.Play();
                        }
                    }
                }
                catch { }
            }
        }
        public static void PlayMenuBGM()
        {
            PlayMusic("On Little Cat Feet.mp3");
        }

        public static void PlayGameBGM()
        {
            PlayMusic("Abandoned Factory.mp3");
        }

        public static void StopBGM()
        {
            bgmPlayer.controls.stop();
            bgmPlayer.URL = "";
        }

        private static void PlayMusic(string fileName)
        {
            if (!MusicEnabled) return;

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", fileName);

            if (File.Exists(path) && bgmPlayer.URL != path)
            {
                bgmPlayer.URL = path;
                bgmPlayer.settings.setMode("loop", true);
                bgmPlayer.settings.volume = 70;
                bgmPlayer.controls.play();
            }
        }
    }
}