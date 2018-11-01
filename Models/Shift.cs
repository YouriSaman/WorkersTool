using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Shift
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Weeknumber { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool Closed { get; set; }
        public List<User> Users { get; set; }
    }
}
