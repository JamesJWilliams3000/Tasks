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
    public partial class JobGroup : UserControl
    {
        string[] headerArrows = { "▶", "▼" };
        public DateTime Date { get; set; }

        public JobGroup()
        {
            InitializeComponent();
        }

        public JobGroup(ICollection<Job> jobs, string head, DateTime date, ICollection<DatesJob> completedJobs)
        {
            InitializeComponent();
            Date = date;
            SetLblCount(jobs.Sum(x => x.Est_Time), jobs.Count());
            AddJobs(jobs, completedJobs);
            Header = head;
            lblHeader.Text = headerArrows[1] + " " + head;                 
        }

        private bool open = false;
        public bool Open
        {
            get { return open; }
            set
            {
                open = value;
                ChangeHeader();
            }
        }

        private string header;
        public string Header
        {
            get { return header; }
            set
            {
                header = value;
                ChangeHeader();
            }
        }

        private void lblHeader_Click(object sender, EventArgs e)
        {
            if (flpJobs.Visible)
            {
                flpJobs.Visible = false;
                Open = false;
            }
            else
            {
                flpJobs.Visible = true;
                Open = true;
            }
        }

        void ChangeHeader()
        {
            if (open)
            {
                lblHeader.Text = headerArrows[1] + " " + header;
            }
            else
            {
                lblHeader.Text = headerArrows[0] + " " + header;
            }

            Invalidate();
        }

        void SetLblCount(int estTime, int count)
        {
            string time = "";
            if (estTime >= 60)
            {
                TimeSpan span = new TimeSpan(0, estTime, 0);
                time = string.Format("{0}h{1:D2}", span.Hours, span.Minutes);
            }
            else
            {
                time = string.Format("{0:D2}m", estTime);
            }

            lblCount.Text = string.Format("{0:D2} tasks - {1}", count, time);
        }

        void AddJobs(ICollection<Job> jobs, ICollection<DatesJob> completedJobs)
        {
            ToolTip ToolTip = new ToolTip
            {
                AutoPopDelay = 5000,
                InitialDelay = 1000,
                ReshowDelay = 500,
                ShowAlways = true
            };

            foreach (var job in jobs)
            {
                bool complete = completedJobs.Select(x => x.Job_ID).Contains(job.Job_ID);
                double daysleft = -1;
                if (job.Deadline != null)
                {
                    daysleft = (((DateTime)job.Deadline).Date - Date.Date).TotalDays;
                }

                var check = new JobCheckBox(job, daysleft) { Margin = new Padding(0), Checked = complete };
                check.CheckedChanged += JobCheckBox_CheckedChanged;                
                flpJobs.Controls.Add(check);
                check.Show();
                ToolTip.SetToolTip(check, check.Caption);
            }
        }

        private void JobCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Job job = ((JobCheckBox)sender).Job;
            DateRepository.ChangeCompleted(job.Job_ID, Date, ((JobCheckBox)sender).Checked);
        }
    }
}
