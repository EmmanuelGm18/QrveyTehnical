using System.Collections.Generic;
using TimeTrackingApi._0.Models;

namespace TimeTrackingApi._2.Repositoty.IRepository
{
    public interface ITaskRepository
    {
        ICollection<Task> GetList();

        ICollection<Task> GetListByProjectId(string projectId);

        ICollection<Task> GetListByUserAndProject(string userId, string projectId);

        Task GetById(string userId, string projectId, string id);

        Task Create(Task task);

        void Update(string taskId, Task task);        
    }
}
