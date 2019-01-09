using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Data;
using Models;
using Repo;

namespace Logic
{
    public class AccountLogic
    {
        private AccountRepo _accountRepo = new AccountRepo(Context.Mssql);
        //private AccountDAO _accountDao;
        private UserLogic _userLogic;

        public AccountLogic()
        {
            //_accountDao = new AccountDAO();
            _userLogic = new UserLogic();
        }

        public void AddBrach(Branch branch, Account account)
        {
            branch.AccountId = _accountRepo.AddAccount(account);
            _accountRepo.AddBranch(branch);
        }

        public void AddAccount(User user, Account account)
        {
            user.AccountId = _accountRepo.AddAccount(account);
            _userLogic.AddUser(user);
        }

        public bool LoginCheck(Account account)
        {
            return _accountRepo.LoginCheck(account);
        }

        public List<Account> GetAllAccounts()
        {
            return _accountRepo.GetAllAccounts();
        }

        public Account GetAccountByUsername(string username)
        {
            return _accountRepo.GetAccountByUsername(username);
        }

        public Account GetAccountById(int accountId)
        {
            return _accountRepo.GetAccountById(accountId);
        }

        public int GetAccountIdByUserId(int userId)
        {
           return _accountRepo.GetAccountIdByUserId(userId);
        }

        public Account GetAccountByUserId(int userId)
        {
            int accountId = GetAccountIdByUserId(userId);
            return GetAccountById(accountId);
        }

        public List<User> GetAllUsers()
        {
            return _userLogic.GetAllUsers();
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

        public void EditAccount(Account account)
        {
            _accountRepo.EditAccount(account);
        }
    }
}
