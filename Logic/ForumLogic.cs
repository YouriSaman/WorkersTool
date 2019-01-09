using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using Repo;

namespace Logic
{
    public class ForumLogic
    {
        //private ForumSqlContext _forumDao;
        private ForumRepo _forumRepo = new ForumRepo(Context.Mssql);
        private AccountLogic accountLogic = new AccountLogic();

        public int AddMessage(Message message)
        {
            return _forumRepo.AddMessage(message);
        }

        public void AddReply(Reply reply)
        {
            _forumRepo.AddReply(reply);
        }

        public List<Message> GetAllMessages()
        {
            var messagesWithMedia = _forumRepo.GetAllMessagesWithMedia();
            var messagesWithoutMedia = _forumRepo.GetAllMessagesWithoutMedia();

            var allMessages = messagesWithMedia.Concat(messagesWithoutMedia).ToList();
            foreach (var message in allMessages)
            {
                message.Account = accountLogic.GetAccountByUserId(message.UserId);
            }
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
            var replies = _forumRepo.GetAllReplies();
            foreach (var reply in replies)
            {
                reply.Account = accountLogic.GetAccountByUserId(reply.UserId);
            }

            return replies;
        }

        public void DeleteMessage(int messageId, int userId)
        {
            _forumRepo.DeleteMessage(messageId, userId);
        }

        public void DeleteReply(int replyId, int userId)
        {
            _forumRepo.DeleteReply(replyId, userId);
        }
    }
}
