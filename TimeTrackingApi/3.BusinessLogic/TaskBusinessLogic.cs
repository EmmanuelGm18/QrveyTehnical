using System;
using System.Collections.Generic;
using TimeTrackingApi._0.Models;
using TimeTrackingApi._2.Repositoty.IRepository;

namespace TimeTrackingApi._3.BusinessLogic
{
    public class TaskBusinessLogic
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskSpentTimeRepository _taskSpentRepository;

        public TaskBusinessLogic(ITaskRepository taskRepository, ITaskSpentTimeRepository taskSpentRepository)
        {
            _taskRepository = taskRepository;
            _taskSpentRepository = taskSpentRepository;
        }

        public Task Create(Task task)
        {
            try
            {
                task.TaskId = string.Empty;
                task.CreationDate = DateTime.Now;
                task.SpentTime = null;
                task.TaskStatus = Status.Created;
                return _taskRepository.Create(task);
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal ICollection<Task> GetTaksList(string userId, string projectId)
        {
            try
            {
               var taskList = _taskRepository.GetListByUserAndProject(userId, projectId);

                foreach (var task in taskList)
                {
                    task.SpentTime = GetSpentTimeDetailst(task.TaskId);
                }

                return taskList;
            }
            catch (Exception ex)
            {
                // write ex information in log
                throw;
            } 
        }

        internal Task GetTaksDetail(string userId, string projectId, string taskId)
        {
            try
            {
                Task taskDetail = _taskRepository.GetById(userId, projectId, taskId);

                if (taskDetail != null)
                    taskDetail.SpentTime = GetSpentTimeDetailst(taskId);

                return taskDetail;
            }
            catch (Exception ex)
            {
                // write ex information in log
                throw;
            }
        }

        internal void RegistrySpentTime(string userId, string taskId, SpentTimeRegistry spentTime)
        {
            try
            {
                spentTime.TaskId = string.Empty;
                spentTime.CreationDate = DateTime.Now;
                spentTime.UserId = userId;
                spentTime.TaskId = taskId;
                
                _taskSpentRepository.Create(spentTime);
            }
            catch (Exception ex)
            {
                // write ex information in log
                throw;
            }
           
        }

        private ICollection<SpentTimeRegistry> GetSpentTimeDetailst(string taskId)
        {
            try
            {
                return _taskSpentRepository.GetListByTaskId(taskId);
            }
            catch (Exception ex)
            {
                // write ex information in log
                throw;
            }
        }

        internal void CloseTask(string userId, string projectId, string taskId)
        {
            try
            {
                Task taskDetail = _taskRepository.GetById(userId, projectId, taskId);
                taskDetail.TaskStatus = Status.Closed;
                taskDetail.TaskId = null;

                _taskRepository.Update(taskId, taskDetail);
            }
            catch (Exception ex)
            {
                // write ex information in log
                throw;
            }
        }

        internal void InitTask(string userId, string projectId, string taskId)
        {
            try
            {
                Task taskDetail = _taskRepository.GetById(userId, projectId, taskId);
                taskDetail.TaskStatus = Status.Created;
                taskDetail.TaskId = null;

                _taskRepository.Update(taskId, taskDetail);
            }
            catch (Exception ex)
            {
                // write ex information in log
                throw;
            }
        }
    }
}
