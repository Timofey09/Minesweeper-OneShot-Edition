using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class About : Form
    {
        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        private const int DWMWA_CAPTION_COLOR = 35;

        public About()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void About_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.PlayNoSound();
        }

        private void About_Load(object sender, EventArgs e)
        {
            try
            {
                int titleBarColor = unchecked((int)0xFF110225);
                DwmSetWindowAttribute(this.Handle, DWMWA_CAPTION_COLOR, ref titleBarColor, sizeof(int));
                this.Refresh();
            }
            catch
            {
                // This won't work on systems except Win11
            }

            label1.Font = new Font(Program.pfc.Families[0], 28, FontStyle.Regular);
            label2.Font = new Font(Program.pfc.Families[0], 14, FontStyle.Regular);
            label3.Font = new Font(Program.pfc.Families[0], 10, FontStyle.Regular);
        }
    }
}
