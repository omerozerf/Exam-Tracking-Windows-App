using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace LGSTracking.Helpers
{
    public class ParsedExamData
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public int MathCorrect, MathWrong;
        public int ScienceCorrect, ScienceWrong;
        public int TurkishCorrect, TurkishWrong;
        public int HistoryCorrect, HistoryWrong;
        public int ReligionCorrect, ReligionWrong;
        public int EnglishCorrect, EnglishWrong;
    }

    public static class PdfExamParser
    {
        public static ParsedExamData Parse(string pdfPath)
        {
            string text = ExtractTextFromPdf(pdfPath);

            if (text.Contains("KAFADENGİ") || text.Contains("MARMARA EĞİTİM KURUMLARI"))
            {
                return ParseKafaDengi(text);
            }
            else if (text.Contains("Maltepe Universitesi") || text.Contains("X Sinavi"))
            {
                return ParseMaltepeFormat(text);
            }
            else if (text.Contains("ANKARA") || text.Contains("ÖZEL DERSEVIM") || text.Contains("SONUÇ BELGESİ"))
            {
                return ParseDersevimFormat(text);
            }
            else
            {
                return null;

            }
        }

        private static ParsedExamData ParseKafaDengi(string text)
        {
            var data = new ParsedExamData();

            data.Title = Extract(text, @"Sınav Adı\s+(.+)");
            data.Date = DateTime.ParseExact(Extract(text, @"Sınav Tarihi\s+(\d{2}\.\d{2}\.\d{4})"), "dd.MM.yyyy", null);

            data.TurkishCorrect = GetInt(text, @"LGS-TÜRKÇE\s+20\s+(\d+)");
            data.TurkishWrong = GetInt(text, @"LGS-TÜRKÇE\s+20\s+\d+\s+(\d+)");

            data.HistoryCorrect = GetInt(text, @"LGS-İNKILAP TARİHİ\s+10\s+(\d+)");
            data.HistoryWrong = GetInt(text, @"LGS-İNKILAP TARİHİ\s+10\s+\d+\s+(\d+)");

            data.ReligionCorrect = GetInt(text, @"LGS-D[İI]N K[ÜU]LT[ÜU]R[ÜU].*?\s+10\s+(\d+)");
            data.ReligionWrong = GetInt(text, @"LGS-D[İI]N K[ÜU]LT[ÜU]R[ÜU].*?\s+10\s+\d+\s+(\d+)");

            data.EnglishCorrect = GetInt(text, @"LGS-İNGİLİZCE\s+10\s+(\d+)");
            data.EnglishWrong = GetInt(text, @"LGS-İNGİLİZCE\s+10\s+\d+\s+(\d+)");

            data.MathCorrect = GetInt(text, @"LGS-MATEMATİK\s+20\s+(\d+)");
            data.MathWrong = GetInt(text, @"LGS-MATEMATİK\s+20\s+\d+\s+(\d+)");

            data.ScienceCorrect = GetInt(text, @"LGS-FEN BİLİMLERİ\s+20\s+(\d+)");
            data.ScienceWrong = GetInt(text, @"LGS-FEN BİLİMLERİ\s+20\s+\d+\s+(\d+)");

            return data;
        }

        private static ParsedExamData ParseMaltepeFormat(string text)
        {
            var data = new ParsedExamData();

            data.Title = Extract(text, @"Sinav Adi:\s*(.+)");
            data.Date = DateTime.ParseExact(Extract(text, @"Sinav Tarihi:\s*(\d{2}\.\d{2}\.\d{4})"), "dd.MM.yyyy", null);

            data.MathCorrect = GetInt(text, @"Matematik\s+(\d+)\s+(\d+)");
            data.MathWrong = GetInt(text, @"Matematik\s+\d+\s+(\d+)");

            data.ScienceCorrect = GetInt(text, @"Fen Bilimleri\s+(\d+)\s+(\d+)");
            data.ScienceWrong = GetInt(text, @"Fen Bilimleri\s+\d+\s+(\d+)");

            data.TurkishCorrect = GetInt(text, @"Turkce\s+(\d+)\s+(\d+)");
            data.TurkishWrong = GetInt(text, @"Turkce\s+\d+\s+(\d+)");

            data.HistoryCorrect = GetInt(text, @"Inkilap Tarihi\s+(\d+)\s+(\d+)");
            data.HistoryWrong = GetInt(text, @"Inkilap Tarihi\s+\d+\s+(\d+)");

            data.ReligionCorrect = GetInt(text, @"Din Kulturu\s+(\d+)\s+(\d+)");
            data.ReligionWrong = GetInt(text, @"Din Kulturu\s+\d+\s+(\d+)");

            data.EnglishCorrect = GetInt(text, @"Ingilizce\s+(\d+)\s+(\d+)");
            data.EnglishWrong = GetInt(text, @"Ingilizce\s+\d+\s+(\d+)");

            return data;
        }

        private static ParsedExamData ParseDersevimFormat(string text)
        {
            var data = new ParsedExamData();

            data.Title = Extract(text, @"ÖZEL DERSEVİM.*KURSU");

            // Tarih görünmüyor, DateTime.MinValue veriyoruz
            data.Date = DateTime.MinValue;

            data.TurkishCorrect = GetInt(text, @"Türkçe\s+20\s+(\d+)");
            data.TurkishWrong = GetInt(text, @"Türkçe\s+20\s+\d+\s+(\d+)");

            data.HistoryCorrect = GetInt(text, @"Tarih\s+10\s+(\d+)");
            data.HistoryWrong = GetInt(text, @"Tarih\s+10\s+\d+\s+(\d+)");

            data.ReligionCorrect = GetInt(text, @"Din\s+K\.v\.e\s+A\.B\.\s+10\s+(\d+)");
            data.ReligionWrong = GetInt(text, @"Din\s+K\.v\.e\s+A\.B\.\s+10\s+\d+\s+(\d+)");

            data.EnglishCorrect = GetInt(text, @"İngilizce\s+10\s+(\d+)");
            data.EnglishWrong = GetInt(text, @"İngilizce\s+10\s+\d+\s+(\d+)");

            data.MathCorrect = GetInt(text, @"Matematik\s+20\s+(\d+)");
            data.MathWrong = GetInt(text, @"Matematik\s+20\s+\d+\s+(\d+)");

            data.ScienceCorrect = GetInt(text, @"Fen\s+20\s+(\d+)");
            data.ScienceWrong = GetInt(text, @"Fen\s+20\s+\d+\s+(\d+)");

            return data;
        }

        private static string Extract(string text, string pattern)
        {
            var match = Regex.Match(text, pattern, RegexOptions.IgnoreCase);
            return match.Success ? match.Groups[1].Value.Trim() : "-";
        }

        private static int GetInt(string text, string pattern)
        {
            var match = Regex.Match(text, pattern, RegexOptions.IgnoreCase);
            if (match.Success)
            {
                string value = match.Groups[1].Value.Replace(",", ".");
                if (double.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double num))
                    return (int)Math.Round(num);
            }
            return 0;
        }

        private static string ExtractTextFromPdf(string path)
        {
            using (PdfReader reader = new PdfReader(path))
            {
                string text = "";
                for (int i = 1; i <= reader.NumberOfPages; i++)
                    text += PdfTextExtractor.GetTextFromPage(reader, i);
                return text;
            }
        }
    }
}