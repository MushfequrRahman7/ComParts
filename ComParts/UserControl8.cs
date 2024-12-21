using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComParts
{
    public partial class UserControl8 : UserControl
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
        public UserControl8()
        {
            InitializeComponent();
        }

        private void UserControl8_Load(object sender, EventArgs e)
        {
            button1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 50, 50));
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
            UserControl9 uc9 = new UserControl9();
            addUserControl(uc9);
            uc9.BindGridViewMouse();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            UserControl10 uc10 = new UserControl10();
            addUserControl(uc10);
            uc10.BindGridViewKeyboard();
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            UserControl11 uc11 = new UserControl11();
            addUserControl(uc11);
            uc11.BindGridViewMonitor();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            UserControl12 uc12 = new UserControl12();
            addUserControl(uc12);
            uc12.BindGridViewCPU();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            UserControl13 uc13 = new UserControl13();
            addUserControl(uc13);
            uc13.BindGridViewRAM();
        }
    }
}
