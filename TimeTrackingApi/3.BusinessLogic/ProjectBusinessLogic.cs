using System;
using System.Collections.Generic;
using TimeTrackingApi._0.Models;
using TimeTrackingApi._2.Repositoty.IRepository;

namespace TimeTrackingApi._3.BusinessLogic
{
    public class ProjectBusinessLogic 
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectBusinessLogic(IProjectRepository iProjectRepository)
        {
            _projectRepository = iProjectRepository;
        }

        public Project Create(Project project)
        {
            try
            {
                project.ProjectId = string.Empty;
                project.CreationDate = DateTime.Now;
                project.TotalSpentTime = new Time();
                return _projectRepository.Create(project);
            }
            catch (Exception)
            {
                // write ex information 
                throw;
            }
        }

        public Project GetProjectById(string id)
        {
            try
            {
                return _projectRepository.GetProjectById(id);
            }
            catch (Exception)
            {
                // write ex information 
                throw;
            }
        }

        public ICollection<Project> GetProjectsList()
        {
            try
            {
                return _projectRepository.GetProjectsList();
            }
            catch (Exception)
            {
                // write ex information 
                throw;
            }
        }

        public bool ProjectNameExist(string projectName)
        {
            try
            {
                return _projectRepository.ProjectNameExist(projectName);
            }
            catch (Exception)
            {
                // write ex information 
                throw;
            }
        }

        public void Update(string ProjectId, Project project)
        {
            try
            {
                _projectRepository.Update(ProjectId, project);
            }
            catch (Exception)
            {
                // write ex information 
                throw;
            }            
        }

    }
}
