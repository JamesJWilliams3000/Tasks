using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks
{
    public class JobRepository
    {
        public static void Add(Job job)
        {
            using (var context = new JOBSEntities())
            {
                context.Jobs.Add(job);
                context.SaveChanges();
            }
        }

        public static void Add(Job job, SubJob subJob)
        {
            using (var context = new JOBSEntities())
            {
                context.Jobs.Add(job);
                context.SubJobs.Add(subJob);
                context.SaveChanges();
            }
        }

        public static SubJob AddMusicSubJob(int jobID, int count, double perDay)
        {
            using (var context = new JOBSEntities())
            {
                SubJob sub = new SubJob()
                {
                    Job_ID = jobID,
                    Count = count,
                    Per_Day = perDay,
                    Total_Done = 0
                };

                context.SubJobs.Add(sub);
                context.SaveChanges();

                return sub;
            }
        }

        public static void AddCurrentMusic(List<Current_Music> music)
        {
            using (var context = new JOBSEntities())
            {
                context.Current_Music.AddRange(music);
                context.SaveChanges();
            }
        }

        public static int GetLastCurrentMusicID()
        {
            using (var context = new JOBSEntities())
            {
                if (context.Current_Music.Count() > 0)
                {
                    return context.Current_Music.OrderByDescending(x => x.Job_ID).FirstOrDefault().Job_ID;
                }
                else
                {
                    return -1;
                }
            }
        }

        public static Job AddMusicJob(string name, DateTime deadline)
        {
            using (var context = new JOBSEntities())
            {
                Job job = new Job()
                {
                    Job_ID = GetLastID() + 1,
                    Name = name,
                    Category = 0,
                    Date_Added = DateTime.Now,
                    Deadline = deadline,
                    Frequency = 1,
                    Importance = 0,
                    Completed = false,
                    Job_Type = 3,
                    Est_Time = 0,
                };

                context.Jobs.Add(job);
                context.SaveChanges();

                return job;
            }
        }

        public static void AddToDate(int dateID, Job job, SubJob subjob)
        {
            using (var context = new JOBSEntities())
            {
                context.DatesJobs.Add(new DatesJob() { Date_ID = dateID, Job_ID = job.Job_ID, Completed = false });
                if (subjob != null)
                    context.DatesSubJobs.Add(new DatesSubJob() { Date_ID = dateID, Job_ID = subjob.Job_ID, Total_Done = 0 });

                context.SaveChanges();
            }
        }

        public static void Add(List<Job> jobs)
        {
            using (var context = new JOBSEntities())
            {
                context.Jobs.AddRange(jobs);
                context.SaveChanges();
            }
        }

        public static int GetLastID()
        {
            using (var context = new JOBSEntities())
            {
                if (context.Jobs.Count() > 0)
                {
                    return context.Jobs.OrderByDescending(x => x.Job_ID).FirstOrDefault().Job_ID;
                }
                else
                {
                    return -1;
                }
            }
        }

        public static List<Job> GetJobs(int dateID)
        {
            using (var context = new JOBSEntities())
            {
                var jobids = context.DatesJobs.Where(x => x.Date_ID == dateID).Select(a => a.Job_ID).ToList();

                List<Job> jobs = new List<Job>();
                foreach (var id in jobids)
                {
                    jobs.Add(context.Jobs.First(x => x.Job_ID == id));
                }

                return jobs;
            }
        }

        public static List<SubJob> GetSubJobs(int dateID)
        {
            using (var context = new JOBSEntities())
            {
                var jobids = context.DatesSubJobs.Where(x => x.Date_ID == dateID).Select(a => a.Job_ID).ToList();

                List<SubJob> subs = new List<SubJob>();
                foreach (var id in jobids)
                {
                    subs.Add(context.SubJobs.First(x => x.Job_ID == id));
                }

                return subs;
            }
        }

        public static List<DatesJob> GetCompletedJobs(int dateID)
        {
            using (var context = new JOBSEntities())
            {
                return context.DatesJobs.Where(x => x.Date_ID == dateID && x.Completed).ToList();
            }
        }

        public static void Delete(Job job)
        {
            using (var context = new JOBSEntities())
            {
                if (context.Entry(job).State == EntityState.Detached)
                {
                    context.Jobs.Attach(job);
                }

                context.Jobs.Remove(job);
                context.SaveChanges();
            }
        }

        public static void Delete(List<Job> jobs)
        {
            using (var context = new JOBSEntities())
            {
                jobs.ForEach(x =>
                {
                    if (context.Entry(x).State == EntityState.Detached)
                    {
                        context.Jobs.Attach(x);
                    }
                });

                context.Jobs.RemoveRange(jobs);
                context.SaveChanges();
            }
        }

        public static void SetComplete(Job job, bool completed)
        {
            using (var context = new JOBSEntities())
            {
                if (context.Entry(job).State == EntityState.Detached)
                {
                    context.Jobs.Attach(job);
                }

                job.Completed = completed;

                if (job.Job_Type == 3)
                {
                    var albums = context.Current_Music.Where(x => x.Job_ID == job.Job_ID).ToList();
                    context.Current_Music.RemoveRange(albums);
                }

                context.SaveChanges();
            }
        }

        public static double GetPerDay(int JobID)
        {
            using (var context = new JOBSEntities())
            {
                return context.SubJobs.First(x => x.Job_ID == JobID).Per_Day;
            }
        }

        public static SubJob GetSubJob(int jobID)
        {
            using (var context = new JOBSEntities())
            {
                return context.SubJobs.First(x => x.Job_ID == jobID);
            }
        }

        public static void SaveDatesSubJob(int jobID, int dateID, int totalDone)
        {
            using (var context = new JOBSEntities())
            {
                DatesSubJob sub = context.DatesSubJobs.First(x => x.Date_ID == dateID && x.Job_ID == jobID);
                sub.Total_Done = totalDone;

                context.SaveChanges();
            }
        }

        public static void SaveSubJob(SubJob sub, int totalDone)
        {
            using (var context = new JOBSEntities())
            {                                
                if (context.Entry(sub).State == EntityState.Detached)
                {
                    context.SubJobs.Attach(sub);
                }

                //if (pageNumber != null)
                //{
                //    sub.Page_Number = (int)pageNumber;
                //}

                sub.Total_Done = totalDone;

                context.SaveChanges();
            }
        }

        public static string GetPath(int ID)
        {
            using (var context = new JOBSEntities())
            {
                return context.Musics.First(x => x.Music_ID == ID).Path.Trim();
            }
        }

        private static SubJob AddBookSubJob(int jobID, double perDay, int startPage, int endPage)
        {
            using (var context = new JOBSEntities())
            {
                SubJob sub = new SubJob()
                {
                    Job_ID = jobID,
                    Count = endPage,
                    Per_Day = perDay,
                    Total_Done = 0,
                    Page_Number = startPage,
                    Base_Count = startPage
                };

                context.SubJobs.Add(sub);
                context.SaveChanges();

                return sub;
            }
        }

        public static Job AddBook(string name, DateTime deadline, int estTime, double perDay, int startPage, int endPage)
        {
            using (var context = new JOBSEntities())
            {
                Job job = new Job()
                {
                    Job_ID = GetLastID() + 1,
                    Name = name,
                    Category = 0,
                    Date_Added = DateTime.Now,
                    Deadline = deadline,
                    Frequency = 1,
                    Importance = 0,
                    Completed = false,
                    Job_Type = 4,
                    Est_Time = estTime,
                };

                context.Jobs.Add(job);
                context.SaveChanges();

                var sub = AddBookSubJob(job.Job_ID, perDay, startPage, endPage);

                if (DateRepository.GetLastDate().Date1.Date == DateTime.Now.Date)
                {
                    AddToDate(DateRepository.GetLastDate().Date_ID, job, sub);
                }

                return job;
            }
        }

    }
}