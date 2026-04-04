using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing.Text;

namespace Minesweeper
{
    public partial class GameForm : Form
    {
        int width = Program.MapWidth;
        int height = Program.MapHeight;
        int minesCount = Program.MinesCount;

        int[,] map; 
        bool[,] revealed; 
        bool[,] flagged;

        bool firstClick = true;
        bool gameOver = false;
        

        int cellSize = 25;

        public GameForm()
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            map = new int[width, height];
            revealed = new bool[width, height];
            flagged = new bool[width, height];

            this.ClientSize = new Size(width * cellSize, height * cellSize);
            this.MouseDown += GameForm_MouseDown;
            this.Paint += GameForm_Paint;
        }

        public void GameForm_Load(object sender, EventArgs e)
        {
            Program.PlayGameBGM();
        }

        void GenerateMines(int safeX, int safeY)
        {
            Random rnd = new Random();
            int placed = 0;

            while (placed < minesCount)
            {
                int x = rnd.Next(width);
                int y = rnd.Next(height);

                if (map[x, y] == -1) continue;
                if (x == safeX && y == safeY) continue;

                map[x, y] = -1;
                placed++;
            }

            CalculateNumbers();
        }

        void CalculateNumbers()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (map[x, y] == -1) continue;

                    int count = 0;

                    for (int dx = -1; dx <= 1; dx++)
                        for (int dy = -1; dy <= 1; dy++)
                        {
                            int nx = x + dx;
                            int ny = y + dy;

                            if (nx >= 0 && ny >= 0 && nx < width && ny < height)
                                if (map[nx, ny] == -1)
                                    count++;
                        }

