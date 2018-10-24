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
        public IActionResult AddSchedule()
        {
            var viewModel = new AddScheduleViewModel();
            viewModel.UserAccounts = accountLogic.GetAllUserAccounts();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddSchedule(AddScheduleViewModel viewodel)
        {

            return View();
        }
    }
}