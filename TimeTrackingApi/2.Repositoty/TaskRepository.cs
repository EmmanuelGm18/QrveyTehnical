using MongoDB.Driver;
using System;
using System.Collections.Generic;
using TimeTrackingApi._0.Models;
using TimeTrackingApi._1.Data.Interface;
using TimeTrackingApi._2.Repositoty.IRepository;

namespace TimeTrackingApi._2.Repositoty
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IMongoCollection<Task> _taskCollection;

        public TaskRepository(ITimeTrackingStoreDatabaseSettings settings, ISettingsService clientSettings)
        {
            MongoClient mdbClient = clientSettings.MongoSettings;
            var database = mdbClient.GetDatabase(settings.DatabaseName);
            _taskCollection = database.GetCollection<Task>(settings.TaskCollectionName);
        }

        public Task Create(Task task)
        {
            _taskCollection.InsertOne(task);
            return task;
        }

        public Task GetById(string userId, string projectId, string taskId)
        {
            return _taskCollection.Find(Task => Task.AsignedUserId.Equals(userId) && Task.ProjectId.Equals(projectId) && Task.TaskId.Equals(taskId)).FirstOrDefault();
        }

        public ICollection<Task> GetList()
        {
            return _taskCollection.Find(task => true).ToList();
        }

        public ICollection<Task> GetListByProjectId(string projectId)
        {
            return _taskCollection.Find(Task => Task.ProjectId.Equals(projectId)).ToList();
        }

        public ICollection<Task> GetListByUserAndProject(string userId, string projectId)
        {
            return _taskCollection.Find(Task => Task.AsignedUserId.Equals(userId) && Task.ProjectId.Equals(projectId)).ToList();
        }

        public void Update(string taskId, Task task)
        {
            _taskCollection.ReplaceOne(Project => Project.ProjectId.Equals(taskId), task);
        }
    }
}
