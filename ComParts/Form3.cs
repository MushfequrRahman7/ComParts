using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComParts
{
    public partial class Form3 : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label44_Click(object sender, EventArgs e)
        {

        }

        private void label48_Click(object sender, EventArgs e)
        {

        }

        private void label65_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form6 f6 = new Form6();
            f6.ShowDialog();
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2();
            f2.ShowDialog();
            this.Close();
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.label2.Text = this.label16.Text;
            f1.ShowDialog();
            this.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form6 f6 = new Form6();
            f6.ShowDialog();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if((int)numericUpDown1.Value == 0)
            {
                MessageBox.Show("Please enter quantity", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if(int.Parse(label11.Text) != 0 && (int)numericUpDown1.Value <= int.Parse(label11.Text))
            {
                SqlConnection con = new SqlConnection(cs);
                string query = "insert into Cart values (@pID,@pName,@price,@quantity,@tPrice,@cName)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@pName", label9.Text);
                cmd.Parameters.AddWithValue("@pID", label13.Text);
                cmd.Parameters.AddWithValue("@price", label69.Text);
                int totalPrice = (int)numericUpDown1.Value * int.Parse(label69.Text);
                cmd.Parameters.AddWithValue("@quantity", numericUpDown1.Value);
                cmd.Parameters.AddWithValue("@tPrice", totalPrice);
                cmd.Parameters.AddWithValue("@cName", label16.Text);

                con.Open();
                int a = cmd.ExecuteNonQuery();
                if (a > 0)
                {
                    MessageBox.Show("Added to Cart", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Not Added to Cart!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Not Available!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form16 f16 = new Form16();
            f16.label18.Text = this.label16.Text;
            f16.BindGridView();
            f16.ShowDialog();
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
            dgv = (DataGridViewImageColumn)dataGridView1.Columns[6];
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

        private void Form3_MouseClick(object sender, MouseEventArgs e)
        {
            this.ActiveControl = null;
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.Hide();
            Form3 f3 = new Form3();
            f3.label16.Text = this.label16.Text;
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
    }
}
