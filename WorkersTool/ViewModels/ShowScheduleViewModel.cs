using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace WorkersTool.ViewModels
{
    public class ShowScheduleViewModel
    {
        public List<Day> Days { get; set; }
        public List<Week> Weeks { get; set; }
    }
}
