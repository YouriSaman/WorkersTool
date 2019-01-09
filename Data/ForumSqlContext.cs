using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http.Headers;
using System.Text;
using IContext;
using Models;

namespace Data
{
    public class ForumSqlContext:IForumContext
    {
        private string connectionstring = "Server=mssql.fhict.local;Database=dbi383661_extra;User Id=dbi383661_extra;Password=YouriS12;";

        public int AddMessage(Message message)
        {
            string query = "INSERT INTO Message(User_Id, Text, Likes)" +
                           "VALUES(@UserId, @Text, @Likes);" +
                           "SELECT @@IDENTITY AS NewId;";

            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@UserId", message.UserId);
                    cmd.Parameters.AddWithValue("@Text", message.Text);
                    cmd.Parameters.AddWithValue("@Likes", 0);

                    SqlDataReader rowsAffected = cmd.ExecuteReader();

                    int messageId = 0;

                    while (rowsAffected.Read())
                    {
                        var decimalId = (decimal)rowsAffected["NewId"];

                        messageId = Convert.ToInt32(decimalId);
                    }
                    conn.Close();

                    return messageId;
                }
            }
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

        public void DeleteMessage(int messageId, int userId)
        {
            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand("dbo.spDeleteMessage", conn))
                {
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MessageId", messageId);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteReply(int replyId, int userId)
        {
            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand("dbo.spDeleteReply", conn))
                {
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MessageId", replyId);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    cmd.ExecuteNonQuery();
                }
            }
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
                                Text = (string)reader["Text"],
                                UserId = (int)reader["User_Id"]
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
                                MediaUrl = (string)reader["Media"],
                                UserId = (int)reader["User_Id"]
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
                                Id = (int)reader["Id"],
                                Text = (string)reader["Text"],
                                MessageId = (int)reader["Message_Id"],
                                UserId = (int)reader["User_Id"]
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
