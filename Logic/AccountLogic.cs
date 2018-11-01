using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Data;
using Models;

namespace Logic
{
    public class AccountLogic
    {
        private AccountContext _accountContext;

        public AccountLogic()
        {
            _accountContext = new AccountContext();
        }

        public void AddBrach(Branch branch, Account account)
        {
            branch.AccountId = _accountContext.AddAccount(account);
            _accountContext.AddBranch(branch);
        }

        public void AddUser(User user, Account account)
        {
            user.AccountId = _accountContext.AddAccount(account);
            _accountContext.AddUser(user);
        }

        public bool LoginCheck(Account account)
        {
            return _accountContext.LoginCheck(account);
        }

        public List<Account> GetAllAccounts()
        {
            return _accountContext.GetAllAccounts();
        }

        public Account GetAccountByUsername(string username)
        {
            return _accountContext.GetAccountByUsername(username);
        }

        public Account GetAccountById(int id)
        {
            return _accountContext.GetAccountById(id);
        }

        public List<User> GetAllUsers()
        {
            return _accountContext.GetAllUsers();
        }

        public List<Account> GetAllUserAccounts()
        {
            var userAccounts = new List<Account>();

            foreach (var user in GetAllUsers())
            {
                var account = GetAccountById(user.AccountId);
                userAccounts.Add(account);
            }

            return userAccounts;
        }
    }
}
