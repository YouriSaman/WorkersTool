using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Day
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public List<Shift> Shifts { get; set; }
    }
}
