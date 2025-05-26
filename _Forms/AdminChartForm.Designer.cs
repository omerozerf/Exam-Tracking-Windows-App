namespace LGSTracking
{
    partial class AdminChartForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.ComboBox comboBoxStudents;
        private System.Windows.Forms.Button btnShowChart;
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
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.comboBoxStudents = new System.Windows.Forms.ComboBox();
            this.btnShowChart = new System.Windows.Forms.Button();
            this.comboChartType = new System.Windows.Forms.ComboBox();

            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();

            // comboBoxStudents
            this.comboBoxStudents.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStudents.Location = new System.Drawing.Point(20, 20);
            this.comboBoxStudents.Size = new System.Drawing.Size(250, 28);

            // comboChartType
            this.comboChartType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboChartType.Items.AddRange(new object[] { "Line", "Column" });
            this.comboChartType.SelectedIndex = 0;
            this.comboChartType.Location = new System.Drawing.Point(280, 20);
            this.comboChartType.Size = new System.Drawing.Size(100, 28);
            this.comboChartType.SelectedIndexChanged += new System.EventHandler(this.comboChartType_SelectedIndexChanged);

            // btnShowChart
            this.btnShowChart.Text = "Grafik Göster";
            this.btnShowChart.Location = new System.Drawing.Point(390, 20);
            this.btnShowChart.Size = new System.Drawing.Size(120, 28);
            this.btnShowChart.Click += new System.EventHandler(this.btnShowChart_Click);

            // chart1
            this.chart1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.chart1.Height = 500;
            this.chart1.ChartAreas.Add(new System.Windows.Forms.DataVisualization.Charting.ChartArea("MainArea"));
            this.chart1.Legends.Add(new System.Windows.Forms.DataVisualization.Charting.Legend("Dersler"));

            // AdminChartForm
            this.Controls.Add(this.comboBoxStudents);
            this.Controls.Add(this.comboChartType);
            this.Controls.Add(this.btnShowChart);
            this.Controls.Add(this.chart1);
            this.Text = "Öğrenci Performans Grafiği (Admin)";
            this.Size = new System.Drawing.Size(800, 600);

            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
        }
    }
}