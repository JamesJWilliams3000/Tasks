using System;
using System.Collections.Generic;
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
    }
}
