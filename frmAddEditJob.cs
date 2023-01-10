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
    public partial class frmAddEditJob : Form
    {
        public bool Edit { get; set; }

        public frmAddEditJob()
        {
            InitializeComponent();
            Edit = false;
        }

        public frmAddEditJob(bool edit)
        {
            InitializeComponent();

            Edit = edit;
            lblTitle.Text = edit ? "Edit Task" : "Add Task";
            btnAdd.Text = edit ? "Edit" : "Add";
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
                    //update job TODO
                }
                else
                {
                    int newID = JobRepository.GetLastID() + 1;

                    if (IsSubJob())
                    {
                        JobRepository.Add(CreateJob(0, newID), CreateSubJob(newID, 0));
                    }
                    else
                    {
                        JobRepository.Add(CreateJob(0, newID));
                    }
                }
            }

            this.Close();
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
                count = (int)nudBase.Value - (int)nudCount.Value;
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
            nudDays.Value = days;
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

        private void nudBase_ValueChanged(object sender, EventArgs e)
        {
            CountValueChanged();
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
                int count = (int)nudBase.Value - (int)nudCount.Value;

                lblTrueCount.Text = string.Format("True Count: {0:0.##}", count);
                SetPerDay(count);
            }
        }
    }
}
