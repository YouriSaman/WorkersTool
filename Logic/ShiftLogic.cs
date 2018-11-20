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
        private WeekLogic _weekLogic = new WeekLogic();
        private DepartmentLogic _departmentLogic = new DepartmentLogic();
        private LeaveOfAbsenceContext _leaveOfAbsenceContext = new LeaveOfAbsenceContext();

        public ShiftLogic()
        {
            _shiftContext = new ShiftContext();
            _weekLogic = new WeekLogic();
            _departmentLogic = new DepartmentLogic();
            _leaveOfAbsenceContext = new LeaveOfAbsenceContext();
        }

        public Shift GetShiftById(int shiftId)
        {
            return _shiftContext.GetShiftById(shiftId);
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

        public List<Day> GiveDatesOfWeekWithFirstShift(Week week)
        {
            var days = GiveDatesOfWeek(week);
            for (int i = 0; i < 7; i++)
            {
                days[i].Shifts.Add(new Shift());
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

        public List<Day> ShowSchedule(int weeknumber, int userId)
        {
            var week = _weekLogic.GetWeekByNumber(weeknumber);
            var days = GiveDatesOfWeek(week);
            var shifts = _shiftContext.GetShiftsOfWeekFromUser(weeknumber, userId);
            foreach (var shift in shifts)
            {
                for (int i = 0; i < days.Count; i++)
                {
                    if (shift.Date == days[i].Date)
                    {
                        var departmentId = shift.DepartmentId;
                        shift.Department = _departmentLogic.GetDepartmentById(departmentId);
                        days[i].Shifts.Add(shift);
                    }
                }
            }

            return days;
        }

        public void AddLeaveOfAbsence(int shiftId, int userId, string reason)
        {
            _leaveOfAbsenceContext.AddLeaveOfAbsence(shiftId, userId, reason);
        }

        public void HideLeaveOfAbsence(int shiftId, int userId)
        {
            _leaveOfAbsenceContext.HideLeaveOfAbsence(shiftId, userId);
        }

        public List<int> GetHiddenShiftIdsOfUser(int userId)
        {
            return _leaveOfAbsenceContext.GetHiddenShiftIdsOfUser(userId);
        }

        public List<LeaveOfAbsence> GetLeaveOfAbsences(int userId)
        {
            var listAbsences = _leaveOfAbsenceContext.GetLeaveOfAbsences();
            for (int i = 0; i < listAbsences.Count; i++)
            {
                foreach (var item in GetHiddenShiftIdsOfUser(userId))
                {
                    if (item == listAbsences[i].ShiftId)
                    {
                        listAbsences.Remove(listAbsences[i]);
                    }
                }
            }

            return listAbsences;
        }
    }
}
