using System.Windows.Forms;

namespace LGSTracking
{
    partial class StudentPanel
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button btnGeneratePDF;
        private System.Windows.Forms.Button btnAddExam;
        private System.Windows.Forms.DataGridView dgvExams;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelChart;
        private System.Windows.Forms.ComboBox comboLesson;
        private System.Windows.Forms.ComboBox comboChartType;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgvExams = new System.Windows.Forms.DataGridView();
            this.btnGeneratePDF = new System.Windows.Forms.Button();
            this.btnAddExam = new System.Windows.Forms.Button();
            this.panelChart = new System.Windows.Forms.Panel();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();

            ((System.ComponentModel.ISupportInitialize)(this.dgvExams)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.panelChart.SuspendLayout();
            this.SuspendLayout();

            // panelHeader
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(46, 134, 193);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(800, 60);
            this.panelHeader.TabIndex = 0;

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(300, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(174, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Öğrenci Paneli";

            // dgvExams
            this.dgvExams.BackgroundColor = System.Drawing.Color.White;
            this.dgvExams.Location = new System.Drawing.Point(20, 80);
            this.dgvExams.Size = new System.Drawing.Size(760, 200);
            this.dgvExams.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvExams.TabIndex = 1;
            this.dgvExams.ScrollBars = ScrollBars.Both;


            // panelChart
            this.panelChart.AutoScroll = true;
            this.panelChart.Location = new System.Drawing.Point(20, 340);
            this.panelChart.Size = new System.Drawing.Size(760, 300);
            this.panelChart.Controls.Add(this.chart1);

            // btnAddExam
            this.btnAddExam.Text = "Sınav Ekle";
            this.btnAddExam.Size = new System.Drawing.Size(150, 40);
            this.btnAddExam.Location = new System.Drawing.Point(20, 290);
            this.btnAddExam.Click += new System.EventHandler(this.btnAddExam_Click);

            // btnGeneratePDF
            this.btnGeneratePDF.Text = "PDF Raporu Al";
            this.btnGeneratePDF.Size = new System.Drawing.Size(150, 40);
            this.btnGeneratePDF.Location = new System.Drawing.Point(180, 290);
            this.btnGeneratePDF.Click += new System.EventHandler(this.btnGeneratePDF_Click);

            // comboChartType
            this.comboChartType = new System.Windows.Forms.ComboBox();
            this.comboChartType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboChartType.Items.AddRange(new object[] { "Line", "Column" });
            this.comboChartType.SelectedIndex = 0;
            this.comboChartType.Size = new System.Drawing.Size(120, 40);
            this.comboChartType.Location = new System.Drawing.Point(340, 290); // sağa alındı
            this.comboChartType.SelectedIndexChanged += new System.EventHandler(this.ComboChanged);

            // comboLesson
            this.comboLesson = new System.Windows.Forms.ComboBox();
            this.comboLesson.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboLesson.Items.AddRange(new object[] {
    "Tümü", "Matematik", "Fen", "Türkçe", "Tarih", "Din", "İngilizce", "Toplam Net"
});
            this.comboLesson.SelectedIndex = 0;
            this.comboLesson.Size = new System.Drawing.Size(150, 40);
            this.comboLesson.Location = new System.Drawing.Point(470, 290); // onun da sağa alınması
            this.comboLesson.SelectedIndexChanged += new System.EventHandler(this.ComboChanged);

            // Controls.Add(...)
            this.Controls.Add(this.comboChartType);
            this.Controls.Add(this.comboLesson);

            // chart1
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart1.ChartAreas.Add(new System.Windows.Forms.DataVisualization.Charting.ChartArea("MainArea"));
            this.chart1.Legends.Add(new System.Windows.Forms.DataVisualization.Charting.Legend("MainLegend"));
            this.chart1.Size = new System.Drawing.Size(1500, 300); // Büyük boyutlu grafik
            this.chart1.Series.Clear();

            // StudentPanel
            this.ClientSize = new System.Drawing.Size(800, 700);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.dgvExams);
            this.Controls.Add(this.btnAddExam);
            this.Controls.Add(this.btnGeneratePDF);
            this.Controls.Add(this.panelChart);
            this.Text = "Öğrenci Paneli";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            ((System.ComponentModel.ISupportInitialize)(this.dgvExams)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelChart.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}