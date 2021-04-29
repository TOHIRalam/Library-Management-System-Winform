using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TLBSMS
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            passwordBox.PasswordChar = '*';
        }

        private void button2_Click(object sender, EventArgs e) { Close(); new Form1().Show(); }

        private void button3_Click(object sender, EventArgs e)
        {
            string username = usernameBox.Text.Trim();
            string password = passwordBox.Text.Trim();
            string loginCheckQuery = "SELECT * FROM Login WHERE username = '" + username + "' AND password = '" + password + "';";
            SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TOHIR\source\repos\TLBSMS\TLBSMS\Database\LibraryBookStore.mdf;Integrated Security=True;Connect Timeout=30");
            SqlDataAdapter dataAdaptar = new SqlDataAdapter(loginCheckQuery, connect);
            DataTable loginData = new DataTable();
            dataAdaptar.Fill(loginData);

            if(loginData.Rows.Count == 1) { Hide(); new Form3().Show(); }
            else { MessageBox.Show("Invalid Username or Password!"); }
        }
    }
}
