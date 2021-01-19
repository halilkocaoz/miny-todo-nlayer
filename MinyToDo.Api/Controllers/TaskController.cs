using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MinyToDo.Abstract.Services;
using MinyToDo.Api.Extensions;
using MinyToDo.Entity.Models;

namespace MinyToDo.Api.Controllers
{
    public class TaskController : ApiController
    {
        private IUserTaskService _userTaskService;
        private IUserCategoryService _userCategoryService;

        public TaskController(IUserTaskService userTaskService)
        {
            _userTaskService = userTaskService;
        }

        [NonAction]
        private async Task<bool> selectedCategoryBelongsToUser(Guid userCategoryId)
        {
            var selectedCategory = await _userCategoryService.GetById(userCategoryId);
            return selectedCategory?.ApplicationUserId == User.GetUserId();
        }

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
        public class UserTaskInput
        {
            [Required]
            public Guid? UserCategoryId { get; set; }
            [Required]
            [MaxLength(200)]
            public string Content { get; set; }
            public string LongDescription { get; set; }
            public DateTime DueDate { get; set; } = DateTime.Now.AddDays(1);
        }

        [HttpPost("User")]
        public async Task<IActionResult> UserTaskCreate([FromBody] UserTaskInput value)
        {
            if (await selectedCategoryBelongsToUser(value.UserCategoryId.Value))
            {
                var newTask = new UserTask // todo: automapper
                {
                    UserCategoryId = value.UserCategoryId.Value,
                    Content = value.Content,
                    LongDescription = value.LongDescription,
                    DueDate = value.DueDate
                };
                newTask.CreatedAt = DateTime.Now;
                var result = await _userTaskService.InsertAsync(newTask);

                return result != null
                ? Created("", new { result })
                : StatusCode(500, new { error = "Sorry, the task could not be added" });
            }
            return Forbid();
        }
    }
}