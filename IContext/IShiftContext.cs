using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace IContext
{
    public interface IShiftContext
    {
        Shift GetShiftById(int shiftId);
        void AddShift(Shift shift);
        List<Shift> GetShiftsOfWeekFromUser(int weeknumber, int userId);
        List<int> GetShiftIdsOfUser(int userId);
        List<int> GetAllShiftIds();
        int GetUserIdOfShift(int shiftId);
    }
}
