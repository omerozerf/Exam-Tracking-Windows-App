using System;
using System.Windows.Forms;
using System.Xml.Linq;
using MySql.Data.MySqlClient;

namespace LGSTracking
{
    public partial class EditStudentForm : Form
    {
        private MySqlConnection connection;
        private int studentID;

        public EditStudentForm(int studentID)
        {
            InitializeComponent();
            this.studentID = studentID;
            connection = new MySqlConnection("Server=localhost;Database=lgstracking;Uid=root;Pwd=1234;");
            LoadStudent();
        }

        private void LoadStudent()
        {
            string query = "SELECT * FROM Students WHERE ID = @id";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", studentID);

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                txtUsername.Text = reader["Username"].ToString();
                txtPassword.Text = reader["Password"].ToString();
                txtName.Text = reader["Name"].ToString();
                txtSurname.Text = reader["Surname"].ToString();
                txtSchool.Text = reader["School"].ToString();
                txtClass.Text = reader["Class"].ToString();
            }
            connection.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string query = "UPDATE Students SET Username = @username, Password = @password, Name = @name, Surname = @surname, School = @school, Class = @class WHERE ID = @id";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", txtUsername.Text);
            command.Parameters.AddWithValue("@password", txtPassword.Text);
            command.Parameters.AddWithValue("@name", txtName.Text);
            command.Parameters.AddWithValue("@surname", txtSurname.Text);
            command.Parameters.AddWithValue("@school", txtSchool.Text);
            command.Parameters.AddWithValue("@class", txtClass.Text);
            command.Parameters.AddWithValue("@id", studentID);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            MessageBox.Show("Öğrenci bilgileri başarıyla güncellendi.");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}