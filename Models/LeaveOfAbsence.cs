using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class LeaveOfAbsence
    {
        public int Id { get; set; }
        public int ShiftId { get; set; }
        public Shift Shift { get; set; }
        public int NewUserId { get; set; }
        public Account AccountOfNewUser { get; set; }
        public string Reason { get; set; }

        public enum Approved
        {
            NeverApproved = 1, Disapproved = 2, Approved = 3 
        }
        public Approved IsApproved { get; set; }
    }
}
