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
    public class ProjectRepository : IProjectRepository
    {
        private readonly IMongoCollection<Project> _projectsCollection;

        public ProjectRepository(ITimeTrackingStoreDatabaseSettings settings, ISettingsService clientSettings)
        {
            MongoClient mdbClient = clientSettings.MongoSettings;
            var database = mdbClient.GetDatabase(settings.DatabaseName);
            _projectsCollection = database.GetCollection<Project>(settings.ProjectsCollectionName);
        }

        public Project Create(Project project)
        {
            _projectsCollection.InsertOne(project);
            return project;
        }

        public Project GetProjectById(string id)
        {
            return _projectsCollection.Find(Project => Project.ProjectId.Equals(id)).FirstOrDefault();
        }

        public ICollection<Project> GetProjectsList()
        {
            return _projectsCollection.Find(cli => true).ToList();
        }

        public bool ProjectNameExist(string projectName)
        {
            return _projectsCollection.Find(Project => Project.Name.Equals(projectName)).Any();
        }

        public void Update(string projectId, Project project)
        {
            _projectsCollection.ReplaceOne(Project => Project.ProjectId.Equals(projectId), project);
        }
    }
}
