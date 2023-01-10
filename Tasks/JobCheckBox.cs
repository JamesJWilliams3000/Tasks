using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tasks
{
    public partial class JobCheckBox : CheckBox
    {
        public Job Job { get; set; }
        public string Caption { get; set; }

        public JobCheckBox(Job job, double daysLeft)
        {
            InitializeComponent();
            Job = job;
            AutoSize = true;

            SetText(job, daysLeft);
            SetContextMenu();
            Caption = SetToolTip();
        }

        private void SetContextMenu()
        {
            ContextMenu menu = new ContextMenu();
            menu.MenuItems.Add("Mark As Complete", new EventHandler(ContextMenuClick));
            menu.MenuItems.Add("Edit", new EventHandler(EditJob));
            ContextMenu = menu;
        }

        private string SetToolTip()
        {
            string[] freqs = { "O", "D", "W", "B", "M", "W/E" };            
            string time = "";
            if (Job.Est_Time >= 60)
            {
                TimeSpan span = new TimeSpan(0, Job.Est_Time, 0);
                time = string.Format("{0}h{1:D2}", span.Hours, span.Minutes);
            }
            else
            {
                time = string.Format("{0:D1}m", Job.Est_Time);
            }
            string caption = string.Format("{0}: {1}", freqs[Job.Frequency], time) + Environment.NewLine;
            
            caption += "Last Finished: ";                        
            if (Job.Last_Finished != null)
            {
                caption += ((DateTime)Job.Last_Finished).ToString("MM/dd/yyyy") + string.Format(" ({0:0.#} days ago)", (DateTime.Now - ((DateTime)Job.Last_Finished).Date).TotalDays);
            }
            else
            {
                caption += "Never";
            }
            caption += Environment.NewLine;
            caption += string.Format("Added: {0} ({1:0.#} days ago)", ((DateTime)Job.Date_Added).ToString("MM/dd/yyyy"), (DateTime.Now - Job.Date_Added.Date).TotalDays);

            if (Job.Deadline != null)
            {
                caption += Environment.NewLine;
                caption += string.Format("Deadline: {0} (in {1:0.#} days)", ((DateTime)Job.Deadline).ToString("MM/dd/yyyy"), (Job.Deadline.Value.Date - DateTime.Now).TotalDays);
                caption += Environment.NewLine;
            }

            return caption;
        }

        private void SetText(Job job, double daysLeft)
        {
            if (job.Job_Type > 0)
            {
                double adjPerDay = GetAmountLeft(JobRepository.GetSubJob(job.Job_ID)) / daysLeft;

                Text = string.Format("{0:0.##} - {1}", adjPerDay, job.Name.Trim());
            }
            else
            {
                Text = job.Name.Trim();
            }
        }

        private void ContextMenuClick(object sender, EventArgs e)
        {
            JobRepository.SetComplete(Job, true);
            Checked = true;
        }

        private void EditJob(object sender, EventArgs e)
        {
            SubJob sub = Job.Job_Type > 0 ? JobRepository.GetSubJob(Job.Job_ID) : null;
            OpenForm(new AddEditJobView(Job, sub));
        }

        void OpenForm(Form frm)
        {
            frm.MdiParent = this.FindForm().MdiParent;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            frm.Show();
            this.FindForm().Close();
        }

        double GetAmountLeft(SubJob sub)
        {
            if (sub.Base_Count != null)
            {
                if (Job.Job_Type == 4)
                {
                    return sub.Count - sub.Total_Done;
                }
                else
                {
                    return sub.Count - (int)sub.Base_Count - sub.Total_Done;
                }
            }
            else
            {
                return sub.Count - sub.Total_Done;
            }
        }

        public JobCheckBox()
        {
            InitializeComponent();

        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            base.OnCheckedChanged(e);

            if (Checked)
            {
                Font = new Font(Font, FontStyle.Strikeout);

                if (Job.Frequency == 0)
                {
                    JobRepository.SetComplete(Job, true);
                }
            }
            else
            {
                Font = new Font(Font, FontStyle.Regular);

                if (Job.Frequency == 0)
                {
                    JobRepository.SetComplete(Job, false);
                }
            }

            Invalidate();
        }
    }
}
