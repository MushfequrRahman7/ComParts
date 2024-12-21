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
    public partial class UserControl4 : UserControl
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public UserControl4()
        {
            InitializeComponent();
        }
        internal void BindGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select CustomerName,ProductID,ProductName,Price,Quantity,TotalPrice from Invoice";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);

            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["Deliver"].Index)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to deliver this item?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    int productID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ProductID"].Value);
                    string customerName = dataGridView1.Rows[e.RowIndex].Cells["CustomerName"].Value.ToString();

                    using (SqlConnection con = new SqlConnection(cs))
                    {
                        con.Open();
                        SqlTransaction transaction = con.BeginTransaction();

                        // Insert the deleted product into the Sells table
                        using (SqlCommand cmdMoveToSells = new SqlCommand("INSERT INTO Sells (CustomerName, ProductID, ProductName, Price, Quantity, TotalPrice) SELECT CustomerName, ProductID, ProductName, Price, Quantity, TotalPrice FROM Invoice WHERE ProductID = @productID AND CustomerName = @customerName;", con, transaction))
                        {
                            cmdMoveToSells.Parameters.AddWithValue("@productID", productID);
                            cmdMoveToSells.Parameters.AddWithValue("@customerName", customerName);
                            cmdMoveToSells.ExecuteNonQuery();
                        }

                        // Delete the selected product from the Invoice
                        using (SqlCommand cmdDeleteFromInvoice = new SqlCommand("DELETE FROM Invoice WHERE ProductID = @productID AND CustomerName = @customerName;", con, transaction))
                        {
                            cmdDeleteFromInvoice.Parameters.AddWithValue("@productID", productID);
                            cmdDeleteFromInvoice.Parameters.AddWithValue("@customerName", customerName);
                            cmdDeleteFromInvoice.ExecuteNonQuery();
                        }

                        // Commit the transaction
                        transaction.Commit();

                        MessageBox.Show("Item delivered successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Refresh the DataGridView after delivery
                        BindGridView();
                    }
                }
            }
        }
    }
}
