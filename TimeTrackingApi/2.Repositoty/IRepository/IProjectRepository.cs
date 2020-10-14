using TimeTrackingApi._0.Models;
using System.Collections.Generic;

namespace TimeTrackingApi._2.Repositoty.IRepository
{
    public interface IProjectRepository
    {
        ICollection<Project> GetProjectsList();

        Project GetProjectById(string id);

        Project Create(Project project);

        void Update(string ProjectId, Project project);

        bool ProjectNameExist(string projectName);
    }
}
