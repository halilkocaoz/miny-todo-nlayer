using System;
using System.ComponentModel.DataAnnotations;
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
        private IUserTaskService _userTaskService;
        private IUserCategoryService _userCategoryService;
        private IMapper _mapper;

        public TaskController(IUserTaskService userTaskService, IUserCategoryService userCategoryService, IMapper mapper)
        {
            _mapper = mapper;
            _userCategoryService = userCategoryService;
            _userTaskService = userTaskService;
        }

        [NonAction]
        private async Task<bool> selectedCategoryBelongsToUser(Guid userCategoryId)
        {
            var selectedCategory = await _userCategoryService.GetById(userCategoryId);
            return selectedCategory?.ApplicationUserId == User.GetUserId();
        }

        private async Task<bool> selectedTaskBelongsToUser(UserTask userTask)
                => await selectedCategoryBelongsToUser(userTask.UserCategoryId);

        [HttpGet("User/{userCategoryId}")]
        public async Task<IActionResult> GetUserTasksByCategoryIdAsync([FromRoute] Guid userCategoryId)
        {
            if (await selectedCategoryBelongsToUser(userCategoryId))
            {
                var result = await _userTaskService.GetAllByCategoryId(userCategoryId);
                return result?.ToList().Count > 0 ? Ok(new { response = result }) : NoContent();
            }
            return Forbid();
        }

        [HttpPost("User")]
        public async Task<IActionResult> CreateUserTask([FromBody] UserTaskInput value)
        {
            if (await selectedCategoryBelongsToUser(value.UserCategoryId.Value))
            {
                var newTask = _mapper.Map<UserTask>(value);
                newTask.CreatedAt = DateTime.Now;
                var result = await _userTaskService.InsertAsync(newTask);

                return result != null
                ? Created("", new { result })
                : StatusCode(500, new { error = "Sorry, the task could not add" });
            }
            return Forbid();
        }

        [HttpPut("User/{userTaskId}")]
        public async Task<IActionResult> UpdateUserTask([FromRoute] Guid userTaskId, [FromBody] UserTaskInput value)
        {
            var updatingTask = await _userTaskService.GetById(userTaskId);
            if (updatingTask == null) return NoContent();

            if (await selectedCategoryBelongsToUser(value.UserCategoryId.Value))
            {
                _mapper.Map(value, updatingTask);
                var result = await _userTaskService.UpdateAsync(updatingTask);

                return result != null
                ? Ok(new { result })
                : StatusCode(500, new { error = "Sorry, the task could not update" });
            }
            return Forbid();
        }

        [HttpDelete("User/{userTaskId}")]
        public async Task<IActionResult> DeleteUserTask([FromRoute] Guid userTaskId)
        {
            var deletingTask = await _userTaskService.GetById(userTaskId);
            if (deletingTask == null) return NoContent();

            if (await selectedTaskBelongsToUser(deletingTask))
            {
                var result = await _userTaskService.DeleteAsync(deletingTask);
                return result
                ? Ok()
                : StatusCode(500, new { error = "Sorry, the task could not delete" });
            }
            return Forbid();
        }
    }
}