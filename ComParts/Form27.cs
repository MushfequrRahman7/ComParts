using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComParts
{
    public partial class Form27 : Form
    {
        public Form27()
        {
            InitializeComponent();
        }
        public Form27(string Username)
        {
            InitializeComponent();
            textBox1.Text = Username;
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2_KeyPress(sender, new KeyPressEventArgs('\0'));
            if (textBox2.Text == "Password")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.UseSystemPasswordChar = false;
                textBox2.Text = "Password";
                textBox2.ForeColor = Color.Silver;
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Username")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Username";
                textBox1.ForeColor = Color.Silver;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form8 f8 = new Form8();
            f8.ShowDialog();
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool status = checkBox1.Checked;
            if (textBox2.Text != "Password")
            {
                switch (status)
                {
                    case true:
                        textBox2.UseSystemPasswordChar = false;
                        break;
                    default:
                        textBox2.UseSystemPasswordChar = true;
                        break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "admin" && textBox2.Text == "123")
            {
                MessageBox.Show("Login Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                Form21 f21 = new Form21();
                f21.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please try again!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
