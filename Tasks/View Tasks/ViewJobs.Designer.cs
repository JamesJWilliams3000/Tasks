namespace Tasks
{
    partial class ViewJobs
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
            this.flpPage = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlDateHeader = new System.Windows.Forms.Panel();
            this.lblBiWeekly = new System.Windows.Forms.Label();
            this.btnNewDay = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cboSort = new System.Windows.Forms.ComboBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.flpTask = new System.Windows.Forms.FlowLayoutPanel();
            this.flpPage.SuspendLayout();
            this.pnlDateHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpPage
            // 
            this.flpPage.Controls.Add(this.pnlDateHeader);
            this.flpPage.Controls.Add(this.flpTask);
            this.flpPage.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpPage.Location = new System.Drawing.Point(12, 12);
            this.flpPage.Name = "flpPage";
            this.flpPage.Size = new System.Drawing.Size(570, 305);
            this.flpPage.TabIndex = 0;
            this.flpPage.WrapContents = false;
            // 
            // pnlDateHeader
            // 
            this.pnlDateHeader.Controls.Add(this.lblBiWeekly);
            this.pnlDateHeader.Controls.Add(this.btnNewDay);
            this.pnlDateHeader.Controls.Add(this.label1);
            this.pnlDateHeader.Controls.Add(this.cboSort);
            this.pnlDateHeader.Controls.Add(this.lblDate);
            this.pnlDateHeader.Controls.Add(this.lblCount);
            this.pnlDateHeader.Location = new System.Drawing.Point(3, 3);
            this.pnlDateHeader.Name = "pnlDateHeader";
            this.pnlDateHeader.Size = new System.Drawing.Size(543, 39);
            this.pnlDateHeader.TabIndex = 1;
            // 
            // lblBiWeekly
            // 
            this.lblBiWeekly.AutoSize = true;
            this.lblBiWeekly.Location = new System.Drawing.Point(106, 2);
            this.lblBiWeekly.Name = "lblBiWeekly";
            this.lblBiWeekly.Size = new System.Drawing.Size(35, 13);
            this.lblBiWeekly.TabIndex = 4;
            this.lblBiWeekly.Text = "label2";
            // 
            // btnNewDay
            // 
            this.btnNewDay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewDay.Location = new System.Drawing.Point(455, 9);
            this.btnNewDay.Name = "btnNewDay";
            this.btnNewDay.Size = new System.Drawing.Size(62, 22);
            this.btnNewDay.TabIndex = 3;
            this.btnNewDay.Text = "New Day";
            this.btnNewDay.UseVisualStyleBackColor = true;
            this.btnNewDay.Click += new System.EventHandler(this.btnNewDay_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(293, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Sort:";
            // 
            // cboSort
            // 
            this.cboSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSort.FormattingEnabled = true;
            this.cboSort.Location = new System.Drawing.Point(328, 11);
            this.cboSort.Name = "cboSort";
            this.cboSort.Size = new System.Drawing.Size(121, 21);
            this.cboSort.TabIndex = 1;
            this.cboSort.SelectedIndexChanged += new System.EventHandler(this.cboSort_SelectedIndexChanged);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(3, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(52, 17);
            this.lblDate.TabIndex = 1;
            this.lblDate.Text = "label1";
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.Location = new System.Drawing.Point(3, 17);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(46, 17);
            this.lblCount.TabIndex = 1;
            this.lblCount.Text = "label1";
            // 
            // flpTask
            // 
            this.flpTask.BackColor = System.Drawing.SystemColors.Control;
            this.flpTask.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpTask.Location = new System.Drawing.Point(3, 48);
            this.flpTask.Name = "flpTask";
            this.flpTask.Size = new System.Drawing.Size(543, 238);
            this.flpTask.TabIndex = 0;
            // 
            // frmViewJobs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 336);
            this.Controls.Add(this.flpPage);
            this.Name = "frmViewJobs";
            this.Text = "frmViewJobs";
            this.Load += new System.EventHandler(this.frmViewJobs_Load);
            this.Resize += new System.EventHandler(this.frmViewJobs_Resize);
            this.flpPage.ResumeLayout(false);
            this.pnlDateHeader.ResumeLayout(false);
            this.pnlDateHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpPage;
        private System.Windows.Forms.FlowLayoutPanel flpTask;
        private System.Windows.Forms.Panel pnlDateHeader;
        private System.Windows.Forms.Button btnNewDay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboSort;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblBiWeekly;
    }
}