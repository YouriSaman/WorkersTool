using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Models;

namespace Logic
{
    public class WeekLogic
    {
        public List<Week> GetThisWeekPlusTwoWeeks()
        {
            var weeks = new List<Week>();
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            DateTime today = DateTime.Now;
            Calendar cal = dfi.Calendar;
            var thisWeek = cal.GetWeekOfYear(today, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);

            var thisYear = today.Year;
            var firstDayOfYear = new DateTime(thisYear, 1, 1);
            var totalDays = (thisWeek - 1) * 7 - 1;
            var firstDayOfWeek = firstDayOfYear.AddDays(totalDays);

            weeks.Add(new Week { From = firstDayOfWeek, Weeknumber = thisWeek });
            weeks.Add(new Week { From = firstDayOfWeek.AddDays(7), Weeknumber = thisWeek + 1 });
            weeks.Add(new Week { From = firstDayOfWeek.AddDays(14), Weeknumber = thisWeek + 2 });

            return weeks;
        }

        public Week GetWeekByNumber(int weeknumber)
        {
            var year = Convert.ToInt16(DateTime.Now.ToString("yyyy"));
            var firstDayOfYear = new DateTime(year, 1, 1);
            var totalDays = (weeknumber - 1) * 7 - 1;
            var firstDayOfWeek = firstDayOfYear.AddDays(totalDays);
            var week = new Week {From = firstDayOfWeek, Weeknumber = weeknumber};
            return week;
        }
    }
}
