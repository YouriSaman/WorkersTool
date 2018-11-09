using System;
using System.Collections.Generic;
using System.Text;
using Data;
using Models;

namespace Logic
{
    public class WeekLogic
    {
        private WeekContext _weekContext = new WeekContext();

        public WeekLogic()
        {
            _weekContext = new WeekContext();
        }

        public List<Week> GetAllWeeks()
        {
            return _weekContext.GetAllWeeks();
        }

        public Week GetWeekByNumber(int weeknumber)
        {
            return _weekContext.GetWeekByNumber(weeknumber);
        }
    }
}
