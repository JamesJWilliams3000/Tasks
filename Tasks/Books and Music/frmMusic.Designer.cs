namespace Tasks
{
    partial class frmMusic
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.flpPage = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlDGV = new System.Windows.Forms.Panel();
            this.nudNewCount = new System.Windows.Forms.NumericUpDown();
            this.rbCount = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.btnAddAlbum = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dgvNeed = new System.Windows.Forms.DataGridView();
            this.dgvTotals = new System.Windows.Forms.DataGridView();
            this.pnlNew = new System.Windows.Forms.Panel();
            this.pnlNewMusic = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nudDays = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nudPerDay = new System.Windows.Forms.NumericUpDown();
            this.dtpDeadline = new System.Windows.Forms.DateTimePicker();
            this.nudCount = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.flpAlbums = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.nudTotal = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnNewMusicToggle = new System.Windows.Forms.Button();
            this.pnlCurrentMusic = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblPerDay = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.flpCurrentAlbums = new System.Windows.Forms.FlowLayoutPanel();
            this.label15 = new System.Windows.Forms.Label();
            this.flpMusic = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.flpPage.SuspendLayout();
            this.pnlDGV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNewCount)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTotals)).BeginInit();
            this.pnlNew.SuspendLayout();
            this.pnlNewMusic.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPerDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTotal)).BeginInit();
            this.pnlCurrentMusic.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.flpMusic.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(3, 32);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(744, 405);
            this.dgv.TabIndex = 0;
            this.dgv.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellValueChanged);
            // 
            // flpPage
            // 
            this.flpPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpPage.Controls.Add(this.pnlDGV);
            this.flpPage.Controls.Add(this.panel4);
            this.flpPage.Controls.Add(this.flpMusic);
            this.flpPage.Location = new System.Drawing.Point(12, 12);
            this.flpPage.Name = "flpPage";
            this.flpPage.Size = new System.Drawing.Size(1644, 452);
            this.flpPage.TabIndex = 1;
            this.flpPage.Visible = false;
            // 
            // pnlDGV
            // 
            this.pnlDGV.AutoSize = true;
            this.pnlDGV.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlDGV.Controls.Add(this.dgv);
            this.pnlDGV.Controls.Add(this.nudNewCount);
            this.pnlDGV.Controls.Add(this.rbCount);
            this.pnlDGV.Controls.Add(this.rbAll);
            this.pnlDGV.Controls.Add(this.btnAddAlbum);
            this.pnlDGV.Location = new System.Drawing.Point(3, 3);
            this.pnlDGV.Name = "pnlDGV";
            this.pnlDGV.Size = new System.Drawing.Size(750, 440);
            this.pnlDGV.TabIndex = 3;
            // 
            // nudNewCount
            // 
            this.nudNewCount.Location = new System.Drawing.Point(619, 6);
            this.nudNewCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudNewCount.Name = "nudNewCount";
            this.nudNewCount.Size = new System.Drawing.Size(47, 20);
            this.nudNewCount.TabIndex = 3;
            // 
            // rbCount
            // 
            this.rbCount.AutoSize = true;
            this.rbCount.Location = new System.Drawing.Point(557, 6);
            this.rbCount.Name = "rbCount";
            this.rbCount.Size = new System.Drawing.Size(56, 17);
            this.rbCount.TabIndex = 2;
            this.rbCount.Text = "Count:";
            this.rbCount.UseVisualStyleBackColor = true;
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Checked = true;
            this.rbAll.Location = new System.Drawing.Point(515, 6);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(36, 17);
            this.rbAll.TabIndex = 1;
            this.rbAll.TabStop = true;
            this.rbAll.Text = "All";
            this.rbAll.UseVisualStyleBackColor = true;
            // 
            // btnAddAlbum
            // 
            this.btnAddAlbum.Enabled = false;
            this.btnAddAlbum.Location = new System.Drawing.Point(672, 3);
            this.btnAddAlbum.Name = "btnAddAlbum";
            this.btnAddAlbum.Size = new System.Drawing.Size(75, 23);
            this.btnAddAlbum.TabIndex = 0;
            this.btnAddAlbum.Text = "Add Album";
            this.btnAddAlbum.UseVisualStyleBackColor = true;
            this.btnAddAlbum.Click += new System.EventHandler(this.btnAddAlbum_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dgvNeed);
            this.panel4.Controls.Add(this.dgvTotals);
            this.panel4.Location = new System.Drawing.Point(759, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(185, 440);
            this.panel4.TabIndex = 7;
            // 
            // dgvNeed
            // 
            this.dgvNeed.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvNeed.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvNeed.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNeed.ColumnHeadersVisible = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvNeed.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvNeed.Location = new System.Drawing.Point(0, 156);
            this.dgvNeed.Name = "dgvNeed";
            this.dgvNeed.RowHeadersVisible = false;
            this.dgvNeed.Size = new System.Drawing.Size(182, 150);
            this.dgvNeed.TabIndex = 2;
            // 
            // dgvTotals
            // 
            this.dgvTotals.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvTotals.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTotals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTotals.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvTotals.Location = new System.Drawing.Point(0, 0);
            this.dgvTotals.Name = "dgvTotals";
            this.dgvTotals.RowHeadersVisible = false;
            this.dgvTotals.Size = new System.Drawing.Size(182, 150);
            this.dgvTotals.TabIndex = 1;
            // 
            // pnlNew
            // 
            this.pnlNew.AutoSize = true;
            this.pnlNew.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlNew.Controls.Add(this.pnlNewMusic);
            this.pnlNew.Controls.Add(this.btnNewMusicToggle);
            this.pnlNew.Location = new System.Drawing.Point(0, 133);
            this.pnlNew.Margin = new System.Windows.Forms.Padding(0);
            this.pnlNew.Name = "pnlNew";
            this.pnlNew.Size = new System.Drawing.Size(346, 233);
            this.pnlNew.TabIndex = 6;
            // 
            // pnlNewMusic
            // 
            this.pnlNewMusic.AutoSize = true;
            this.pnlNewMusic.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlNewMusic.Controls.Add(this.panel1);
            this.pnlNewMusic.Controls.Add(this.flpAlbums);
            this.pnlNewMusic.Controls.Add(this.panel2);
            this.pnlNewMusic.Controls.Add(this.btnAdd);
            this.pnlNewMusic.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlNewMusic.Location = new System.Drawing.Point(3, 29);
            this.pnlNewMusic.Name = "pnlNewMusic";
            this.pnlNewMusic.Size = new System.Drawing.Size(340, 201);
            this.pnlNewMusic.TabIndex = 2;
            this.pnlNewMusic.Visible = false;
            this.pnlNewMusic.WrapContents = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.nudDays);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.nudPerDay);
            this.panel1.Controls.Add(this.dtpDeadline);
            this.panel1.Controls.Add(this.nudCount);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(334, 106);
            this.panel1.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Days:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Per Day:";
            // 
            // nudDays
            // 
            this.nudDays.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudDays.Location = new System.Drawing.Point(63, 2);
            this.nudDays.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nudDays.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDays.Name = "nudDays";
            this.nudDays.Size = new System.Drawing.Size(57, 20);
            this.nudDays.TabIndex = 10;
            this.nudDays.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDays.ValueChanged += new System.EventHandler(this.nudDays_ValueChanged);
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Location = new System.Drawing.Point(-3, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(339, 2);
            this.label6.TabIndex = 4;
            this.label6.Text = "label6";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Count:";
            // 
            // nudPerDay
            // 
            this.nudPerDay.DecimalPlaces = 2;
            this.nudPerDay.Location = new System.Drawing.Point(64, 79);
            this.nudPerDay.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.nudPerDay.Name = "nudPerDay";
            this.nudPerDay.Size = new System.Drawing.Size(56, 20);
            this.nudPerDay.TabIndex = 14;
            this.nudPerDay.ValueChanged += new System.EventHandler(this.nudPerDay_ValueChanged);
            // 
            // dtpDeadline
            // 
            this.dtpDeadline.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDeadline.Location = new System.Drawing.Point(64, 28);
            this.dtpDeadline.Name = "dtpDeadline";
            this.dtpDeadline.Size = new System.Drawing.Size(94, 20);
            this.dtpDeadline.TabIndex = 11;
            this.dtpDeadline.ValueChanged += new System.EventHandler(this.dtpDeadline_ValueChanged);
            // 
            // nudCount
            // 
            this.nudCount.Enabled = false;
            this.nudCount.Location = new System.Drawing.Point(64, 54);
            this.nudCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudCount.Name = "nudCount";
            this.nudCount.Size = new System.Drawing.Size(56, 20);
            this.nudCount.TabIndex = 13;
            this.nudCount.ValueChanged += new System.EventHandler(this.nudCount_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "End Date:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(126, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "from now";
            // 
            // flpAlbums
            // 
            this.flpAlbums.AutoSize = true;
            this.flpAlbums.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpAlbums.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpAlbums.Location = new System.Drawing.Point(3, 115);
            this.flpAlbums.MinimumSize = new System.Drawing.Size(20, 20);
            this.flpAlbums.Name = "flpAlbums";
            this.flpAlbums.Size = new System.Drawing.Size(20, 20);
            this.flpAlbums.TabIndex = 7;
            this.flpAlbums.WrapContents = false;
            this.flpAlbums.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.flpAlbums_ControlRemoved);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.nudTotal);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(3, 141);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(334, 28);
            this.panel2.TabIndex = 7;
            // 
            // nudTotal
            // 
            this.nudTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudTotal.Enabled = false;
            this.nudTotal.Location = new System.Drawing.Point(275, 3);
            this.nudTotal.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudTotal.Name = "nudTotal";
            this.nudTotal.Size = new System.Drawing.Size(56, 20);
            this.nudTotal.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Total:";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(262, 175);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 12;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnNewMusicToggle
            // 
            this.btnNewMusicToggle.Location = new System.Drawing.Point(0, 0);
            this.btnNewMusicToggle.Name = "btnNewMusicToggle";
            this.btnNewMusicToggle.Size = new System.Drawing.Size(129, 23);
            this.btnNewMusicToggle.TabIndex = 5;
            this.btnNewMusicToggle.Text = "Add New Music";
            this.btnNewMusicToggle.UseVisualStyleBackColor = true;
            this.btnNewMusicToggle.Click += new System.EventHandler(this.btnNewMusicToggle_Click);
            // 
            // pnlCurrentMusic
            // 
            this.pnlCurrentMusic.AutoSize = true;
            this.pnlCurrentMusic.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlCurrentMusic.Controls.Add(this.label15);
            this.pnlCurrentMusic.Controls.Add(this.flowLayoutPanel1);
            this.pnlCurrentMusic.Location = new System.Drawing.Point(0, 0);
            this.pnlCurrentMusic.Margin = new System.Windows.Forms.Padding(0);
            this.pnlCurrentMusic.Name = "pnlCurrentMusic";
            this.pnlCurrentMusic.Size = new System.Drawing.Size(346, 133);
            this.pnlCurrentMusic.TabIndex = 8;
            this.pnlCurrentMusic.Visible = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.panel5);
            this.flowLayoutPanel1.Controls.Add(this.flpCurrentAlbums);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 29);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(340, 101);
            this.flowLayoutPanel1.TabIndex = 2;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.lblPerDay);
            this.panel5.Controls.Add(this.label10);
            this.panel5.Controls.Add(this.lblCount);
            this.panel5.Controls.Add(this.lblEndDate);
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(334, 69);
            this.panel5.TabIndex = 7;
            // 
            // lblPerDay
            // 
            this.lblPerDay.AutoSize = true;
            this.lblPerDay.Location = new System.Drawing.Point(3, 47);
            this.lblPerDay.Name = "lblPerDay";
            this.lblPerDay.Size = new System.Drawing.Size(51, 13);
            this.lblPerDay.TabIndex = 7;
            this.lblPerDay.Text = "Per Day: ";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Location = new System.Drawing.Point(-3, 65);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(339, 2);
            this.label10.TabIndex = 4;
            this.label10.Text = "label10";
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(3, 22);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(41, 13);
            this.lblCount.TabIndex = 6;
            this.lblCount.Text = "Count: ";
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(3, 0);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(58, 13);
            this.lblEndDate.TabIndex = 5;
            this.lblEndDate.Text = "End Date: ";
            // 
            // flpCurrentAlbums
            // 
            this.flpCurrentAlbums.AutoSize = true;
            this.flpCurrentAlbums.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpCurrentAlbums.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpCurrentAlbums.Location = new System.Drawing.Point(3, 78);
            this.flpCurrentAlbums.MinimumSize = new System.Drawing.Size(20, 20);
            this.flpCurrentAlbums.Name = "flpCurrentAlbums";
            this.flpCurrentAlbums.Size = new System.Drawing.Size(20, 20);
            this.flpCurrentAlbums.TabIndex = 7;
            this.flpCurrentAlbums.WrapContents = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(9, 6);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(113, 17);
            this.label15.TabIndex = 5;
            this.label15.Text = "Current Music:";
            // 
            // flpMusic
            // 
            this.flpMusic.Controls.Add(this.pnlCurrentMusic);
            this.flpMusic.Controls.Add(this.pnlNew);
            this.flpMusic.Location = new System.Drawing.Point(950, 3);
            this.flpMusic.Name = "flpMusic";
            this.flpMusic.Size = new System.Drawing.Size(360, 440);
            this.flpMusic.TabIndex = 9;
            // 
            // frmMusic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1665, 739);
            this.Controls.Add(this.flpPage);
            this.Name = "frmMusic";
            this.Text = "frmMusic";
            this.Load += new System.EventHandler(this.frmMusic_Load);
            this.Shown += new System.EventHandler(this.frmMusic_Shown);
            this.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.frmMusic_ControlRemoved);
            this.Resize += new System.EventHandler(this.frmMusic_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.flpPage.ResumeLayout(false);
            this.flpPage.PerformLayout();
            this.pnlDGV.ResumeLayout(false);
            this.pnlDGV.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNewCount)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTotals)).EndInit();
            this.pnlNew.ResumeLayout(false);
            this.pnlNew.PerformLayout();
            this.pnlNewMusic.ResumeLayout(false);
            this.pnlNewMusic.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPerDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTotal)).EndInit();
            this.pnlCurrentMusic.ResumeLayout(false);
            this.pnlCurrentMusic.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.flpMusic.ResumeLayout(false);
            this.flpMusic.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.FlowLayoutPanel flpPage;
        private System.Windows.Forms.DataGridView dgvTotals;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpDeadline;
        private System.Windows.Forms.NumericUpDown nudDays;
        private System.Windows.Forms.NumericUpDown nudCount;
        private System.Windows.Forms.NumericUpDown nudPerDay;
        private System.Windows.Forms.Panel pnlDGV;
        private System.Windows.Forms.NumericUpDown nudNewCount;
        private System.Windows.Forms.RadioButton rbCount;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.Button btnAddAlbum;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.FlowLayoutPanel flpAlbums;
        private System.Windows.Forms.Button btnNewMusicToggle;
        private System.Windows.Forms.Panel pnlNew;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.FlowLayoutPanel pnlNewMusic;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.NumericUpDown nudTotal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView dgvNeed;
        private System.Windows.Forms.Panel pnlCurrentMusic;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblPerDay;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.FlowLayoutPanel flpCurrentAlbums;
        private System.Windows.Forms.FlowLayoutPanel flpMusic;
    }
}