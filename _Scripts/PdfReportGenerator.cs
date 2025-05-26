using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using System.Windows.Forms.DataVisualization.Charting;
using DrawingFont = System.Drawing.Font;
using System.Drawing;
using System.Drawing.Imaging;

namespace LGSTracking.Helpers
{
    public static class StudentReportGenerator
    {


        public static void GenerateBulkReport(int studentID)
        {
            string connectionString = "Server=localhost;Database=lgstracking;Uid=root;Pwd=1234;";
            string studentName = "";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (var studentCmd = new MySqlCommand("SELECT Name, Surname FROM Students WHERE ID = @id", connection))
                {
                    studentCmd.Parameters.AddWithValue("@id", studentID);
                    using (var reader = studentCmd.ExecuteReader())
                    {
                        if (reader.Read())
                            studentName = reader["Name"] + " " + reader["Surname"];
                    }
                }

                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string filePath = Path.Combine(desktopPath, $"{ReplaceTurkishCharacters(studentName.Replace(" ", "_"))}_Tum_Sinavlar_Rapor.pdf");

                Document doc = new Document(PageSize.A4, 50, 50, 25, 25);
                PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
                doc.Open();

                string examQuery = @"SELECT Title FROM Exams WHERE StudentID = @id ORDER BY Date ASC";
                using (var examCmd = new MySqlCommand(examQuery, connection))
                {
                    examCmd.Parameters.AddWithValue("@id", studentID);
                    using (var reader = examCmd.ExecuteReader())
                    {
                        var examTitles = new System.Collections.Generic.List<string>();
                        while (reader.Read())
                            examTitles.Add(reader.GetString(0));

                        foreach (string title in examTitles)
                        {
                            AddSingleExamToDocument(doc, connectionString, studentID, studentName, title);
                            doc.NewPage();
                        }
                    }
                }

                doc.Close();
                MessageBox.Show("Tüm sınavlar PDF olarak masaüstüne kaydedildi.");
            }
        }

        private static void AddSingleExamToDocument(Document doc, string connectionString, int studentID, string studentName, string selectedExamTitle)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = @"SELECT Title, Date,
                        MathCorrect, MathWrong,
                        ScienceCorrect, ScienceWrong,
                        TurkishCorrect, TurkishWrong,
                        HistoryCorrect, HistoryWrong,
                        ReligionCorrect, ReligionWrong,
                        EnglishCorrect, EnglishWrong
                    FROM Exams WHERE StudentID = @id AND Title = @title";

