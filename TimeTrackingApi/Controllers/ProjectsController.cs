using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TimeTrackingApi._0.Models;
using TimeTrackingApi._2.Repositoty.IRepository;
using TimeTrackingApi._3.BusinessLogic;

namespace TimeTrackingApi.Controllers
{
    [Route("api/users/{userId:length(24)}/projects")]
    [ApiExplorerSettings(GroupName = "ApiProjects")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ApiController]
    public class ProjectsController : ControllerBase
    {

        private readonly ProjectBusinessLogic _projectBll;

        #region constructor method

        public ProjectsController(IProjectRepository projectRepository)
        {
            _projectBll = new ProjectBusinessLogic(projectRepository);
        }

        #endregion

        /// <summary>
        /// Returns to all projects list 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(ICollection<Project>))]       
        [ProducesResponseType(500)]
        [HttpGet]
        public IActionResult GetProjectlist(string userId)
        {
            try
            {
                return Ok(_projectBll.GetProjectsList());
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Returns to a project information by projectId
        /// </summary>
        /// <param name="projectId">Project unique identificator</param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(Project))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpGet("{projectId:length(24)}", Name = "GetProject")]
        public IActionResult Get(string projectId)
        {
            try
            {
                var project = _projectBll.GetProjectById(projectId);

                if (project == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(project);
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Create a new project
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="projectInfo">Project information</param>
        /// <returns></returns>
        ///  [HttpPost]
        [ProducesResponseType(201, Type = typeof(Project))]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        [HttpPost]
        public IActionResult Post(string userId, [FromBody] Project projectInfo)
        {
            try
            {
                if (!_projectBll.ProjectNameExist(projectInfo.Name))
                {
                    _projectBll.Create(projectInfo);
                    return Created(string.Empty, projectInfo);
                }
                else
                {
                    return Conflict();
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }        

    }
}
