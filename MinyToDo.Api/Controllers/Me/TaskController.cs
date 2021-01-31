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
        private async Task<bool> isCategoryRelatedToUser(Guid userCategoryId)
        {
            var selectedCategory = await _userCategoryService.GetById(userCategoryId);
            return selectedCategory?.ApplicationUserId == User.GetAuthorizedUserId();
        }
        [NonAction]
        private async Task<bool> isTaskBelongToUser(UserTask userTask)
                        => await isCategoryRelatedToUser(userTask.UserCategoryId);
        #endregion

        #region read
        [HttpGet("{userCategoryId}")]
        public async Task<IActionResult> GetUserTasksByCategoryId([FromRoute] Guid userCategoryId)
        {
            if (await isCategoryRelatedToUser(userCategoryId))
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

            if (await isCategoryRelatedToUser(value.UserCategoryId.Value))
            {
                var result = await _userTaskService.InsertAsync(value);

                return result != null
                ? Created("", new { response = result })
                : BadRequest(new { error = "Sorry, the task could not be added" });
            }

            return Forbid();
        }

        [HttpPut("{userTaskId}")]
        public async Task<IActionResult> UpdateUserTask([FromRoute] Guid userTaskId, [FromBody] UserTaskRequest newValue)
        {
            var toBeUpdatedTask = await _userTaskService.GetById(userTaskId);
            if (toBeUpdatedTask == null) return NotFound("Task is not exist");

            var noNeedToCheckRelate = newValue.UserCategoryId.HasValue == false
            || toBeUpdatedTask.UserCategoryId == newValue.UserCategoryId.Value;

            if (await isTaskBelongToUser(toBeUpdatedTask) && (noNeedToCheckRelate || await isCategoryRelatedToUser(newValue.UserCategoryId.Value)))
            {
                var result = await _userTaskService.UpdateAsync(toBeUpdatedTask, newValue);

                return result != null
                ? Ok(new { response = result })
                : BadRequest(new { error = "Sorry, the task could not be updated" });
            }

            return Forbid();
        }

        [HttpDelete("{userTaskId}")]
        public async Task<IActionResult> DeleteUserTask([FromRoute] Guid userTaskId)
        {
            var toBeDeletedTask = await _userTaskService.GetById(userTaskId);
            if (toBeDeletedTask == null) return NotFound("Task is not exist");

            if (await isTaskBelongToUser(toBeDeletedTask))
            {
                return await _userTaskService.DeleteAsync(toBeDeletedTask)
                ? Ok()
                : BadRequest(new { error = "Sorry, the task could not be deleted" });
            }

            return Forbid();
        }
        #endregion
    }
}