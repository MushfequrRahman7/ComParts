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

namespace ComParts
{
    public partial class UserControl2 : UserControl
    {
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
        public UserControl2()
        {
            InitializeComponent();
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UserControl2_Load(object sender, EventArgs e)
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
        private void button9_Click(object sender, EventArgs e)
        {
            UserControl3 uc3 = new UserControl3();
            addUserControl(uc3);
            uc3.BindGridView();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            UserControl4 uc4 = new UserControl4();
            addUserControl(uc4);
            uc4.BindGridView();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            UserControl5 uc5 = new UserControl5();
            addUserControl(uc5);
            uc5.BindGridView();
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            UserControl6 uc6 = new UserControl6();
            addUserControl(uc6);
            uc6.BindGridViewMouse();
            uc6.BindGridViewKeyboard();
            uc6.BindGridViewMonitor();
            uc6.BindGridViewCPU();
            uc6.BindGridViewRAM();
        }
    }
}
