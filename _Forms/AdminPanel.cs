using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace LGSTracking
{
    public partial class AdminPanel : Form
    {
        private MySqlConnection connection;

        public AdminPanel()
        {
            InitializeComponent();
            connection = new MySqlConnection("Server=localhost;Database=lgstracking;Uid=root;Pwd=1234;");
            LoadStudents();
        }

        // Öğrencileri Yükle
        private void LoadStudents()
        {
            string query = "SELECT ID, Username, Name, Surname, School, Class FROM Students";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dgvStudents.DataSource = table;
            dgvStudents.Columns[0].Visible = false;
        }

        // Öğrenci Ekle
        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            AddStudentForm addStudentForm = new AddStudentForm();
            if (addStudentForm.ShowDialog() == DialogResult.OK)
            {
                LoadStudents();
            }
        }

        // Öğrenci Düzenle
        private void btnEditStudent_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen düzenlemek istediğiniz öğrenciyi seçin.");
                return;
            }

            int studentID = Convert.ToInt32(dgvStudents.SelectedRows[0].Cells[0].Value);
            EditStudentForm editStudentForm = new EditStudentForm(studentID);
            if (editStudentForm.ShowDialog() == DialogResult.OK)
            {
                LoadStudents();
            }
        }

        // Öğrenci Sil
        private void btnDeleteStudent_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen silmek istediğiniz öğrenciyi seçin.");
                return;
            }

            int studentID = Convert.ToInt32(dgvStudents.SelectedRows[0].Cells[0].Value);

            DialogResult result = MessageBox.Show("Öğrenciyi silmek istediğinizden emin misiniz?", "Öğrenci Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                string query = "DELETE FROM Students WHERE ID = @id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", studentID);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                LoadStudents();
            }
        }

        // Sınavları Görüntüle
        private void btnViewExams_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen sınavlarını görüntülemek istediğiniz öğrenciyi seçin.");
                return;
            }

            int studentID = Convert.ToInt32(dgvStudents.SelectedRows[0].Cells[0].Value);
            ExamsForm examsForm = new ExamsForm(studentID);
            examsForm.Show();
        }

        // Çıkış Yap
        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }
    }
}