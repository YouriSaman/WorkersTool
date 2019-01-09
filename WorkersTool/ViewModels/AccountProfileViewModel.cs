using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Models;

namespace WorkersTool.ViewModels
{
    public class AccountProfileViewModel
    {
        public Account Account { get; set; }
        public User User { get; set; }
        public IFormFile AccountImage { get; set; }
    }
}
