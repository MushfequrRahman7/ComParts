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
    public partial class Form10 : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

        // Integer values to represent filter options
        private const int FilterAvailable = 1;
        private const int FilterOutOfStock = 2;
        private const int FilterAll = 3;

        // Variable to store the current filter option
        private int currentFilter = FilterAll;

        public Form10()
        {
            InitializeComponent();
        }

        //internal void BindGridViewKeyboard()
        //{
        //    SqlConnection con = new SqlConnection(cs);

        //    // Build the query based on the selected filter option
        //    string query = GetQueryBasedOnFilterOption();

        //    SqlDataAdapter sda = new SqlDataAdapter(query, con);

        //    DataTable data = new DataTable();
        //    sda.Fill(data);
        //    dataGridView1.DataSource = data;

        //    DataGridViewImageColumn dgv = new DataGridViewImageColumn();
        //    dgv = (DataGridViewImageColumn)dataGridView1.Columns[6];
        //    dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;

        //    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        //}

        internal void BindGridViewKeyboard()
        {
            SqlConnection con = new SqlConnection(cs);

            // Build the query based on the selected filter option
            string query = GetQueryBasedOnFilterOption();

            // Add a condition to filter by price range using TrackBar value
            //query += $" AND Price <= {trackBar1.Value}";
            if (trackBar1.Value > 0)
            {
                query += $" AND Price <= {trackBar1.Value}";
            }

            SqlDataAdapter sda = new SqlDataAdapter(query, con);

            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;

            DataGridViewImageColumn dgv = new DataGridViewImageColumn();
            dgv = (DataGridViewImageColumn)dataGridView1.Columns[6];
            dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }


        private string GetQueryBasedOnFilterOption()
        {
            string baseQuery = "SELECT * FROM Products WHERE type = 'Keyboard'";

            switch (currentFilter)
            {
                case FilterAvailable:
                    return $"{baseQuery} AND Quantity > 0";
                case FilterOutOfStock:
                    return $"{baseQuery} AND Quantity = 0";
                case FilterAll:
                default:
                    return baseQuery;
            }
        }

        private void UpdateGridViewBasedOnFilter()
        {
            BindGridViewKeyboard();
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                currentFilter = FilterAvailable;
            }
            else if (radioButton2.Checked)
            {
                currentFilter = FilterOutOfStock;
            }
            else if (radioButton3.Checked)
            {
                currentFilter = FilterAll;
            }

            UpdateGridViewBasedOnFilter();
        }
        /// <summary>
        /// 
        /// </summary>
        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.label2.Text = this.label2.Text;
            f1.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form11 f11 = new Form11();
            f11.ShowDialog();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form12 f12 = new Form12();
            f12.ShowDialog();
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form13 f13 = new Form13();
            f13.ShowDialog();
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
        bool expand1 = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }
        bool expand2 = false;
        private void timer2_Tick(object sender, EventArgs e)
        {
            
        }
        bool expand3 = false;
        private void timer3_Tick(object sender, EventArgs e)
        {
            
        }

        private void button27_MouseHover(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button31_MouseHover(object sender, EventArgs e)
        {
            timer2.Start();
        }

        private void button10_MouseHover(object sender, EventArgs e)
        {
            timer3.Start();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2();
            f2.ShowDialog();
            this.Close();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form10 f10 = new Form10();
            f10.ShowDialog();
            this.Close();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form16 f16 = new Form16();
            f16.label18.Text = this.label2.Text;
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
            dataGridView2.DataSource = data;

            DataGridViewImageColumn dgv = new DataGridViewImageColumn();
            dgv = (DataGridViewImageColumn)dataGridView2.Columns[6];
            dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;

            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            dataGridView2.Visible = true;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (!dataGridView2.RectangleToScreen(dataGridView2.DisplayRectangle).Contains(Cursor.Position))
            {
                dataGridView2.Visible = false;
            }
        }

        private void Form10_Load(object sender, EventArgs e)
        {

        }

        private void Form10_MouseClick(object sender, MouseEventArgs e)
        {
            this.ActiveControl = null;
        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.Hide();
            Form3 f3 = new Form3();
            f3.label16.Text = this.label2.Text;
            f3.label9.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
            f3.label8.Text = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
            f3.label11.Text = dataGridView2.SelectedRows[0].Cells[4].Value.ToString();
            f3.label12.Text = dataGridView2.SelectedRows[0].Cells[5].Value.ToString();
            f3.label13.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
            f3.pictureBox1.Image = GetImage((byte[])dataGridView2.SelectedRows[0].Cells[6].Value);
            f3.label69.Text = dataGridView2.SelectedRows[0].Cells[3].Value.ToString();
            f3.ShowDialog();
            this.Close();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            // Update the label to display the current range
            UpdatePriceRangeLabel();
            // Refresh the DataGridView based on the updated range
            UpdateGridViewBasedOnFilter();
        }
        private void UpdatePriceRangeLabel()
        {
            //label4.Text = $"{trackBar1.Value} - {trackBar1.Value + 1000}";
            label4.Text = $"{trackBar1.Value}";
        }
    }
}
