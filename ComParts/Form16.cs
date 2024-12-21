using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComParts
{
    public partial class Form16 : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public Form16()
        {
            InitializeComponent();
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }
        internal void BindGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select ProductID,ProductName,Price,Quantity,TotalPrice from Cart where CustomerName = @cName";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@cName", label18.Text);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            int sum = 0; // Initialize sum before the loop

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                sum += Convert.ToInt32(dataGridView1.Rows[i].Cells["TotalPrice"].Value);
            }

            label15.Text = sum.ToString();
        }



        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "" && richTextBox1.Text != "" && (radioButton1.Checked == true || radioButton2.Checked == true || radioButton3.Checked == true))
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction();

                    // Update Customer table
                    using (SqlCommand cmdUpdateCustomer = new SqlCommand("UPDATE Customer SET Address = @address,Number = @number WHERE Username = @cName;", con, transaction))
                    {
                        cmdUpdateCustomer.Parameters.AddWithValue("@address", richTextBox1.Text);
                        cmdUpdateCustomer.Parameters.AddWithValue("@number", textBox3.Text);
                        cmdUpdateCustomer.Parameters.AddWithValue("@cName", label18.Text);
                        cmdUpdateCustomer.ExecuteNonQuery();
                    }

                    // Move data from Cart to Invoice table
                    using (SqlCommand cmdMoveToInvoice = new SqlCommand("INSERT INTO Invoice (CustomerName, ProductID, ProductName, Price, Quantity, TotalPrice) SELECT CustomerName, ProductID, ProductName, Price, Quantity, TotalPrice FROM Cart WHERE CustomerName = @cName;", con, transaction))
                    {
                        cmdMoveToInvoice.Parameters.AddWithValue("@cName", label18.Text);
                        cmdMoveToInvoice.ExecuteNonQuery();
                    }

                    // Update Product quantities based on Cart data
                    using (SqlCommand cmdUpdateProducts = new SqlCommand("UPDATE Products SET Products.Quantity = p.Quantity - c.Quantity FROM Products p INNER JOIN Cart c ON p.ID = c.ProductID WHERE c.CustomerName = @cName;", con, transaction))
                    {
                        cmdUpdateProducts.Parameters.AddWithValue("@cName", label18.Text);
                        cmdUpdateProducts.ExecuteNonQuery();
                    }

                    // Delete rows from Cart table
                    using (SqlCommand cmdDeleteFromCart = new SqlCommand("DELETE FROM Cart WHERE CustomerName = @cName;", con, transaction))
                    {
                        cmdDeleteFromCart.Parameters.AddWithValue("@cName", label18.Text);
                        cmdDeleteFromCart.ExecuteNonQuery();
                    }

                    // Commit the transaction
                    transaction.Commit();

                    MessageBox.Show("Order Placed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    this.Hide();
                    Form1 f1 = new Form1();
                    f1.ShowDialog();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Please enter informations!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure? Your Cart will be empty!", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction();

                    // Delete rows from Cart table
                    using (SqlCommand cmdDeleteFromCart = new SqlCommand("DELETE FROM Cart WHERE CustomerName = @cName;", con, transaction))
                    {
                        cmdDeleteFromCart.Parameters.AddWithValue("@cName", label18.Text);
                        cmdDeleteFromCart.ExecuteNonQuery();
                    }

                    // Commit the transaction
                    transaction.Commit();
                }
                this.Hide();
                Form1 f1 = new Form1();
                f1.label2.Text = this.label18.Text;
                f1.ShowDialog();
                this.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure? Your Cart will be empty!", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction();

                    // Delete rows from Cart table
                    using (SqlCommand cmdDeleteFromCart = new SqlCommand("DELETE FROM Cart WHERE CustomerName = @cName;", con, transaction))
                    {
                        cmdDeleteFromCart.Parameters.AddWithValue("@cName", label18.Text);
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
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this item?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    int productID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ProductID"].Value);

                    using (SqlConnection con = new SqlConnection(cs))
                    {
                        con.Open();
                        SqlTransaction transaction = con.BeginTransaction();

                        // Delete the selected product from the Cart
                        using (SqlCommand cmdDeleteFromCart = new SqlCommand("DELETE FROM Cart WHERE ProductID = @productID AND CustomerName = @cName;", con, transaction))
                        {
                            cmdDeleteFromCart.Parameters.AddWithValue("@productID", productID);
                            cmdDeleteFromCart.Parameters.AddWithValue("@cName", label18.Text);
                            cmdDeleteFromCart.ExecuteNonQuery();
                        }

                        // Commit the transaction
                        transaction.Commit();

                        MessageBox.Show("Item deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Refresh the DataGridView after deletion
                        BindGridView();
                    }
                }
            }
        }
    }
}
