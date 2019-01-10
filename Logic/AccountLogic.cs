using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Data;
using Models;
using Repo;

namespace Logic
{
    public class AccountLogic
    {
        private AccountRepo _accountRepo = new AccountRepo(Context.Mssql);
        private UserLogic _userLogic;

        public AccountLogic()
        {
            _userLogic = new UserLogic();
        }

        public void AddBrach(Branch branch, Account account)
        {
            branch.AccountId = _accountRepo.AddAccount(account);
            _accountRepo.AddBranch(branch);
        }

        public void AddAccount(User user, Account account)
        {
            if (AccountCheck(account))
            {
                user.AccountId = _accountRepo.AddAccount(account);
                /*return*/ _userLogic.AddUser(user);
            }
            else
            {
                //return false;
            }


        }

        private bool AccountCheck(Account account)
        {
            if (EmptyFieldCheck(account) && NameCheck(account.Name) && PostalCodeCheck(account.Postalcode))
            {
                return true;
            }

            return false;
        }

        public bool EmptyFieldCheck(Account account)
        {
            try
            {
                if (account.Username.Length > 0 && account.Password.Length > 0 && account.Name.Length > 0 && account.Postalcode.Length > 0 && account.Adress.Length > 0 && account.Email.Length > 0 && account.Image.Length > 0 && account.Phonenumber.Length > 0 && account.Residence.Length > 0)
                {
                    return true;
                }
            }
            catch (NullReferenceException)
            {
                return false;
            }

            return false;
        }

        private bool NameCheck(string name)
        {
            try
            {
                //Name (Secondname, etc.) Lastname
                //Does not work for d'Haag, de Boer, etc.
                return Regex.IsMatch(name, @"^[A-Z][a-z]*(\s[A-Z][a-z]*)+$");
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }

        private bool PostalCodeCheck(string postalcode)
        {
            try
            {
                /*Dutch postalcode*/
                return Regex.IsMatch(postalcode, @"^[1-9][0-9]{3}\s?[a-zA-Z]{2}$");
            }
            catch (NullReferenceException)
            {
                return false;
            }
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

        public bool EditAccount(Account account)
        {
            if (AccountCheck(account))
            {
                _accountRepo.EditAccount(account);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
