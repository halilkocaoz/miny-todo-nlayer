using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinyToDo.Abstract.Services;
using MinyToDo.WebAPI.Extensions;
using MinyToDo.Models.DTO.Request;

namespace MinyToDo.WebAPI.Controllers.Me
{
    [Authorize(AuthenticationSchemes = "Bearer"), ApiController, Route("me/tasks")]
    public class TaskController : BaseController
    {
        private readonly IUserTaskService taskUserService;
        public TaskController(IUserTaskService userTaskService)
        {
            taskUserService = userTaskService;
        }

        #region read
        [HttpGet("{userCategoryId}")]
        public async Task<IActionResult> GetUserTasksByCategoryId([FromRoute] Guid userCategoryId)
        {
            return ApiReturn(await taskUserService.GetAllByCategoryIdAsync(User.GetAuthorizedUserId(), userCategoryId));
        }
        #endregion

        #region create - update - delete
        [HttpPost]
        public async Task<IActionResult> CreateUserTask([FromBody] UserTaskRequest userTaskRequest)
        {
            return ApiReturn(await taskUserService.InsertAsync(User.GetAuthorizedUserId(), userTaskRequest));
        }

        [HttpPut("{userTaskId}")]
        public async Task<IActionResult> UpdateUserTask([FromRoute] Guid userTaskId, [FromBody] UserTaskRequest userTaskRequest)
        {
            return ApiReturn(await taskUserService.UpdateAsync(User.GetAuthorizedUserId(), userTaskId, userTaskRequest));
        }

        [HttpDelete("{userTaskId}")]
        public async Task<IActionResult> DeleteUserTask([FromRoute] Guid userTaskId)
        {
            return ApiReturn(await taskUserService.DeleteAsync(User.GetAuthorizedUserId(), userTaskId));
        }
        #endregion
    }
}