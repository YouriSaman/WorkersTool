using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Message : Forumpart
    {
        public string MediaUrl { get; set; }
        public List<Reply> Replies { get; set; }

        public Message()
        {
            Replies = new List<Reply>();
        }
    }
}
