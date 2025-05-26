using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Windows.Forms.DataVisualization.Charting;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using LGSTracking.Helpers;

namespace LGSTracking
{
    public partial class ExamsForm : Form
    {
        private MySqlConnection connection;
        private int studentID;

        public ExamsForm(int studentID)
        {
            InitializeComponent();
            this.studentID = studentID;
            connection = new MySqlConnection("Server=localhost;Database=lgstracking;Uid=root;Pwd=1234;");

            comboLesson.SelectedIndexChanged += ComboChanged;
            comboChartType.SelectedIndexChanged += ComboChanged;

            LoadExams();
            LoadChart();
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
            table.Columns.Add("Matematik Net");
            table.Columns.Add("Fen Net");
            table.Columns.Add("Türkçe Net");
            table.Columns.Add("Tarih Net");
            table.Columns.Add("Din Net");
            table.Columns.Add("İngilizce Net");
            table.Columns.Add("Toplam Net");

            while (reader.Read())
            {
                string title = reader["Title"].ToString();
                DateTime date = Convert.ToDateTime(reader["Date"]);

                double mathNet = Net(reader["MathCorrect"], reader["MathWrong"]);
                double scienceNet = Net(reader["ScienceCorrect"], reader["ScienceWrong"]);
                double turkishNet = Net(reader["TurkishCorrect"], reader["TurkishWrong"]);
                double historyNet = Net(reader["HistoryCorrect"], reader["HistoryWrong"]);
                double religionNet = Net(reader["ReligionCorrect"], reader["ReligionWrong"]);
                double englishNet = Net(reader["EnglishCorrect"], reader["EnglishWrong"]);
                double totalNet = mathNet + scienceNet + turkishNet + historyNet + religionNet + englishNet;

                table.Rows.Add(title, date.ToString("dd/MM/yyyy"),
                    mathNet.ToString("0.00"),
                    scienceNet.ToString("0.00"),
                    turkishNet.ToString("0.00"),
                    historyNet.ToString("0.00"),
                    religionNet.ToString("0.00"),
                    englishNet.ToString("0.00"),
                    totalNet.ToString("0.00"));
            }

            connection.Close();
            dgvExams.DataSource = table;
        }

        private double Net(object correctObj, object wrongObj)
        {
            int correct = correctObj == DBNull.Value ? 0 : Convert.ToInt32(correctObj);
            int wrong = wrongObj == DBNull.Value ? 0 : Convert.ToInt32(wrongObj);
            return correct - (wrong / 3.0);
        }

        private void ComboChanged(object sender, EventArgs e)
        {
            LoadChart();
        }


