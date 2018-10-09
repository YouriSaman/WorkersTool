using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Mvc;

namespace WorkersTool.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            AccountLogic logic = new AccountLogic();
            return View();
        }

        public IActionResult Registreer()
        {

            return View();
        }

        public IActionResult RegistreerGebruiker()
        {
            return View();
        }
    }
}