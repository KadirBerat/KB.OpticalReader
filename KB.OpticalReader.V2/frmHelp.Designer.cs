namespace KB.OpticalReader.V2
{
    partial class frmHelp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHelp));
            label7 = new Label();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 9);
            label7.Name = "label7";
            label7.Size = new Size(590, 255);
            label7.TabIndex = 9;
            label7.Text = resources.GetString("label7.Text");
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(5, 296);
            label1.Name = "label1";
            label1.Size = new Size(122, 15);
            label1.TabIndex = 10;
            label1.Text = "Kadir Berat Güventürk";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(570, 296);
            label2.Name = "label2";
            label2.Size = new Size(37, 15);
            label2.TabIndex = 11;
            label2.Text = "v2.0.1";
            // 
            // frmHelp
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(609, 315);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(label7);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmHelp";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "Yardım";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label7;
        private Label label1;
        private Label label2;
    }
}