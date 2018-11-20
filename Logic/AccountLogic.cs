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

        public Account GetAccountById(int accountId)
        {
            return _accountContext.GetAccountById(accountId);
        }

        public Account GetAccountByUserId(int userId)
        {
            int accountId = _accountContext.GetAccountIdByUserId(userId);
            return GetAccountById(accountId);
        }

        public List<User> GetAllUsers()
        {
            return _accountContext.GetAllUsers();
        }

        public User GetUserByAccountId(int accountId)
        {
            return _accountContext.GetUserByAccountId(accountId);
        }

        public List<User> GetAllUsersWithAccounts()
        {
            var userAccounts = GetAllUsers();

            foreach (var user in userAccounts)
            {
                var account = GetAccountById(user.AccountId);
                user.Account = account;
            }

            return userAccounts;
        }
    }
}
