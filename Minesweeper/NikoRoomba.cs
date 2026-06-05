using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class NikoRoomba : Form
    {
        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_TRANSPARENT = 0x00000020;
        private const int WS_EX_LAYERED = 0x00080000;

        private Timer storyTimer;
        private Timer moveTimer;
        private Timer fadeTimer;

        private int secondsPassed = 0;
        private Random random = new Random();

        private PictureBox nikoBox;
        private Rectangle screenBounds;
        private Point startLocation;

        private int dx = 0;
        private int dy = 0;
        private int baseSpeed = 4;
        private double targetOpacity = 1.0;

        public NikoRoomba(Form parentForm)
        {
            this.Opacity = 0;
            fadeTimer = new Timer();
            fadeTimer.Interval = 75; 
            fadeTimer.Tick += (s, e) => {
                if (this.Opacity < targetOpacity) this.Opacity += 0.05;
                else fadeTimer.Stop();
            };
            fadeTimer.Start();

            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.StartPosition = FormStartPosition.Manual;
            this.DoubleBuffered = true;
            this.Text = Properties.Resources.NikoOnRoomba;

            this.AllowTransparency = true;
            this.BackColor = Color.Lime;
            this.TransparencyKey = Color.Lime;

            nikoBox = new PictureBox();
            nikoBox.Size = new Size(48, 64);
            nikoBox.SizeMode = PictureBoxSizeMode.Zoom;
            nikoBox.BackColor = Color.Transparent;
            this.Controls.Add(nikoBox);

            this.Size = nikoBox.Size;

            Screen currentScreen = Screen.FromControl(parentForm);
            screenBounds = currentScreen.WorkingArea;

            int startX = screenBounds.X + (screenBounds.Width / 2) - (this.Width / 2);
            int startY = screenBounds.Y + screenBounds.Height - this.Height;
            startLocation = new Point(startX, startY);
            this.Location = startLocation;

            nikoBox.Image = LoadEmbeddedImage("NikoRoombaDown.png");

            storyTimer = new Timer();
            storyTimer.Interval = 1000;
            storyTimer.Tick += StoryTimer_Tick;

            moveTimer = new Timer();
            moveTimer.Interval = 20;
            moveTimer.Tick += MoveTimer_Tick;
            this.FormClosing += NikoRoomba_FormClosing;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            int initialStyle = GetWindowLong(this.Handle, GWL_EXSTYLE);
            SetWindowLong(this.Handle, GWL_EXSTYLE, initialStyle | WS_EX_TRANSPARENT | WS_EX_LAYERED);

            StartScene();
        }

        private void StartScene()
        {
            secondsPassed = 0;
            Program.NikoRidong = true;
            if (Program.MusicEnabled)
            {
                Program.StopBGM();

                Assembly assembly = Assembly.GetExecutingAssembly();
                string namespaceName = typeof(NikoRoomba).Namespace;
                string fileName = "IT'S TIME TO FIGHT CRIME.mp3";
                string resourcePath = $"{namespaceName}.Resources.{fileName}";
                string tempPath = Path.Combine(Path.GetTempPath(), fileName);

                try
                {
                    if (!File.Exists(tempPath))
                    {
                        using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
                        using (FileStream fileStream = new FileStream(tempPath, FileMode.Create, FileAccess.Write))
                        {
                            stream?.CopyTo(fileStream);
                        }
                    }
                    Program.bgmPlayer.URL = tempPath;
                    Program.bgmPlayer.settings.setMode("loop", false);
                    Program.bgmPlayer.settings.volume = 70;
                    Program.bgmPlayer.controls.play();
                }
                catch { }
            }

            File.WriteAllText(Program.GetNikoDataPath(), "busy");

            storyTimer.Start();
            moveTimer.Start();
        }

        private void MoveTimer_Tick(object sender, EventArgs e)
        {
            if (secondsPassed < 11)
            {
                int shakeX = random.Next(-1, 1);
                int shakeY = random.Next(-1, 1);
                this.Location = new Point(startLocation.X + shakeX, startLocation.Y + shakeY);
                return;
            }

            this.Left += dx;
            this.Top += dy;

            if (this.Left <= screenBounds.Left)
            {
                this.Left = screenBounds.Left;
                dx = -dx;
                UpdateSprite();
            }
            else if (this.Right >= screenBounds.Right)
            {
                this.Left = screenBounds.Right - this.Width;
                dx = -dx;
                UpdateSprite();
            }

            if (this.Top <= screenBounds.Top)
            {
                this.Top = screenBounds.Top;
                dy = -dy;
                UpdateSprite();
            }
            else if (this.Bottom >= screenBounds.Bottom)
            {
                if (secondsPassed < 75)
                {
                    this.Top = screenBounds.Bottom - this.Height;
                    dy = -dy;
                    UpdateSprite();
                }
            }
        }

        private void StoryTimer_Tick(object sender, EventArgs e)
        {
            secondsPassed++;

            if (secondsPassed == 11)
            {
                this.Location = startLocation;
                dx = random.Next(0, 2) == 0 ? baseSpeed : -baseSpeed;
                dy = -baseSpeed;
                UpdateSprite();
            }

            if (secondsPassed > 11 && secondsPassed < 75 && secondsPassed % 3 == 0)
            {
                ChooseRandomDirection();
            }

            if (secondsPassed == 75)
            {
                dx = 0;
                dy = baseSpeed;
                UpdateSprite();
            }

            if (secondsPassed >= 90)
            {
                storyTimer.Stop();
                moveTimer.Stop();
                Program.StopBGM();

                string path = Program.GetNikoDataPath();
                if (File.Exists(path)) File.Delete(path);
                Program.NikoRidong = false;
                this.Close();
            }
        }

        private void ChooseRandomDirection()
        {
            int roll = random.Next(0, 4);
            switch (roll)
            {
                case 0: dx = baseSpeed; dy = 0; break;
                case 1: dx = -baseSpeed; dy = 0; break;
                case 2: dx = 0; dy = -baseSpeed; break;
                case 3: dx = 0; dy = baseSpeed; break;
            }
            UpdateSprite();
        }

        private void UpdateSprite()
        {
            string imageName = "NikoRoombaDown.png";

            if (dx > 0) imageName = "NikoRoombaRight.png";
            else if (dx < 0) imageName = "NikoRoombaLeft.png";
            else if (dy < 0) imageName = "NikoRoombaUp.png";
            else if (dy > 0) imageName = "NikoRoombaDown.png";

            Image newImg = LoadEmbeddedImage(imageName);
            if (newImg != null)
            {
                nikoBox.Image?.Dispose();
                nikoBox.Image = newImg;
            }
        }

        private Image LoadEmbeddedImage(string fileName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string namespaceName = typeof(NikoRoomba).Namespace;
            string resourcePath = $"{namespaceName}.Resources.{fileName}";

            using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
            {
                if (stream == null) return null;
                return Image.FromStream(stream);
            }
        }

        private async void NikoRoomba_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Program.NikoRidong)
            {
                Program.PlayLoseSound();
                await Task.Delay(500);
                Application.Exit();
            }
        }
    }
}