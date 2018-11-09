using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace WorkersTool.ViewModels
{
    public class LeaveOfAbsenceViewModel
    {
        public Shift Shift { get; set; }
        public int UserId { get; set; }
        public string ReasonOfAbsence { get; set; }
    }
}
