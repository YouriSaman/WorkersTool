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
        AccountLogic accountLogic = new AccountLogic();
        ShiftLogic shiftLogic = new ShiftLogic();
        public IActionResult AddSchedule()
        {
            var viewModel = new AddScheduleViewModel();
            viewModel.UserAccounts = accountLogic.GetAllUserAccounts();
            viewModel.Weeks = shiftLogic.GetAllWeeks();
            var week = shiftLogic.GetWeekByNumber(43);
            viewModel.Days = shiftLogic.GiveDatesOfWeek(week);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddSchedule(AddScheduleViewModel viewodel)
        {
            var days = viewodel.Days;
            var weeknumber = viewodel.Weeknumber;
            shiftLogic.AddShift(days, weeknumber);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ShowSchedule()
        {
            var viewModel = new ShowScheduleViewModel();
            return View(viewModel);
        }
    }
}