using System;
using System.Collections.Generic;
using TimeTrackingApi._0.Models;
using TimeTrackingApi._2.Repositoty.IRepository;

namespace TimeTrackingApi._3.BusinessLogic
{
    public class UserBusinessLogic 
    {
        private readonly IUserRepository _userRepository;

        public UserBusinessLogic(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ICollection<User> GetUsersList()
        {
            try
            {
                return _userRepository.GetUsersList();
            }
            catch (Exception)
            {
                // write ex information 
                throw;
            }            
        }

        public User GetUserById(string id)
        {
            try
            {
                return _userRepository.GetUserById(id);
            }
            catch (Exception)
            {
                // write ex information 
                throw;
            }           
        }

        public User GetUserByUserName(string userName)
        {
            try
            {
                return _userRepository.GetUserByUserName(userName);
            }
            catch (Exception)
            {
                // write ex information 
                throw;
            }            
        }

        public User SaveNew(User user)
        {
            try
            {
                user.UserId = string.Empty;
                user.CreationDate = DateTime.Now;
                _userRepository.SaveNew(user);
                return user;
            }
            catch (Exception)
            {
                // write ex information 
                throw;
            }            
        }

        public void Update(string userId, User user)
        {
            try
            {
                user.UserId = null;
                _userRepository.Update(userId, user);
            }
            catch (Exception)
            {
                // write ex information 
                throw;
            }            
        }

        public bool UserNameExist(string userName)
        {
            try
            {
                return _userRepository.UserNameExist(userName);
            }
            catch (Exception)
            {
                // write ex information 
                throw;
            }            
        }

    }
}
