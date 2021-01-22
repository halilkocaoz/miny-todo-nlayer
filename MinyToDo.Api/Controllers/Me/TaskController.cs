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
        #region _
        private IUserTaskService _userTaskService;
        private IUserCategoryService _userCategoryService;

        public TaskController(IUserTaskService userTaskService, IUserCategoryService userCategoryService)
        {
            _userCategoryService = userCategoryService;
            _userTaskService = userTaskService;
        }
        #endregion

        #region  user: checks
        [NonAction]
        private async Task<bool> selectedCategoryRelatedToUser(Guid userCategoryId)
        {
            var selectedCategory = await _userCategoryService.GetById(userCategoryId);
            return selectedCategory?.ApplicationUserId == User.GetAuthorizedUserId();
        }

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
            if (await selectedCategoryRelatedToUser(value.UserCategoryId.Value))
            {
                var result = await _userTaskService.InsertAsync(value);

                return result != null
                ? Created("", new { result })
                : BadRequest(new { error = "Sorry, the task could not add" });
            }

            return Forbid();
        }

        [HttpPut("{userTaskId}")]
        public async Task<IActionResult> UpdateUserTask([FromRoute] Guid userTaskId, [FromBody] UserTaskRequest newValues)
        {
            var toBeUpdatedTask = await _userTaskService.GetById(userTaskId);
            if (toBeUpdatedTask == null) return NoContent();

            var isSelectedCategorySameToCurrent = toBeUpdatedTask.UserCategoryId == newValues.UserCategoryId.Value;
            if (isSelectedCategorySameToCurrent || await selectedCategoryRelatedToUser(newValues.UserCategoryId.Value))
            {
                var result = await _userTaskService.UpdateAsync(toBeUpdatedTask, newValues);

                return result != null
                ? Ok(new { result })
                : BadRequest(new { error = "Sorry, the task could not update" });
            }

            return Forbid();
        }

        [HttpDelete("{userTaskId}")]
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