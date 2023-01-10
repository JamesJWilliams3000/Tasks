using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks
{
	public class ViewTasksPresenter
	{
		IViewTasksView view;

        Date Date;
        ICollection<Job> Jobs;
        ICollection<DatesJob> CompletedJobs;
        public ViewTasksPresenter(IViewTasksView view)
        {
            this.view = view;

            //db file
            string path = @"C:\Users\shuka\Files\Programming\Tasks\Tasks\bin";
            AppDomain.CurrentDomain.SetData("DataDirectory", path);

            view.OnNewDayClicked += View_OnNewDayClicked;

            InitializeDataFromDB();
        }

        private void View_OnNewDayClicked(object sender, EventArgs e)
        {
            DateRepository.Add();

            //update view
            InitializeDataFromDB();
            view.UpdateView();
        }

        void InitializeDataFromDB()
        {
            using (var context = new JOBSEntities())
            {
                if (context.Dates.Count() == 0)
                {
                    //create day if none exist
                    DateRepository.Add();
                }
            }

            Date = DateRepository.GetLastDate();
            Jobs = JobRepository.GetJobs(Date.Date_ID);
            CompletedJobs = JobRepository.GetCompletedJobs(Date.Date_ID);          

            view.Date = Date;
            view.Jobs = Jobs;
            view.CompletedJobs = CompletedJobs;
            view.rows = GetCalenderRows();

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

        public void Show()
        {
            view.ShowView();
        }
    }
}