                    map[x, y] = count;
                }
            }
        }

        bool OpenCells(int x, int y)
        {
            if (x < 0 || y < 0 || x >= width || y >= height)
                return false;

            if (revealed[x, y] || flagged[x, y])
                return false;

            revealed[x, y] = true;

            if (map[x, y] == 0)
            {
                for (int dx = -1; dx <= 1; dx++)
                    for (int dy = -1; dy <= 1; dy++)
                        OpenCells(x + dx, y + dy);
            }

            CheckWin();
            return true;
        }

        void RevealAllMines()
        {
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    if (map[x, y] == -1)
                        revealed[x, y] = true;
        }

        void ShowEndDialog(string Message, MessageBoxIcon IconType)
        {
            
            Program.StopBGM();
            if (IconType == MessageBoxIcon.Warning)
            {
                ShakeWindow(800,18);
            }
            
            var result = MessageBox.Show(Message, "Судьба мира", MessageBoxButtons.YesNo, IconType);

            if (result == DialogResult.Yes)
            {
                Program.PlayYesSound();
                gameOver = false;
                RestartGame();
                
            }
            else
            {
                Program.PlayNoSound();
                Program.NikoJumpscareDone = false;
                this.Close();
            }
        }


        async void ShakeWindow(int duration = 600, int startAmplitude = 15)
        {
            Point originalLocation = this.Location;
            Random rnd = new Random();
            int elapsed = 0;
            int interval = 10;

            while (elapsed < duration)
            {
               double progress = (double)elapsed / duration;
                int currentAmplitude = (int)(startAmplitude * (1.0 - progress));
    
                if (currentAmplitude <= 0) break;

                int shakeX = rnd.Next(-currentAmplitude, currentAmplitude + 1);
                int shakeY = rnd.Next(-currentAmplitude, currentAmplitude + 1);

                this.Location = new Point(originalLocation.X + shakeX, originalLocation.Y + shakeY);

                await Task.Delay(interval);
                elapsed += interval;
            }
            this.Location = originalLocation;
        }

        async void NikoJumpscare() 
        {
            try
            {
                if (!Program.NikoJumpscareDone)
                {
                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Niko_Message.png");

                    System.Media.SystemSounds.Hand.Play();

                    Process p = Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
                    Program.NikoJumpscareDone = true;

                    await Task.Run(() => {
                        p?.WaitForExit();
                    });
                }
            }
            catch { }
        }

        void CheckWin()
        {
            if (gameOver) return;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (map[x, y] != -1 && !revealed[x, y])
                        return;
                }
            }

            RevealAllMines();
            Invalidate();
            this.BeginInvoke(new Action(() =>
            {
                Program.PlayWinSound();
                ShowEndDialog("Нико донёс Солнце до самой вершины!\nСвет вернулся в этот мир, и теперь все будет хорошо.\nМы справились, " + Program.userName + ".\n\nХочешь пережить это приключение снова?", MessageBoxIcon.None);
            }));
        }

        void RestartGame()
        {
            map = new int[width, height];
            revealed = new bool[width, height];
            flagged = new bool[width, height];

            firstClick = true;
            gameOver = false;

            Program.PlayGameBGM();
            Invalidate();
        }

        bool TryOpenAroundNumber(int x, int y)
        {
            int flagsAround = 0;

            for (int dx = -1; dx <= 1; dx++)
                for (int dy = -1; dy <= 1; dy++)
                {
                    int nx = x + dx;
                    int ny = y + dy;

                    if (nx >= 0 && ny >= 0 && nx < width && ny < height)
                        if (flagged[nx, ny])
                            flagsAround++;
                }

            if (flagsAround == map[x, y])
            {
                bool anyOpened = false;

                for (int dx = -1; dx <= 1; dx++)
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        int nx = x + dx;
                        int ny = y + dy;

                        if (nx >= 0 && ny >= 0 && nx < width && ny < height)
                        {
                            if (!flagged[nx, ny] && !revealed[nx, ny])
                            {
                                if (map[nx, ny] == -1)
                                {
                                    GameOver();
                                    gameOver = true;
                                }

                                if (OpenCells(nx, ny))
                                    anyOpened = true;
                            }
                        }
                    }

                return anyOpened;
            }

            return false;
        }

        private void GameOver()
        {
            Program.StopBGM();
            Program.PlayLoseSound();
            Random random = new Random();
            if (random.Next(1, 5) == 1)
            {
                NikoJumpscare();
            }
            gameOver = true;
            RevealAllMines();
            Invalidate();
            this.BeginInvoke(new Action(() => ShowEndDialog("Лампочка разбилась... \nУ Нико не было второго шанса, и этот мир угас навсегда. \nТебе не удалось его спасти, "+ Program.userName + ". " + "\n\nПопробовать восстановить мир из пепла?", MessageBoxIcon.Warning)));
        }

        private void GameForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (gameOver) return;

            int x = e.X / cellSize;
            int y = e.Y / cellSize;

            if (x < 0 || y < 0 || x >= width || y >= height)
                return;

            if (e.Button == MouseButtons.Left)
            {
                if (firstClick)
                {
                    GenerateMines(x, y);
                    firstClick = false;
                }

                if (revealed[x, y] && map[x, y] > 0)
                {
                    if (TryOpenAroundNumber(x, y))
                    {

                        Program.PlayOpenSound();
                    }
                }
                else if (!flagged[x, y])
                {
                    if (map[x, y] == -1)
                    {
                        GameOver();
                    }

                    if (OpenCells(x, y))
                    {
                        Program.PlayOpenSound();
                    }
                }
            }
            
            else if (e.Button == MouseButtons.Right)
            {
                if (!revealed[x, y])
                {
                    flagged[x, y] = !flagged[x, y];
                    CheckWin();
                }
            }

            Invalidate();
        }

        private void GameForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Font font = new Font("Segoe UI Emoji", 14);
            

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Rectangle rect = new Rectangle(x * cellSize, y * cellSize, cellSize, cellSize);

                    if (revealed[x, y])
                    {
                        g.FillRectangle(Brushes.White, rect);

                        if (map[x, y] == -1)
                            g.FillEllipse(Brushes.Black, rect.X + 5, rect.Y + 5, cellSize - 10, cellSize - 10);
                        else if (map[x, y] > 0)
                        {
                            Brush color = Brushes.Black;
                            if (map[x, y] == 1) color = Brushes.Blue;
                            else if (map[x, y] == 2) color = Brushes.Green;
                            else if (map[x, y] == 3) color = Brushes.Red;
                            else if (map[x, y] == 4) color = Brushes.DarkBlue;
                            else if (map[x, y] == 5) color = Brushes.DarkRed;
                            else if (map[x, y] == 6) color = Brushes.Cyan;
                            else if (map[x, y] == 7) color = Brushes.Black;
                            else if (map[x, y] == 8) color = Brushes.Gray;
                            else if (map[x, y] == 9) color = Brushes.Purple;

                            g.DrawString(map[x, y].ToString(), font, color, rect.X + 5, rect.Y);
                        }
                    }
                    else
                    {
                        g.FillRectangle(Brushes.LightGray, rect);

                        if (flagged[x, y])
                            g.DrawString("🚩", font, Brushes.Red, rect.X - 2, rect.Y);
                    }

                    g.DrawRectangle(Pens.Gray, rect);
                }
            }
        }

        private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.NikoJumpscareDone = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f is MainMenu menu)
                {
                    menu.Show();
                    if (Program.MusicEnabled)
                    {
                        menu.MainMenu_Load(null, null); 
                    }
                    break;
                }
            }
        }
    }
}
