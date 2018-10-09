using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Message : Forumpart
    {
        public string MediaUrl { get; set; }
        public int Likes { get; set; }
    }
}
