namespace LGSTracking
{
    partial class AddExamForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblTitleText;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.DateTimePicker dtpExamDate;

        private System.Windows.Forms.Label lblMathCorrect;
        private System.Windows.Forms.Label lblMathWrong;
        private System.Windows.Forms.TextBox txtMathCorrect;
        private System.Windows.Forms.TextBox txtMathWrong;

        private System.Windows.Forms.Label lblScienceCorrect;
        private System.Windows.Forms.Label lblScienceWrong;
        private System.Windows.Forms.TextBox txtScienceCorrect;
        private System.Windows.Forms.TextBox txtScienceWrong;

        private System.Windows.Forms.Label lblTurkishCorrect;
        private System.Windows.Forms.Label lblTurkishWrong;
        private System.Windows.Forms.TextBox txtTurkishCorrect;
        private System.Windows.Forms.TextBox txtTurkishWrong;

        private System.Windows.Forms.Label lblHistoryCorrect;
        private System.Windows.Forms.Label lblHistoryWrong;
        private System.Windows.Forms.TextBox txtHistoryCorrect;
        private System.Windows.Forms.TextBox txtHistoryWrong;

        private System.Windows.Forms.Label lblReligionCorrect;
        private System.Windows.Forms.Label lblReligionWrong;
        private System.Windows.Forms.TextBox txtReligionCorrect;
        private System.Windows.Forms.TextBox txtReligionWrong;

        private System.Windows.Forms.Label lblEnglishCorrect;
        private System.Windows.Forms.Label lblEnglishWrong;
        private System.Windows.Forms.TextBox txtEnglishCorrect;
        private System.Windows.Forms.TextBox txtEnglishWrong;

        private System.Windows.Forms.Button btnSave;

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
            this.lblTitleText = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.dtpExamDate = new System.Windows.Forms.DateTimePicker();

            this.lblMathCorrect = CreateLabel("Matematik Doğru", 30, 120);
            this.txtMathCorrect = CreateTextBox(200, 120);
            this.lblMathWrong = CreateLabel("Matematik Yanlış", 410, 120);
            this.txtMathWrong = CreateTextBox(580, 120);

            this.lblScienceCorrect = CreateLabel("Fen Doğru", 30, 160);
            this.txtScienceCorrect = CreateTextBox(200, 160);
            this.lblScienceWrong = CreateLabel("Fen Yanlış", 410, 160);
            this.txtScienceWrong = CreateTextBox(580, 160);

            this.lblTurkishCorrect = CreateLabel("Türkçe Doğru", 30, 200);
            this.txtTurkishCorrect = CreateTextBox(200, 200);
            this.lblTurkishWrong = CreateLabel("Türkçe Yanlış", 410, 200);
            this.txtTurkishWrong = CreateTextBox(580, 200);

            this.lblHistoryCorrect = CreateLabel("Tarih Doğru", 30, 240);
            this.txtHistoryCorrect = CreateTextBox(200, 240);
            this.lblHistoryWrong = CreateLabel("Tarih Yanlış", 410, 240);
            this.txtHistoryWrong = CreateTextBox(580, 240);

            this.lblReligionCorrect = CreateLabel("Din Doğru", 30, 280);
            this.txtReligionCorrect = CreateTextBox(200, 280);
            this.lblReligionWrong = CreateLabel("Din Yanlış", 410, 280);
            this.txtReligionWrong = CreateTextBox(580, 280);

            this.lblEnglishCorrect = CreateLabel("İngilizce Doğru", 30, 320);
            this.txtEnglishCorrect = CreateTextBox(200, 320);
            this.lblEnglishWrong = CreateLabel("İngilizce Yanlış", 410, 320);
            this.txtEnglishWrong = CreateTextBox(580, 320);

            this.btnSave = new System.Windows.Forms.Button();

            this.panelHeader.SuspendLayout();
            this.SuspendLayout();

            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(46, 134, 193);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Size = new System.Drawing.Size(800, 60);

            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(280, 15);
            this.lblTitle.Text = "Sınav Ekleme";

            this.lblTitleText = CreateLabel("Başlık:", 30, 80);
            this.txtTitle = CreateTextBox(200, 80);

            this.dtpExamDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.dtpExamDate.Location = new System.Drawing.Point(200, 360);
            this.dtpExamDate.Size = new System.Drawing.Size(200, 29);

            this.btnSave.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(300, 420);
            this.btnSave.Size = new System.Drawing.Size(200, 40);
            this.btnSave.Text = "Kaydet";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.lblTitleText);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.dtpExamDate);
            this.Controls.Add(this.lblMathCorrect);
            this.Controls.Add(this.txtMathCorrect);
            this.Controls.Add(this.lblMathWrong);
            this.Controls.Add(this.txtMathWrong);
            this.Controls.Add(this.lblScienceCorrect);
            this.Controls.Add(this.txtScienceCorrect);
            this.Controls.Add(this.lblScienceWrong);
            this.Controls.Add(this.txtScienceWrong);
            this.Controls.Add(this.lblTurkishCorrect);
            this.Controls.Add(this.txtTurkishCorrect);
            this.Controls.Add(this.lblTurkishWrong);
            this.Controls.Add(this.txtTurkishWrong);
            this.Controls.Add(this.lblHistoryCorrect);
            this.Controls.Add(this.txtHistoryCorrect);
            this.Controls.Add(this.lblHistoryWrong);
            this.Controls.Add(this.txtHistoryWrong);
            this.Controls.Add(this.lblReligionCorrect);
            this.Controls.Add(this.txtReligionCorrect);
            this.Controls.Add(this.lblReligionWrong);
            this.Controls.Add(this.txtReligionWrong);
            this.Controls.Add(this.lblEnglishCorrect);
            this.Controls.Add(this.txtEnglishCorrect);
            this.Controls.Add(this.lblEnglishWrong);
            this.Controls.Add(this.txtEnglishWrong);
            this.Controls.Add(this.btnSave);

            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.AutoSize = false;

            this.dtpExamDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.dtpExamDate.Location = new System.Drawing.Point(200, 360);
            this.dtpExamDate.Size = new System.Drawing.Size(380, 35);

            this.ClientSize = new System.Drawing.Size(800, 500);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sınav Ekleme";
            this.panelHeader.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label CreateLabel(string text, int x, int y)
        {
            var lbl = new System.Windows.Forms.Label();
            lbl.Text = text;
            lbl.Location = new System.Drawing.Point(x, y);
            lbl.Font = new System.Drawing.Font("Segoe UI", 12F);
            lbl.Size = new System.Drawing.Size(160, 25);
            return lbl;
        }

        private System.Windows.Forms.TextBox CreateTextBox(int x, int y)
        {
            var txt = new System.Windows.Forms.TextBox();
            txt.Location = new System.Drawing.Point(x, y);
            txt.Font = new System.Drawing.Font("Segoe UI", 12F);
            txt.Size = new System.Drawing.Size(180, 29);
            return txt;
        }
    }
}