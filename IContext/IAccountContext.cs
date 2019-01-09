using System;
using System.Collections.Generic;
using Models;

namespace IContext
{
    public interface IAccountContext
    {
        int AddAccount(Account account);
        void AddBranch(Branch branch); //Voor branch stuff
        bool LoginCheck(Account account);
        void EditAccount(Account account);
        List<Account> GetAllAccounts();
        Account GetAccountByUsername(string username); //Is deze wel nodig?
        Account GetAccountById(int id);
        int GetAccountIdByUserId(int userId);
    }
}
