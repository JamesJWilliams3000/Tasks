using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tasks
{
    public partial class CalenderTable : UserControl
    {
        public BindingList<UpdatedJobCalendarRow> Rows { get; set; }

        public CalenderTable()
        {
            InitializeComponent();
        }

        public CalenderTable(List<UpdatedJobCalendarRow> rows)
        {
            InitializeComponent();
            Rows = new BindingList<UpdatedJobCalendarRow>(rows);

            SetBinding();
        }

        public void SetBinding()
        {
            Rows.AllowEdit = true;
            dgv.AutoGenerateColumns = false;

            string[] names = {"", "", "Need:", "Left:",  "Baseline:", "Deadline:", "Est. Date:", "Adj. Per Day:", "Days Left:", "Goal:", "Even:" };
            string[] values = { "Name", "DoneToday", "Need", "Left", "Baseline", "Deadline", "EstDeadline", "AjustedBaseline", "DaysUntilColumn", "Goal", "Even" };

            for (int i = 0; i < names.Length; i++)
            {
                bool read = true;
                if (i == 1) read = false;
                SetColumn(names[i], values[i], read);
            }          

            dgv.AutoResizeColumns();
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv.Columns[0].DefaultCellStyle.Font = new Font(dgv.Font, FontStyle.Bold);
            dgv.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgv.Columns[1].Width = 100;
            dgv.Columns[2].DefaultCellStyle.Format = "+0.0;-#.0;0.0";
            dgv.Columns[4].DefaultCellStyle.Format = "0.0";
            dgv.Columns[7].DefaultCellStyle.Format = "0.0";
            dgv.Columns[7].DefaultCellStyle.Font = new Font(dgv.Font,FontStyle.Bold);            
            dgv.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            dgv.DataSource = Rows;
        }

        void SetColumn(string name, string dataName, bool ReadOnly)
        {
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn()
            {
                Name = name,
                DataPropertyName = dataName,
            };
            col.ReadOnly = ReadOnly;
            dgv.Columns.Add(col);
        }

        public void sizeDGV()
        {
            DataGridViewElementStates states = DataGridViewElementStates.None;
            dgv.ScrollBars = ScrollBars.None;
            var totalHeight = dgv.Rows.GetRowsHeight(states) + dgv.ColumnHeadersHeight;
            var totalWidth = dgv.Columns.GetColumnsWidth(states);
            dgv.ClientSize = new Size(totalWidth, totalHeight);
        }

        public void Save()
        {
            var jobs = new List<Job>();
            foreach (var row in Rows)
            {                
                JobRepository.SaveSubJob(row.SubJob, row.TotalDone);
                JobRepository.SaveDatesSubJob(row.Job.Job_ID, row.DateID, row.DoneToday);

                using (var context = new JOBSEntities())
                {
                    var sub = context.SubJobs.FirstOrDefault(x => x.Job_ID == row.Job.Job_ID);

                    if (sub.Total_Done == sub.Count)
                    {
                        JobRepository.SetComplete(row.Job, true);
                    }
                }
            }
        }

        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            UpdatedJobCalendarRow row = (UpdatedJobCalendarRow)dgv.Rows[e.RowIndex].DataBoundItem;

            row.TotalDone = row.CalcTotalDone();
            //row.DoneToday = row.CalcDoneToday();
            row.Need = row.CalcNeed();
            row.Left = row.CalcLeft();
            //row.EstDeadline = row.CalcEstFinishedDate();
            //row.AjustedBaseline = row.CalcAdjBaseline();

            dgv.Refresh();
        }

        private void dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception != null && e.Context == DataGridViewDataErrorContexts.Commit)
            {
                MessageBox.Show("Must be integer only.");
            }
        }
    }
}
