using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Shift
    {
        public DateTime StarTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool Closed { get; set; }
        public List<User> Users { get; set; }
    }
}