        private void LoadChart()
        {
            if (chart1 == null || dgvExams == null || comboLesson == null || comboChartType == null) return;

            chart1.Series.Clear();
            chart1.Titles.Clear();
            chart1.Legends.Clear();

            chart1.Titles.Add("Ders Bazlı Net Dağılımı");
            chart1.Legends.Add("Dersler");

            // Grafik tipi belirle
            var selectedType = comboChartType.SelectedItem.ToString() == "Line"
                ? SeriesChartType.Line
                : SeriesChartType.Column;

            // Ders adı belirle
            string selectedLesson = comboLesson.SelectedItem.ToString();

            // Series tanımları
            Series seriesMath = new Series("Matematik") { Color = System.Drawing.Color.Blue, ChartType = selectedType };
            Series seriesScience = new Series("Fen") { Color = System.Drawing.Color.Green, ChartType = selectedType };
            Series seriesTurkish = new Series("Türkçe") { Color = System.Drawing.Color.Red, ChartType = selectedType };
            Series seriesHistory = new Series("Tarih") { Color = System.Drawing.Color.Orange, ChartType = selectedType };
            Series seriesReligion = new Series("Din") { Color = System.Drawing.Color.Purple, ChartType = selectedType };
            Series seriesEnglish = new Series("İngilizce") { Color = System.Drawing.Color.Gray, ChartType = selectedType };
            Series seriesTotal = new Series("Toplam Net") { Color = System.Drawing.Color.Black, ChartType = SeriesChartType.Line, BorderDashStyle = ChartDashStyle.Dash };

            foreach (DataGridViewRow row in dgvExams.Rows)
            {
                if (row.IsNewRow) continue;
                string date = row.Cells["Tarih"].Value?.ToString();
                if (string.IsNullOrEmpty(date)) continue;

                double math = Convert.ToDouble(row.Cells["Matematik Net"].Value);
                double science = Convert.ToDouble(row.Cells["Fen Net"].Value);
                double turkish = Convert.ToDouble(row.Cells["Türkçe Net"].Value);
                double history = Convert.ToDouble(row.Cells["Tarih Net"].Value);
                double religion = Convert.ToDouble(row.Cells["Din Net"].Value);
                double english = Convert.ToDouble(row.Cells["İngilizce Net"].Value);
                double total = Convert.ToDouble(row.Cells["Toplam Net"].Value);

                if (selectedLesson == "Tümü" || selectedLesson == "Matematik") seriesMath.Points.AddXY(date, math);
                if (selectedLesson == "Tümü" || selectedLesson == "Fen") seriesScience.Points.AddXY(date, science);
                if (selectedLesson == "Tümü" || selectedLesson == "Türkçe") seriesTurkish.Points.AddXY(date, turkish);
                if (selectedLesson == "Tümü" || selectedLesson == "Tarih") seriesHistory.Points.AddXY(date, history);
                if (selectedLesson == "Tümü" || selectedLesson == "Din") seriesReligion.Points.AddXY(date, religion);
                if (selectedLesson == "Tümü" || selectedLesson == "İngilizce") seriesEnglish.Points.AddXY(date, english);
                if (selectedLesson == "Tümü" || selectedLesson == "Toplam Net") seriesTotal.Points.AddXY(date, total);
            }

            if (selectedLesson == "Tümü" || selectedLesson == "Matematik") chart1.Series.Add(seriesMath);
            if (selectedLesson == "Tümü" || selectedLesson == "Fen") chart1.Series.Add(seriesScience);
            if (selectedLesson == "Tümü" || selectedLesson == "Türkçe") chart1.Series.Add(seriesTurkish);
            if (selectedLesson == "Tümü" || selectedLesson == "Tarih") chart1.Series.Add(seriesHistory);
            if (selectedLesson == "Tümü" || selectedLesson == "Din") chart1.Series.Add(seriesReligion);
            if (selectedLesson == "Tümü" || selectedLesson == "İngilizce") chart1.Series.Add(seriesEnglish);
            if (selectedLesson == "Tümü" || selectedLesson == "Toplam Net") chart1.Series.Add(seriesTotal);
        }

        private void btnAddExam_Click(object sender, EventArgs e)
        {
            AddExamForm addExamForm = new AddExamForm(studentID);
            if (addExamForm.ShowDialog() == DialogResult.OK)
            {
                LoadExams();
                LoadChart();
            }
        }

        private void btnGeneratePDF_Click(object sender, EventArgs e)
        {
            GeneratePDFReport();
        }

        private void comboLesson_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadChart();
        }

        private void comboChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadChart();
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

