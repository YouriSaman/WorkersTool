using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Mvc;
using WorkersTool.ViewModels;

namespace WorkersTool.Controllers
{
    public class AccountController : Controller
    {
        AccountLogic accountLogic = new AccountLogic();
        public IActionResult Login()
        {
            AccountLogic logic = new AccountLogic();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterBranchViewModel viewModel)
        {
            AccountLogic logic = new AccountLogic();
            logic.AddBrach(viewModel.Branch, viewModel.Account);
            return RedirectToAction("Login");
        }

        public IActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterUser(RegisterUserViewModel viewModel)
        {
            accountLogic.AddUser(viewModel.User, viewModel.Account);
            return View();
        }
    }
}