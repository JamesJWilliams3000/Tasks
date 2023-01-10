using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks
{
    public static class DateRepository
    {
        public static void Add()
        {
            Date date = new Date()
            {
                Date1 = DateTime.Now,
                Date_ID = GetNewID()
            };
            var jobs = GetNewJobs();

            List<DatesJob> datesjobs = new List<DatesJob>();
            List<DatesSubJob> datesSubjobs = new List<DatesSubJob>();
            foreach (Job job in jobs)
            {
                datesjobs.Add(new DatesJob() {Date_ID = date.Date_ID, Job_ID = job.Job_ID, Completed = false });

                if (job.Job_Type > 0)
                {
                    datesSubjobs.Add(new DatesSubJob() { Date_ID = date.Date_ID, Job_ID = job.Job_ID, Total_Done = 0 });
                }
            }

            using (var context = new JOBSEntities())
            {
                context.Dates.Add(date);
                if(datesjobs.Count() > 0)    context.DatesJobs.AddRange(datesjobs);
                if(datesSubjobs.Count() > 0) context.DatesSubJobs.AddRange(datesSubjobs);

                context.SaveChanges();
            }
        }        

        public static int GetLastID()
        {
            using (var context = new JOBSEntities())
            {
                if (context.Dates.Count() > 0)
                {
                    return context.Dates.OrderByDescending(x => x.Date_ID).FirstOrDefault().Date_ID;
                }
                else
                {
                    return -1;
                }
            }
        }

        public static int GetNewID()
        {
            return GetLastID() + 1;
        }

        public static Date GetDate(DateTime date)
        {
            using (var context = new JOBSEntities())
            {
                return context.Dates.Where(x => DbFunctions.TruncateTime(x.Date1) == DbFunctions.TruncateTime(date)).FirstOrDefault();
            }
        }

        public static Date GetLastDate()
        {
            using (var context = new JOBSEntities())
            {
                return context.Dates.OrderByDescending(x => x.Date_ID).FirstOrDefault();
            }
        }

        public static List<Job> GetLastDaysJobs()
        {
            using (var context = new JOBSEntities())
            {
                List<Job> jobs = new List<Job>();

                if (context.Dates.Count() > 0)
                {
                    var lastDateID = context.Dates.OrderByDescending(x => x.Date_ID).FirstOrDefault().Date_ID;
                    var jobsID = context.DatesJobs.Where(x => x.Date_ID == lastDateID && !x.Completed).Select(a => a.Job_ID).ToList();


                    foreach (var ID in jobsID)
                    {
                        Job j = context.Jobs.Where(x => x.Job_ID == ID).First();
                        if (j.Frequency > 1 && j.Frequency < 5)
                        {
                            jobs.Add(j);
                        }
                    }
                }

                return jobs;
            }
        }

        public static List<Job> GetNewJobs()
        {
            DateTime today = DateTime.Now;
            int weekNumber = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(today, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

            Date lastDate = GetLastDate();
            List<Job> newJobs = new List<Job>();
            newJobs.AddRange(GetLastDaysJobs());
            using (var context = new JOBSEntities())
            {
                //daily/once jobs
                newJobs.AddRange(context.Jobs.Where(x => !x.Completed && x.Frequency < 2).ToList());

                //whenever
                newJobs.AddRange(context.Jobs.Where(x => !x.Completed && x.Frequency == 5).ToList());

                //add monthly
                if (today.Day == 24)
                {
                    newJobs.AddRange(context.Jobs.Where(x => !x.Completed && x.Frequency == 4).ToList());
                }
                else if( NeedMonthly(lastDate) )
                {
                    newJobs.AddRange(context.Jobs.Where(x => !x.Completed && x.Frequency == 4).ToList());
                }

                //weekly/biweekly
                if (today.DayOfWeek == DayOfWeek.Friday)
                {
                    //add weekly
                    newJobs.AddRange(context.Jobs.Where(x => !x.Completed && x.Frequency == 2).ToList());

                    if ((weekNumber % 2) > 0)
                    {
                        //add bi weekly
                        newJobs.AddRange(context.Jobs.Where(x => !x.Completed && x.Frequency == 3).ToList());
                    }
                }
                else if ( NeedWeekly(lastDate) )
                {
                    //add weekly
                    newJobs.AddRange(context.Jobs.Where(x => !x.Completed && x.Frequency == 2).ToList());


                    if ( NeedBiWeekly(lastDate) )
                    {
                        //add bi weekly
                        newJobs.AddRange(context.Jobs.Where(x => !x.Completed && x.Frequency == 3).ToList());
                    }
                }
            }

            return newJobs.GroupBy(x => x.Job_ID).Select(a => a.First()).ToList();
        }

        public static bool DateExists(DateTime date)
        {
            using (var context = new JOBSEntities())
            {
                Date d = context.Dates.Where(x => DbFunctions.TruncateTime(x.Date1) == DbFunctions.TruncateTime(date)).FirstOrDefault();
                if (d != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static void ChangeCompleted(int JobID, DateTime date, bool finished )
        {
            int dateID = GetDate(date).Date_ID;

            using (var context = new JOBSEntities())
            {
                DatesJob d = context.DatesJobs.Where(x => x.Date_ID == dateID && x.Job_ID == JobID).FirstOrDefault();
                d.Completed = finished;

                Job job = context.Jobs.First(x => x.Job_ID == JobID);
                job.Last_Finished = date;
                context.SaveChanges();

            }
        }

        public static void Delete(Date date)
        {
            using (var context = new JOBSEntities())
            {
                if (context.Entry(date).State == EntityState.Detached)
                {
                    context.Dates.Attach(date);
                }

                context.Dates.Remove(date);
                context.SaveChanges();
            }
        }

        public static bool NeedMonthly(Date lastDate)
        {
            int nowday = DateTime.Now.Day;
            int lastday = lastDate.Date1.Day;

            if (lastday < 24 && 24 < nowday )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool NeedWeekly(Date lastDate)
        {
            DateTime nowday = DateTime.Now;
            int diff = (nowday - lastDate.Date1).Days;

            if (diff >= 7)
            {
                return true;
            }
            else
            {
                //var startDate = lastDate.Date1;
                //for (int i = 0; i < diff; i++)
                //{
                //    var date = startDate.AddDays(i);
                //    if (date.DayOfWeek == DayOfWeek.Friday && i != 0)
                //    {
                //        return true;
                //    }
                //}

                var startDate = lastDate.Date1;
                var testDate = startDate;
                //for (int i = 0; DateTime.Compare(testDate, nowday) < 0; i++)
                for (int i = 0; testDate.Date.CompareTo(nowday.Date) < 0; i++)
                {
                    testDate = startDate.AddDays(i);
                    if (testDate.DayOfWeek == DayOfWeek.Friday && i != 0)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public static bool NeedBiWeekly(Date lastDate)
        {           
            if (NeedWeekly(lastDate))
            {
                //bool need = false;

                //int diff = (DateTime.Now - lastDate.Date1).Days;
                //var startDate = lastDate.Date1;
                //for (int i = 0; i < diff; i++)
                //{
                //    var date = startDate.AddDays(i);
                //    if (date.DayOfWeek == DayOfWeek.Friday)
                //    {
                //        int weekNumber = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

                //        if ((weekNumber % 2) > 0)
                //        {
                //            //add bi weekly
                //            return true;
                //        }
                //    }
                //}

                //return need;

                bool need = false;

                DateTime nowday = DateTime.Now;
                var startDate = lastDate.Date1;
                var testDate = startDate;
                for (int i = 0; testDate.Date.CompareTo(nowday.Date) < 0; i++)
                {
                    testDate = startDate.AddDays(i);
                    if (testDate.DayOfWeek == DayOfWeek.Friday && i != 0)
                    {
                        int weekNumber = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(testDate, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

                        if ((weekNumber % 2) > 0)
                        {
                            //add bi weekly
                            return true;
                        }
                    }
                }

                return need;

            }
            else
            {
                return false;
            }
        }
    }
}
