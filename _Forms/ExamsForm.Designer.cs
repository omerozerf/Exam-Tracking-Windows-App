using System.Drawing;
using System.Windows.Forms;

namespace LGSTracking
{
    partial class ExamsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvExams;
        private System.Windows.Forms.Button btnAddExam;
        private System.Windows.Forms.Button btnGeneratePDF;
        private System.Windows.Forms.Panel panelChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.ComboBox comboLesson;
        private System.Windows.Forms.ComboBox comboChartType;
        private Button btnImportPdf;
        private Button btnOcrImport;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvExams = new System.Windows.Forms.DataGridView();
            this.btnAddExam = new System.Windows.Forms.Button();
            this.btnGeneratePDF = new System.Windows.Forms.Button();
            this.panelChart = new System.Windows.Forms.Panel();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.comboLesson = new System.Windows.Forms.ComboBox();
            this.comboChartType = new System.Windows.Forms.ComboBox();

            ((System.ComponentModel.ISupportInitialize)(this.dgvExams)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panelChart.SuspendLayout();
            this.SuspendLayout();

            // dgvExams
            this.dgvExams.BackgroundColor = System.Drawing.Color.FromArgb(244, 246, 247);
            this.dgvExams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExams.Location = new System.Drawing.Point(20, 20);
            this.dgvExams.Name = "dgvExams";
            this.dgvExams.Size = new System.Drawing.Size(850, 200);
            this.dgvExams.TabIndex = 0;
            this.dgvExams.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvExams.ScrollBars = System.Windows.Forms.ScrollBars.Both;


            // btnAddExam
            this.btnAddExam.Text = "Sınav Ekle";
            this.btnAddExam.Location = new Point(20, 230);
            this.btnAddExam.Size = new System.Drawing.Size(150, 40);
            this.btnAddExam.Click += new System.EventHandler(this.btnAddExam_Click);

            // btnGeneratePDF
            this.btnGeneratePDF.Text = "PDF Raporu Al";
            this.btnGeneratePDF.Location = new Point(180, 230);
            this.btnGeneratePDF.Size = new System.Drawing.Size(150, 40);
            this.btnGeneratePDF.Click += new System.EventHandler(this.btnGeneratePDF_Click);

            // comboChartType
            this.comboChartType = new System.Windows.Forms.ComboBox();
            this.comboChartType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboChartType.Items.AddRange(new object[] { "Line", "Column" });
            this.comboChartType.SelectedIndex = 0;
            this.comboChartType.Location = new Point(340, 230);
            this.comboChartType.Size = new System.Drawing.Size(100, 40);
            this.comboChartType.SelectedIndexChanged += new System.EventHandler(this.ComboChanged);

            // comboLesson
            this.comboLesson = new System.Windows.Forms.ComboBox();
            this.comboLesson.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboLesson.Items.AddRange(new object[] {
    "Tümü", "Matematik", "Fen", "Türkçe", "Tarih", "Din", "İngilizce", "Toplam Net"
});
            this.comboLesson.SelectedIndex = 0;
            this.comboLesson.Location = new Point(450, 230);
            this.comboLesson.Size = new Size(100, 40);
            this.comboLesson.SelectedIndexChanged += new System.EventHandler(this.ComboChanged);

            // btnImportPdf
            this.btnImportPdf = new System.Windows.Forms.Button();
            this.btnImportPdf.Text = "PDF'den Ekle";
            this.btnImportPdf.Location = new Point(600, 230);
            this.btnImportPdf.Size = new Size(120, 40);
            this.btnImportPdf.Click += new System.EventHandler(this.btnImportPdf_Click);

            // btnOcrImport
            this.btnOcrImport = new System.Windows.Forms.Button();
            this.btnOcrImport.Text = "OCR ile Ekle";
            this.btnOcrImport.Location = new Point(730, 230);
            this.btnOcrImport.Size = new Size(140, 40);
            this.btnOcrImport.Click += new System.EventHandler(this.btnOcrImport_Click);


            // panelChart
            this.panelChart.Location = new System.Drawing.Point(20, 280);
            this.panelChart.Size = new Size(860, 300);
            this.panelChart.Controls.Add(this.chart1);

            // chart1
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart1.ChartAreas.Add(new System.Windows.Forms.DataVisualization.Charting.ChartArea("MainArea"));
            this.chart1.Legends.Add(new System.Windows.Forms.DataVisualization.Charting.Legend("MainLegend"));
            this.chart1.Size = new System.Drawing.Size(1500, 300);
            this.chart1.Series.Clear();

            // ExamsForm
            this.ClientSize = new Size(900, 600);
            this.Controls.Add(this.dgvExams);
            this.Controls.Add(this.btnAddExam);
            this.Controls.Add(this.btnGeneratePDF);
            this.Controls.Add(this.comboLesson);
            this.Controls.Add(this.comboChartType);
            this.Controls.Add(this.btnImportPdf);     
            this.Controls.Add(this.btnOcrImport); 
            this.Controls.Add(this.panelChart);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sınavlar";

            ((System.ComponentModel.ISupportInitialize)(this.dgvExams)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.panelChart.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}