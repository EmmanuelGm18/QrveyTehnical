using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeTrackingApi._0.Models;
using TimeTrackingApi._2.Repositoty.IRepository;
using TimeTrackingApi._3.BusinessLogic;

namespace TimeTrackingApi.Controllers
{
    [Route("api/users/{userId:length(24)}/projects/{projectId:length(24)}/tasks")]
    [ApiExplorerSettings(GroupName = "ApiTaks")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TaskBusinessLogic _taskBll;

        #region constructor method

        public TasksController(ITaskRepository taskRepository, ITaskSpentTimeRepository taskSpentRepositor)
        {
            _taskBll = new TaskBusinessLogic(taskRepository, taskSpentRepositor);
        }

        #endregion

        /// <summary>
        /// Returns to tasks list for the user in the project
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(ICollection<Task>))]
        [ProducesResponseType(500)]
        [HttpGet]
        public IActionResult Get(string userId, string projectId)
        {
            try
            {
                return Ok(_taskBll.GetTaksList(userId, projectId));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

  
        /// <summary>
        /// Returns to a specific task information
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="projectId"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpGet("{taskId:length(24)}", Name = "GetTask")]
        [ProducesResponseType(200, Type = typeof(Project))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetTask(string userId, string projectId, string taskId)
        {
            try
            {
                var task = _taskBll.GetTaksDetail(userId, projectId, taskId);

                if (task == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(task);
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Create a new task in the selected project
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="projectId"></param>
        /// <param name="taskInfo"></param>
        /// <returns></returns>
        [ProducesResponseType(201, Type = typeof(Project))]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        [HttpPost]
        public IActionResult Post(string userId, string projectId,  [FromBody] Task taskInfo)
        {
            try
            {
                taskInfo.ProjectId = projectId;
                taskInfo.AsignedUserId = userId;
                _taskBll.Create(taskInfo);               
                return Created(string.Empty, taskInfo);                
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Update task status to inProgress
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="projectId"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpPut("InitTask/{taskId:length(24)}", Name = "InitTask")]
        public IActionResult Put(string userId, string projectId, string taskId)
        {
            try
            {
                var task = _taskBll.GetTaksDetail(userId, projectId, taskId);

                if (task == null)
                {
                    return NotFound();
                }
                else
                {
                    _taskBll.CloseTask( userId,  projectId,  taskId);
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        ///  Update task status to closed
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="projectId"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpPut("CloseTask/{taskId:length(24)}", Name = "CloseTask")]
        public IActionResult CloseTask(string userId, string projectId, string taskId)
        {
            try
            {
                var task = _taskBll.GetTaksDetail(userId, projectId, taskId);

                if (task == null)
                {
                    return NotFound();
                }
                else
                {
                    _taskBll.CloseTask(userId, projectId, taskId);
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Save time record for the task 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="projectId"></param>
        /// <param name="taskId"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        [HttpPut("RegistrySpentTime/{taskId:length(24)}", Name = "RegistrySpentTime")]
        public IActionResult RegistrySpentTime(string userId, string projectId, string taskId, SpentTimeRegistry time)
        {
            try
            {
                var task = _taskBll.GetTaksDetail(userId, projectId, taskId);

                if (task == null)
                {
                    return NotFound();
                }
                else
                {

                    _taskBll.RegistrySpentTime(userId, taskId, time);
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }


    }
}
