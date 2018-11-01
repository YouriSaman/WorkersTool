using System;
using System.Collections.Generic;
using System.Text;
using Data;
using Models;

namespace Logic
{
    public class ShiftLogic
    {
        private ShiftContext _shiftContext = new ShiftContext();

        public List<Week> GetAllWeeks()
        {
            return _shiftContext.GetAllWeeks();
        }

        public Week GetWeekByNumber(int weeknumber)
        {
            return _shiftContext.GetWeekByNumber(weeknumber);
        }

        public List<Day> GiveDatesOfWeek(Week week)
        {
            var days = new List<Day>(new Day[7]);
            var day = week.From;
            for (int i = 0; i < 7; i++)
            {
                days[i] = new Day();
                days[i].Date = day;
                day = day.AddDays(1);
            }

            return days;
        }

        public void AddShift(List<Day> days, int weeknumber)
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    var shift = days[i].Shifts[j];
                    if (shift.StartTime < shift.EndTime)
                    {
                        shift.Weeknumber = weeknumber;
                        _shiftContext.AddShift(shift);
                    }
                }
            }
            
        }

        public string ConverTimeSpan(TimeSpan time)
        {
            return time.ToString(@"hh\:mm");
        }
    }
}
