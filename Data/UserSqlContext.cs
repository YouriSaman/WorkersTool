using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace Data
{
    public class UserSqlContext
    {
        private string connectionstring = "Server=mssql.fhict.local;Database=dbi383661_extra;User Id=dbi383661_extra;Password=YouriS12;";

        public void AddUser(User user)
        {
            string query = "INSERT INTO [User](Birthday, Gender, Account_Id)" +
                           "VALUES (@Birthday, @Gender, @AccountId)";

            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Birthday", user.Birthday);
                    cmd.Parameters.AddWithValue("@Gender", user.Gender);
                    cmd.Parameters.AddWithValue("@AccountId", user.AccountId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EditUser(User user)
        {
            string query = "UPDATE [User] SET Birthday= @Birthday, Gender= @Gender, Account_Id= @AccountId WHERE Id= @UserId";

            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Birthday", user.Birthday);
                    cmd.Parameters.AddWithValue("@Gender", user.Gender);
                    cmd.Parameters.AddWithValue("@AccountId", user.AccountId);
                    cmd.Parameters.AddWithValue("@UserId", user.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<User> GetAllUsers()
        {
            string query = "SELECT * FROM [User]";
            var users = new List<User>();

            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new User()
                            {
                                Id = (int)reader["Id"],
                                Birthday = (DateTime)reader["Birthday"],
                                Gender = (int)reader["Gender"],
                                Rights = (bool)reader["Rights"],
                                AccountId = (int)reader["Account_Id"]
                            };

                            users.Add(user);
                        }

                        return users;
                    }
                }
            }
        }

        public User GetUserByAccountId(int accountId)
        {
            string query = "SELECT * FROM [User] WHERE Account_Id= @AccountId";
            var user = new User();

            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@AccountId", accountId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user = new User()
                            {
                                Id = (int)reader["Id"],
                                Birthday = (DateTime)reader["Birthday"],
                                Gender = (int)reader["Gender"],
                                Rights = (bool)reader["Rights"],
                                AccountId = (int)reader["Account_Id"]
                            };
                        }

                        return user;
                    }
                }
            }
        }
    }
}
