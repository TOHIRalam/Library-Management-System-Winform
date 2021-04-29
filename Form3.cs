using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TLBSMS
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            passwordBox.PasswordChar = '*';
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            DataTable managersData = new DataTable();
            SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TOHIR\source\repos\TLBSMS\TLBSMS\Database\LibraryBookStore.mdf;Integrated Security=True;Connect Timeout=30");
            SqlDataAdapter managerListAdapter = new SqlDataAdapter("SELECT * FROM Managers", connect);
            managerListAdapter.Fill(managersData);
            managerList.DataSource = managersData;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string managerName = managerNameBox.Text.Trim();
            string password = passwordBox.Text.Trim();
            if(managerName.Length < 3 || password.Length < 4) { MessageBox.Show("Name length should be atleast 3 and\npassword length should be atleast 4"); }
            else
            {
                string checkQuery = "SELECT name FROM Managers WHERE name = '" + managerName + "';";
                string insertData = $"INSERT INTO Managers VALUES ('{managerName}', '{password}')";
                DataTable managersData = new DataTable();
                DataTable checkManagers = new DataTable();
                SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TOHIR\source\repos\TLBSMS\TLBSMS\Database\LibraryBookStore.mdf;Integrated Security=True;Connect Timeout=30");
                SqlDataAdapter checkDataAdapter = new SqlDataAdapter(checkQuery, connect);
                checkDataAdapter.Fill(checkManagers);
                if (checkManagers.Rows.Count == 1) { MessageBox.Show("Username exists!\nPlease change username...."); }
                else
                {
                    new SqlDataAdapter(insertData, connect).Fill(managersData);
                    new SqlDataAdapter("SELECT * FROM Managers;", connect).Fill(managersData);
                    managerList.DataSource = managersData;
                    managerNameBox.Text = ""; passwordBox.Text = "";
                }
            }
        }

        private void delete_manager_button_Click(object sender, EventArgs e)
        {
            string deletedManagersName = deleteManagerTextBox.Text.Trim();
            if(deletedManagersName.Length == 0) { MessageBox.Show("Invalid Operation!"); }
            else
            {
                DataTable deleteData = new DataTable();
                SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TOHIR\source\repos\TLBSMS\TLBSMS\Database\LibraryBookStore.mdf;Integrated Security=True;Connect Timeout=30");
                new SqlDataAdapter($"DELETE FROM Managers WHERE name = '{deletedManagersName}'", connect).Fill(deleteData);
                new SqlDataAdapter("SELECT * FROM Managers", connect).Fill(deleteData);
                managerList.DataSource = deleteData;
                deleteManagerTextBox.Text = "";
            }
        }

        private void label8_Click(object sender, EventArgs e) { Hide(); new Form1().Show(); }

        private void pictureBox7_Click(object sender, EventArgs e) { Hide(); new Form6().Show(); }
    }
}
