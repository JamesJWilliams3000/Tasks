using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks__MVP_;

namespace Tasks
{
	public interface IViewTasksView : IView
	{
		Date Date { get; set; }
		ICollection<Job> Jobs { get; set; }
		ICollection<DatesJob> CompletedJobs { get; set; }
		List<UpdatedJobCalendarRow> rows { get; set; }

		event EventHandler OnNewDayClicked;
		void UpdateView();
	}
}
