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
    public partial class UserControl5 : UserControl
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public UserControl5()
        {
            InitializeComponent();
        }
        public void BindGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select Type,Name,Quantity,Image from Products order by type";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);

            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;

            DataGridViewImageColumn dgv = new DataGridViewImageColumn();
            dgv = (DataGridViewImageColumn)dataGridView1.Columns[3];
            dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;
            
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //chart1.Series[0].XValueMember = "Type";
            //chart1.Series[0].YValueMembers = "Type";

            //chart1.Series[1].XValueMember = "Quantity";
            //chart1.Series[1].YValueMembers = "Quantity";

            //chart1.DataSource = dataGridView1.DataSource;
            //chart1.DataBind();
            DataTable dataTable = (DataTable)dataGridView1.DataSource;

            // Use LINQ to group by Type and sum Quantity
            var groupedData = from row in dataTable.AsEnumerable()
                              group row by row.Field<string>("Type") into grp
                              select new
                              {
                                  Type = grp.Key,
                                  Quantity = grp.Sum(row => row.Field<int>("Quantity"))
                              };

            // Clear existing series data
            chart1.Series[0].Points.Clear();

            // Add grouped data to the chart
            foreach (var item in groupedData)
            {
                chart1.Series[0].Points.AddXY(item.Type, item.Quantity);
            }
        }
    }
}
