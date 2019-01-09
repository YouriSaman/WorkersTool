using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using IContext;
using Models;

namespace Data
{
    public class LeaveOfAbsenceSqlContext:ILeaveOfAbsenceContext
    {
        private string connectionstring = "Server=mssql.fhict.local;Database=dbi383661_extra;User Id=dbi383661_extra;Password=YouriS12;";

        public void AddLeaveOfAbsence(int shiftId, int userId, string reason)
        {
            string query = "INSERT INTO LeaveOfAbsence(Shift_Id, Reason)" +
                           "VALUES(@ShiftId, @Reason)";

            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@ShiftId", shiftId);
                    cmd.Parameters.AddWithValue("@Reason", reason);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<LeaveOfAbsence> GetLeaveOfAbsences()
        {
            string query = "SELECT * FROM LeaveOfAbsence";
            var listLOA = new List<LeaveOfAbsence>();

            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var LOA = new LeaveOfAbsence()
                            {
                                Id = (int)reader["LeaveOfAbsenceId"],
                                ShiftId = (int)reader["Shift_Id"],
                                Reason = (string)reader["Reason"]
                            };
                            listLOA.Add(LOA);
                        }

                        return listLOA;
                    }
                }
            }
        }

        public void HideLeaveOfAbsence(int leaveOfAbsenceId, int userId)
        {
            string query = "INSERT INTO [Hide_LeaveOfAbsence]([LeaveOfAbsence_Id], [User_Id])" +
                           "VALUES(@LeaveOfAbsenceId, @UserId)";

            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@LeaveOfAbsenceId", leaveOfAbsenceId);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<int> GetHiddenLOAIdsOfUser(int userId)
        {
            string query = "SELECT LeaveOfAbsence_Id FROM [Hide_LeaveOfAbsence] WHERE User_Id= @UserId";
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
                            var id = (int) reader["LeaveOfAbsence_Id"];
                            ids.Add(id);
                        }

                        return ids;
                    }
                }
            }
        }

        public void AcceptLeaveOfAbsence(int leaveOfAbsenceId, int userId)
        {
            string query = "INSERT INTO User_LeaveOfAbsence(NewUser_Id, LeaveOfAbsence_Id, Approved)" +
                           "VALUES(@NewUserId, @LeaveOfAbsenceId, @Approved)";

            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@NewUserId", userId);
                    cmd.Parameters.AddWithValue("@LeaveOfAbsenceId", leaveOfAbsenceId);
                    cmd.Parameters.AddWithValue("@Approved", false);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<LeaveOfAbsence> GetLeaveOfAbsencesWithApproval()
        {
            string query = "SELECT * FROM LeaveOfAbsence inner join Shift on LeaveOfAbsence.Shift_Id=Shift.Id inner join Department on Department_Id=Department.Id inner join User_LeaveOfAbsence on LeaveOfAbsence.LeaveOfAbsenceId=User_LeaveOfAbsence.LeaveOfAbsence_Id WHERE Approved IS NOT NULL";

            var leaveOfAbsences = new List<LeaveOfAbsence>();

            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var LOA = new LeaveOfAbsence
                            {
                                Id = (int) reader["LeaveOfAbsenceId"],
                                Reason = (string) reader["Reason"],
                                IsApproved = (LeaveOfAbsence.Approved) reader["Approved"],
                                NewUserId = (int)reader["NewUser_Id"],
                                Shift = new Shift
                                {
                                    Id = (int)reader["Shift_Id"],
                                    Date = (DateTime) reader["Date"],
                                    StartTime = (TimeSpan) reader["StartTime"],
                                    EndTime = (TimeSpan) reader["EndTime"],
                                    Department = new Department()
                                    {
                                        Name = (string) reader["Name"]
                                    }
                                }
                            };
                            leaveOfAbsences.Add(LOA);
                        }

                        return leaveOfAbsences;
                    }
                }
            }
        }

        public void ApproveLeaveOfAbsence(int newUserId, int leaveOfAbsenceId)
        {
            string query = "DELETE FROM User_LeaveOfAbsence WHERE NewUser_Id=@NewUserId AND LeaveOfAbsence_Id=@LeaveOfAbsenceId";

            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@NewUserId", newUserId);
                    cmd.Parameters.AddWithValue("@LeaveOfAbsenceId", leaveOfAbsenceId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DisapproveLeaveOfAbsence(int newUserId, int leaveOfAbsenceId)
        {
            string query = "UPDATE User_LeaveOfAbsence SET Approved=@Approved WHERE NewUser_Id=@NewUserId AND LeaveOfAbsence_Id=@LeaveOfAbsenceId";

            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@NewUserId", newUserId);
                    cmd.Parameters.AddWithValue("@LeaveOfAbsenceId", leaveOfAbsenceId);
                    cmd.Parameters.AddWithValue("Approved", Convert.ToInt32(LeaveOfAbsence.Approved.Disapproved));

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ChangeUserToShift(int newUserId, int shiftId)
        {
            string query = "UPDATE Shift SET User_Id=@NewUserId WHERE Id=@ShiftId";

            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@NewUserId", newUserId);
                    cmd.Parameters.AddWithValue("@ShiftId", shiftId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
