using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MySql.Data.MySqlClient;
using System.Drawing;

namespace LGSTracking
{
    public partial class AdminChartForm : Form
    {
        private MySqlConnection connection;

        public AdminChartForm()
        {
            InitializeComponent();
            connection = new MySqlConnection("Server=localhost;Database=lgstracking;Uid=root;Pwd=1234;");
            LoadStudentList();
        }

        private void LoadStudentList()
        {
            string query = "SELECT ID, Name, Surname FROM Students";
            MySqlCommand command = new MySqlCommand(query, connection);

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                comboBoxStudents.Items.Add(reader["ID"] + " - " + reader["Name"] + " " + reader["Surname"]);
            }
            connection.Close();
        }

        private void btnShowChart_Click(object sender, EventArgs e)
        {
            if (comboBoxStudents.SelectedIndex == -1) return;

            int studentID = Convert.ToInt32(comboBoxStudents.SelectedItem.ToString().Split(' ')[0]);
            LoadStudentChart(studentID);
        }

        private void comboChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxStudents.SelectedIndex != -1)
            {
                int studentID = Convert.ToInt32(comboBoxStudents.SelectedItem.ToString().Split(' ')[0]);
                LoadStudentChart(studentID);
            }
        }

        private void LoadStudentChart(int studentID)
        {
            chart1.Series.Clear();
            chart1.Titles.Clear();
            chart1.Legends.Clear();

            chart1.Titles.Add("Öğrenci Performans Grafiği");
            var legend = new Legend
            {
                Title = "Dersler",
                Docking = Docking.Bottom,
                Alignment = StringAlignment.Center
            };
            chart1.Legends.Add(legend);

            SeriesChartType chartType = comboChartType.SelectedItem.ToString() == "Line"
                ? SeriesChartType.Line
                : SeriesChartType.Column;

            var seriesMath = new Series("Matematik") { Color = Color.Blue, ChartType = chartType };
            var seriesScience = new Series("Fen") { Color = Color.Green, ChartType = chartType };
            var seriesTurkish = new Series("Türkçe") { Color = Color.Red, ChartType = chartType };
            var seriesHistory = new Series("Tarih") { Color = Color.Orange, ChartType = chartType };
            var seriesReligion = new Series("Din") { Color = Color.Purple, ChartType = chartType };
            var seriesEnglish = new Series("İngilizce") { Color = Color.Gray, ChartType = chartType };

            chart1.Series.Add(seriesMath);
            chart1.Series.Add(seriesScience);
            chart1.Series.Add(seriesTurkish);
            chart1.Series.Add(seriesHistory);
            chart1.Series.Add(seriesReligion);
            chart1.Series.Add(seriesEnglish);

            string query = "SELECT Date, Math, Science, Turkish, History, Religion, English FROM Exams WHERE StudentID = @studentID";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@studentID", studentID);

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string examDate = Convert.ToDateTime(reader["Date"]).ToString("dd/MM/yyyy");

                seriesMath.Points.AddXY(examDate, Convert.ToDouble(reader["Math"]));
                seriesScience.Points.AddXY(examDate, Convert.ToDouble(reader["Science"]));
                seriesTurkish.Points.AddXY(examDate, Convert.ToDouble(reader["Turkish"]));
                seriesHistory.Points.AddXY(examDate, Convert.ToDouble(reader["History"]));
                seriesReligion.Points.AddXY(examDate, Convert.ToDouble(reader["Religion"]));
                seriesEnglish.Points.AddXY(examDate, Convert.ToDouble(reader["English"]));
            }

            connection.Close();
        }
    }
}