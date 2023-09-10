using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;

namespace clock_Overlay_soft2
{

    public partial class OverlayForm : Form
    {
        public Label lblMessage;


        public OverlayForm()
        {

            TransparencyKey = Color.Gray;
            StartPosition = FormStartPosition.Manual;

            // 画面の幅と高さを取得
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;



            Location = new Point(screenWidth - this.Width - 25, screenHeight - this.Height - 25);

            Console.WriteLine(screenWidth);
            Console.WriteLine(screenHeight);

            Console.WriteLine(screenWidth - this.Width);
            Console.WriteLine(screenHeight - this.Height);

            Console.WriteLine(this.Width);
            Console.WriteLine(this.Height);

            TopMost = true;

            InitializeComponent();
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020;
                return cp;
            }
        }

        public void CloseForm()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => this.Close()));
            }
            else
            {
                this.Close();
            }
        }


        private void OverlayForm_Load(object sender, EventArgs e)
        {

        }


        public void Change_Tlabel(String[] TlabelText)
        {
            Invoke(new Action(() =>
            {
                T_tab.Text = TlabelText[0];
                T_slash.Text = TlabelText[1];
                T_asterisk.Text = TlabelText[2];
                T_minus.Text = TlabelText[3];
                T7.Text = TlabelText[4];
                T8.Text = TlabelText[5];
                T9.Text = TlabelText[6];
                T_plus.Text = TlabelText[7];
                T4.Text = TlabelText[8];
                T5.Text = TlabelText[9];
                T6.Text = TlabelText[10];
                T_bs.Text = TlabelText[11];
                T1.Text = TlabelText[12];
                T2.Text = TlabelText[13];
                T3.Text = TlabelText[14];
                T_enter.Text = TlabelText[15];
                T0.Text = TlabelText[16];
                T000.Text = TlabelText[17];
                T_dot.Text = TlabelText[18];
            }));
        }

        private void T7_Click(object sender, EventArgs e)
        {

        }

        private void T4_Click(object sender, EventArgs e)
        {

        }

        private void T1_Click(object sender, EventArgs e)
        {

        }

        private void T0_Click(object sender, EventArgs e)
        {

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void T_plus_Click(object sender, EventArgs e)
        {

        }

        private void T_bs_Click(object sender, EventArgs e)
        {

        }

        private void T3_Click(object sender, EventArgs e)
        {

        }
    }



}
