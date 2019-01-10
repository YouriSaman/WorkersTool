using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using Repo;

namespace Logic
{
    public class ShiftLogic
    {
        private ShiftRepo _shiftRepo = new ShiftRepo(Context.Mssql);
        private WeekLogic _weekLogic = new WeekLogic();
        private DepartmentLogic _departmentLogic = new DepartmentLogic();
        private AccountLogic _accountLogic = new AccountLogic();
        private LeaveOfAbsenceRepo _leaveOfAbsenceRepo = new LeaveOfAbsenceRepo(Context.Mssql);
        
        public Shift GetShiftById(int shiftId)
        {
            return _shiftRepo.GetShiftById(shiftId);
        }

        public List<Shift> GetShiftsByIds(List<int> shiftIds)
        {
            var shifts = new List<Shift>();
            foreach (var shiftId in shiftIds)
            {
                var shift = GetShiftById(shiftId);
                shifts.Add(shift);
            }

            return shifts;
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
                        _shiftRepo.AddShift(shift);
                    }
                }
            }
        }

        public int GetUserIdOfShift(int shiftId)
        {
            return _shiftRepo.GetUserIdOfShift(shiftId);
        }

        public string ConverTimeSpan(TimeSpan time)
        {
            return time.ToString(@"hh\:mm");
        }

        public List<Day> ShowSchedule(int weeknumber, int userId)
        {
            var week = _weekLogic.GetWeekByNumber(weeknumber);
            var days = GiveDatesOfWeek(week);
            var shifts = _shiftRepo.GetShiftsOfWeekFromUser(weeknumber, userId);
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
            _leaveOfAbsenceRepo.AddLeaveOfAbsence(shiftId, userId, reason);
        }

        public void HideLeaveOfAbsence(int leaveOfAbsenceId, int userId)
        {
            _leaveOfAbsenceRepo.HideLeaveOfAbsence(leaveOfAbsenceId, userId);
        }

        public List<int> GetHiddenLOAIdsOfUser(int userId)
        {
            return _leaveOfAbsenceRepo.GetHiddenLOAIdsOfUser(userId);
        }

        public List<int> GetShiftIdsOfUser(int userId)
        {
            return _shiftRepo.GetShiftIdsOfUser(userId);
        }

        public List<int> GetAllShiftIds()
        {
            return _shiftRepo.GetAllShiftIds();
        }

        public void AcceptLeaveOfAbsence(int leaveOfAbsenceId, int userId)
        {
            _leaveOfAbsenceRepo.AcceptLeaveOfAbsence(leaveOfAbsenceId, userId);
            _leaveOfAbsenceRepo.HideLeaveOfAbsence(leaveOfAbsenceId, userId);
        }

        public List<LeaveOfAbsence> GetLeaveOfAbsences(int userId)
        {
            var listAbsences = _leaveOfAbsenceRepo.GetLeaveOfAbsences();
            for (int i = 0; i < listAbsences.Count; i++)
            {
                //Hidden aanvragen niet tonen
                foreach (var hiddenLoaId in GetHiddenLOAIdsOfUser(userId))
                {
                    if (hiddenLoaId == listAbsences[i].Id)
                    {
                        listAbsences.Remove(listAbsences[i]);
                    }
                }

                //Geen aanvragen van User zelf tonen
                if (listAbsences.Count != 0)
                {
                    foreach (var shiftId in GetShiftIdsOfUser(userId))
                    {
                        if (listAbsences.Count != 0)
                        {
                            if (shiftId == listAbsences[i].ShiftId)
                            {
                                listAbsences.Remove(listAbsences[i]);
                            }
                        }
                    }
                }

                //Oude aanvragen niet meer tonen ---> Misschien gewoon verwijderen na 1 week in Database
                if (listAbsences.Count != 0)
                {
                    foreach (var shiftById in GetShiftsByIds(GetAllShiftIds()))
                    {
                        if (listAbsences.Count != 0)
                        {
                            if (shiftById.Id == listAbsences[i].ShiftId)
                            {
                                if (shiftById.Date < DateTime.Now)
                                {
                                    listAbsences.Remove(listAbsences[i]);
                                }
                            }
                        }
                    }
                }
            }

            return listAbsences;
        }

        public List<LeaveOfAbsence> GetLeaveOfAbsencesWithApproval()
        {
            return _leaveOfAbsenceRepo.GetLeaveOfAbsencesWithApproval();
        }

        public List<LeaveOfAbsence> GetLeaveOfAbsences()
        {
            var leaveOfAbsences = GetLeaveOfAbsencesWithApproval();

            foreach (var leaveOfAbsence in leaveOfAbsences)
            {
                int userIdOfShift = GetUserIdOfShift(leaveOfAbsence.Shift.Id);
                leaveOfAbsence.Shift.Account = _accountLogic.GetAccountByUserId(userIdOfShift);
                int newUserId = leaveOfAbsence.NewUserId;
                leaveOfAbsence.AccountOfNewUser = _accountLogic.GetAccountByUserId(newUserId);
            }

            return leaveOfAbsences;
        }

        public void ApproveLeaveOfAbsence(int newUserId, int leaveOfAbsenceId, int shiftId)
        {
            _leaveOfAbsenceRepo.ApproveLeaveOfAbsence(newUserId, leaveOfAbsenceId);
            _leaveOfAbsenceRepo.ChangeUserToShift(newUserId, shiftId);
        }

        public void DisapproveLeaveOfAbsence(int newUserId, int leaveOfAbsenceId)
        {
            _leaveOfAbsenceRepo.DisapproveLeaveOfAbsence(newUserId, leaveOfAbsenceId);
        }
    }
}
