using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TimeTrackingApi._0.Models;
using TimeTrackingApi._2.Repositoty.IRepository;
using TimeTrackingApi._3.BusinessLogic;

namespace TimeTrackingApi.Controllers
{
    [Route("api/users")]
    [ApiExplorerSettings(GroupName = "ApiUsers")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserBusinessLogic _userBll;
       
        public UsersController(IUserRepository userRepository)
        {
            _userBll = new UserBusinessLogic(userRepository);
        }

        /// <summary>
        /// Returns user list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<User>))]
        [ProducesResponseType(500)]
        public IActionResult Get()
        {
            try
            {
                return Ok(_userBll.GetUsersList());
            }
            catch (Exception)
            {                
                return StatusCode(500);
            }           
        }

        /// <summary>
        /// Returns user information by userId
        /// </summary>
        /// <param name="userId">is user value</param>
        /// <returns></returns>
        [HttpGet("{userId:length(24)}", Name = "GetUser")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Get(string userId)
        {
            try
            {
                var user = _userBll.GetUserById(userId);

                if(user == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(user);
                }                
            }
            catch (Exception)
            {               
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Returns user information by userName
        /// </summary>
        /// <param name="userName">user name value</param>
        /// <returns></returns>        
        [HttpGet("GetByUserName/{userName}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetByUserName(string userName)
        {
            try
            {
                var userInfo = _userBll.GetUserByUserName(userName);

                if (userInfo == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(userInfo);
                }
            }
            catch (Exception)
            {                
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="userInfo">user information</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(User))]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult Post([FromBody] User userInfo)
        {
            try
            {
                if (!_userBll.UserNameExist(userInfo.UserName))
                {
                    _userBll.SaveNew(userInfo);
                    return CreatedAtRoute("GetUser", new { userId = userInfo.UserId }, userInfo);
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

        /// <summary>
        /// Update a selected user information
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="userInfo">user information</param>
        /// <returns></returns>
        [HttpPut("{userId:length(24)}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Put(string userId, [FromBody] User userInfo)
        {
            try
            {
                var user = _userBll.GetUserById(userId);

                if (user == null)
                {
                    return NotFound();
                }
                else
                {
                    
                    _userBll.Update(userId, userInfo);
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
