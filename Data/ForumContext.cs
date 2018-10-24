using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Data
{
    public class ForumContext
    {
        private string connectionstring = "Server=mssql.fhict.local;Database=dbi383661_extra;User Id=dbi383661_extra;Password=YouriS12;";

        public void AddMessage(Message messag)
        {
            throw new NotImplementedException();
        }

        public void AddReply(Reply reply, int MessageId)
        {
            throw new NotImplementedException();
        }

        public void DeleteMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public void DeleteReply(Reply reply)
        {
            throw new NotImplementedException();
        }
    }
}
