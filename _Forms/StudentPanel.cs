using System;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MySql.Data.MySqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using LGSTracking.Helpers;

namespace LGSTracking
{
    public partial class StudentPanel : Form
    {
        private MySqlConnection connection;
        private int studentID;

        public StudentPanel(int studentID)
        {
            InitializeComponent();
            this.studentID = studentID;
            connection = new MySqlConnection("Server=localhost;Database=lgstracking;Uid=root;Pwd=1234;");
            LoadExams();
            LoadStudentChart();
        }



        private void LoadExams()
        {
            string query = @"
        SELECT 
            Title, Date,
            MathCorrect, MathWrong,
            ScienceCorrect, ScienceWrong,
            TurkishCorrect, TurkishWrong,
            HistoryCorrect, HistoryWrong,
            ReligionCorrect, ReligionWrong,
            EnglishCorrect, EnglishWrong
        FROM Exams
        WHERE StudentID = @studentID";

            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@studentID", studentID);

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();

            DataTable table = new DataTable();
            table.Columns.Add("Başlık");
            table.Columns.Add("Tarih");
            table.Columns.Add("Matematik");
            table.Columns.Add("Fen");
            table.Columns.Add("Türkçe");
            table.Columns.Add("Tarih Dersi");
            table.Columns.Add("Din");
            table.Columns.Add("İngilizce");
            table.Columns.Add("Toplam Net");

            while (reader.Read())
            {
                string title = reader["Title"]?.ToString() ?? "-";
                string date = Convert.ToDateTime(reader["Date"]).ToString("dd/MM/yyyy");

                double math = Net(reader["MathCorrect"], reader["MathWrong"]);
                double science = Net(reader["ScienceCorrect"], reader["ScienceWrong"]);
                double turkish = Net(reader["TurkishCorrect"], reader["TurkishWrong"]);
                double history = Net(reader["HistoryCorrect"], reader["HistoryWrong"]);
                double religion = Net(reader["ReligionCorrect"], reader["ReligionWrong"]);
                double english = Net(reader["EnglishCorrect"], reader["EnglishWrong"]);
                double totalNet = math + science + turkish + history + religion + english;

                table.Rows.Add(
                    title, date,
                    math.ToString("0.00"),
                    science.ToString("0.00"),
                    turkish.ToString("0.00"),
                    history.ToString("0.00"),
                    religion.ToString("0.00"),
                    english.ToString("0.00"),
                    totalNet.ToString("0.00")
                );
            }

            connection.Close();
            dgvExams.DataSource = table;
        }

