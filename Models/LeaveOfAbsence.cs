using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class LeaveOfAbsence
    {
        public int ShiftId { get; set; }
        public Shift Shift { get; set; }
        public User NewUser { get; set; }
        public string Reason { get; set; }
        public bool Shown { get; set; }
        public bool Closed { get; set; }
    }
}
