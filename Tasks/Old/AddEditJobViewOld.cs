using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tasks
{
    public partial class AddEditJobViewOld : Form
    {
        public bool Edit { get; set; }
        public Job Job { get; set; }
        public SubJob Sub { get; set; }

        public AddEditJobViewOld()
        {
            InitializeComponent();
            Edit = false;
        }

        public AddEditJobViewOld(bool edit, Job job, SubJob sub)
        {
            InitializeComponent();

            Edit = edit;
            lblTitle.Text = edit ? "Edit Task" : "Add Task";
            btnAdd.Text = edit ? "Edit" : "Add";
            Job = job;
            Sub = sub;
        }

        public void FillEditControls()
        {
            txtName.Text = Job.Name.Trim();
            cboCategory.SelectedIndex = Job.Category;
            dtpDeadline.Value = Job.Deadline != null ? Job.Deadline.Value : dtpDeadline.Value;
            cboFrequency.SelectedIndex = Job.Frequency;
            cboType.SelectedIndex = Job.Job_Type;
            nudMinutes.Value = Job.Est_Time;

            if (Sub != null)
            {
                nudBase.Value = Sub.Base_Count != null ? Sub.Base_Count.Value : 0;
                nudCount.Value = Sub.Count;
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                if (Edit)
                {
                    EditJob();
                }
                else
                {
                    int newID = JobRepository.GetLastID() + 1;
                    var job = CreateJob(0, newID);
                    SubJob subjob = null;

                    if (IsSubJob())
                    {
                        subjob = CreateSubJob(newID, 0);
                        JobRepository.Add(job, subjob);
                    }
                    else
                    {
                        JobRepository.Add(job);
                    }

                    if (AddToLastDate(cboFrequency.SelectedIndex))
                    {
                        int dateID = DateRepository.GetLastDate().Date_ID;
                        JobRepository.AddToDate(dateID, job, subjob);
                    }
                }
            }

            this.Close();
        }

        private void EditJob()
        {
            using (var context = new JOBSEntities())
            {
                var job = context.Jobs.First(x => x.Job_ID == Job.Job_ID);
                job.Name = txtName.Text.Trim();
                job.Category = cboCategory.SelectedIndex;
                job.Deadline = cboDeadline.SelectedIndex == 0 ? (DateTime?)null : dtpDeadline.Value;
                job.Frequency = cboFrequency.SelectedIndex;
                job.Job_Type = cboType.SelectedIndex;
                job.Est_Time = (int)nudMinutes.Value;

                if (IsSubJob())
                {
                    var sub = context.SubJobs.First(x => x.Job_ID == Job.Job_ID);
                    sub.Count = (int)nudCount.Value;

                    if (cboType.SelectedIndex == 2)
                    {
                        sub.Per_Day = (double)((int)nudCount.Value - (int)nudBase.Value) / (int)nudDays.Value;
                        sub.Base_Count = (int)nudBase.Value;
                    }
                    else
                    {
                        sub.Per_Day = (double)nudCount.Value / (int)nudDays.Value;
                        sub.Base_Count = null;
                    }
                }

                context.SaveChanges();
            }
        }

        private SubJob GetSubjob(int newID)
        {
            return CreateSubJob(newID, 0);
        }

        bool AddToLastDate(int freq)
        {
            DateTime lastDate = DateRepository.GetLastDate().Date1;
            CultureInfo cul = CultureInfo.CurrentCulture;
            int weekNumber = cul.Calendar.GetWeekOfYear(lastDate, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

            if (freq < 2)
            {
                return true;
            }
            else if (freq == 2)
            {
                //weekly
                if (lastDate.DayOfWeek == DayOfWeek.Friday)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (freq == 3)
            {
                //biweekly
                if (lastDate.DayOfWeek == DayOfWeek.Friday)
                {
                    if ((weekNumber % 2) > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (lastDate.Day == 24)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        bool IsSubJob()
        {
            bool subJob = false;
            if (cboType.SelectedIndex != 0)
            {
                subJob = true;
            }

            return subJob;
        }

        Job CreateJob(int import, int newID)
        {           
            return new Job()
            {
                Job_ID = newID,
                Name = txtName.Text.Trim(),
                Category = cboCategory.SelectedIndex,
                Date_Added = DateTime.Now,
                Deadline = cboDeadline.SelectedIndex == 0 ? (DateTime?)null : dtpDeadline.Value,
                Frequency = cboFrequency.SelectedIndex,
                Importance = import,
                Completed = false,
                Job_Type = cboType.SelectedIndex,
                Est_Time = (int)nudMinutes.Value,
            };
        }

        SubJob CreateSubJob(int id, int total)
        {
            int? baseCount = null;
            int count = (int)nudCount.Value;
            
            if (cboType.SelectedIndex == 2)
            {
                baseCount = (int)nudBase.Value;
                count = (int)nudCount.Value - (int)nudBase.Value;
            }

            int days = (int)nudDays.Value;
            double perDay = (double)count / days;

            return new SubJob()
            {
                Job_ID = id,
                Count = (int)nudCount.Value,
                Per_Day = perDay,
                Total_Done = total,
                Base_Count = baseCount,
            };
        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                txtName.Focus();
                errorProvider1.SetError(txtName, "Enter name.");
            }
            else
            {
                errorProvider1.SetError(txtName, null);
            }
        }

        private void frmAddEditJob_Load(object sender, EventArgs e)
        {
            cboCategory.BindingContext = new BindingContext();
            cboCategory.DataSource = Enum.GetValues(typeof(JobCat));

            cboFrequency.BindingContext = new BindingContext();
            cboFrequency.DataSource = Enum.GetValues(typeof(Frequency));

            cboDeadline.SelectedIndex = 0;
            cboType.SelectedIndex = 0;

            if(Edit) FillEditControls();
        }

        private void cbo_Validating(object sender, CancelEventArgs e)
        {
            ValidatingCombobox((ComboBox)sender);
        }

        void ValidatingCombobox(ComboBox cbo)
        {
            if (cbo.SelectedItem == null)
            {
                cbo.Focus();
                errorProvider1.SetError(cbo, "Select item.");
            }
            else
            {
                errorProvider1.SetError(cbo, null);
            }
        }

        private void frmAddEditJob_Resize(object sender, EventArgs e)
        {
            pnlPage.Location = new Point(this.Width / 2 - pnlPage.Width / 2, this.Height / 2 - pnlPage.Height / 2);
        }

        private void cboDeadline_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDeadline.SelectedIndex == 0)
            {
                tblDeadline.Enabled = false;
            }
            else
            {
                tblDeadline.Enabled = true;
            }
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboType.SelectedIndex == 0)
            {
                nudBase.Enabled = false;
                nudCount.Enabled = false;
            }
            else if (cboType.SelectedIndex == 1)
            {
                nudBase.Enabled = false;
                nudCount.Enabled = true;
            }
            else
            {
                nudBase.Enabled = true;
                nudCount.Enabled = true;
            }

            if (cboType.SelectedIndex > 0)
            {
                cboDeadline.SelectedIndex = 1;
            }        
        }

        private void nudDays_ValueChanged(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            int days = (int)nudDays.Value;

            dtpDeadline.ValueChanged -= dtpDeadline_ValueChanged;
            dtpDeadline.Value = now.AddDays(days);
            dtpDeadline.ValueChanged += dtpDeadline_ValueChanged;

            if ((int)nudCount.Value != 0)
            {
                CountValueChanged();
            }
        }

        private void dtpDeadline_ValueChanged(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            int days = (int)(dtpDeadline.Value.Date - now.Date).TotalDays;

            nudDays.ValueChanged -= nudDays_ValueChanged;
            if(days > 0) nudDays.Value = days;
            nudDays.ValueChanged += nudDays_ValueChanged;

            if ((int)nudCount.Value != 0)
            {
                CountValueChanged();
            }
        }

        private void nudCount_ValueChanged(object sender, EventArgs e)
        {
            CountValueChanged();
        }

        void SetPerDay(int count)
        {
            int days = (int)nudDays.Value;
            double perDay = (double)count / days;
            lblPerDay.Text = string.Format("={0:0.##} per day", perDay);
        }

        void CountValueChanged()
        {
            if ((int)nudBase.Value == 0)
            {
                int count = (int)nudCount.Value;
                SetPerDay(count);
            }
            else
            {
                int count = (int)nudCount.Value - (int)nudBase.Value;

                lblTrueCount.Text = string.Format("True Count: {0:0.##}", count);
                SetPerDay(count);
            }
        }

        private void nudBase_ValueChanged(object sender, EventArgs e)
        {
            CountValueChanged();
        }


    }
}
