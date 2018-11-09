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
            var weeknumber = 43;
            viewModel.Weeks = _weekLogic.GetAllWeeks();
            viewModel.UserAccounts = _accountLogic.GetAllUserAccounts();
            //var week = shiftLogic.GetWeekByNumber(weeknumber);
            //viewModel.Days = shiftLogic.GiveDatesOfWeekWithFirstShift(week);
            //viewModel.Departments = shiftLogic.GetAllDepartments();
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
            return PartialView(viewModel);
        }

        public IActionResult MySchedule(ShowScheduleViewModel viewModel)
        {
            viewModel.Weeks = _weekLogic.GetAllWeeks();
            return View(viewModel);
        }

        public IActionResult ShowSchedule(ShowScheduleViewModel showScheduleViewModel)
        {
            var viewModel = new ShowScheduleViewModel();
            var weeknumber = showScheduleViewModel.Weeknumber;
            var userId = Convert.ToInt32(Convert.ToString(User.Claims.Where(claim => claim.Type == "Id").Select(claim => claim.Value).SingleOrDefault())); ;
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
    }
}