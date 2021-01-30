using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinyToDo.Abstract.Services;
using MinyToDo.Api.Extensions;
using MinyToDo.Entity.DTO.Request;
using MinyToDo.Entity.Models;

namespace MinyToDo.Api.Controllers.Me
{
    [Route("Api/Me/[controller]")]
    [Authorize]
    public class TaskController : ApiController
    {
        private readonly IUserTaskService _userTaskService;
        private readonly IUserCategoryService _userCategoryService;

        public TaskController(IUserTaskService userTaskService, IUserCategoryService userCategoryService)
        {
            _userCategoryService = userCategoryService;
            _userTaskService = userTaskService;
        }

        #region  user: checks
        [NonAction]
        private async Task<bool> selectedCategoryRelatedToUser(Guid userCategoryId)
        {
            var selectedCategory = await _userCategoryService.GetById(userCategoryId);
            return selectedCategory?.ApplicationUserId == User.GetAuthorizedUserId();
        }
        [NonAction]
        private async Task<bool> selectedTaskBelongsToUser(UserTask userTask)
                        => await selectedCategoryRelatedToUser(userTask.UserCategoryId);
        #endregion

        #region read
        [HttpGet("{userCategoryId}")]
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

        #region create - update - delete
        [HttpPost]
        public async Task<IActionResult> CreateUserTask([FromBody] UserTaskRequest value)
        {
            if (value.UserCategoryId.HasValue is false)
                return BadRequest("You need to select a Category to add a new Task.");
                /*
                    ApiController attribute does validation but UserCategoryId property in UserTaskRequest not marked 
                    as required because update metot also uses UserTaskRequest
                    and every update metot won't need to change category.
                */
            
            if (await selectedCategoryRelatedToUser(value.UserCategoryId.Value))
            {
                var result = await _userTaskService.InsertAsync(value);

                return result != null
                ? Created("", new { response = result })
                : BadRequest(new { error = "Sorry, the task could not add" });
            }

            return Forbid();
        }

        [HttpPut("{userTaskId}")]
        public async Task<IActionResult> UpdateUserTask([FromRoute] Guid userTaskId, [FromBody] UserTaskRequest newValues)
        {
            var toBeUpdatedTask = await _userTaskService.GetById(userTaskId);
            if (toBeUpdatedTask == null) return NotFound("Task is not exist");

            var noNeedToCheckRelate = newValues.UserCategoryId.HasValue == false
            || toBeUpdatedTask.UserCategoryId == newValues.UserCategoryId.Value;

            if (noNeedToCheckRelate || await selectedCategoryRelatedToUser(newValues.UserCategoryId.Value))
            {
                var result = await _userTaskService.UpdateAsync(toBeUpdatedTask, newValues);

                return result != null
                ? Ok(new { response = result })
                : BadRequest(new { error = "Sorry, the task could not update" });
            }

            return Forbid();
        }

        [HttpDelete("{userTaskId}")]
        public async Task<IActionResult> DeleteUserTask([FromRoute] Guid userTaskId)
        {
            var toBeDeletedTask = await _userTaskService.GetById(userTaskId);
            if (toBeDeletedTask == null) return NotFound("Task is not exist");

            if (await selectedTaskBelongsToUser(toBeDeletedTask))
            {
                return await _userTaskService.DeleteAsync(toBeDeletedTask)
                ? Ok()
                : BadRequest(new { error = "Sorry, the task could not delete" });
            }

            return Forbid();
        }
        #endregion
    }
}