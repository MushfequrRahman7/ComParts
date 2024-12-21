using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.Expando;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComParts
{
    public partial class Form1 : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button29_Click(object sender, EventArgs e)
        {

        }
        bool expand1 = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (expand1 == false)
            {
                flowLayoutPanel1.Height += 15;
                if (flowLayoutPanel1.Height >= flowLayoutPanel1.MaximumSize.Height)
                {
                    timer1.Stop();
                    expand1 = true;
                }
            }
            else
            {
                flowLayoutPanel1.Height -= 15;
                if (flowLayoutPanel1.Height <= flowLayoutPanel1.MinimumSize.Height)
                {
                    timer1.Stop();
                    expand1 = false;
                }
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            
        }

        private void button27_MouseHover(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button31_Click(object sender, EventArgs e)
        {

        }

        private void button31_MouseHover(object sender, EventArgs e)
        {
            timer2.Start();
        }
        bool expand2 = false;
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (expand2 == false)
            {
                flowLayoutPanel2.Height += 15;
                if (flowLayoutPanel2.Height >= flowLayoutPanel2.MaximumSize.Height)
                {
                    timer2.Stop();
                    expand2 = true;
                }
            }
            else
            {
                flowLayoutPanel2.Height -= 15;
                if (flowLayoutPanel2.Height <= flowLayoutPanel2.MinimumSize.Height)
                {
                    timer2.Stop();
                    expand2 = false;
                }
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2();
            f2.label2.Text = this.label2.Text;
            f2.BindGridViewMouse();
            f2.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();

                // Delete rows from Cart table
                using (SqlCommand cmdDeleteFromCart = new SqlCommand("DELETE FROM Cart WHERE CustomerName = @cName;", con, transaction))
                {
                    cmdDeleteFromCart.Parameters.AddWithValue("@cName", label2.Text);
                    cmdDeleteFromCart.ExecuteNonQuery();
                }

                // Commit the transaction
                transaction.Commit();
            }
            this.Hide();
            Form6 f6 = new Form6();
            f6.ShowDialog();
            this.Close();
        }

        private void button29_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form10 f10 = new Form10();
            f10.label2.Text = this.label2.Text;
            f10.BindGridViewKeyboard();
            f10.ShowDialog();
            this.Close();
        }
        int count = 0;
        private void timer3_Tick(object sender, EventArgs e)
        {
            if(count < 5)
            {
                pictureBox1.Image = imageList1.Images[count];
                count++;
            }
            else
            {
                count = 0;
            }
            
        }
        bool expand3 = false;
        private void timer4_Tick(object sender, EventArgs e)
        {
            
        }

        private void button30_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void button32_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form24 f24 = new Form24();
            f24.label2.Text = this.label2.Text;
            f24.BindGridViewCPU();
            f24.ShowDialog();
            this.Close();
        }

        private void button34_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form25 f25 = new Form25();
            f25.label2.Text = this.label2.Text;
            f25.BindGridViewRAM();
            f25.ShowDialog();
            this.Close();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form26 f26 = new Form26();
            f26.label2.Text = this.label2.Text;
            f26.BindGridViewMonitor();
            f26.ShowDialog();
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            var query = "SELECT * FROM Products WHERE Name LIKE '%" + this.textBox2.Text + "%' OR Type LIKE '%" + this.textBox2.Text + "%'";
            SqlConnection con = new SqlConnection(cs);
            SqlDataAdapter sda = new SqlDataAdapter(query, con);

            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;

            DataGridViewImageColumn dgv = new DataGridViewImageColumn();
            dgv = (DataGridViewImageColumn)dataGridView1.Columns[6]; // Assuming Image column is at index 1
            dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (!dataGridView1.RectangleToScreen(dataGridView1.DisplayRectangle).Contains(Cursor.Position))
            {
                dataGridView1.Visible = false;
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            this.ActiveControl = null;
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.Hide();
            Form3 f3 = new Form3();
            f3.label16.Text = this.label2.Text;
            f3.label9.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            f3.label8.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            f3.label11.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            f3.label12.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            f3.label13.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            f3.pictureBox1.Image = GetImage((byte[])dataGridView1.SelectedRows[0].Cells[6].Value);
            f3.label69.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            f3.ShowDialog();
            this.Close();
        }
        private Image GetImage(byte[] photo)
        {
            MemoryStream ms = new MemoryStream(photo);
            return Image.FromStream(ms);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form16 f16 = new Form16();
            f16.label18.Text = this.label2.Text;
            f16.BindGridView();
            f16.ShowDialog();
            this.Close();
        }
    }
}
