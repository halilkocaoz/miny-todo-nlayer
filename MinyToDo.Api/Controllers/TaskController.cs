using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinyToDo.Abstract.Services;
using MinyToDo.Api.Extensions;
using MinyToDo.Api.Models;
using MinyToDo.Entity.Models;

namespace MinyToDo.Api.Controllers
{
    [Authorize]
    public class TaskController : ApiController
    {
        #region _
        private IUserTaskService _userTaskService;
        private IUserCategoryService _userCategoryService;
        private IMapper _mapper;

        public TaskController(IUserTaskService userTaskService, IUserCategoryService userCategoryService, IMapper mapper)
        {
            _mapper = mapper;
            _userCategoryService = userCategoryService;
            _userTaskService = userTaskService;
        }
        #endregion

        #region  user: checks
        [NonAction]
        private async Task<bool> selectedCategoryRelatedToUser(Guid userCategoryId)
        {
            var selectedCategory = await _userCategoryService.GetById(userCategoryId);
            return selectedCategory?.ApplicationUserId == User.GetUserId();
        }

        private async Task<bool> selectedTaskBelongsToUser(UserTask userTask)
                => await selectedCategoryRelatedToUser(userTask.UserCategoryId);
        #endregion

        #region user: read
        [HttpGet("User/{userCategoryId}")]
        public async Task<IActionResult> GetUserTasksByCategoryId([FromRoute] Guid userCategoryId)
        {
            if (await selectedCategoryRelatedToUser(userCategoryId))
            {
                var result = await _userTaskService.GetAllByCategoryId(userCategoryId);
                return result?.ToList().Count > 0 ? Ok(new { response = result }) : NoContent();
            }
            return Forbid();
        }
        #endregion

        #region user: create - update - delete
        [HttpPost("User")]
        public async Task<IActionResult> CreateUserTask([FromBody] UserTaskInput value)
        {
            if (await selectedCategoryRelatedToUser(value.UserCategoryId.Value))
            {
                var newUserTask = _mapper.Map<UserTask>(value);
                newUserTask.CreatedAt = DateTime.Now;
                var result = await _userTaskService.InsertAsync(newUserTask);

                return result != null
                ? Created("", new { result })
                : BadRequest(new { error = "Sorry, the task could not add" });
            }
            return Forbid();
        }

        [HttpPut("User/{userTaskId}")]
        public async Task<IActionResult> UpdateUserTask([FromRoute] Guid userTaskId, [FromBody] UserTaskInput value)
        {
            var toBeUpdatedTask = await _userTaskService.GetById(userTaskId);
            if (toBeUpdatedTask == null) return NoContent();

            if (await selectedCategoryRelatedToUser(value.UserCategoryId.Value))
            {
                _mapper.Map(value, toBeUpdatedTask);
                var result = await _userTaskService.UpdateAsync(toBeUpdatedTask);

                return result != null
                ? Ok(new { result })
                : BadRequest(new { error = "Sorry, the task could not update" });

            }
            return Forbid();
        }

        [HttpDelete("User/{userTaskId}")]
        public async Task<IActionResult> DeleteUserTask([FromRoute] Guid userTaskId)
        {
            var toBeDeletedTask = await _userTaskService.GetById(userTaskId);
            if (toBeDeletedTask == null) return NoContent();

            if (await selectedTaskBelongsToUser(toBeDeletedTask))
            {
                var result = await _userTaskService.DeleteAsync(toBeDeletedTask);
                return result
                ? Ok()
                : BadRequest(new { error = "Sorry, the task could not delete" });
            }
            return Forbid();
        }
        #endregion
    }
}