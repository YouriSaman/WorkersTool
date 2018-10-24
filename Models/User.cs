using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class User
    {
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public bool Rights { get; set; }
        public int AccountId { get; set; }
    }
}
