using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TLBSMS
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable(); DataTable bookListTable = new DataTable();
            SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TOHIR\source\repos\TLBSMS\TLBSMS\Database\LibraryBookStore.mdf;Integrated Security=True;Connect Timeout=30");
            new SqlDataAdapter("SELECT Name FROM Login_Status;", connect).Fill(dataTable);
            label1.Text = dataTable.Rows[0][0].ToString() + "'s Dashboard";
            new SqlDataAdapter("SELECT BookID, BookName, WriterName, AvailableBook FROM ManagerBookEntry;", connect).Fill(bookListTable);
            BookList.DataSource = bookListTable;
        }

        private void label8_Click(object sender, EventArgs e) { Hide(); new Form1().Show(); }

        private void button1_Click(object sender, EventArgs e)
        {
            string bookName = bookNameBox.Text.ToString().Trim();
            string writerName = writerNameBox.Text.ToString().Trim();
            string catagory = catagoryComboBox.GetItemText(catagoryComboBox.SelectedItem);
            string entryDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");

            
            if(bookName != "" && writerName != "" && catagory != "" && entryDate != "")
            {
                if(quantityBox.Text.ToString() != "" && bookPublishYearBox.Text.ToString() != "" && availableBookBox.Text.ToString() != "")
                {
                    int quantity = Convert.ToInt32(quantityBox.Text.ToString());
                    int bookPublishYear = Convert.ToInt32(bookPublishYearBox.Text.ToString());
                    int availableBooks = Convert.ToInt32(availableBookBox.Text.ToString());
                    try
                    {
                        if (availableBooks > quantity) { MessageBox.Show("Number of available books can't be\ngreater than the whole quantity of the books!"); }
                        else
                        {
                            DataTable dataTable = new DataTable(); DataTable showBookList = new DataTable(); DataTable insertBookList = new DataTable(); DataTable checkExistance = new DataTable();
                            SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TOHIR\source\repos\TLBSMS\TLBSMS\Database\LibraryBookStore.mdf;Integrated Security=True;Connect Timeout=30");
                            new SqlDataAdapter($"SELECT BookName FROM ManagerBookEntry WHERE BookName = '{bookName}';", connect).Fill(checkExistance);
                            if (checkExistance.Rows.Count == 1) { MessageBox.Show("Book already exist!!"); }
                            else
                            {
                                new SqlDataAdapter("SELECT TOP 1 BookID FROM ManagerBookEntry ORDER BY BookID DESC;", connect).Fill(dataTable);
                                int currentBookId = (dataTable.Rows.Count == 1) ? Convert.ToInt32(dataTable.Rows[0][0].ToString()) + 1 : 1101;
                                string insertQueryString = $"INSERT INTO ManagerBookEntry VALUES({currentBookId}, '{bookName}'," +
                                $"{bookPublishYear}, '{writerName}', {quantity}, '{catagory}', '{entryDate}', {availableBooks}, {quantity - availableBooks});";
                                new SqlDataAdapter(insertQueryString, connect).Fill(insertBookList);
                                new SqlDataAdapter("SELECT BookID, BookName, WriterName, AvailableBook FROM ManagerBookEntry;", connect).Fill(showBookList);
                                BookList.DataSource = showBookList; bookNameBox.Text = ""; writerNameBox.Text = "";newQuantityBox.Text = ""; 
                                bookPublishYearBox.Text = ""; availableBookBox.Text = ""; quantityBox.Text = "";
                            }
                        }
                    } catch (Exception exc) { MessageBox.Show(exc.Message); }
                } else { MessageBox.Show("Invalid Operation!"); }
            } else { MessageBox.Show("Invalid Operation!"); }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            try
            {
                string catagory = "";
                if(comboBox1.SelectedItem != null) { catagory = comboBox1.GetItemText(comboBox1.SelectedItem.ToString()); } 
                string searchBoxText = searchBox.Text.Trim(); DataTable dataTable = new DataTable();
                string query = $"SELECT BookID, BookName, WriterName, AvailableBook FROM ManagerBookEntry WHERE CatagoryName = '{catagory}';";
                if ((catagory == "" || catagory == "All Catagory") && searchBoxText == "") { query = $"SELECT BookID, BookName, WriterName, AvailableBook FROM ManagerBookEntry;"; }
                else if (searchBoxText != "") { query = $"SELECT BookID, BookName, WriterName, AvailableBook FROM ManagerBookEntry WHERE BookName LIKE '{searchBoxText}%';"; }
                SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TOHIR\source\repos\TLBSMS\TLBSMS\Database\LibraryBookStore.mdf;Integrated Security=True;Connect Timeout=30");
                new SqlDataAdapter(query, connect).Fill(dataTable);
                BookList.DataSource = dataTable;
            } catch (Exception exc) { MessageBox.Show(exc.Message); }
        }

        private void updateBookInfo_Click(object sender, EventArgs e)
        {
            string bookName = bookNameBox.Text.ToString().Trim();
            string writerName = writerNameBox.Text.ToString().Trim();
            string catagory = catagoryComboBox.GetItemText(catagoryComboBox.SelectedItem);
            string entryDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");


            if (bookName.Length != 0 && writerName.Length != 0 && catagory.Length != 0 && entryDate.Length != 0)
            {
                if (bookPublishYearBox.Text.ToString().Length != 0 && availableBookBox.Text.ToString().Length != 0 && newQuantityBox.Text.ToString().Length != 0)
                {
                    int newQuantity = Convert.ToInt32(newQuantityBox.Text.ToString());
                    int bookPublishYear = Convert.ToInt32(bookPublishYearBox.Text.ToString());
                    int availableBooks = Convert.ToInt32(availableBookBox.Text.ToString());
                    try
                    {
                        DataTable showBookList = new DataTable(); DataTable updateBookList = new DataTable(); DataTable getQuantity = new DataTable();
                        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TOHIR\source\repos\TLBSMS\TLBSMS\Database\LibraryBookStore.mdf;Integrated Security=True;Connect Timeout=30");
                        new SqlDataAdapter($"SELECT QuantityOfBook FROM ManagerBookEntry WHERE BookID = {Convert.ToInt32(bookIDBox.Text.ToString())};", connect).Fill(getQuantity);
                        int currentQuantity = Convert.ToInt32(getQuantity.Rows[0][0].ToString()) + newQuantity;
                        if (availableBooks > currentQuantity) { MessageBox.Show("Number of available books can't be\ngreater than the whole quantity of the books!"); }
                        else
                        {
                            if (getQuantity.Rows.Count == 1) 
                            {
                                string updateQueryString = $"UPDATE ManagerBookEntry SET BookName = '{bookName}', BookPublishYear = {bookPublishYear}, " +
                                $"WriterName = '{writerName}', QuantityOfBook = {currentQuantity}, CatagoryName = '{catagory}', EntryDate = '{entryDate}', " +
                                $"AvailableBook = {availableBooks}, BorrowBook = {currentQuantity - availableBooks} WHERE BookID = {Convert.ToInt32(bookIDBox.Text.ToString())};";
                                new SqlDataAdapter(updateQueryString, connect).Fill(updateBookList);
                                new SqlDataAdapter("SELECT BookID, BookName, WriterName, AvailableBook FROM ManagerBookEntry;", connect).Fill(showBookList);
                                BookList.DataSource = showBookList; bookNameBox.Text = ""; writerNameBox.Text = ""; bookIDBox.Text = ""; newQuantityBox.Text = ""; bookPublishYearBox.Text = ""; availableBookBox.Text = "";
                            } else { MessageBox.Show("Invalid BookID!"); }
                        }
                    } catch (Exception exc) { MessageBox.Show(exc.Message); }
                } else { MessageBox.Show("Invalid Operation!"); }
            } else { MessageBox.Show("Invalid Operation!"); }
        }

        private void deleteBookButton_Click(object sender, EventArgs e)
        {
            string deleteBookID = deleteBox.Text.Trim();
            if (deleteBookID.Length == 0) { MessageBox.Show("Empty Book ID Field!"); }
            else
            {
                DataTable deleteData = new DataTable();
                SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\TOHIR\source\repos\TLBSMS\TLBSMS\Database\LibraryBookStore.mdf;Integrated Security=True;Connect Timeout=30");
                new SqlDataAdapter($"DELETE FROM ManagerBookEntry WHERE BookID = '{Convert.ToInt32(deleteBookID)}'", connect).Fill(deleteData);
                new SqlDataAdapter("SELECT BookID, BookName, WriterName, AvailableBook FROM ManagerBookEntry;", connect).Fill(deleteData);
                BookList.DataSource = deleteData;
                deleteBox.Text = "";
            }
        }
    }
}
