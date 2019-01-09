using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class User
    {
        public int Id { get; set; }
        public DateTime Birthday { get; set; }
        public int Gender { get; set; }
        public bool Rights { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
