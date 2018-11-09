using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Data
{
    public class LeaveOfAbsenceContext
    {
        private string connectionstring = "Server=mssql.fhict.local;Database=dbi383661_extra;User Id=dbi383661_extra;Password=YouriS12;";

        public void AddLeaveOfAbsence(int shiftId, int userId, int userIdTake, string reason)
        {
            string query = "INSERT INTO SHIFT(Shift_Id, User_Id, User_Id_Take, Reason)" +
                           "VALUES(@ShiftId, @UserId, @UserIdTake, @Reason)";

            using (var conn = new SqlConnection(connectionstring))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@ShiftId", shiftId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@UserIdTake", userIdTake);
                    cmd.Parameters.AddWithValue("@Reason", reason);

                    cmd.ExecuteNonQuery();
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
