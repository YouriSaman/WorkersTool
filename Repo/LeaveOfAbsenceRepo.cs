using System;
using System.Collections.Generic;
using System.Text;
using Data;
using IContext;
using Models;

namespace Repo
{
    public class LeaveOfAbsenceRepo
    {
        private ILeaveOfAbsenceContext iContext;

        public LeaveOfAbsenceRepo(Context contextType)
        {
            if (contextType == Context.Mssql)
            {
                iContext = new LeaveOfAbsenceSqlContext();
            }
            else if (contextType == Context.Memory)
            {
                //iContext = MemoryContext;
            }
        }

        public void AddLeaveOfAbsence(int shiftId, int userId, string reason)
        {
            iContext.AddLeaveOfAbsence(shiftId, userId, reason);
        }

        public List<LeaveOfAbsence> GetLeaveOfAbsences()
        {
            return iContext.GetLeaveOfAbsences();
        }

        public void HideLeaveOfAbsence(int leaveOfAbsenceId, int userId)
        {
            iContext.HideLeaveOfAbsence(leaveOfAbsenceId, userId);
        }

        public List<int> GetHiddenLOAIdsOfUser(int userId)
        {
            return iContext.GetHiddenLOAIdsOfUser(userId);
        }

        public void AcceptLeaveOfAbsence(int leaveOfAbsenceId, int userId)
        {
            iContext.AcceptLeaveOfAbsence(leaveOfAbsenceId, userId);
        }

        public List<LeaveOfAbsence> GetLeaveOfAbsencesWithApproval()
        {
            return iContext.GetLeaveOfAbsencesWithApproval();
        }

        public void ApproveLeaveOfAbsence(int newUserId, int leaveOfAbsenceId)
        {
            iContext.ApproveLeaveOfAbsence(newUserId, leaveOfAbsenceId);
        }

        public void DisapproveLeaveOfAbsence(int newUserId, int leaveOfAbsenceId)
        {
            iContext.DisapproveLeaveOfAbsence(newUserId, leaveOfAbsenceId);
        }

        public void ChangeUserToShift(int newUserId, int shiftId)
        {
            iContext.ChangeUserToShift(newUserId, shiftId);
        }
    }
}
