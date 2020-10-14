using System.Collections.Generic;
using TimeTrackingApi._0.Models;

namespace TimeTrackingApi._2.Repositoty.IRepository
{
    public interface IUserRepository
    {
        ICollection<User> GetUsersList();

        User GetUserById(string id);

        User GetUserByUserName(string userName);

        User SaveNew(User user);

        void Update(string userId, User user);      

        bool UserNameExist(string userName);

    }
}
