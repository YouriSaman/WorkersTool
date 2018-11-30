using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Forumpart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public int Likes { get; set; }
    }
}
