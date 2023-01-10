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
    public partial class Testing : Form
    {
        public Testing()
        {
            InitializeComponent();

            //var cal = new CalenderTable(TestRows());            
            //Controls.Add(cal);
            //cal.Show();
            //cal.sizeDGV();

           

        }


        #region Testing Calender
        Date TestDate()
        {
            return new Date()
            {
                Date_ID = 1,
                Date1 = DateTime.Parse("06/07/2019")

            };
        }

        List<JobCalenderRow> TestRows()
        {
            List<JobCalenderRow> rows = new List<JobCalenderRow>();
            

            for (int i = 0; i < 3; i++)
            {
                rows.Add(new JobCalenderRow(TestJobs()[i], TestSubJobs()[i], TestDate()));
            }

            return rows;
        }

        List<Job> TestJobs()
        {
            List<Job> jobs = new List<Job>();
            jobs.Add(new Job()
            {
                Job_ID = 2,
                Name = "Music",
                Category = 0,
                Last_Finished = DateTime.Now,
                Date_Added = DateTime.Parse("06/07/2019"),
                Deadline = DateTime.Parse("06/30/2019"),
                Frequency = 1,
                Job_Type = 3,
                Est_Time = 10
            });

            jobs.Add(new Job()
            {
                Job_ID = 3,
                Name = "Bkmks",
                Category = 0,
                Last_Finished = DateTime.Now,
                Date_Added = DateTime.Now.AddDays(-2),
                Deadline = DateTime.Now.AddDays(32),
                Frequency = 1,
                Job_Type = 1,
                Est_Time = 10
            });

            jobs.Add(new Job()
            {
                Job_ID = 4,
                Name = "NatGeo",
                Category = 0,
                Last_Finished = DateTime.Now,
                Date_Added = DateTime.Now.AddDays(-2),
                Deadline = DateTime.Now.AddDays(32),
                Frequency = 1,
                Job_Type = 4,
                Est_Time = 10
            });


            return jobs;
        }

        List<SubJob> TestSubJobs()
        {
            List<SubJob> subjobs = new List<SubJob>();
            subjobs.Add(new SubJob()
            {
                Job_ID = 2,
                Count = 257,
                Per_Day = 11.17,
                Total_Done = 0
            });

            subjobs.Add(new SubJob()
            {
                Job_ID = 3,
                Count = 110,
                Per_Day = 6.3,
                Total_Done = 50
            });

            subjobs.Add(new SubJob()
            {
                Job_ID = 4,
                Count = 110,
                Per_Day = 6.5,
                Total_Done = 50,
                Page_Number = 50
            });

            return subjobs;
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = richTextBox1.Rtf;
        }
    }
}
