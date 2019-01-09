using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace IContext
{
    public interface ILeaveOfAbsenceContext
    {
        void AddLeaveOfAbsence(int shiftId, int userId, string reason);
        List<LeaveOfAbsence> GetLeaveOfAbsences();
        void HideLeaveOfAbsence(int leaveOfAbsenceId, int userId);
        List<int> GetHiddenLOAIdsOfUser(int userId);
        void AcceptLeaveOfAbsence(int leaveOfAbsenceId, int userId);
        List<LeaveOfAbsence> GetLeaveOfAbsencesWithApproval();
        void ApproveLeaveOfAbsence(int newUserId, int leaveOfAbsenceId);
        void DisapproveLeaveOfAbsence(int newUserId, int leaveOfAbsenceId);
        void ChangeUserToShift(int newUserId, int shiftId);
    }
}
