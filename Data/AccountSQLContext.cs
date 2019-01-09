using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using IContext;
using Models;

namespace Data
{
    public class AccountSqlContext: IAccountContext
    {
        private string connectionstring = "Server=mssql.fhict.local;Database=dbi383661_extra;User Id=dbi383661_extra;Password=YouriS12;";

        public int AddAccount(Account account)
        {
            string query = "INSERT INTO Account (Username, Password, Email, Name, Phonenumber, Adress, Postalcode, Residence, Image)" +
                           "VALUES (@Username, @Password, @Email, @Name, @Phonenumber, @Adress, @Postalcode, @Residence, @Image);" +
                           "SELECT @@IDENTITY AS NewId;";

            using (var conn = new SqlConnection(connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                { 
                    cmd.Parameters.AddWithValue("@Username", account.Username);
                    cmd.Parameters.AddWithValue("@Password", account.Password);
                    cmd.Parameters.AddWithValue("@Email", account.Email);
                    cmd.Parameters.AddWithValue("@Name", account.Name);
                    cmd.Parameters.AddWithValue("@Phonenumber", account.Phonenumber);
                    cmd.Parameters.AddWithValue("@Adress", account.Adress);
                    cmd.Parameters.AddWithValue("@Postalcode", account.Postalcode);
                    cmd.Parameters.AddWithValue("@Residence", account.Residence);
                    cmd.Parameters.AddWithValue("@Image", account.Image);

                    try
                    {
                        conn.Open();
                        SqlDataReader rowsAffected = cmd.ExecuteReader();

                        int accountId = 0;

                        while (rowsAffected.Read())
                        {
                            var decimalId = (decimal) rowsAffected["NewId"];

                            accountId = Convert.ToInt32(decimalId);
                        }
                        conn.Close();

                        return accountId;
                    }
                    catch (Exception errorException)
                    {
                        throw errorException;
                    }
                }
            }
        }

        public void AddBranch(Branch branch)
        {
            string query = "INSERT INTO Branch (Branchnumber, Account_Id)" +
                           "VALUES (@Branchnumber, @AccountId)";

            using (var conn = new SqlConnection(connectionstring))
            {

                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Branchnumber", branch.Branchnumber);
                    cmd.Parameters.AddWithValue("@AccountId", branch.AccountId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool LoginCheck(Account account)
        {
            string query = "SELECT * FROM Account WHERE Username=@Username AND Password=@Password";

            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Username", account.Username);
                    cmd.Parameters.AddWithValue("@Password", account.Password);

                    int result = cmd.ExecuteNonQuery();
                    if (result != 0)
                    {
                        return true;
                    }

                    return false;
                }
            }
        }

        public void EditAccount(Account account)
        {
            string query = "UPDATE Account SET Username = @Username, Password= @Password, Email= @Email, Name= @Name, Phonenumber = @Phonenumber, Adress= @Adress, Postalcode= @Postalcode, Residence= @Residence, Image= @Image WHERE Id= @AccountId";

            using (var conn = new SqlConnection(connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Username", account.Username);
                    cmd.Parameters.AddWithValue("@Password", account.Password);
                    cmd.Parameters.AddWithValue("@Email", account.Email);
                    cmd.Parameters.AddWithValue("@Name", account.Name);
                    cmd.Parameters.AddWithValue("@Phonenumber", account.Phonenumber);
                    cmd.Parameters.AddWithValue("@Adress", account.Adress);
                    cmd.Parameters.AddWithValue("@Postalcode", account.Postalcode);
                    cmd.Parameters.AddWithValue("@Residence", account.Residence);
                    cmd.Parameters.AddWithValue("@AccountId", account.Id);
                    cmd.Parameters.AddWithValue("@Image", account.Image);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Account> GetAllAccounts()
        {
            string query = "SELECT * FROM Account";
            var accounts = new List<Account>();

            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var account = new Account()
                            {
                                Id = (int)reader["Id"],
                                Username = (string)reader["Username"],
                                Password = (string)reader["Password"],
                                Email = (string)reader["Email"],
                                Name = (string)reader["Name"],
                                Phonenumber = (string)reader["Phonenumber"],
                                Adress = (string)reader["Adress"],
                                Postalcode = (string)reader["Postalcode"],
                                Residence = (string)reader["Residence"],
                                Image = (string)reader["Image"]
                            };

                            accounts.Add(account);
                        }

                        return accounts;
                    }
                    
                }
            }

        }

        public Account GetAccountByUsername(string username)
        {
            string query = "SELECT * FROM Account WHERE Username= @Username";
            var account = new Account();

            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Username", username);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            account = new Account()
                            {
                                Id = (int)reader["Id"],
                                Username = (string)reader["Username"],
                                Password = (string)reader["Password"],
                                Email = (string)reader["Email"],
                                Name = (string)reader["Name"],
                                Phonenumber = (string)reader["Phonenumber"],
                                Adress = (string)reader["Adress"],
                                Postalcode = (string)reader["Postalcode"],
                                Residence = (string)reader["Residence"],
                                Image = (string)reader["Image"]
                            };
                        }

                        return account;
                    }
                }
            }
        }

        public Account GetAccountById(int id)
        {
            string query = "SELECT * FROM Account WHERE Id= @Id";
            var account = new Account();

            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            account = new Account()
                            {
                                Id = (int)reader["Id"],
                                Username = (string)reader["Username"],
                                Password = (string)reader["Password"],
                                Email = (string)reader["Email"],
                                Name = (string)reader["Name"],
                                Phonenumber = (string)reader["Phonenumber"],
                                Adress = (string)reader["Adress"],
                                Postalcode = (string)reader["Postalcode"],
                                Residence = (string)reader["Residence"],
                                Image = (string)reader["Image"]
                            };
                        }

                        return account;
                    }
                }
            }
        }

        public int GetAccountIdByUserId(int userId)
        {
            string query = "SELECT Account_Id FROM [User] WHERE Id= @Id";
            int accountId = 0;

            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Id", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            accountId = (int) reader["Account_Id"];
                        }
                    }

                    return accountId;
                }
            }
        }

        public void DeleteAccount(Account account)
        {
            throw new NotImplementedException();
        }
    }
}
