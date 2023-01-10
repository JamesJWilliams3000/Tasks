using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks
{
	public class UpdatedJobCalendarRow
	{
		string[] names = { "", "", "Need:", "Left:", "Baseline:", "Est. Date:", "Adj. Per Day:", "Days Left:", "Goal:", "Even:" };
		string[] values = { "Name", "DoneToday", "Need", "Left",
			"Baseline", "EstDeadline", "AjustedBaseline", "DaysUntilColumn", "Goal", "Even" };

		public int DateID;
		public UpdatedJobCalendarRow(Date today, Job job, SubJob subJob)
		{
			Job = job;
			SubJob = subJob;
			SubJob.Base_Count = SubJob.Base_Count == null ? 0 : SubJob.Base_Count;
			Name = job.Name.Trim();
			TotalDone = subJob.Total_Done;
			SetInitialDoneTodayValue();
			Today = today.Date1;
			DateID = today.Date_ID;
			Left = CalcLeft();
			Need = CalcNeed();
			goal = CalcGoal(DoneToday, (int)SubJob.Per_Day);
		}

		private int CalcGoal(int doneToday, int per_Day)
		{
			switch ((JobType)Job.Job_Type)
			{
				case JobType.DailyCountable:
					return Left - per_Day;
				case JobType.TotalCountUp:
					return doneToday == 0 ? (int)SubJob.Base_Count + per_Day : doneToday + per_Day;
				case JobType.TotalCountDown:
					return TotalDone - per_Day;
				default:
					return 0;
			}
		}

		public DateTime Deadline => Job.Deadline.Value.Date;
		public string Name { get; set; }
		public int DoneToday { get; set; }
		private void SetInitialDoneTodayValue()
		{
			switch ((JobType)Job.Job_Type)
			{
				case JobType.DailyCountable:
					DoneToday = 0;
					break;
				case JobType.TotalCountDown:
					DoneToday = SubJob.Count - TotalDone;
					break;
				case JobType.TotalCountUp:
					DoneToday = TotalDone;
					break;
				default:
					break;
			}
		}

		public double Need { get; set; }

		public double CalcNeed()
		{
			var need = TotalDone - (CalcDaysSinceStart() * SubJob.Per_Day) - DoneToday;

			if ((JobType)Job.Job_Type == JobType.TotalCountUp)
			{
				need = TotalDone - (CalcDaysSinceStart() * SubJob.Per_Day) - (int)SubJob.Base_Count;
			}

			return need > Left ? Left : need;
		}

		public DateTime Today { get; set; }
		public Job Job { get; set; }
		public SubJob SubJob { get; set; }
		public int TotalDone { get; set; }
		public int Left { get; set; }

		public int CalcLeft()
		{
			return SubJob.Count  - TotalDone;
		}

		public double Baseline => SubJob.Per_Day;
		public DateTime EstDeadline { get => CalcEstFinishedDate(); }
		private DateTime CalcEstFinishedDate()
		{
			int days = (int)Math.Ceiling((decimal)(Left / SubJob.Per_Day));

			return Today.Date.AddDays(days);
		}
		public double AjustedBaseline => Left / ((double)DaysUntilColumn - 1);
		public int DaysUntilColumn => (int)(((DateTime)Job.Deadline).Date - Today.Date).TotalDays;

		int CalcDaysSinceStart()
		{
			return (int)(Today.Date - Job.Date_Added.Date).TotalDays + 1;
		}

		private int goal;
		public int Goal => goal;

		public int Even
		{
			get
			{
				int amount = 0;
				if (Need < 0)
				{
					
					if ((JobType)Job.Job_Type == JobType.TotalCountUp)
					{
						amount = TotalDone + (-1 * (int)Need);
					}
					else
					{
						amount = Left - (int)Math.Ceiling(Need * -1);
					}
				}
				else
				{
					if ((JobType)Job.Job_Type == JobType.DailyCountable)
					{
						amount = Left;
					}
					else
					{
						amount = DoneToday;
					}
				}

				return amount;
			}
		}

		public int CalcDoneToday()
		{
			switch ((JobType)Job.Job_Type)
			{
				case JobType.DailyCountable:
					return DoneToday;
				case JobType.TotalCountUp:
					return DoneToday - TotalDone;
				case JobType.TotalCountDown:
					return SubJob.Count - TotalDone - DoneToday;
				default:
					return 0;
			}
		}

		public int CalcTotalDone()
		{
			switch ((JobType)Job.Job_Type)
			{
				case JobType.DailyCountable:
					return DoneToday + TotalDone;
				case JobType.TotalCountUp:
					return DoneToday;
				case JobType.TotalCountDown:
					return SubJob.Count - DoneToday;
				default:
					return 0;
			}
		}



	}
}
