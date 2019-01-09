using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace IContext
{
    public interface IForumContext
    {
        int AddMessage(Message message);
        void AddReply(Reply reply);
        void DeleteMessage(int messageId, int userId);
        void DeleteReply(int replyId, int userId);
        List<Message> GetAllMessagesWithoutMedia();
        List<Message> GetAllMessagesWithMedia();
        List<Reply> GetAllReplies();
    }
}
