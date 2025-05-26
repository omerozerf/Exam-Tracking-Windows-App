using System;
using System.Windows.Forms;
using System.Xml.Linq;
using MySql.Data.MySqlClient;

namespace LGSTracking
{
    public partial class AddStudentForm : Form
    {
        private MySqlConnection connection;

        public AddStudentForm()
        {
            InitializeComponent();
            connection = new MySqlConnection("Server=localhost;Database=lgstracking;Uid=root;Pwd=1234;");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string name = txtName.Text;
            string surname = txtSurname.Text;
            string school = txtSchool.Text;
            string studentClass = txtClass.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.");
                return;
            }

            string query = "INSERT INTO Students (Username, Password, Name, Surname, School, Class) VALUES (@username, @password, @name, @surname, @school, @class)";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@surname", surname);
            command.Parameters.AddWithValue("@school", school);
            command.Parameters.AddWithValue("@class", studentClass);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            MessageBox.Show("Öğrenci başarıyla eklendi.");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}