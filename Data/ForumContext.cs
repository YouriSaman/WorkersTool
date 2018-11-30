using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Http.Headers;
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

        public void AddReply(Reply reply)
        {
            string query = "INSERT INTO Message(User_Id, Message_Id, Text, Likes)" +
                           "VALUES(@UserId, @MessageId, @Text, @Likes)";

            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@UserId", reply.UserId);
                    cmd.Parameters.AddWithValue("@MessageId", reply.MessageId);
                    cmd.Parameters.AddWithValue("@Text", reply.Text);
                    cmd.Parameters.AddWithValue("@Likes", 0);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public void DeleteReply(Reply reply)
        {
            throw new NotImplementedException();
        }

        public List<Message> GetAllMessagesWithoutMedia()
        {
            string query = "SELECT * FROM Message WHERE Message_Id IS NULL AND Media IS NULL";
            var messages = new List<Message>();

            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var message = new Message()
                            {
                                Id = (int)reader["Id"],
                                Text = (string)reader["Text"]
                            };
                            messages.Add(message);
                        }

                        return messages;
                    }
                }
            }

        }

        public List<Message> GetAllMessagesWithMedia()
        {
            string query = "SELECT * FROM Message WHERE Message_Id IS NULL AND Media IS NOT NULL";
            var messages = new List<Message>();

            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var message = new Message()
                            {
                                Id = (int)reader["Id"],
                                Text = (string)reader["Text"],
                                MediaUrl = (string)reader["Media"]
                            };
                            messages.Add(message);
                        }

                        return messages;
                    }
                }
            }

        }

        public List<Reply> GetAllReplies()
        {
            string query = "SELECT * FROM Message WHERE Message_Id IS NOT NULL";
            var replies = new List<Reply>();

            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var reply = new Reply()
                            {
                                Text = (string)reader["Text"],
                                MessageId = (int)reader["Message_Id"]
                            };
                            replies.Add(reply);
                        }

                        return replies;
                    }
                }
            }
        }
    }
}