        private string GetStudentFullName()
        {
            string fullName = "Bilinmiyor";
            string query = "SELECT Name, Surname FROM Students WHERE ID = @studentID";

            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@studentID", studentID);
                connection.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        fullName = reader["Name"] + " " + reader["Surname"];
                    }
                }
                connection.Close();
            }

            return fullName;
        }

        private double Net(object correctObj, object wrongObj)
        {
            int correct = correctObj == DBNull.Value ? 0 : Convert.ToInt32(correctObj);
            int wrong = wrongObj == DBNull.Value ? 0 : Convert.ToInt32(wrongObj);
            return correct - (wrong / 3.0);
        }

        private void LoadStudentChart()
        {
            if (chart1 == null || dgvExams == null) return;

            chart1.Series.Clear();
            chart1.Titles.Clear();
            chart1.Legends.Clear();

            chart1.Titles.Add("Öğrenci Performans Grafiği");
            chart1.Legends.Add("Dersler");

            var selectedChartType = comboChartType.SelectedItem.ToString() == "Line"
                ? SeriesChartType.Line
                : SeriesChartType.Column;

            var dersler = new (string name, string column, System.Drawing.Color color)[]
            {
        ("Matematik", "Matematik", System.Drawing.Color.Blue),
        ("Fen", "Fen", System.Drawing.Color.Green),
        ("Türkçe", "Türkçe", System.Drawing.Color.Red),
        ("Tarih", "Tarih Dersi", System.Drawing.Color.Orange),
        ("Din", "Din", System.Drawing.Color.Purple),
        ("İngilizce", "İngilizce", System.Drawing.Color.Gray),
        ("Toplam Net", "Toplam Net", System.Drawing.Color.Black)
            };

            foreach (var (name, column, color) in dersler)
            {
                if (comboLesson.SelectedItem.ToString() != "Tümü" && comboLesson.SelectedItem.ToString() != name)
                    continue;

                var chartType = column == "Toplam Net" ? SeriesChartType.Line : selectedChartType;
                Series series = new Series(name) { Color = color, ChartType = chartType, BorderDashStyle = column == "Toplam Net" ? ChartDashStyle.Dash : ChartDashStyle.Solid };

                foreach (DataGridViewRow row in dgvExams.Rows)
                {
                    if (row.IsNewRow) continue;
                    string dateStr = row.Cells["Tarih"].Value?.ToString();
                    if (!DateTime.TryParseExact(dateStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dt)) continue;
                    string label = dt.ToString("dd/MM");

                    double value = SafeToDouble(row.Cells[column].Value);
                    series.Points.AddXY(label, value);
                }

                chart1.Series.Add(series);
            }
        }

        private void btnAddExam_Click(object sender, EventArgs e)
        {
            AddExamForm addExamForm = new AddExamForm(studentID);
            if (addExamForm.ShowDialog() == DialogResult.OK)
            {
                LoadExams();
                LoadStudentChart();
            }
        }

        private void btnGeneratePDF_Click(object sender, EventArgs e)
        {
            GeneratePDFReport();
        }

        private void GeneratePDFReport()
        {
            if (dgvExams.SelectedRows.Count > 0)
            {
                string selectedTitle = dgvExams.SelectedRows[0].Cells["Başlık"].Value?.ToString();
                if (!string.IsNullOrEmpty(selectedTitle))
                {
                    StudentReportGenerator.GenerateReport(studentID, selectedTitle);
                }
                else
                {
                    StudentReportGenerator.GenerateBulkReport(studentID);
                }
            }
            else
            {
                StudentReportGenerator.GenerateBulkReport(studentID);
            }

            return;

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, "ExamReport.pdf");

            Document document = new Document();
            PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
            document.Open();

            document.Add(new Paragraph("Ogrenci Performans Raporu"));
            document.Add(new Paragraph("Sinav Sonuclari:"));
            document.Add(new Paragraph(" "));

            foreach (DataGridViewRow row in dgvExams.Rows)
            {
                if (row.IsNewRow) continue;

                document.Add(new Paragraph(ReplaceTurkishCharacters($"Tarih: {row.Cells["Tarih"].Value}")));
                document.Add(new Paragraph(ReplaceTurkishCharacters($"- Matematik: {row.Cells["Math"].Value}")));
                document.Add(new Paragraph(ReplaceTurkishCharacters($"- Fen: {row.Cells["Science"].Value}")));
                document.Add(new Paragraph(ReplaceTurkishCharacters($"- Turkce: {row.Cells["Turkish"].Value}")));
                document.Add(new Paragraph(ReplaceTurkishCharacters($"- Tarih: {row.Cells["History"].Value}")));
                document.Add(new Paragraph(ReplaceTurkishCharacters($"- Din: {row.Cells["Religion"].Value}")));
                document.Add(new Paragraph(ReplaceTurkishCharacters($"- Ingilizce: {row.Cells["English"].Value}")));
                document.Add(new Paragraph(ReplaceTurkishCharacters($"Toplam Net: {row.Cells["Toplam Net"].Value}")));
                document.Add(new Paragraph(" "));
            }

            document.Close();
            MessageBox.Show("PDF raporu masaüstüne kaydedildi.");
        }

        private double SafeToDouble(object value)
        {
            if (value == DBNull.Value || value == null)
                return 0;

            if (value is string str)
                return double.TryParse(str, out double d) ? d : 0;

            return Convert.ToDouble(value);
        }

        private string ReplaceTurkishCharacters(string text)
        {
            return text
                .Replace("İ", "I")
                .Replace("ı", "i")
                .Replace("Ö", "O")
                .Replace("ö", "o")
                .Replace("Ü", "U")
                .Replace("ü", "u")
                .Replace("Ç", "C")
                .Replace("ç", "c")
                .Replace("Ğ", "G")
                .Replace("ğ", "g")
                .Replace("Ş", "S")
                .Replace("ş", "s");
        }

        private void ComboChanged(object sender, EventArgs e)
        {
            LoadStudentChart();
        }
    }
}