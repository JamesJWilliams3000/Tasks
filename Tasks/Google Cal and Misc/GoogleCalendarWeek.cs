using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3.Data;


namespace Tasks
{
    class GoogleCalendarWeek
    {
        public string Monday { get; set; }

        public string Tuesday { get; set; }

        public string Wendsday { get; set; }

        public string Thursday { get; set; }

        public string Friday { get; set; }

        public string Saturday { get; set; }

        public string Sunday { get; set; }

        public bool SetDay(DayOfWeek dayOfWeek, Event dayEvent)
        {
            string text = FormatEvent(dayEvent);

            switch (dayOfWeek)
            {
                case DayOfWeek.Sunday:
                    if (string.IsNullOrEmpty(Sunday)) { Sunday = text; return true; }
                    else { return false; }
                case DayOfWeek.Monday:
                    if (string.IsNullOrEmpty(Monday)) { Monday = text; return true; }
                    else { return false; }
                case DayOfWeek.Tuesday:
                    if (string.IsNullOrEmpty(Tuesday)) { Tuesday = text; return true; }
                    else { return false; }
                case DayOfWeek.Wednesday:
                    if (string.IsNullOrEmpty(Wendsday)) { Wendsday = text; return true; }
                    else { return false; }
                case DayOfWeek.Thursday:
                    if (string.IsNullOrEmpty(Thursday)) { Thursday = text; return true; }
                    else { return false; }
                case DayOfWeek.Friday:
                    if (string.IsNullOrEmpty(Friday)) { Friday = text; return true; }
                    else { return false; }
                case DayOfWeek.Saturday:
                    if (string.IsNullOrEmpty(Saturday)) { Saturday = text; return true; }
                    else { return false; }
                default:
                    return false;
            }
        }

        string FormatEvent(Event dayEvent)
        {
            string when = null;
            if (dayEvent.Start.DateTime != null)
            {
                when = dayEvent.Start.DateTime.Value.ToString("hh:mm tt");
                when += " - " + dayEvent.End.DateTime.Value.ToString("hh:mm tt");
            }

            return dayEvent.Summary + (String.IsNullOrEmpty(when) ? "" : string.Format(" [{0}]", when));
        }

    }
}
