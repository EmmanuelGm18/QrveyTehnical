using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTrackingApi._0.Models;
using TimeTrackingApi._1.Data.Interface;
using TimeTrackingApi._2.Repositoty.IRepository;

namespace TimeTrackingApi._2.Repositoty
{
    public class TaskSpentTimeRepository : ITaskSpentTimeRepository
    {
        private readonly IMongoCollection<SpentTimeRegistry> _TaskSpentCollection;

        public TaskSpentTimeRepository(ITimeTrackingStoreDatabaseSettings settings, ISettingsService clientSettings)
        {
            MongoClient mdbClient = clientSettings.MongoSettings;
            var database = mdbClient.GetDatabase(settings.DatabaseName);
            _TaskSpentCollection = database.GetCollection<SpentTimeRegistry>(settings.TaskSpentTimeProjectsCollectionName);
        }

        public SpentTimeRegistry Create(SpentTimeRegistry taskSpentTime)
        {           
            _TaskSpentCollection.InsertOne(taskSpentTime);
            return taskSpentTime;
        }
       
        public ICollection<SpentTimeRegistry> GetListByTaskId(string taskId)
        {
            return _TaskSpentCollection.Find(SpentTimeRegistry => SpentTimeRegistry.TaskId.Equals(taskId)).ToList();
        }

        public ICollection<SpentTimeRegistry> GetListByUserId(string userId)
        {
            return _TaskSpentCollection.Find(SpentTimeRegistry => SpentTimeRegistry.UserId.Equals(userId)).ToList();
        }
    }
}
