using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using IContext;
using Models;

namespace Data
{
    public class ShiftSqlContext:IShiftContext
    {
        private string connectionstring = "Server=mssql.fhict.local;Database=dbi383661_extra;User Id=dbi383661_extra;Password=YouriS12;";

        public Shift GetShiftById(int shiftId)
        {
            string query = "SELECT * FROM Shift WHERE Id= @Id";
            var shift = new Shift();

            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Id", shiftId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            shift = new Shift()
                            {
                                Id = (int)reader["Id"],
                                UserId = (int)reader["User_Id"],
                                DepartmentId = (int)reader["Department_Id"],
                                Date = (DateTime)reader["Date"],
                                StartTime = (TimeSpan)reader["StartTime"],
                                EndTime = (TimeSpan)reader["EndTime"],
                                Weeknumber = (int)reader["Weeknumber"]
                            };
                        }

                        return shift;
                    }
                }
            }
        }

        public void AddShift(Shift shift)
        {
            string query = "INSERT INTO Shift([User_Id], [Date], [StartTime], [EndTime], [Weeknumber], [Department_Id]) " +
                           "VALUES(@UserId, @Date, @StartTime, @EndTime, @Weeknumber, @DepartmentId)";

            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", shift.UserId);
                    cmd.Parameters.AddWithValue("@Date", shift.Date);
                    cmd.Parameters.AddWithValue("@StartTime", shift.StartTime);
                    cmd.Parameters.AddWithValue("@EndTime", shift.EndTime);
                    cmd.Parameters.AddWithValue("@Weeknumber", shift.Weeknumber);
                    cmd.Parameters.AddWithValue("@DepartmentId", shift.Department.Id);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (Exception errorException)
                    {
                        throw errorException;
                    }
                }
            }
        }

        public List<Shift> GetShiftsOfWeekFromUser(int weeknumber, int userId)
        {
            string query = "SELECT * FROM Shift WHERE Weeknumber= @Weeknumber AND User_Id= @UserId";
            var shifts = new List<Shift>();

            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Weeknumber", weeknumber);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var shift = new Shift()
                            {
                                Id = (int)reader["Id"],
                                UserId = (int)reader["User_Id"],
                                Date = (DateTime)reader["Date"],
                                StartTime = (TimeSpan)reader["StartTime"],
                                EndTime = (TimeSpan)reader["EndTime"],
                                Weeknumber = (int)reader["Weeknumber"],
                                DepartmentId = (int)reader["Department_Id"]
                            };
                            shifts.Add(shift);
                        }

                        return shifts;
                    }
                }
            }
        }
        
        public List<int> GetShiftIdsOfUser(int userId)
        {
            string query = "SELECT Id FROM [Shift] WHERE User_Id= @UserId";
            var ids = new List<int>();

            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var id = (int)reader["Id"];
                            ids.Add(id);
                        }

                        return ids;
                    }
                }
            }
        }

        public List<int> GetAllShiftIds()
        {
            string query = "SELECT Id FROM [Shift]";
            var ids = new List<int>();

            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var id = (int)reader["Id"];
                            ids.Add(id);
                        }

                        return ids;
                    }
                }
            }
        }

        public int GetUserIdOfShift(int shiftId)
        {
            string query = "SELECT User_Id FROM Shift WHERE Id= @ShiftId";
            int userId = 0;

            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@ShiftId", shiftId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userId = (int) reader["User_Id"];
                        }

                        return userId;
                    }
                }
            }
        }
    }
}
