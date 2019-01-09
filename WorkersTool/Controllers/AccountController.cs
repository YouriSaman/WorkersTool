using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using WorkersTool.ViewModels;

namespace WorkersTool.Controllers
{
    public class AccountController : Controller
    {
        AccountLogic accountLogic = new AccountLogic();
        UserLogic userLogic = new UserLogic();
        private IHostingEnvironment _hostingEnvironment;

        public AccountController(IHostingEnvironment environment)
        {
            _hostingEnvironment = environment;
        }

        [HttpPost]
        public IActionResult Login(Account account)
        {
            if (accountLogic.LoginCheck(account))
            {
                var userAccount = accountLogic.GetAccountByUsername(account.Username);
                var user = userLogic.GetUserByAccountId(userAccount.Id);
                PerformLogin(userAccount, user);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
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
        public async Task<IActionResult> RegisterUser(RegisterUserViewModel viewModel)
        {
            var user = viewModel.User;
            var account = viewModel.Account;
            var accountImage = viewModel.AccountImage;
            string accountImageFileName = accountImage.FileName;
            account.Image = accountImageFileName;
            accountLogic.AddAccount(user, account);

            //Add image to root of app
            string mapRoot = "images/UserImages/";

            var accountImagePath = Path.Combine(_hostingEnvironment.WebRootPath, mapRoot);
            await AddFileToDirectory(accountImage, accountImagePath);

            return View();
        }

        public IActionResult Profile()
        {
            var viewModel = new AccountProfileViewModel();
            int userId = Convert.ToInt32(Convert.ToString(User.Claims.Where(claim => claim.Type == "Id").Select(claim => claim.Value).SingleOrDefault()));
            var account = accountLogic.GetAccountByUserId(userId);
            viewModel.Account = account;
            viewModel.User = userLogic.GetUserByAccountId(account.Id);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(AccountProfileViewModel viewModel)
        {
            int userId = Convert.ToInt32(Convert.ToString(User.Claims.Where(claim => claim.Type == "Id").Select(claim => claim.Value).SingleOrDefault()));
            int accountId = accountLogic.GetAccountIdByUserId(userId);
            var user = viewModel.User;
            var account = viewModel.Account;
            if (viewModel.AccountImage != null)
            {
                var accountImage = viewModel.AccountImage;
                string accountImageFileName = accountImage.FileName;
                account.Image = accountImageFileName;
                
                //Add image to root of app
                string mapRoot = "images/UserImages/";

                var accountImagePath = Path.Combine(_hostingEnvironment.WebRootPath, mapRoot);
                await AddFileToDirectory(accountImage, accountImagePath);
            }
            else
            {
                account.Image = viewModel.Account.Image;
            }
            
            user.Id = userId;
            user.AccountId = accountId;

            account.Id = accountId;

            //Toevoegen in database
            userLogic.EditUser(user);
            accountLogic.EditAccount(account);

            return RedirectToAction("Profile");
        }

        private void PerformLogin(Account account, User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.Name),
                new Claim("Id", user.Id.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)).Wait();
        }

        public async Task AddFileToDirectory(IFormFile file, string path)
        {
            if (file.Length > 0)
            {
                try
                {
                    var filePath = Path.Combine(path, file.FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);

                    }
                }
                catch (DirectoryNotFoundException)
                {
                    Directory.CreateDirectory(path);

                    await AddFileToDirectory(file, path);
                }
            }
            else
            {
                throw new Exception("File is leeg");
            }
        }
    }
}