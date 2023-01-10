﻿using System;
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
    public partial class frmViewJobs : Form
    {
        public Date Date { get; set; }
        public List<Job> Jobs { get; set; }
        public List<DatesJob> CompletedJobs { get; set; }

        int count = -1;

        public frmViewJobs()
        {
            InitializeComponent();
            SuspendLayout();

            //string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //string path = (System.IO.Path.GetDirectoryName(executable));
            string path = @"C:\Users\shuka\Files\Programming\Tasks\Tasks\bin";
        
            AppDomain.CurrentDomain.SetData("DataDirectory", path);

            //using (var context = new JOBSEntities(connectionString))
            using (var context = new JOBSEntities())
            {
                count = context.Dates.Count();
            }

            if (count > 0)
            {
                Date = DateRepository.GetLastDate();
                Jobs = JobRepository.GetJobs(Date.Date_ID);
                CompletedJobs = JobRepository.GetCompletedJobs(Date.Date_ID);

                lblDate.Text = Date.Date1.ToString("MM-dd-yyyy");
                SetLblCount(Jobs.Sum(x => x.Est_Time), Jobs.Count());
                SetLblBiWeekly();

                List<UpdatedJobCalendarRow> rows = GetCalenderRows();
                if (rows.Count() > 0)
                {
                    var cal = new CalenderTable(rows);
                    flpPage.Controls.Add(cal);
                    cal.Show();
                    cal.sizeDGV();
                }

                //using (var context = new JOBSEntities())
                //{
                //    if (context.Folders.Count() > 0)
                //    {
                //        //add folders panel
                //    }
                //}

                var folder = new FolderCounts();
                flpPage.Controls.Add(folder);
                folder.Show();

            }            

            ResumeLayout();
        }

        void SetLblBiWeekly()
        {
            int weekNumber = System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(Date.Date1, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday);

            if ((weekNumber % 2) > 0)
            {
                //is bi weekly
                lblBiWeekly.Text = "B:Y";
            }
            else
            {
                lblBiWeekly.Text = "B:N";
            }
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

        List<UpdatedJobCalendarRow> GetCalenderRows()
        {
            var subs = JobRepository.GetSubJobs(Date.Date_ID).OrderBy(x => x.Job_ID).ToList();
            var jobs = Jobs.Where(a => a.Job_Type > 0).OrderBy(x => x.Job_ID).ToList();

            var rows = new List<UpdatedJobCalendarRow>();


            if (subs.Count() > 0)
            {
                for (int i = 0; i < subs.Count(); i++)
                {
                    rows.Add(new UpdatedJobCalendarRow(Date, jobs[i], subs[i]));
                }
            }

            return rows;
        }

        private void frmViewJobs_Load(object sender, EventArgs e)
        {
            SuspendLayout();
            string[] sort = {"Category (Where)", "Last Finished", "Date Added", "Deadline", "Importance", "Frequency", "Est. Time", "Overlooked" };
            cboSort.BindingContext = new BindingContext();
            cboSort.DataSource = sort;
            cboSort.SelectedIndex = 5;

            if (count == 0)
            {
                NewDay();
            }

            ResumeLayout();
        }

        void ShowJobs(int sortID)
        {
            SuspendLayout();
            flpTask.Controls.Clear();
            var groups = SortJobs(sortID).ToArray();
            flpTask.Controls.AddRange(groups);
            foreach (var group in groups)
            {
                group.Show();
            }
            ResumeLayout();
        }

        List<JobGroup> SortJobs(int sortID)
        {
            string[] sort = { "Category (Where)", "Last Finished", "Date Added", "Deadline", "Importance", "Frequency", "Est. Time", "Overlooked" };
            List<JobGroup> groups = new List<JobGroup>();
            switch (sortID)
            {
                case 0:
                    groups = SortByCategory();
                    break;
                case 1:
                    groups = SortByLastFinished();
                    break;
                case 2:
                    groups = SortByDateAdded();
                    break;
                case 3:
                    groups = SortByDeadline();
                    break;
                case 4:
                    groups = SortByImportance();
                    break;
                case 5:
                    groups = SortByFrequency();
                    break;
                case 6:
                    groups = SortByEstTime();
                    break;
                case 7:
                    groups = SortByOverlooked();
                    break;
                default:
                    break;
            }

            return groups;
        }

        List<JobGroup> SortByCategory()
        {
            List<JobGroup> groups = new List<JobGroup>();
            for (int i = 0; i < 4; i++)
            {
                var jobs = Jobs.Where(x => x.Category == i).ToList();
                if(jobs.Count() > 0) groups.Add( new JobGroup(jobs, ( (JobCat)i).ToString(), Date.Date1, CompletedJobs) );
            }

            return groups;
        }

        List<JobGroup> SortByLastFinished()
        {
            List<JobGroup> groups = new List<JobGroup>();
            List<Job> day = new List<Job>();
            List<Job> week = new List<Job>();
            List<Job> twoweek = new List<Job>();
            List<Job> month = new List<Job>();
            List<Job> sixMonths = new List<Job>();
            List<Job> year = new List<Job>();
            List<Job> twoYear = new List<Job>();
            List<Job> never = new List<Job>();

            var jobsByDate = Jobs.OrderByDescending(x => x.Last_Finished).ToList();

            foreach (var job in jobsByDate)
            {
                if (job.Last_Finished != null)
                {
                    int days = (int)(Date.Date1.Date - (DateTime)job.Last_Finished).TotalDays;

                    if (days < 2)
                    {
                        day.Add(job);
                    }
                    else if (days < 8)
                    {
                        week.Add(job);
                    }
                    else if (days >= 8 && days < 15)
                    {
                        twoweek.Add(job);
                    }
                    else if (days >= 15 && days < 31)
                    {
                        month.Add(job);
                    }
                    else if (days >= 31 && days < 180)
                    {
                        sixMonths.Add(job);
                    }
                    else if (days >= 180 && days < 365)
                    {
                        year.Add(job);
                    }
                    else
                    {
                        twoYear.Add(job);
                    }
                }
                else
                {
                    never.Add(job);
                }
            }

            never = never.OrderByDescending(x => x.Frequency).ToList();

            if (day.Count() > 0) groups.Add(new JobGroup(day, "Yesterday", Date.Date1, CompletedJobs));
            if (week.Count() > 0) groups.Add(new JobGroup(week, "Within A Week", Date.Date1, CompletedJobs));
            if (twoweek.Count() > 0) groups.Add(new JobGroup(twoweek, "Within Two Weeks", Date.Date1, CompletedJobs));
            if (month.Count() > 0) groups.Add(new JobGroup(month, "Within A Month", Date.Date1, CompletedJobs));
            if (sixMonths.Count() > 0) groups.Add(new JobGroup(sixMonths, "Within Six Months", Date.Date1, CompletedJobs));
            if (year.Count() > 0) groups.Add(new JobGroup(year, "Within A Year", Date.Date1, CompletedJobs));
            if (twoYear.Count() > 0) groups.Add(new JobGroup(twoYear, "Over A Year Ago", Date.Date1, CompletedJobs));
            if (never.Count() > 0) groups.Add(new JobGroup(never, "Never Completed", Date.Date1, CompletedJobs));

            return groups;
        }

        List<JobGroup> SortByDateAdded()
        {
            List<JobGroup> groups = new List<JobGroup>();
            List<Job> week = new List<Job>();
            List<Job> month = new List<Job>();
            List<Job> sixMonths = new List<Job>();
            List<Job> year = new List<Job>();
            List<Job> twoYear = new List<Job>();
            List<Job> never = new List<Job>();

            foreach (var job in Jobs)
            {
                int days = (int)(Date.Date1.Date - job.Date_Added).TotalDays;

                if (days < 8)
                {
                    week.Add(job);
                }
                else if (days >= 8 && days < 31)
                {
                    month.Add(job);
                }
                else if (days >= 31 && days < 180)
                {
                    sixMonths.Add(job);
                }
                else if (days >= 180 && days < 365)
                {
                    year.Add(job);
                }
                else
                {
                    twoYear.Add(job);
                }

            }

            if (week.Count() > 0) groups.Add(new JobGroup(week, "Within A Week", Date.Date1, CompletedJobs));
            if (month.Count() > 0) groups.Add(new JobGroup(month, "Within A Month", Date.Date1, CompletedJobs));
            if (sixMonths.Count() > 0) groups.Add(new JobGroup(sixMonths, "Within Six Months", Date.Date1, CompletedJobs));
            if (year.Count() > 0) groups.Add(new JobGroup(year, "Within A Year", Date.Date1, CompletedJobs));
            if (twoYear.Count() > 0) groups.Add(new JobGroup(twoYear, "Over A Year Ago", Date.Date1, CompletedJobs));

            return groups;
        }

        List<JobGroup> SortByDeadline()
        {
            List<JobGroup> groups = new List<JobGroup>();
            List<Job> week = new List<Job>();
            List<Job> month = new List<Job>();
            List<Job> sixMonths = new List<Job>();
            List<Job> year = new List<Job>();
            List<Job> twoYear = new List<Job>();
            List<Job> never = new List<Job>();

            foreach (var job in Jobs)
            {
                if (job.Deadline != null)
                {
                    int days = (int)(Date.Date1.Date - (DateTime)job.Deadline).TotalDays;

                    if (days < 8)
                    {
                        week.Add(job);
                    }
                    else if (days >= 8 && days < 31)
                    {
                        month.Add(job);
                    }
                    else if (days >= 31 && days < 180)
                    {
                        sixMonths.Add(job);
                    }
                    else if (days >= 180 && days < 365)
                    {
                        year.Add(job);
                    }
                    else
                    {
                        twoYear.Add(job);
                    }
                }
                else
                {
                    never.Add(job);
                }

            }

            if (week.Count() > 0) groups.Add(new JobGroup(week, "Within A Week", Date.Date1, CompletedJobs));
            if (month.Count() > 0) groups.Add(new JobGroup(month, "Within A Month", Date.Date1, CompletedJobs));
            if (sixMonths.Count() > 0) groups.Add(new JobGroup(sixMonths, "Within Six Months", Date.Date1, CompletedJobs));
            if (year.Count() > 0) groups.Add(new JobGroup(year, "Within A Year", Date.Date1, CompletedJobs));
            if (twoYear.Count() > 0) groups.Add(new JobGroup(twoYear, "Over A Year From Now", Date.Date1, CompletedJobs));
            if (never.Count() > 0) groups.Add(new JobGroup(never, "No Deadline", Date.Date1, CompletedJobs));

            return groups;
        }

        List<JobGroup> SortByImportance()
        {
            List<JobGroup> groups = new List<JobGroup>();
            var jobs = Jobs.OrderBy(x => x.Importance).ToList();
            int end = (jobs.Count() / 4) + 1;

            List<Job> most = new List<Job>();
            List<Job> midmost = new List<Job>();
            List<Job> midleast = new List<Job>();
            List<Job> least = new List<Job>();

            for (int i = 0; i < jobs.Count(); i++)
            {
                if (i <= end)
                {
                    most.Add(jobs[i]);
                }
                else if (i > end && i <= end * 2)
                {
                    midmost.Add(jobs[i]);
                }
                else if (i > end * 2 && i <= end * 3)
                {
                    midleast.Add(jobs[i]);
                }
                else
                {
                    least.Add(jobs[i]);
                }
            }

            if (most.Count() > 0) groups.Add(new JobGroup(most, "Most", Date.Date1, CompletedJobs));
            if (midmost.Count() > 0) groups.Add(new JobGroup(midmost, "MidMost", Date.Date1, CompletedJobs));
            if (midleast.Count() > 0) groups.Add(new JobGroup(midleast, "MidLeast", Date.Date1, CompletedJobs));
            if (least.Count() > 0) groups.Add(new JobGroup(least, "Least", Date.Date1, CompletedJobs));

            return groups;
        }

        List<JobGroup> SortByFrequency()
        {
            List<JobGroup> groups = new List<JobGroup>();
            for (int i = 0; i < 6; i++)
            {
                var jobs = Jobs.Where(x => x.Frequency == i).ToList();
                if (jobs.Count() > 0) groups.Add(new JobGroup(jobs, ((Frequency)i).ToString(), Date.Date1, CompletedJobs));
            }

            return groups;
        }

        List<JobGroup> SortByEstTime()
        {
            List<JobGroup> groups = new List<JobGroup>();
            var jobs = Jobs.OrderByDescending(x => x.Est_Time).ToList();
            int end = (jobs.Count() / 4) + 1;

            List<Job> most = new List<Job>();
            List<Job> midmost = new List<Job>();
            List<Job> midleast = new List<Job>();
            List<Job> least = new List<Job>();

            for (int i = 0; i < jobs.Count(); i++)
            {
                if (i <= end)
                {
                    most.Add(jobs[i]);
                }
                else if (i > end && i <= end * 2)
                {
                    midmost.Add(jobs[i]);
                }
                else if (i > end * 2 && i <= end * 3)
                {
                    midleast.Add(jobs[i]);
                }
                else
                {
                    least.Add(jobs[i]);
                }
            }

            if (most.Count() > 0) groups.Add(new JobGroup(most, "Most", Date.Date1, CompletedJobs));
            if (midmost.Count() > 0) groups.Add(new JobGroup(midmost, "MidMost", Date.Date1, CompletedJobs));
            if (midleast.Count() > 0) groups.Add(new JobGroup(midleast, "MidLeast", Date.Date1, CompletedJobs));
            if (least.Count() > 0) groups.Add(new JobGroup(least, "Least", Date.Date1, CompletedJobs));

            return groups;
        }

        List<JobGroup> SortByOverlooked()
        {
            List<JobGroup> groups = new List<JobGroup>();
                      
            List<Job> most = new List<Job>();
            List<Job> midmost = new List<Job>();
            List<Job> midleast = new List<Job>();
            List<Job> least = new List<Job>();

            List<OverlookedJob> ovjobs = new List<OverlookedJob>();
            using (var context = new JOBSEntities())
            {
                ovjobs = Jobs.Where(x => x.Frequency == 1).Select(x => new OverlookedJob(x, context.DatesJobs.Count(a => a.Job_ID == x.Job_ID && a.Completed), Date.Date1)).ToList();
            }
            ovjobs = ovjobs.OrderBy(x => x.PercentDone).ToList();
            var jobs = ovjobs.Select(x => x.Job).ToList();
            int end = (jobs.Count() / 4) + 1;

            for (int i = 0; i < jobs.Count(); i++)
            {
                if (i <= end)
                {
                    most.Add(jobs[i]);
                }
                else if (i > end && i <= end * 2)
                {
                    midmost.Add(jobs[i]);
                }
                else if (i > end * 2 && i <= end * 3)
                {
                    midleast.Add(jobs[i]);
                }
                else
                {
                    least.Add(jobs[i]);
                }
            }

            if (most.Count() > 0) groups.Add(new JobGroup(most, "Most", Date.Date1, CompletedJobs));
            if (midmost.Count() > 0) groups.Add(new JobGroup(midmost, "MidMost", Date.Date1, CompletedJobs));
            if (midleast.Count() > 0) groups.Add(new JobGroup(midleast, "MidLeast", Date.Date1, CompletedJobs));
            if (least.Count() > 0) groups.Add(new JobGroup(least, "Least", Date.Date1, CompletedJobs));

            return groups;
        }

        private void frmViewJobs_Resize(object sender, EventArgs e)
        {
            flpPage.Width = Width - 100;
            pnlDateHeader.Width = flpPage.Width;
            flpTask.Width = flpPage.Width;

            flpPage.Height = Height - 70;

            if (flpPage.Controls.OfType<CalenderTable>().Count() > 0)
            {
                flpTask.Height = flpPage.Height - flpPage.Controls.OfType<CalenderTable>().ToList()[0].Height - flpPage.Controls.OfType<FolderCounts>().ToList()[0].Height - 70;
            }
            else
            {
                flpTask.Height = flpPage.Height - 50;
            }

            flpPage.Location = new Point(this.Width / 2 - flpPage.Width / 2, this.Height / 2 - flpPage.Height / 2);
        }

        private void cboSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowJobs(cboSort.SelectedIndex);
        }

        private void btnNewDay_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {               
                if (flpPage.Controls.OfType<CalenderTable>().Count() > 0)
                {
                    flpPage.Controls.OfType<CalenderTable>().ToList()[0].Save();
                }

                NewDay();
            }
        }

        void NewDay()
        {
            DateRepository.Add();
            OpenForm(new frmViewJobs());
        }

        void OpenForm(Form frm)
        {
            frm.MdiParent = this.MdiParent;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            frm.Show();
            Close();
        }
    }
}
