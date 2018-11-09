using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace WorkersTool.ViewModels
{
    public class AddScheduleViewModel
    {
        public int Weeknumber { get; set; }
        public List<Account> UserAccounts { get; set; }
        public List<Week> Weeks { get; set; }
        public List<Day> Days { get; set; }
        public List<Department> Departments { get; set; }

        public AddScheduleViewModel()
        {
            Days = new List<Day>();
            Departments = new List<Department>();
        }
    }
}
