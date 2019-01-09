using System;
using System.Collections.Generic;
using System.Text;
using Data;
using Models;

namespace Logic
{
    public class UserLogic
    {
        private UserSqlContext _userDao = new UserSqlContext();

        public void AddUser(User user)
        {
            _userDao.AddUser(user);
        }

        public List<User> GetAllUsers()
        {
            return _userDao.GetAllUsers();
        }

        public User GetUserByAccountId(int accountId)
        {
            return _userDao.GetUserByAccountId(accountId);
        }

        public void EditUser(User user)
        {
            _userDao.EditUser(user);
        }
    }
}
