using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks
{
    class OverlookedJob
    {
        public Job Job { get; set; }

        public double PercentDone { get; set; }

        public OverlookedJob(Job job, int amountCompleted, DateTime date)
        {
            Job = job;

            double days = (date - job.Date_Added).TotalDays;
            PercentDone = amountCompleted / days;
        }
    }
}