                using (var examCmd = new MySqlCommand(query, connection))
                {
                    examCmd.Parameters.AddWithValue("@id", studentID);
                    examCmd.Parameters.AddWithValue("@title", selectedExamTitle);

                    using (var reader = examCmd.ExecuteReader())
                    {
                        if (!reader.Read()) return;

                        iTextSharp.text.Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
                        iTextSharp.text.Font subFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                        iTextSharp.text.Font normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                        iTextSharp.text.Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.WHITE);

                        DateTime examDate = Convert.ToDateTime(reader["Date"]);

                        doc.Add(new Paragraph(ReplaceTurkishCharacters("Maltepe Üniversitesi"), titleFont));
                        doc.Add(new Paragraph(ReplaceTurkishCharacters("LGS Deneme Sınavı Sonuç Raporu"), subFont));
                        doc.Add(new Paragraph(" "));

                        doc.Add(new Paragraph(ReplaceTurkishCharacters($"Ad Soyad: {studentName}"), normalFont));
                        doc.Add(new Paragraph(ReplaceTurkishCharacters($"Numara: {studentID}"), normalFont));
                        doc.Add(new Paragraph(ReplaceTurkishCharacters($"Sınav Adı: {selectedExamTitle}"), normalFont));
                        doc.Add(new Paragraph(ReplaceTurkishCharacters($"Sınav Tarihi: {examDate:dd.MM.yyyy}"), normalFont));
                        doc.Add(new Paragraph(" "));

                        doc.Add(new Paragraph(ReplaceTurkishCharacters("Ders Analizi"), subFont));
                        doc.Add(new Paragraph(" "));

                        PdfPTable table = new PdfPTable(6);
                        table.WidthPercentage = 100;
                        table.SetWidths(new float[] { 2f, 1f, 1f, 1f, 1f, 1f });

                        string[] headers = { "Ders", "Doğru", "Yanlış", "Boş", "Net", "Toplam" };
                        foreach (var header in headers)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(ReplaceTurkishCharacters(header), headerFont));
                            cell.BackgroundColor = new BaseColor(64, 64, 64);
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.Padding = 5f;
                            table.AddCell(cell);
                        }

                        double netMath = AddLessonRow(table, "Matematik", reader["MathCorrect"], reader["MathWrong"], 20, normalFont);
                        double netScience = AddLessonRow(table, "Fen Bilimleri", reader["ScienceCorrect"], reader["ScienceWrong"], 20, normalFont);
                        double netTurkish = AddLessonRow(table, "Türkçe", reader["TurkishCorrect"], reader["TurkishWrong"], 20, normalFont);
                        double netHistory = AddLessonRow(table, "İnkılap Tarihi", reader["HistoryCorrect"], reader["HistoryWrong"], 10, normalFont);
                        double netReligion = AddLessonRow(table, "Din Kültürü", reader["ReligionCorrect"], reader["ReligionWrong"], 10, normalFont);
                        double netEnglish = AddLessonRow(table, "İngilizce", reader["EnglishCorrect"], reader["EnglishWrong"], 10, normalFont);

                        doc.Add(table);
                        doc.Add(new Paragraph(" "));

                        double totalNet = netMath + netScience + netTurkish + netHistory + netReligion + netEnglish;
                        double score = (totalNet / 90.0) * 500.0;

                        doc.Add(new Paragraph(ReplaceTurkishCharacters($"Toplam Net: {totalNet:0.00}"), normalFont));
                        doc.Add(new Paragraph(ReplaceTurkishCharacters($"LGS Puanı (500 Tam Puan Üzerinden): {score:0.00}"), normalFont));
                        doc.Add(new Paragraph(" "));

                        AddChartToPdf(doc, netMath, netScience, netTurkish, netHistory, netReligion, netEnglish);

                        doc.Add(new Paragraph(" "));
                        doc.Add(new Paragraph(ReplaceTurkishCharacters("Bu belge sistem tarafından otomatik olarak oluşturulmuştur."), FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 8, BaseColor.GRAY)));
                    }
                }
            }
        }




        public static void GenerateReport(int studentID, string selectedExamTitle)
        {
            string connectionString = "Server=localhost;Database=lgstracking;Uid=root;Pwd=1234;";
            string studentName = "";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (var studentCmd = new MySqlCommand("SELECT Name, Surname FROM Students WHERE ID = @id", connection))
                {
                    studentCmd.Parameters.AddWithValue("@id", studentID);
                    using (var reader = studentCmd.ExecuteReader())
                    {
                        if (reader.Read())
                            studentName = reader["Name"] + " " + reader["Surname"];
                    }
                }

                string query = @"SELECT Title, Date,
                        MathCorrect, MathWrong,
                        ScienceCorrect, ScienceWrong,
                        TurkishCorrect, TurkishWrong,
                        HistoryCorrect, HistoryWrong,
                        ReligionCorrect, ReligionWrong,
                        EnglishCorrect, EnglishWrong
                    FROM Exams WHERE StudentID = @id AND Title = @title";

                using (var examCmd = new MySqlCommand(query, connection))
                {
                    examCmd.Parameters.AddWithValue("@id", studentID);
                    examCmd.Parameters.AddWithValue("@title", selectedExamTitle);

                    using (var reader = examCmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            MessageBox.Show("Seçilen sınav bulunamadı.");
                            return;
                        }

                        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        string filePath = Path.Combine(desktopPath, $"{ReplaceTurkishCharacters(studentName.Replace(" ", "_"))}_{ReplaceTurkishCharacters(selectedExamTitle.Replace(" ", "_"))}_Rapor.pdf");

                        Document doc = new Document(PageSize.A4, 50, 50, 25, 25);
                        PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
                        doc.Open();

                        iTextSharp.text.Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
                        iTextSharp.text.Font subFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                        iTextSharp.text.Font normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                        iTextSharp.text.Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.WHITE);

                        DateTime examDate = Convert.ToDateTime(reader["Date"]);

                        doc.Add(new Paragraph(ReplaceTurkishCharacters("Maltepe Üniversitesi"), titleFont));
                        doc.Add(new Paragraph(ReplaceTurkishCharacters("LGS Deneme Sınavı Sonuç Raporu"), subFont));
                        doc.Add(new Paragraph(" "));

                        doc.Add(new Paragraph(ReplaceTurkishCharacters($"Ad Soyad: {studentName}"), normalFont));
                        doc.Add(new Paragraph(ReplaceTurkishCharacters($"Numara: {studentID}"), normalFont));
                        doc.Add(new Paragraph(ReplaceTurkishCharacters($"Sınav Adı: {selectedExamTitle}"), normalFont));
                        doc.Add(new Paragraph(ReplaceTurkishCharacters($"Sınav Tarihi: {examDate:dd.MM.yyyy}"), normalFont));
                        doc.Add(new Paragraph(" "));

                        doc.Add(new Paragraph(ReplaceTurkishCharacters("Ders Analizi"), subFont));
                        doc.Add(new Paragraph(" "));

                        PdfPTable table = new PdfPTable(6);
                        table.WidthPercentage = 100;
                        table.SetWidths(new float[] { 2f, 1f, 1f, 1f, 1f, 1f });

                        string[] headers = { "Ders", "Doğru", "Yanlış", "Boş", "Net", "Toplam" };
                        foreach (var header in headers)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(ReplaceTurkishCharacters(header), headerFont));
                            cell.BackgroundColor = new BaseColor(64, 64, 64);
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.Padding = 5f;
                            table.AddCell(cell);
                        }

                        double netMath = AddLessonRow(table, "Matematik", reader["MathCorrect"], reader["MathWrong"], 20, normalFont);
                        double netScience = AddLessonRow(table, "Fen Bilimleri", reader["ScienceCorrect"], reader["ScienceWrong"], 20, normalFont);
                        double netTurkish = AddLessonRow(table, "Türkçe", reader["TurkishCorrect"], reader["TurkishWrong"], 20, normalFont);
                        double netHistory = AddLessonRow(table, "İnkılap Tarihi", reader["HistoryCorrect"], reader["HistoryWrong"], 10, normalFont);
                        double netReligion = AddLessonRow(table, "Din Kültürü", reader["ReligionCorrect"], reader["ReligionWrong"], 10, normalFont);
                        double netEnglish = AddLessonRow(table, "İngilizce", reader["EnglishCorrect"], reader["EnglishWrong"], 10, normalFont);

                        doc.Add(table);
                        doc.Add(new Paragraph(" "));

                        double totalNet = netMath + netScience + netTurkish + netHistory + netReligion + netEnglish;
                        double score = (totalNet / 90.0) * 500.0;

                        doc.Add(new Paragraph(ReplaceTurkishCharacters($"Toplam Net: {totalNet:0.00}"), normalFont));
                        doc.Add(new Paragraph(ReplaceTurkishCharacters($"LGS Puanı (500 Tam Puan Üzerinden): {score:0.00}"), normalFont));
                        doc.Add(new Paragraph(" "));

                        AddChartToPdf(doc, netMath, netScience, netTurkish, netHistory, netReligion, netEnglish);

                        doc.Add(new Paragraph(" "));
                        doc.Add(new Paragraph(ReplaceTurkishCharacters("Bu belge sistem tarafından otomatik olarak oluşturulmuştur."), FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 8, BaseColor.GRAY)));

                        doc.Close();
                        MessageBox.Show("PDF başarıyla oluşturuldu ve masaüstüne kaydedildi.");
                    }
                }
            }
        }

        private static double AddLessonRow(PdfPTable table, string lesson, object correctObj, object wrongObj, int total, iTextSharp.text.Font font)
        {
            int correct = correctObj == DBNull.Value ? 0 : Convert.ToInt32(correctObj);
            int wrong = wrongObj == DBNull.Value ? 0 : Convert.ToInt32(wrongObj);
            int empty = total - (correct + wrong);
            double net = correct - (wrong / 3.0);

            string[] values = {
                ReplaceTurkishCharacters(lesson),
                correct.ToString(),
                wrong.ToString(),
                empty.ToString(),
                net.ToString("0.00"),
                total.ToString()
            };

            foreach (string val in values)
            {
                PdfPCell cell = new PdfPCell(new Phrase(val, font));
                cell.BackgroundColor = new BaseColor(245, 245, 245);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Padding = 4f;
                table.AddCell(cell);
            }

            return net;
        }

        private static void AddChartToPdf(Document doc, double math, double science, double turkish, double history, double religion, double english)
        {
            var chart = new Chart { Width = 600, Height = 400 };
            chart.ChartAreas.Add(new ChartArea());

            var series = new Series("Net")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true,
                LabelFormat = "0.00"
            };

            series.Points.AddXY("Matematik", (float)math);
            series.Points.AddXY("Fen", (float)science);
            series.Points.AddXY("Türkçe", (float)turkish);
            series.Points.AddXY("Tarih", (float)history);
            series.Points.AddXY("Din", (float)religion);
            series.Points.AddXY("İngilizce", (float)english);

            Color[] colors = { Color.Blue, Color.Green, Color.Red, Color.Orange, Color.Purple, Color.Gray };
            for (int i = 0; i < series.Points.Count; i++)
                series.Points[i].Color = colors[i];

            chart.Series.Add(series);

            using (var ms = new MemoryStream())
            {
                chart.SaveImage(ms, ChartImageFormat.Png);
                ms.Position = 0;
                iTextSharp.text.Image chartImage = iTextSharp.text.Image.GetInstance(ms);
                chartImage.Alignment = Element.ALIGN_CENTER;
                chartImage.ScaleToFit(450f, 300f);
                doc.Add(new Paragraph(ReplaceTurkishCharacters("Ders Bazlı Net Dağılım Grafiği"), FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
                doc.Add(chartImage);
            }
        }

        private static string ReplaceTurkishCharacters(string text)
        {
            return text
                .Replace("İ", "I").Replace("ı", "i")
                .Replace("Ö", "O").Replace("ö", "o")
                .Replace("Ü", "U").Replace("ü", "u")
                .Replace("Ç", "C").Replace("ç", "c")
                .Replace("Ğ", "G").Replace("ğ", "g")
                .Replace("Ş", "S").Replace("ş", "s");
        }
    }
}