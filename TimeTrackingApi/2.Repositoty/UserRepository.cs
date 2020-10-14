using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using TimeTrackingApi._0.Models;
using TimeTrackingApi._1.Data.Interface;
using TimeTrackingApi._2.Repositoty.IRepository;

namespace TimeTrackingApi._2.Repositoty
{
    public class UserRepository : IUserRepository
    {

        private readonly IMongoCollection<User> _usersCollection;

        public UserRepository(ITimeTrackingStoreDatabaseSettings settings, ISettingsService clientSettings)
        {   
            MongoClient mdbClient = clientSettings.MongoSettings;
            var database = mdbClient.GetDatabase(settings.DatabaseName);
            _usersCollection = database.GetCollection<User>(settings.UsersCollectionName);
        }

        public ICollection<User> GetUsersList()
        {
            return _usersCollection.Find(cli => true).ToList();
        }

        public User GetUserById(string id)
        {           
            return _usersCollection.Find(User => User.UserId.Equals(id)).FirstOrDefault();
        }

        public User GetUserByUserName(string userName)
        {
            return _usersCollection.Find(User => User.UserName.Equals(userName)).FirstOrDefault();
        }

        public User SaveNew(User user)
        {
            _usersCollection.InsertOne(user);
            return user;
        }

        public void Update(string userId, User user)
        {
            _usersCollection.FindOneAndReplace(User => user.UserId == userId, user);         
        }

        public bool UserNameExist(string userName)
        {
            return _usersCollection.Find(User => User.UserName.Equals(userName)).Any();
        }
    }
}
