using System;
using System.Collections.Generic;
using System.Text;
using Data;
using IContext;
using Models;

namespace Repo
{
    public class ShiftRepo
    {
        private IShiftContext iContext;

        public ShiftRepo(Context contextType)
        {
            if (contextType == Context.Mssql)
            {
                iContext = new ShiftSqlContext();
            }
            else if (contextType == Context.Memory)
            {
                //iContext = MemoryContext;
            }
        }

        public Shift GetShiftById(int shiftId)
        {
            return iContext.GetShiftById(shiftId);
        }

        public void AddShift(Shift shift)
        {
            iContext.AddShift(shift);
        }

        public List<Shift> GetShiftsOfWeekFromUser(int weeknumber, int userId)
        {
            return iContext.GetShiftsOfWeekFromUser(weeknumber, userId);
        }

        public List<int> GetShiftIdsOfUser(int userId)
        {
            return iContext.GetShiftIdsOfUser(userId);
        }

        public List<int> GetAllShiftIds()
        {
            return iContext.GetAllShiftIds();
        }

        public int GetUserIdOfShift(int shiftId)
        {
            return iContext.GetUserIdOfShift(shiftId);
        }
    }
}
