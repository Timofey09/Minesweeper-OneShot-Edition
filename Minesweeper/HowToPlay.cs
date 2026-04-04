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
    public partial class HowToPlay : Form
    {
        public HowToPlay()
        {
            InitializeComponent();
            label1.Text = "Сапёр — игра несложная.\r\nОна полностью построена на логике и иногда на везении.\r\n\r\nТвоя цель — открыть всё игровое поле,\r\nпометив места с минами флажками 🚩.\r\n\r\nЛевой кнопкой мыши открывай клетки.\r\nПравой кнопкой мыши ставь или убирай флаг.\r\n\r\nЕсли ты открыл клетку с миной — игра окончена.\r\nЦифры в клетках показывают, сколько мин находится рядом с этой клеткой.\r\nБудь внимателен, думай наперёд и постарайся очистить всё поле без ошибок.\r\n\r\nУдачи," + Program.userName + "!\r\n";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.PlayNoSound();
        }

        private void HowToPlay_Load(object sender, EventArgs e)
        {
            label1.Font = new Font(Program.pfc.Families[0], 12, FontStyle.Regular);
            button1.Font = new Font(Program.pfc.Families[0], 14, FontStyle.Regular);
        }
    }
}
