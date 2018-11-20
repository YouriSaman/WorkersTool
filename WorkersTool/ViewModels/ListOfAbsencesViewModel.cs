using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace WorkersTool.ViewModels
{
    public class ListOfAbsencesViewModel
    {
        public List<LeaveOfAbsence> LeaveOfAbsences { get; set; }
        public int ShiftId { get; set; }
        public int UserId { get; set; }
    }
}
