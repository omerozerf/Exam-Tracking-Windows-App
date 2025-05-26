using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace LGSTracking
{
    public partial class LoginForm : Form
    {
        private MySqlConnection connection;

        public LoginForm()
        {
            InitializeComponent();
            connection = new MySqlConnection("Server=localhost;Database=lgstracking;Uid=root;Pwd=1234;");
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            if (rbtnAdmin.Checked)
            {
                AdminLogin(username, password);
            }
            else if (rbtnStudent.Checked)
            {
                StudentLogin(username, password);
            }
            else
            {
                MessageBox.Show("Please select a user type (Admin or Student).");
            }
        }

        private void AdminLogin(string username, string password)
        {
            string query = "SELECT * FROM Admins WHERE Username = @username AND Password = @password";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Close();
                connection.Close();
                AdminPanel adminPanel = new AdminPanel();
                adminPanel.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Admin username or password.");
            }
            connection.Close();
        }

        private void StudentLogin(string username, string password)
        {
            string query = "SELECT * FROM Students WHERE Username = @username AND Password = @password";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                int studentID = Convert.ToInt32(reader["ID"]);
                reader.Close();
                connection.Close();

                StudentPanel studentPanel = new StudentPanel(studentID);
                studentPanel.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Student username or password.");
            }
            connection.Close();
        }
    }
}