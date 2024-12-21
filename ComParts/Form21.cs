using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComParts
{
    public partial class Form21 : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
            (
                int nLeft,
                int nTop,
                int nRight,
                int nBottom,
                int nWidthEllipse,
                int nHeightEllipse
            );

        bool expand;
        public Form21()
        {
            InitializeComponent();
            UserControl2 uc2 = new UserControl2();
            addUserControl(uc2);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(expand)
            {
                flowLayoutPanel1.Width -= 10;
                if(flowLayoutPanel1.Width == flowLayoutPanel1.MinimumSize.Width)
                {
                    expand = false;
                    timer1.Stop();
                }
            }
            else
            {
                flowLayoutPanel1.Width += 10;
                if(flowLayoutPanel1.Width == flowLayoutPanel1.MaximumSize.Width)
                {
                    expand = true;
                    timer1.Stop();
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void Form21_Load(object sender, EventArgs e)
        {
            button6.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button6.Width, button6.Height, 50, 50));
            button7.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button7.Width, button7.Height, 50, 50));
            button8.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button8.Width, button8.Height, 50, 50));
            button9.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button9.Width, button9.Height, 50, 50));
        }

        private void addUserControl(UserControl userControl)
        {
            panel8.Controls.Clear();
            panel8.Controls.Add(userControl);
            userControl.BringToFront();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            UserControl1 uc1 = new UserControl1();
            addUserControl(uc1);
            uc1.BindGridView();
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserControl2 uc2 = new UserControl2();
            addUserControl(uc2);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            UserControl7 uc7 = new UserControl7();
            addUserControl(uc7);
            uc7.BindGridView();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UserControl8 uc8 = new UserControl8();
            addUserControl(uc8);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form8 f8 = new Form8();
            f8.ShowDialog();
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
        }
    }
}
