using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Data
{
    public class ShiftContext
    {
        private string connectionstring = "Server=mssql.fhict.local;Database=dbi383661_extra;User Id=dbi383661_extra;Password=YouriS12;";

        public List<Department> GetAllDepartments()
        {
            throw new NotImplementedException();
        }

        public void AddShift(Shift shift)
        {
            throw new NotImplementedException();
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


    }
}
