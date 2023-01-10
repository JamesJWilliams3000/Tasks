using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;

namespace Tasks
{
    public partial class GoogleCalendar : UserControl
    {
        const DayOfWeek FirstDateofWeek = DayOfWeek.Monday;
        List<GoogleCalendarWeek> Week = new List<GoogleCalendarWeek>();
        DateTime StartDate = new DateTime();
        List<Event> WeekEvents = new List<Event>();

        static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        static string ApplicationName = "Tasks";

        public GoogleCalendar()
        {
            InitializeComponent();
        }

        public GoogleCalendar(DateTime date)
        {
            InitializeComponent();
            StartDate = FirstDateInWeek(date);
            WeekEvents = GetEvents(StartDate);
            Week = CreateRows();

            SetBinding();
        }

        //stackoverflow.com/questions/38039/how-can-i-get-the-datetime-for-the-start-of-the-week
        public static DateTime FirstDateInWeek(DateTime dt)
        {
            while (dt.DayOfWeek != FirstDateofWeek)
                dt = dt.AddDays(-1);
            return dt;
        }

        //gets events for the week
        static List<Event> GetEvents(DateTime startDate)
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                //Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 100;
            request.TimeMax = startDate.AddDays(7);
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List events.
            Events events = request.Execute();
            if (events.Items != null && events.Items.Count > 0)
            {
                return events.Items.ToList();
            }
            else
            {
                return null;
            }
        }

        GoogleCalendarDate GetDayOfWeek(DateTime date, List<Event> events)
        {
            return new GoogleCalendarDate(date, events.Where(x => x.Start.DateTime.Value.Date == date).ToList());
        }

        List<GoogleCalendarDate> GetWeeksEvents(DateTime date, List<Event> events)
        {            
            List<GoogleCalendarDate> week = new List<GoogleCalendarDate>();
            for (int i = 0; i < 7; i++)
            {
                week.Add(GetDayOfWeek(date, events));
                date.AddDays(1);
            }

            return week;
        }

        public void SetBinding()
        {
            dgv.DataSource = Week;

            for (int i = 0; i < 7; i++)
            {
                dgv.Columns[i].HeaderText = (StartDate.Day + i) + Environment.NewLine + dgv.Columns[i].HeaderText;
                dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }           
        }

        List<GoogleCalendarWeek> CreateRows()
        {
            var events = WeekEvents.OrderBy(x => x.Start.Date).ToList();
            List<GoogleCalendarWeek> rows = new List<GoogleCalendarWeek>();
            

            while (events.Count > 0)
            {
                var row = new GoogleCalendarWeek();
                
                for (int i = 0; i < 7; i++)
                {
                    var timedEvents = events.Where(x => string.IsNullOrEmpty(x.Start.Date)).ToList();
                    var eventsForDay = timedEvents.Where(x => x.Start.DateTime.Value.DayOfWeek == (DayOfWeek)i).ToList().OrderBy(a => a.Start.DateTime.Value).ToList();
                    if (eventsForDay.Count() > 0)
                    {
                        if (row.SetDay((DayOfWeek)i, eventsForDay.First()))
                        {
                            events.Remove(events.Single(x => x.Summary == eventsForDay.First().Summary));
                            timedEvents.Remove(timedEvents.Single(x => x.Summary == eventsForDay.First().Summary));
                        }
                    }

                }

                var allDayEvents = events.Where(x => !string.IsNullOrEmpty(x.Start.Date)).ToList();
                for (int i = 0; i < 7; i++)
                {
                    var eventsForDay
                        = allDayEvents.Where(x => DateTime.ParseExact(x.Start.Date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture).DayOfWeek == (DayOfWeek)i).ToList();
                    //var eventsForDay = events.Where(x => x.Start.DateTime.Value == (DayOfWeek)i).ToList();
                    if (eventsForDay.Count() > 0)
                    {
                        if (row.SetDay((DayOfWeek)i, eventsForDay.First()))
                        {
                            events.Remove(eventsForDay.First());
                            allDayEvents.Remove(eventsForDay.First());
                        }
                    }                    
                }

                rows.Add(row);
            }

            return rows;
        }

        private void GoogleCalendar_Load(object sender, EventArgs e)
        {
            dgv.Width = dgv.Columns.Cast<DataGridViewColumn>().Sum(x => x.Width) + (dgv.RowHeadersVisible ? dgv.RowHeadersWidth : 0) + 3;
            dgv.Height = dgv.Rows.Cast<DataGridViewRow>().Sum(x => x.Height) + (dgv.ColumnHeadersVisible ? dgv.ColumnHeadersHeight : 0) + 3;
        }
    }
}
