using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace TLBSMS
{
    public partial class Form6 : Form
    {
        public Form6() { InitializeComponent(); }

        private void LoadEvent(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TOHIR\source\repos\TLBSMS\TLBSMS\Database\LibraryBookStore.mdf;Integrated Security=True;Connect Timeout=30");
            new SqlDataAdapter("SELECT * FROM Login_Status;", connect).Fill(dataTable);
            managerActivityList.DataSource = dataTable;
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Hide(); new Form3().Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable(); 
            SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TOHIR\source\repos\TLBSMS\TLBSMS\Database\LibraryBookStore.mdf;Integrated Security=True;Connect Timeout=30");
            new SqlDataAdapter("DELETE FROM Login_Status;", connect).Fill(dataTable);
            managerActivityList.DataSource = dataTable;
        }
    }
}
