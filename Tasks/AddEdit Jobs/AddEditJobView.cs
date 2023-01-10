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
    public partial class AddEditJobView : Form
    {
		#region Fields
		public Job Job { get; set; }
		public SubJob Sub { get; set; }

		public string JobName
		{
			get => txtName.Text.Trim();
			set => txtName.Text = value;
		}

		public JobCat JobCategory
		{
			get => (JobCat)cboCategory.SelectedIndex;
			set => cboCategory.SelectedIndex = (int)value;
		}

		public Frequency Frequency
		{
			get => (Frequency)cboFrequency.SelectedIndex;
			set => cboFrequency.SelectedIndex = (int)value;
		}

		public int TimeToComplete
		{
			get => (int)nudMinutes.Value;
			set => nudMinutes.Value = value;
		}

		public DateTime Deadline
		{
			get => dtpDeadline.Value;
			set
			{
				dtpDeadline.Value = value;
				MatchDateValues(Deadline);
			}
		}
		public JobType JobType
		{
			get => (JobType)cboType.SelectedIndex;
			set => cboType.SelectedIndex = (int)value;
		}
		public int Offset
		{
			get => (int)nudOffset.Value;
			set => nudOffset.Value = value;
		}
		public int TotalItems
		{
			get => (int)nudTotalItems.Value;
			set => nudTotalItems.Value = value;
		}

		bool edit = false;
		#endregion

		//add
		public AddEditJobView()
		{
			InitializeComponent();


			cboType.DataSource = Enums.GetJobTypeNames();

			cboCategory.BindingContext = new BindingContext();
			cboCategory.DataSource = Enum.GetValues(typeof(JobCat));

			cboFrequency.BindingContext = new BindingContext();
			cboFrequency.DataSource = Enum.GetValues(typeof(Frequency));

			cboDeadline.SelectedIndex = 0;
			cboType.SelectedIndex = 0;
			cboType.SelectedIndex = 0;
		}

		//edit
		public AddEditJobView(Job job, SubJob sub) : this()
		{
			Job = job;
			Sub = sub;

			edit = true;
			lblTitle.Text = "Edit Task";
			btnAdd.Text = "Edit";
		}

		#region Logic
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
					sub.Count = (int)nudTotalItems.Value;

					if (cboType.SelectedIndex == 2)
					{
						sub.Per_Day = (double)((int)nudTotalItems.Value - (int)nudOffset.Value) / (int)nudDays.Value;
						sub.Base_Count = (int)nudOffset.Value;
					}
					else
					{
						sub.Per_Day = (double)nudTotalItems.Value / (int)nudDays.Value;
						sub.Base_Count = null;
					}
				}

				context.SaveChanges();
			}
		}

		bool IsSubJob()
		{
			return cboType.SelectedIndex != 0 ? true : false;
		}

		void SetPerDay(int count)
		{
			int days = (int)nudDays.Value;
			double perDay = (double)count / days;
			lblPerDay.Text = string.Format("={0:0.##} per day", perDay);
		}

		void CountValueChanged()
		{
			int count = (int)nudTotalItems.Value - (int)nudOffset.Value;
			SetPerDay(count);

			if ((int)nudOffset.Value > 0)
			{
				lblTrueCount.Text = string.Format("True Count: {0:0.##}", count);
			}
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			if (ValidateChildren(ValidationConstraints.Enabled))
			{
                if (edit)
                {
					EditJob();
                }
                else
                {
					var job = CreateJob();
					SubJob subjob = null;

					if (JobType != JobType.NonCountableTask)
					{
						subjob = CreateSubJob(job.Job_ID, 0);
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

		private SubJob GetSubjob(int newID)
		{
			return CreateSubJob(newID, 0);
		}

		Job CreateJob()
		{
			return new Job()
			{
				Job_ID = JobRepository.GetLastID() + 1,
				Name = JobName,
				Category = (int)JobCategory,
				Date_Added = DateTime.Now,
				Deadline = cboDeadline.SelectedIndex == 0 ? (DateTime?)null : Deadline,
				Frequency = (int)Frequency,
				Importance = 0,
				Completed = false,
				Job_Type = (int)JobType,
				Est_Time = TimeToComplete,
			};
		}

		SubJob CreateSubJob(int id, int total)
		{
			int count = TotalItems - Offset;

			int days = (int)nudDays.Value;
			double perDay = (double)count / days;

			return new SubJob()
			{
				Job_ID = id,
				Count = TotalItems,
				Per_Day = perDay,
				Total_Done = total,
				Base_Count = Offset,
			};
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
		#endregion

		#region Validation
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
		#endregion


		#region GUI Stuff
		private void AddEditJobView_Load(object sender, EventArgs e)
		{
            if (edit)
            {
				JobName = Job.Name.Trim();
				JobCategory = (JobCat)Job.Category;
				Frequency = (Frequency)Job.Frequency;
				TimeToComplete = Job.Est_Time;
				if (Job.Deadline != null) Deadline = (DateTime)Job.Deadline;
				JobType = (JobType)Job.Job_Type;
                if (Sub != null)
				{
					if (Sub.Base_Count != null) Offset = (int)Sub.Base_Count;
					TotalItems = Sub.Count;
				}
			}
		}

		private void AddEditJobView_Shown(object sender, EventArgs e)
		{

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
			if (JobType == JobType.NonCountableTask)
			{
				nudOffset.Enabled = false;
				nudTotalItems.Enabled = false;
			}
			else
			{
				nudOffset.Enabled = true;
				nudTotalItems.Enabled = true;
			}

			if (JobType != JobType.NonCountableTask)
			{
				cboDeadline.SelectedIndex = 1;
			}
		}

		private void nudDays_ValueChanged(object sender, EventArgs e)
		{
			MatchDateValues((int)nudDays.Value);

			if ((int)nudTotalItems.Value != 0)
			{
				CountValueChanged();
			}
		}

		private void dtpDeadline_ValueChanged(object sender, EventArgs e)
		{
			MatchDateValues(Deadline);

			if ((int)nudTotalItems.Value != 0)
			{
				CountValueChanged();
			}
		}

		private void MatchDateValues(int days)
		{
			var deadline = DateTime.Now.AddDays(days);
			if (!deadline.Equals(Deadline))
			{
				Deadline = deadline;
			}
		}

		private void MatchDateValues(DateTime deadline)
		{
			int days = (int)(deadline.Date - DateTime.Now.Date).TotalDays;
			if (days != (int)nudDays.Value)
			{
				nudDays.Value = days;
			}
		}

		private void nudTotalItems_ValueChanged(object sender, EventArgs e)
		{
			CountValueChanged();
		}

		private void nudOffset_ValueChanged(object sender, EventArgs e)
		{
			CountValueChanged();
		}

        #endregion


    }
}
