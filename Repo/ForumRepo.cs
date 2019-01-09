using System;
using System.Collections.Generic;
using System.Text;
using Data;
using IContext;
using Models;

namespace Repo
{
    public class ForumRepo
    {
        private IForumContext iContext;

        public ForumRepo(Context contextType)
        {
            if (contextType == Context.Mssql)
            {
                iContext = new ForumSqlContext();
            }
            else if (contextType == Context.Memory)
            {
                //iContext = MemoryContext;
            }
        }

        public int AddMessage(Message message)
        {
            return iContext.AddMessage(message);
        }

        public void AddReply(Reply reply)
        {
            iContext.AddReply(reply);
        }

        public void DeleteMessage(int messageId, int userId)
        {
            iContext.DeleteMessage(messageId, userId);
        }

        public void DeleteReply(int replyId, int userId)
        {
            iContext.DeleteReply(replyId, userId);
        }

        public List<Message> GetAllMessagesWithoutMedia()
        {
            return iContext.GetAllMessagesWithoutMedia();
        }

        public List<Message> GetAllMessagesWithMedia()
        {
            return iContext.GetAllMessagesWithMedia();
        }

        public List<Reply> GetAllReplies()
        {
            return iContext.GetAllReplies();
        }
    }
}
