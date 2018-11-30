using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;
using Models;

namespace Logic
{
    public class ForumLogic
    {
        private ForumContext forumContext;

        public ForumLogic()
        {
            forumContext = new ForumContext();
        }

        public int AddMessage(Message message)
        {
            return forumContext.AddMessage(message);
        }

        public void AddReply(Reply reply)
        {
            forumContext.AddReply(reply);
        }

        public List<Message> GetAllMessages()
        {
            var allMessages = new List<Message>();
            var messagesWithMedia = forumContext.GetAllMessagesWithMedia();
            var messagesWithoutMedia = forumContext.GetAllMessagesWithoutMedia();

            allMessages = messagesWithMedia.Concat(messagesWithoutMedia).ToList();
            var replies = GetAllReplies();
            for (int i = 0; i < replies.Count; i++)
            {
                foreach (var message in allMessages)
                {
                    if (message.Id == replies[i].MessageId)
                    {
                        message.Replies.Add(replies[i]);
                    }
                }
            }

            return allMessages;
        }

        public List<Reply> GetAllReplies()
        {
            return forumContext.GetAllReplies();
        }
    }
}
