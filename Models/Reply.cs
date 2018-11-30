using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Reply: Forumpart
    {
        public int MessageId { get; set; }

        public Reply()
        {

        }

        public Reply(int messageId, string text, int userId)
        {
            MessageId = messageId;
            Text = text;
            UserId = userId;
        }
    }
}
