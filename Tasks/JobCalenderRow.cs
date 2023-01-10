using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks
{
    public class JobCalenderRow
    {
        public Job Job { get; set; }

        public string Name { get; set; }

        public SubJob SubJob { get; set; }

        public Date Date { get; set; }

        public double Need { get; set; }

        public string NeedString { get; set; }

        public int Left { get; set; }

        public DateTime EstDeadline { get; set; }

        public double AjustedBaseline { get; set; }

        public int DaysUntil { get; set; }

        public string DaysUntilColumn { get; set; }

        public int DoneToday { get; set; }

        public int TotalDone { get; set; }

        public double Baseline { get; set; }

        public int DoneYesterday { get; set; }

        public int Goal { get; set; }

        public int Even { get; set; }

        public JobCalenderRow(Job job, SubJob subJob, Date date)
        {
            Job = job;
            Name = job.Name.Trim();
            SubJob = subJob;
            Date = date;

            DoneToday = Job.Job_Type == 4 ? (int)SubJob.Page_Number : 0;            
            Baseline = subJob.Per_Day;
            TotalDone = subJob.Total_Done;
            Need = CalcNeed();
            Left = CalcLeft();
            EstDeadline = CalcEstFinishedDate();            
            DaysUntil = CalcDaysUntil();
            AjustedBaseline = CalcAdjBaseline();
            DaysUntilColumn = GetDaysUntilColumn();
            DaysUntil = CalcDaysUntil();

            if (job.Job_Type == 2)
            {
                DoneYesterday = subJob.Count - subJob.Total_Done;
                DoneToday = DoneYesterday;
            }

            Goal = CalcGoal();
            Even = CalcEven();
        }

        public int CalcEven()
        {
            if (Job.Job_Type == 0)
            {
                if (Need > 0)
                {
                    return 0;
                }
                else
                {
                    return (int)Math.Ceiling(Need * -1);
                }
            }
            else if (Job.Job_Type == 1)
            {
                if (Need > 0)
                {
                    return 0;
                }
                else
                {
                    return Left - (int)Math.Ceiling(Need * -1);
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public int CalcGoal()
        {
            if (Job.Job_Type == 0)
            {
                return SubJob.Count;
            }
            else
            {
                if (Job.Job_Type == 2)
                {
                    return DoneToday - (int)Math.Ceiling((decimal)AjustedBaseline);
                }
                else if (Job.Job_Type == 4)
                {
                    return DoneToday + (int)Math.Ceiling((decimal)AjustedBaseline);
                }
                else
                {
                    return 0;
                }
            }
        }

        public int CalcLikesDifference()
        {
            return DoneYesterday - DoneToday;
        }

        public int CalcTotalDone()
        {
            if (Job.Job_Type == 2)
            {
                return SubJob.Total_Done + CalcLikesDifference();
            }
            else if (Job.Job_Type == 4)
            {
                return DoneToday;
            }
            else
            {
                return SubJob.Total_Done + DoneToday;
            }
        }

        public int CalcDoneToday()
        {
            if (Job.Job_Type == 2)
            {
                return CalcLikesDifference();
            }
            else if (Job.Job_Type == 4)
            {
                return DoneToday - (int)SubJob.Page_Number;
            }
            else
            {
                return DoneToday;
            }
        }

        public int? GetPageNumber()
        {
            if (Job.Job_Type == 4)
            {
                return DoneToday;
            }
            else
            {
                return null;
            }

        }

        public double CalcNeed()
        {
            if (Job.Job_Type == 4)
            {
                if (SubJob.Base_Count != null)
                {
                    return TotalDone - (CalcDaysSinceStart() * SubJob.Per_Day) - (int)SubJob.Base_Count;
                }
                else
                {
                    return TotalDone - (CalcDaysSinceStart() * SubJob.Per_Day);
                }
            }
            else
            {
                return TotalDone - (CalcDaysSinceStart() * SubJob.Per_Day);
            }
        }

        string GetDaysUntilColumn()
        {
            DaysUntil = CalcDaysUntil();
            return string.Format("{0:D2} - {1}", DaysUntil, ((DateTime)Job.Deadline).Date.ToString("MM/dd/yyyy"));
        }

        int CalcDaysSinceStart()
        {
            return (int)(Date.Date1.Date - Job.Date_Added.Date).TotalDays + 1;
        }

        public int CalcLeft()
        {
            if (SubJob.Base_Count != null)
            {              
                if (Job.Job_Type == 4)
                {
                    return SubJob.Count - TotalDone;
                }
                else
                {
                    return SubJob.Count - (int)SubJob.Base_Count - TotalDone;
                }
            }
            else
            {
                return SubJob.Count - TotalDone;
            }
        }

        public DateTime CalcEstFinishedDate()
        {
            int days = (int)Math.Ceiling((decimal)(Left / SubJob.Per_Day));

            return Date.Date1.Date.AddDays(days);
        }

        public double CalcAdjBaseline()
        {
            return Left / (double)DaysUntil;
        }

        int CalcDaysUntil()
        {
            return (int)(((DateTime)Job.Deadline).Date - Date.Date1.Date).TotalDays;
        }
    }
}
