using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace WorkersTool.ViewModels
{
    public class AcceptableLeaveOfAbsencesViewModel
    {
        public List<LeaveOfAbsence> LeaveOfAbsences { get; set; }
        public int NewUserId { get; set; }
        public int LeaveOfAbsenceId { get; set; }
        public int ShiftId { get; set; }
    }
}
