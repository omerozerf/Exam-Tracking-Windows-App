namespace LGSTracking
{
    partial class StudentChartForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button btnGeneratePDF;
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
            this.btnGeneratePDF = new System.Windows.Forms.Button();
            this.comboChartType = new System.Windows.Forms.ComboBox();

            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();

            // comboChartType
            this.comboChartType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboChartType.Items.AddRange(new object[] { "Line", "Column" });
            this.comboChartType.SelectedIndex = 0;
            this.comboChartType.Location = new System.Drawing.Point(20, 15);
            this.comboChartType.Size = new System.Drawing.Size(150, 28);
            this.comboChartType.SelectedIndexChanged += new System.EventHandler(this.comboChartType_SelectedIndexChanged);

            // btnGeneratePDF
            this.btnGeneratePDF.Text = "PDF Raporu Al";
            this.btnGeneratePDF.Location = new System.Drawing.Point(180, 15);
            this.btnGeneratePDF.Size = new System.Drawing.Size(150, 28);
            this.btnGeneratePDF.Click += new System.EventHandler(this.btnGeneratePDF_Click);

            // chart1
            this.chart1.Location = new System.Drawing.Point(20, 60);
            this.chart1.Size = new System.Drawing.Size(740, 480);
            this.chart1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom |
                                  System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.chart1.ChartAreas.Add(new System.Windows.Forms.DataVisualization.Charting.ChartArea("MainArea"));

            // StudentChartForm
            this.Controls.Add(this.comboChartType);
            this.Controls.Add(this.btnGeneratePDF);
            this.Controls.Add(this.chart1);
            this.Text = "Öğrenci Performans Grafiği";
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.StudentChartForm_Load);

            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
        }
    }
}