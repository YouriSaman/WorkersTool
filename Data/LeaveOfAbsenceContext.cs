using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace Data
{
    public class LeaveOfAbsenceContext
    {
        private string connectionstring = "Server=mssql.fhict.local;Database=dbi383661_extra;User Id=dbi383661_extra;Password=YouriS12;";

        public void AddLeaveOfAbsence(int shiftId, int userId, string reason)
        {
            string query = "INSERT INTO LeaveOfAbsence(Shift_Id, User_Id_Shift, Reason)" +
                           "VALUES(@ShiftId, @UserId, @Reason)";

            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@ShiftId", shiftId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
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

        public void HideLeaveOfAbsence(int shiftId, int userId)
        {
            string query = "INSERT INTO [Show_LOA]([Shift_Id], [User_Id], [Show])" +
                           "VALUES(@ShiftId, @UserId, @Show)";

            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@ShiftId", shiftId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("Show", false);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<int> GetHiddenShiftIdsOfUser(int userId)
        {
            string query = "SELECT Shift_Id FROM [Show_LOA] WHERE User_Id= @UserId";
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
                            var id = (int) reader["Shift_Id"];
                            ids.Add(id);
                        }

                        return ids;
                    }
                }
            }
        }

        public void ApproveLeaveOfAbsence()
        {
            throw new NotImplementedException();
        }

        public void ChangeUserToShift(int CurrentUserId, int NewUserId, int ShiftId)
        {
            throw new NotImplementedException();
        }
    }
}
