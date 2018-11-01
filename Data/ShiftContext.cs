using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace Data
{
    public class ShiftContext
    {
        private string connectionstring = "Server=mssql.fhict.local;Database=dbi383661_extra;User Id=dbi383661_extra;Password=YouriS12;";

        public List<Week> GetAllWeeks()
        {
            string query = "SELECT * From Week";
            var weeks = new List<Week>();

            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {   
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var week = new Week()
                            {
                                Weeknumber = (int)reader["Weeknumber"],
                                From = (DateTime)reader["From"],
                                Till = (DateTime)reader["Till"]
                            };
                            weeks.Add(week);
                        }

                        return weeks;
                    }
                }
            }
        }

        public Week GetWeekByNumber(int weeknumber)
        {
            string query = "SELECT * FROM Week WHERE Weeknumber=@Weeknumber";
            var week = new Week();
            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Weeknumber", weeknumber);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            week = new Week()
                            {
                                Weeknumber = (int)reader["Weeknumber"],
                                From = (DateTime)reader["From"],
                                Till = (DateTime)reader["Till"]
                            };
                        }
                    }

                    return week;
                }
            }
        }
        
        public List<Department> GetAllDepartments()
        {
            throw new NotImplementedException();
        }

        public void AddShift(Shift shift)
        {
            string query = "INSERT INTO Shift([User_Id], [Date], [StartTime], [EndTime]) " +
                           "VALUES(@UserId, @Date, @StartTime, @EndTime)";

            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", shift.UserId);
                    cmd.Parameters.AddWithValue("@Date", shift.Date);
                    cmd.Parameters.AddWithValue("@StartTime", shift.StartTime);
                    cmd.Parameters.AddWithValue("@EndTime", shift.EndTime);

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

        public void AddUserToShift(int ShiftId, int UserId)
        {
            throw new NotImplementedException();
        }

        public void AddLeaveOfAbsence(DateTime startTime, DateTime endTime, int UserId, string Reason)
        {
            throw new NotImplementedException();
        }

        public void ChangeUserToShift(int CurrentUserId, int NewUserId, int ShiftId)
        {
            throw new NotImplementedException();
        }

        public void ApproveLeaveOfAbsence()
        {
            throw new NotImplementedException();
        }

        public List<Shift> GetShiftsOfWeekFromUser(int weeknumber, int userId)
        {
            string query = "SELECT * FROM Shift WHERE Weeknumber=@Weeknumber AND User_Id=@UserId";
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
                                Weeknumber = (int)reader["Weeknumber"]
                            };
                            shifts.Add(shift);
                        }

                        return shifts;
                    }
                }
            }
        }
    }
}
