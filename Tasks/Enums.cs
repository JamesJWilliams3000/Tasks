using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks
{
    public enum JobCat { Media, Chores, Outside, School }

    public enum Frequency { Once, Daily, Weekly, BiWeekly, Monthly, Whenever }

    public enum JobType { NonCountableTask, DailyCountable, TotalCountUp, TotalCountDown }

    public static class Enums
    {
        public static string[] GetJobTypeNames()
		{
            return new string[]{"Non-Countable Task", "Countable - Daily (Incremental)", 
                "Countable - Total (Count Up)", "Countable - Total (Count Down)" };
		}

    }
}
