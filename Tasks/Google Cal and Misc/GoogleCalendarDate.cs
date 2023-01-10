using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3.Data;

namespace Tasks
{
    class GoogleCalendarDate
    {
        public DateTime Date { get; set; }

        public List<Event> Events { get; set; }

        public GoogleCalendarDate(DateTime date, List<Event> events)
        {
            Date = date;
            Events = events;
        }

    }
}