        private void btnOcrImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Görsel veya PDF (*.jpg;*.png;*.pdf)|*.jpg;*.png;*.pdf";
            if (ofd.ShowDialog() == DialogResult.OK)
            {

                var parsed = OcrOpticalFormReader.ParseFromImage(ofd.FileName);

                if (parsed == null)
                {
                    MessageBox.Show("Optik formdan veri okunamadı.");
                    return;
                }

                string query = @"INSERT INTO Exams (StudentID, Title, Date,
            MathCorrect, MathWrong,
            ScienceCorrect, ScienceWrong,
            TurkishCorrect, TurkishWrong,
            HistoryCorrect, HistoryWrong,
            ReligionCorrect, ReligionWrong,
            EnglishCorrect, EnglishWrong)
            VALUES (@StudentID, @Title, @Date,
                    @MathCorrect, @MathWrong,
                    @ScienceCorrect, @ScienceWrong,
                    @TurkishCorrect, @TurkishWrong,
                    @HistoryCorrect, @HistoryWrong,
                    @ReligionCorrect, @ReligionWrong,
                    @EnglishCorrect, @EnglishWrong)";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@StudentID", studentID);
                    cmd.Parameters.AddWithValue("@Title", parsed.Title);
                    cmd.Parameters.AddWithValue("@Date", parsed.Date);
                    cmd.Parameters.AddWithValue("@MathCorrect", parsed.MathCorrect);
                    cmd.Parameters.AddWithValue("@MathWrong", parsed.MathWrong);
                    cmd.Parameters.AddWithValue("@ScienceCorrect", parsed.ScienceCorrect);
                    cmd.Parameters.AddWithValue("@ScienceWrong", parsed.ScienceWrong);
                    cmd.Parameters.AddWithValue("@TurkishCorrect", parsed.TurkishCorrect);
                    cmd.Parameters.AddWithValue("@TurkishWrong", parsed.TurkishWrong);
                    cmd.Parameters.AddWithValue("@HistoryCorrect", parsed.HistoryCorrect);
                    cmd.Parameters.AddWithValue("@HistoryWrong", parsed.HistoryWrong);
                    cmd.Parameters.AddWithValue("@ReligionCorrect", parsed.ReligionCorrect);
                    cmd.Parameters.AddWithValue("@ReligionWrong", parsed.ReligionWrong);
                    cmd.Parameters.AddWithValue("@EnglishCorrect", parsed.EnglishCorrect);
                    cmd.Parameters.AddWithValue("@EnglishWrong", parsed.EnglishWrong);

                    connection.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Optik formdan sınav başarıyla eklendi.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Veri eklenemedi: " + ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                        LoadExams();
                        LoadChart();
                    }
                }
            }
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

                document.Add(new Paragraph(ReplaceTurkishCharacters($"Başlık: {row.Cells["Başlık"].Value}")));
                document.Add(new Paragraph(ReplaceTurkishCharacters($"Tarih: {row.Cells["Tarih"].Value}")));
                document.Add(new Paragraph(ReplaceTurkishCharacters($"- Matematik Net: {row.Cells["Matematik Net"].Value}")));
                document.Add(new Paragraph(ReplaceTurkishCharacters($"- Fen Net: {row.Cells["Fen Net"].Value}")));
                document.Add(new Paragraph(ReplaceTurkishCharacters($"- Türkçe Net: {row.Cells["Türkçe Net"].Value}")));
                document.Add(new Paragraph(ReplaceTurkishCharacters($"- Tarih Net: {row.Cells["Tarih Net"].Value}")));
                document.Add(new Paragraph(ReplaceTurkishCharacters($"- Din Net: {row.Cells["Din Net"].Value}")));
                document.Add(new Paragraph(ReplaceTurkishCharacters($"- İngilizce Net: {row.Cells["İngilizce Net"].Value}")));
                document.Add(new Paragraph(ReplaceTurkishCharacters($"Toplam Net: {row.Cells["Toplam Net"].Value}")));
                document.Add(new Paragraph(" "));
            }

            document.Close();
            MessageBox.Show("PDF raporu masaüstüne kaydedildi.");
        }


        private void btnImportPdf_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PDF Dosyaları (*.pdf)|*.pdf";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var parsed = PdfExamParser.Parse(ofd.FileName);

                if (parsed is null)
                {
                    MessageBox.Show("Yüklenen PDF formatı tanınamadı. Lütfen geçerli bir sınav raporu yükleyin.");
                    return;
                }

                string query = @"INSERT INTO Exams (StudentID, Title, Date,
            MathCorrect, MathWrong,
            ScienceCorrect, ScienceWrong,
            TurkishCorrect, TurkishWrong,
            HistoryCorrect, HistoryWrong,
            ReligionCorrect, ReligionWrong,
            EnglishCorrect, EnglishWrong)
            VALUES (@StudentID, @Title, @Date,
                    @MathCorrect, @MathWrong,
                    @ScienceCorrect, @ScienceWrong,
                    @TurkishCorrect, @TurkishWrong,
                    @HistoryCorrect, @HistoryWrong,
                    @ReligionCorrect, @ReligionWrong,
                    @EnglishCorrect, @EnglishWrong)";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@StudentID", studentID);
                cmd.Parameters.AddWithValue("@Title", parsed.Title);
                cmd.Parameters.AddWithValue("@Date", parsed.Date);
                cmd.Parameters.AddWithValue("@MathCorrect", parsed.MathCorrect);
                cmd.Parameters.AddWithValue("@MathWrong", parsed.MathWrong);
                cmd.Parameters.AddWithValue("@ScienceCorrect", parsed.ScienceCorrect);
                cmd.Parameters.AddWithValue("@ScienceWrong", parsed.ScienceWrong);
                cmd.Parameters.AddWithValue("@TurkishCorrect", parsed.TurkishCorrect);
                cmd.Parameters.AddWithValue("@TurkishWrong", parsed.TurkishWrong);
                cmd.Parameters.AddWithValue("@HistoryCorrect", parsed.HistoryCorrect);
                cmd.Parameters.AddWithValue("@HistoryWrong", parsed.HistoryWrong);
                cmd.Parameters.AddWithValue("@ReligionCorrect", parsed.ReligionCorrect);
                cmd.Parameters.AddWithValue("@ReligionWrong", parsed.ReligionWrong);
                cmd.Parameters.AddWithValue("@EnglishCorrect", parsed.EnglishCorrect);
                cmd.Parameters.AddWithValue("@EnglishWrong", parsed.EnglishWrong);

                connection.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("PDF'den sınav başarıyla eklendi.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veri ekleme hatası: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }

                LoadExams();
                LoadChart();
            }
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
    }
}