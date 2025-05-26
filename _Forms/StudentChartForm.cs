using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MySql.Data.MySqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace LGSTracking
{
    public partial class StudentChartForm : Form
    {
        private MySqlConnection connection;
        private int studentID;

        public StudentChartForm(int studentID)
        {
            InitializeComponent();
            this.studentID = studentID;
            connection = new MySqlConnection("Server=localhost;Database=lgstracking;Uid=root;Pwd=1234;");
        }

        private void StudentChartForm_Load(object sender, EventArgs e)
        {
            LoadStudentChart();
        }

        private void comboChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStudentChart();
        }

        private void LoadStudentChart()
        {
            chart1.Series.Clear();

            SeriesChartType chartType = comboChartType.SelectedItem.ToString() == "Line"
                ? SeriesChartType.Line
                : SeriesChartType.Column;

            var seriesMath = new Series("Matematik") { ChartType = chartType, Color = System.Drawing.Color.Blue };
            var seriesScience = new Series("Fen") { ChartType = chartType, Color = System.Drawing.Color.Green };
            var seriesTurkish = new Series("Türkçe") { ChartType = chartType, Color = System.Drawing.Color.Red };
            var seriesHistory = new Series("Tarih") { ChartType = chartType, Color = System.Drawing.Color.Orange };
            var seriesReligion = new Series("Din") { ChartType = chartType, Color = System.Drawing.Color.Purple };
            var seriesEnglish = new Series("İngilizce") { ChartType = chartType, Color = System.Drawing.Color.Gray };

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
                string date = Convert.ToDateTime(reader["Date"]).ToString("dd/MM");

                seriesMath.Points.AddXY(date, Convert.ToDouble(reader["Math"]));
                seriesScience.Points.AddXY(date, Convert.ToDouble(reader["Science"]));
                seriesTurkish.Points.AddXY(date, Convert.ToDouble(reader["Turkish"]));
                seriesHistory.Points.AddXY(date, Convert.ToDouble(reader["History"]));
                seriesReligion.Points.AddXY(date, Convert.ToDouble(reader["Religion"]));
                seriesEnglish.Points.AddXY(date, Convert.ToDouble(reader["English"]));
            }
            connection.Close();
        }

        private void btnGeneratePDF_Click(object sender, EventArgs e)
        {
            GeneratePDFReport();
        }

        private void GeneratePDFReport()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, "StudentReport.pdf");

            Document document = new Document();
            PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
            document.Open();

            document.Add(new Paragraph("Ogrenci Performans Raporu"));
            document.Add(new Paragraph("Sinav Sonuclari:"));
            document.Add(new Paragraph(" "));

            foreach (var series in chart1.Series)
            {
                document.Add(new Paragraph(series.Name + ":"));
                foreach (var point in series.Points)
                {
                    document.Add(new Paragraph(point.AxisLabel + " - " + point.YValues[0]));
                }
                document.Add(new Paragraph(" "));
            }

            document.Close();
            MessageBox.Show("PDF raporu masaüstüne kaydedildi.");
        }
    }
}