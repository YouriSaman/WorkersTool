using System;
using System.Collections.Generic;
using Data;
using IContext;
using Models;

namespace Repo
{
    public class AccountRepo
    {
        private IAccountContext iContext;

        public AccountRepo(Context contextType)
        {
            if (contextType == Context.Mssql)
            {
                iContext = new AccountSqlContext();
            }
            else if(contextType == Context.Memory)
            {
                //iContext = MemoryContext;
            }
        }

        public int AddAccount(Account account)
        {
            return iContext.AddAccount(account);
        }

        public void AddBranch(Branch branch) //Voor branch stuff
        {
            iContext.AddBranch(branch);
        }

        public bool LoginCheck(Account account)
        {
            return iContext.LoginCheck(account);
        }

        public void EditAccount(Account account)
        {
            iContext.EditAccount(account);
        }

        public List<Account> GetAllAccounts()
        {
            return iContext.GetAllAccounts();
        }

        public Account GetAccountByUsername(string username)
        {
            return iContext.GetAccountByUsername(username);
        }

        public Account GetAccountById(int id)
        {
            return iContext.GetAccountById(id);
        }

        public int GetAccountIdByUserId(int userId)
        {
            return iContext.GetAccountIdByUserId(userId);
        }
    }
}
