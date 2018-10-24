using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace WorkersTool.ViewModels
{
    public class AddScheduleViewModel
    {
        public List<Account> UserAccounts { get; set; }
        public TimeSpan Time1 { get; set; }
    }
}
