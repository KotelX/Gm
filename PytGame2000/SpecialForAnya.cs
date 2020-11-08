using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PytGame2000
{
    public partial class SpecialForAnya : Form
    {
        [DllImport("Gdi32.dll")]
        public static extern IntPtr CreateRoundRectRgn(int nLeftRect,
                                                      int nTopRect,
                                                      int nRightRect,
                                                      int nBottomRect,
                                                      int nWidthEllipse,
                                                      int nHeightEllipse);
        [DllImport("user32.dll")]
        public static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw);

        public SpecialForAnya()
        {
            InitializeComponent();
            this.Load += new EventHandler(Form1_Load);

        }
        void Form1_Load(object sender, EventArgs e)
        {
            IntPtr hRgn = CreateRoundRectRgn(0, 0, pictureBox1.Width, pictureBox1.Height, 80, 80);
            SetWindowRgn(pictureBox1.Handle, hRgn, true);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
