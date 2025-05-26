using System;
using System.Drawing;
using System.IO;
using Tesseract;
using LGSTracking.Helpers;
using Org.BouncyCastle.Asn1.Pkcs;
using System.Windows.Forms;

namespace LGSTracking.Helpers
{
    public static class OcrOpticalFormReader
    {

        public static ParsedExamData ParseFromImage(string imagePath)
        {
            if (!File.Exists(imagePath))
            {
                MessageBox.Show("Dosya bulunamadı.");
                return null;
            }

            try
            {
                using (var engine = new TesseractEngine(@"./tessdata", "tur", EngineMode.Default))
                using (var img = Pix.LoadFromFile(imagePath))
                using (var page = engine.Process(img))
                {
                    string text = page.GetText();
                    if (string.IsNullOrWhiteSpace(text))
                    {
                        MessageBox.Show("Tanımlanamayan optik veya boş optik.");
                        return null;
                    }

                    var parsed = new ParsedExamData();
                    parsed.Title = Extract(text, @"SINAV ADI[\s:.]+(.+)");
                    parsed.Date = DateTime.Now;

                    parsed.TurkishCorrect = CountFilled(text, "TÜRKÇE", 20);
                    parsed.HistoryCorrect = CountFilled(text, "TARİHİ", 10);
                    parsed.ReligionCorrect = CountFilled(text, "DİN KÜLTÜRÜ", 10);
                    parsed.EnglishCorrect = CountFilled(text, "İNGİLİZCE", 10);
                    parsed.MathCorrect = CountFilled(text, "MATEMATİK", 20);
                    parsed.ScienceCorrect = CountFilled(text, "FEN BİLİMLERİ", 20);

                    parsed.TurkishWrong = 0;
                    parsed.HistoryWrong = 0;
                    parsed.ReligionWrong = 0;
                    parsed.EnglishWrong = 0;
                    parsed.MathWrong = 0;
                    parsed.ScienceWrong = 0;

                    return parsed;
                }
            }
           
            catch (Exception)
            {
                MessageBox.Show("Tanımlanamayan optik veya boş optik.");
                return null;
            }
        }

        private static int CountFilled(string text, string section, int questionCount)
        {
            int count = 0;
            for (int i = 1; i <= questionCount; i++)
            {
                string pattern = $@"{section}[\r\n]+.*?{i}\s+[A-E]";
                if (System.Text.RegularExpressions.Regex.IsMatch(text, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                    count++;
            }
            return count;
        }

        private static string Extract(string text, string pattern)
        {
            var match = System.Text.RegularExpressions.Regex.Match(text, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            return match.Success ? match.Groups[1].Value.Trim() : "-";
        }
    }
}
