using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TLBSMS
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            passwordBox.PasswordChar = '*';
        }

        private void button2_Click(object sender, EventArgs e) { Hide(); new Form1().Show(); }

        private void button3_Click(object sender, EventArgs e)
        {
            string username = usernameBox.Text.Trim();
            string password = passwordBox.Text.Trim();

            string managerQuery = $"SELECT * FROM Managers WHERE name = '{username}' AND password = '{password}';";
            SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TOHIR\source\repos\TLBSMS\TLBSMS\Database\LibraryBookStore.mdf;Integrated Security=True;Connect Timeout=30");
            DataTable dataTable = new DataTable();
            new SqlDataAdapter(managerQuery, connect).Fill(dataTable);
            if(dataTable.Rows.Count == 1) 
            { 
                new SqlDataAdapter($"INSERT INTO Login_Status VALUES ('{username}', '{DateTime.Now.ToString()}');", connect).Fill(dataTable);
                Hide(); new Form5().Show();
            }
            else { MessageBox.Show("Invalid username or password!"); }
        }
    }
}
