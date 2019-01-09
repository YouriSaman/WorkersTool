using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Mvc;
using WorkersTool.ViewModels;

namespace WorkersTool.Controllers
{
    public class ShiftController : Controller
    {
        AccountLogic _accountLogic = new AccountLogic();
        ShiftLogic _shiftLogic = new ShiftLogic();
        WeekLogic _weekLogic = new WeekLogic();
        DepartmentLogic _departmentLogic = new DepartmentLogic();

        public IActionResult AddSchedule()
        {
            var viewModel = new AddScheduleViewModel();
            viewModel.Weeks = _weekLogic.GetThisWeekPlusTwoWeeks();
            viewModel.UsersWithAccount = _accountLogic.GetAllUsersWithAccounts();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddSchedule(AddScheduleViewModel viewModel)
        {
            var days = viewModel.Days;
            var weeknumber = viewModel.Weeknumber;
            _shiftLogic.AddShift(days, weeknumber);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult NewSchedule(AddScheduleViewModel viewModel)
        {
            var weeknumber = viewModel.Weeknumber;
            var week = _weekLogic.GetWeekByNumber(weeknumber);
            viewModel.Days = _shiftLogic.GiveDatesOfWeekWithFirstShift(week);
            viewModel.Departments = _departmentLogic.GetAllDepartments();
            _departmentLogic.GetAllDepartments();
            return PartialView(viewModel);
        }

        public IActionResult MySchedule(ShowScheduleViewModel viewModel)
        {
            viewModel.Weeks = _weekLogic.GetThisWeekPlusTwoWeeks();
            return View(viewModel);
        }

        public IActionResult ShowSchedule(ShowScheduleViewModel showScheduleViewModel)
        {
            var viewModel = new ShowScheduleViewModel();
            var weeknumber = showScheduleViewModel.Weeknumber;
            var userId = Convert.ToInt32(Convert.ToString(User.Claims.Where(claim => claim.Type == "Id").Select(claim => claim.Value).SingleOrDefault()));
            viewModel.Days = _shiftLogic.ShowSchedule(weeknumber, userId);
            return PartialView(viewModel);
        }

        public IActionResult LeaveOfAbsence(int shiftId, int userId)
        {
            var viewModel = new LeaveOfAbsenceViewModel();
            var shift = _shiftLogic.GetShiftById(shiftId);
            shift.Department = _departmentLogic.GetDepartmentById(shift.DepartmentId);
            viewModel.Shift = shift;
            viewModel.UserId = userId;
            return PartialView(viewModel);
        }

        [HttpPost]
        public IActionResult LeaveOfAbsence(LeaveOfAbsenceViewModel viewModel)
        {
            var userId = Convert.ToInt32(Convert.ToString(User.Claims.Where(claim => claim.Type == "Id").Select(claim => claim.Value).SingleOrDefault()));
            _shiftLogic.AddLeaveOfAbsence(viewModel.Shift.Id, userId, viewModel.ReasonOfAbsence);
            return RedirectToAction("MySchedule"); //Nog met weekId meekrijgen en meegeven, maar dat pas later
        }

        public IActionResult ListOfAbsences()
        {
            var viewModel = new ListOfAbsencesViewModel();
            var userId = Convert.ToInt32(Convert.ToString(User.Claims.Where(claim => claim.Type == "Id").Select(claim => claim.Value).SingleOrDefault()));
            viewModel.LeaveOfAbsences = _shiftLogic.GetLeaveOfAbsences(userId);
            foreach (var item in viewModel.LeaveOfAbsences)
            {
                item.Shift = _shiftLogic.GetShiftById(item.ShiftId);
                item.Shift.Department = _departmentLogic.GetDepartmentById(item.Shift.DepartmentId);
                item.Shift.Account = _accountLogic.GetAccountByUserId(item.Shift.UserId);
            }
            return PartialView(viewModel);
        }

        [HttpPost]
        public IActionResult AcceptLeaveOfAbsenceFromList(ListOfAbsencesViewModel viewModel)
        {
            var leaveOfAbsenceId = viewModel.LeaveOfAbsenceId;
            var userId = Convert.ToInt32(Convert.ToString(User.Claims.Where(claim => claim.Type == "Id").Select(claim => claim.Value).SingleOrDefault())); ;
            _shiftLogic.AcceptLeaveOfAbsence(leaveOfAbsenceId, userId);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult HideLeaveOfAbsence(ListOfAbsencesViewModel viewModel)
        {
            var leaveOfAbsenceId = viewModel.LeaveOfAbsenceId;
            var userId = Convert.ToInt32(Convert.ToString(User.Claims.Where(claim => claim.Type == "Id").Select(claim => claim.Value).SingleOrDefault())); ;
            _shiftLogic.HideLeaveOfAbsence(leaveOfAbsenceId, userId);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AcceptableLeaveOfAbsences()
        {
            var viewModel = new AcceptableLeaveOfAbsencesViewModel();
            viewModel.LeaveOfAbsences = _shiftLogic.GetLeaveOfAbsences();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ApproveLeaveOfAbsence(AcceptableLeaveOfAbsencesViewModel viewModel)
        {
            int newUserId = viewModel.NewUserId;
            int leaveOfAbsenceId = viewModel.LeaveOfAbsenceId;
            int shiftId = viewModel.ShiftId;
            _shiftLogic.ApproveLeaveOfAbsence(newUserId, leaveOfAbsenceId, shiftId);
            return RedirectToAction("AcceptableLeaveOfAbsences");
        }

        [HttpPost]
        public IActionResult DisapproveLeaveOfAbsence(AcceptableLeaveOfAbsencesViewModel viewModel)
        {
            int newUserId = viewModel.NewUserId;
            int leaveOfAbsenceId = viewModel.LeaveOfAbsenceId;
            _shiftLogic.DisapproveLeaveOfAbsence(newUserId, leaveOfAbsenceId);
            return RedirectToAction("AcceptableLeaveOfAbsences");
        }
    }
}