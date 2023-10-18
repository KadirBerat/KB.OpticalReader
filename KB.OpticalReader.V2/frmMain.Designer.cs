namespace KB.OpticalReader.V2
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            msMenu = new MenuStrip();
            tsmiLoadFile = new ToolStripMenuItem();
            tsmiCalculateSettings = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            tstxtExamScore = new ToolStripTextBox();
            toolStripMenuItem2 = new ToolStripMenuItem();
            tstxtCoefficient = new ToolStripTextBox();
            tsmiCalculate = new ToolStripMenuItem();
            tsmiArrangement = new ToolStripMenuItem();
            tscmbArrangement = new ToolStripComboBox();
            tsmiExportToExcel = new ToolStripMenuItem();
            tsmiExit = new ToolStripMenuItem();
            tstxtQuestion = new ToolStripTextBox();
            tstxtExam = new ToolStripTextBox();
            tstxtCount = new ToolStripTextBox();
            lvwResults = new ListView();
            chIndex = new ColumnHeader();
            chStudentNumber = new ColumnHeader();
            chBookletType = new ColumnHeader();
            chCorrect = new ColumnHeader();
            chWrong = new ColumnHeader();
            chBlank = new ColumnHeader();
            chRoundedScore = new ColumnHeader();
            chScore = new ColumnHeader();
            lblAverage = new Label();
            lblMax = new Label();
            lblMin = new Label();
            lblStudentCount = new Label();
            lblError = new Label();
            btnError = new Button();
            msMenu.SuspendLayout();
            SuspendLayout();
            // 
            // msMenu
            // 
            msMenu.Items.AddRange(new ToolStripItem[] { tsmiLoadFile, tsmiCalculateSettings, tsmiCalculate, tsmiArrangement, tsmiExportToExcel, tsmiExit, tstxtQuestion, tstxtExam, tstxtCount });
            msMenu.Location = new Point(0, 0);
            msMenu.Name = "msMenu";
            msMenu.Size = new Size(921, 27);
            msMenu.TabIndex = 0;
            msMenu.Text = "menuStrip1";
            // 
            // tsmiLoadFile
            // 
            tsmiLoadFile.Name = "tsmiLoadFile";
            tsmiLoadFile.Size = new Size(83, 23);
            tsmiLoadFile.Text = "Dosya Yükle";
            tsmiLoadFile.Click += tsmiLoadFile_Click;
            // 
            // tsmiCalculateSettings
            // 
            tsmiCalculateSettings.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem1, toolStripMenuItem2 });
            tsmiCalculateSettings.Enabled = false;
            tsmiCalculateSettings.Name = "tsmiCalculateSettings";
            tsmiCalculateSettings.Size = new Size(121, 23);
            tsmiCalculateSettings.Text = "Hesaplama Ayarları";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { tstxtExamScore });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(135, 22);
            toolStripMenuItem1.Text = "Sınav Puanı";
            // 
            // tstxtExamScore
            // 
            tstxtExamScore.Name = "tstxtExamScore";
            tstxtExamScore.Size = new Size(100, 23);
            tstxtExamScore.Text = "100";
            tstxtExamScore.TextChanged += tstxtExamScore_TextChanged;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.DropDownItems.AddRange(new ToolStripItem[] { tstxtCoefficient });
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(135, 22);
            toolStripMenuItem2.Text = "Soru Puanı";
            // 
            // tstxtCoefficient
            // 
            tstxtCoefficient.Name = "tstxtCoefficient";
            tstxtCoefficient.Size = new Size(100, 23);
            tstxtCoefficient.Text = "0";
            tstxtCoefficient.TextChanged += tstxtCoefficient_TextChanged;
            // 
            // tsmiCalculate
            // 
            tsmiCalculate.Enabled = false;
            tsmiCalculate.Name = "tsmiCalculate";
            tsmiCalculate.Size = new Size(61, 23);
            tsmiCalculate.Text = "Hesapla";
            tsmiCalculate.Click += tsmiCalculate_Click;
            // 
            // tsmiArrangement
            // 
            tsmiArrangement.DropDownItems.AddRange(new ToolStripItem[] { tscmbArrangement });
            tsmiArrangement.Enabled = false;
            tsmiArrangement.Name = "tsmiArrangement";
            tsmiArrangement.Size = new Size(83, 23);
            tsmiArrangement.Text = "Listeyi Sırala";
            // 
            // tscmbArrangement
            // 
            tscmbArrangement.Items.AddRange(new object[] { "Varsayılan", "Kitapçık Türü", "Doğru Sayısı Artan", "Doğru Sayısı Azalan", "Yanlış Sayısı Artan", "Yanlış Sayısı Azalan", "Boş Sayısı Artan", "Boş Sayısı Azalan", "Puan Artan", "Puan Azalan" });
            tscmbArrangement.MaxDropDownItems = 15;
            tscmbArrangement.Name = "tscmbArrangement";
            tscmbArrangement.Size = new Size(121, 23);
            tscmbArrangement.SelectedIndexChanged += tscmbArrangement_SelectedIndexChanged;
            // 
            // tsmiExportToExcel
            // 
            tsmiExportToExcel.Enabled = false;
            tsmiExportToExcel.Name = "tsmiExportToExcel";
            tsmiExportToExcel.Size = new Size(86, 23);
            tsmiExportToExcel.Text = "Excel'e Aktar";
            tsmiExportToExcel.Click += tsmiExportToExcel_Click;
            // 
            // tsmiExit
            // 
            tsmiExit.Name = "tsmiExit";
            tsmiExit.Size = new Size(44, 23);
            tsmiExit.Text = "Çıkış";
            // 
            // tstxtQuestion
            // 
            tstxtQuestion.Alignment = ToolStripItemAlignment.Right;
            tstxtQuestion.Enabled = false;
            tstxtQuestion.Name = "tstxtQuestion";
            tstxtQuestion.Size = new Size(100, 23);
            tstxtQuestion.Text = "Soru Katsayısı: 0";
            // 
            // tstxtExam
            // 
            tstxtExam.Alignment = ToolStripItemAlignment.Right;
            tstxtExam.Enabled = false;
            tstxtExam.Name = "tstxtExam";
            tstxtExam.Size = new Size(100, 23);
            tstxtExam.Text = "Sınav Puanı: 100";
            // 
            // tstxtCount
            // 
            tstxtCount.Alignment = ToolStripItemAlignment.Right;
            tstxtCount.Enabled = false;
            tstxtCount.Name = "tstxtCount";
            tstxtCount.Size = new Size(100, 23);
            tstxtCount.Text = "Soru Sayısı: 0";
            // 
            // lvwResults
            // 
            lvwResults.Columns.AddRange(new ColumnHeader[] { chIndex, chStudentNumber, chBookletType, chCorrect, chWrong, chBlank, chRoundedScore, chScore });
            lvwResults.Location = new Point(12, 30);
            lvwResults.Name = "lvwResults";
            lvwResults.Size = new Size(606, 419);
            lvwResults.TabIndex = 1;
            lvwResults.UseCompatibleStateImageBehavior = false;
            lvwResults.View = View.Details;
            // 
            // chIndex
            // 
            chIndex.Text = "#";
            chIndex.Width = 30;
            // 
            // chStudentNumber
            // 
            chStudentNumber.Text = "Öğrenci Numarası";
            chStudentNumber.Width = 120;
            // 
            // chBookletType
            // 
            chBookletType.Text = "Kitapçık Türü";
            chBookletType.Width = 81;
            // 
            // chCorrect
            // 
            chCorrect.Text = "Doğru Sayısı";
            chCorrect.Width = 77;
            // 
            // chWrong
            // 
            chWrong.Text = "Yanlış Sayısı";
            chWrong.Width = 74;
            // 
            // chBlank
            // 
            chBlank.Text = "Boş Sayısı";
            chBlank.Width = 63;
            // 
            // chRoundedScore
            // 
            chRoundedScore.Text = "Puan";
            // 
            // chScore
            // 
            chScore.Text = "Ham Puan";
            chScore.Width = 90;
            // 
            // lblAverage
            // 
            lblAverage.AutoSize = true;
            lblAverage.Location = new Point(624, 42);
            lblAverage.Name = "lblAverage";
            lblAverage.Size = new Size(101, 15);
            lblAverage.TabIndex = 2;
            lblAverage.Text = "Sınıf Ortalaması: -";
            // 
            // lblMax
            // 
            lblMax.AutoSize = true;
            lblMax.Location = new Point(624, 57);
            lblMax.Name = "lblMax";
            lblMax.Size = new Size(101, 15);
            lblMax.TabIndex = 3;
            lblMax.Text = "En Yüksek Puan: -";
            // 
            // lblMin
            // 
            lblMin.AutoSize = true;
            lblMin.Location = new Point(624, 72);
            lblMin.Name = "lblMin";
            lblMin.Size = new Size(97, 15);
            lblMin.TabIndex = 4;
            lblMin.Text = "En Düşük Puan: -";
            // 
            // lblStudentCount
            // 
            lblStudentCount.AutoSize = true;
            lblStudentCount.Location = new Point(624, 27);
            lblStudentCount.Name = "lblStudentCount";
            lblStudentCount.Size = new Size(160, 15);
            lblStudentCount.TabIndex = 5;
            lblStudentCount.Text = "Sınava Giren Öğrenci Sayısı: -";
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.Location = new Point(624, 408);
            lblError.Name = "lblError";
            lblError.Size = new Size(159, 15);
            lblError.TabIndex = 6;
            lblError.Text = "Eksik Veya Hatalı Veri Sayısı: -";
            lblError.Visible = false;
            // 
            // btnError
            // 
            btnError.Enabled = false;
            btnError.Location = new Point(624, 426);
            btnError.Name = "btnError";
            btnError.Size = new Size(89, 23);
            btnError.TabIndex = 7;
            btnError.Text = "Verileri Göster";
            btnError.UseVisualStyleBackColor = true;
            btnError.Visible = false;
            btnError.Click += btnError_Click;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(921, 461);
            Controls.Add(btnError);
            Controls.Add(lblError);
            Controls.Add(lblStudentCount);
            Controls.Add(lblMin);
            Controls.Add(lblMax);
            Controls.Add(lblAverage);
            Controls.Add(lvwResults);
            Controls.Add(msMenu);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = msMenu;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Optik Okuyucu";
            HelpButtonClicked += frmMain_HelpButtonClicked;
            Load += frmMain_Load;
            msMenu.ResumeLayout(false);
            msMenu.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip msMenu;
        private ToolStripMenuItem tsmiLoadFile;
        private ToolStripMenuItem tsmiExportToExcel;
        private ToolStripMenuItem tsmiExit;
        private ToolStripMenuItem tsmiCalculateSettings;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripTextBox tstxtExamScore;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripTextBox tstxtCoefficient;
        private ToolStripTextBox tstxtQuestion;
        private ToolStripTextBox tstxtExam;
        private ToolStripTextBox tstxtCount;
        private ToolStripMenuItem tsmiCalculate;
        private ListView lvwResults;
        private ColumnHeader chIndex;
        private ColumnHeader chStudentNumber;
        private ColumnHeader chCorrect;
        private ColumnHeader chWrong;
        private ColumnHeader chBlank;
        private ColumnHeader chScore;
        private ColumnHeader chBookletType;
        private ToolStripMenuItem tsmiArrangement;
        private ToolStripComboBox tscmbArrangement;
        private Label lblAverage;
        private Label lblMax;
        private Label lblMin;
        private Label lblStudentCount;
        private ColumnHeader chRoundedScore;
        private Label lblError;
        private Button btnError;
    }
}