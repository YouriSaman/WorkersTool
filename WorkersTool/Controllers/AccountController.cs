using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Models;
using WorkersTool.ViewModels;

namespace WorkersTool.Controllers
{
    public class AccountController : Controller
    {
        AccountLogic accountLogic = new AccountLogic();

        [HttpPost]
        public IActionResult Login(Account account)
        {
            if (accountLogic.LoginCheck(account))
            {
                var userAccount = accountLogic.GetAccountByUsername(account.Username);
                PerformLogin(userAccount);
            }

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

        private void PerformLogin(Account account)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.Name),
                new Claim("Id", account.Id.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)).Wait();
        }
    }
}