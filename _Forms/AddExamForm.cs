using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace LGSTracking
{
    public partial class AddExamForm : Form
    {
        private MySqlConnection connection;
        private int studentID;

        public AddExamForm(int studentID)
        {
            InitializeComponent();
            this.studentID = studentID;
            connection = new MySqlConnection("Server=localhost;Database=lgstracking;Uid=root;Pwd=1234;");
        }

        private int ToInt(TextBox box)
        {
            return int.TryParse(box.Text, out int val) ? val : 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text) ||
                string.IsNullOrWhiteSpace(txtMathCorrect.Text) || string.IsNullOrWhiteSpace(txtMathWrong.Text) ||
                string.IsNullOrWhiteSpace(txtScienceCorrect.Text) || string.IsNullOrWhiteSpace(txtScienceWrong.Text) ||
                string.IsNullOrWhiteSpace(txtTurkishCorrect.Text) || string.IsNullOrWhiteSpace(txtTurkishWrong.Text) ||
                string.IsNullOrWhiteSpace(txtHistoryCorrect.Text) || string.IsNullOrWhiteSpace(txtHistoryWrong.Text) ||
                string.IsNullOrWhiteSpace(txtReligionCorrect.Text) || string.IsNullOrWhiteSpace(txtReligionWrong.Text) ||
                string.IsNullOrWhiteSpace(txtEnglishCorrect.Text) || string.IsNullOrWhiteSpace(txtEnglishWrong.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.");
                return;
            }

            if ((ToInt(txtMathCorrect) + ToInt(txtMathWrong)) > 20 ||
                (ToInt(txtScienceCorrect) + ToInt(txtScienceWrong)) > 20 ||
                (ToInt(txtTurkishCorrect) + ToInt(txtTurkishWrong)) > 20 ||
                (ToInt(txtHistoryCorrect) + ToInt(txtHistoryWrong)) > 10 ||
                (ToInt(txtReligionCorrect) + ToInt(txtReligionWrong)) > 10 ||
                (ToInt(txtEnglishCorrect) + ToInt(txtEnglishWrong)) > 10)
            {
                MessageBox.Show("Bazı derslerde doğru + yanlış toplamı soru sayısını aşıyor.");
                return;
            }

            string query = @"INSERT INTO Exams 
        (StudentID, Title, Date, 
         MathCorrect, MathWrong, 
         ScienceCorrect, ScienceWrong, 
         TurkishCorrect, TurkishWrong, 
         HistoryCorrect, HistoryWrong, 
         ReligionCorrect, ReligionWrong, 
         EnglishCorrect, EnglishWrong)
        VALUES 
        (@StudentID, @Title, @Date, 
         @MathCorrect, @MathWrong, 
         @ScienceCorrect, @ScienceWrong, 
         @TurkishCorrect, @TurkishWrong, 
         @HistoryCorrect, @HistoryWrong, 
         @ReligionCorrect, @ReligionWrong, 
         @EnglishCorrect, @EnglishWrong)";

            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@StudentID", studentID);
            command.Parameters.AddWithValue("@Title", txtTitle.Text);
            command.Parameters.AddWithValue("@Date", dtpExamDate.Value);

            command.Parameters.AddWithValue("@MathCorrect", Convert.ToInt32(txtMathCorrect.Text));
            command.Parameters.AddWithValue("@MathWrong", Convert.ToInt32(txtMathWrong.Text));
            command.Parameters.AddWithValue("@ScienceCorrect", Convert.ToInt32(txtScienceCorrect.Text));
            command.Parameters.AddWithValue("@ScienceWrong", Convert.ToInt32(txtScienceWrong.Text));
            command.Parameters.AddWithValue("@TurkishCorrect", Convert.ToInt32(txtTurkishCorrect.Text));
            command.Parameters.AddWithValue("@TurkishWrong", Convert.ToInt32(txtTurkishWrong.Text));
            command.Parameters.AddWithValue("@HistoryCorrect", Convert.ToInt32(txtHistoryCorrect.Text));
            command.Parameters.AddWithValue("@HistoryWrong", Convert.ToInt32(txtHistoryWrong.Text));
            command.Parameters.AddWithValue("@ReligionCorrect", Convert.ToInt32(txtReligionCorrect.Text));
            command.Parameters.AddWithValue("@ReligionWrong", Convert.ToInt32(txtReligionWrong.Text));
            command.Parameters.AddWithValue("@EnglishCorrect", Convert.ToInt32(txtEnglishCorrect.Text));
            command.Parameters.AddWithValue("@EnglishWrong", Convert.ToInt32(txtEnglishWrong.Text));

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            MessageBox.Show("Sınav başarıyla eklendi.");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}