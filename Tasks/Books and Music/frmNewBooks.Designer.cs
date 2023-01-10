namespace Tasks
{
    partial class frmNewBooks
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlPage = new System.Windows.Forms.Panel();
            this.btnImport = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.nudPages = new System.Windows.Forms.NumericUpDown();
            this.dtpBought = new System.Windows.Forms.DateTimePicker();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPages)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Lines should be in this format: ";
            // 
            // pnlPage
            // 
            this.pnlPage.Controls.Add(this.label4);
            this.pnlPage.Controls.Add(this.lblFilePath);
            this.pnlPage.Controls.Add(this.btnCancel);
            this.pnlPage.Controls.Add(this.btnAdd);
            this.pnlPage.Controls.Add(this.dtpBought);
            this.pnlPage.Controls.Add(this.nudPages);
            this.pnlPage.Controls.Add(this.txtTitle);
            this.pnlPage.Controls.Add(this.label3);
            this.pnlPage.Controls.Add(this.label2);
            this.pnlPage.Controls.Add(this.btnImport);
            this.pnlPage.Controls.Add(this.label1);
            this.pnlPage.Location = new System.Drawing.Point(12, 12);
            this.pnlPage.Name = "pnlPage";
            this.pnlPage.Size = new System.Drawing.Size(584, 166);
            this.pnlPage.TabIndex = 1;
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(3, 7);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(91, 23);
            this.btnImport.TabIndex = 1;
            this.btnImport.Text = "Import From Txt";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(146, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "title | pages | bought";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Or Manually Enter Book:";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(3, 110);
            this.txtTitle.MaxLength = 300;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(418, 20);
            this.txtTitle.TabIndex = 4;
            // 
            // nudPages
            // 
            this.nudPages.Increment = new decimal(new int[] {
            96,
            0,
            0,
            0});
            this.nudPages.Location = new System.Drawing.Point(427, 110);
            this.nudPages.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudPages.Name = "nudPages";
            this.nudPages.Size = new System.Drawing.Size(55, 20);
            this.nudPages.TabIndex = 5;
            this.nudPages.ThousandsSeparator = true;
            // 
            // dtpBought
            // 
            this.dtpBought.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBought.Location = new System.Drawing.Point(488, 110);
            this.dtpBought.Name = "dtpBought";
            this.dtpBought.Size = new System.Drawing.Size(90, 20);
            this.dtpBought.TabIndex = 6;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(422, 136);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(503, 136);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Location = new System.Drawing.Point(98, 12);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(10, 13);
            this.lblFilePath.TabIndex = 9;
            this.lblFilePath.Text = ".";
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Location = new System.Drawing.Point(3, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(575, 1);
            this.label4.TabIndex = 10;
            this.label4.Text = ".";
            // 
            // frmNewBooks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 189);
            this.Controls.Add(this.pnlPage);
            this.Name = "frmNewBooks";
            this.Text = "frmNewBooks";
            this.pnlPage.ResumeLayout(false);
            this.pnlPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPages)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlPage;
        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DateTimePicker dtpBought;
        private System.Windows.Forms.NumericUpDown nudPages;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Label label4;
    }
}