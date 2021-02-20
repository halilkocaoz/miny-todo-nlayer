using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinyToDo.Abstract.Services;
using MinyToDo.Api.Extensions;
using MinyToDo.Models.DTO.Request;
using MinyToDo.Models.Entity;

namespace MinyToDo.Api.Controllers.Me
{
    [Route("Api/Me/[controller]")]
    [Authorize]
    public class CategoryController : ApiController
    {
        private readonly IUserCategoryService _userCategoryService;
        public CategoryController(IUserCategoryService userCategoryService)
        {
            _userCategoryService = userCategoryService;
        }

        #region read
        [HttpGet]
        public async Task<IActionResult> GetAllCategoriesForAuthorizedUser([FromQuery] bool withTasks)
        {
            var result = withTasks
            ? await _userCategoryService.GetAllWithTasksByUserId(User.GetAuthorizedUserId())
            : await _userCategoryService.GetAllByUserId(User.GetAuthorizedUserId());

            return result?.ToList().Count > 0 ? Ok(new { response = result }) : NoContent();
        }
        #endregion

        #region create - update - delete

        [HttpPost]
        public async Task<IActionResult> CreateUserCategory([FromBody] UserCategoryRequest value)
        {
            var result = await _userCategoryService.InsertAsync(User.GetAuthorizedUserId(), value);

            return result != null
            ? Created("", new { response = result })
            : BadRequest(new { error = "Sorry, the category could not be added" });
        }

        [HttpPut("{userCategoryId}")]
        public async Task<IActionResult> UpdateUserCategory([FromRoute] Guid userCategoryId, [FromBody] UserCategoryRequest newValues)
        {
            var toBeUpdatedCategory = await _userCategoryService.GetById(userCategoryId);
            if (toBeUpdatedCategory == null) return NotFound("Category is not exist");

            if (toBeUpdatedCategory.RelatedToGivenUserId(User.GetAuthorizedUserId()))
            {
                var result = await _userCategoryService.UpdateAsync(toBeUpdatedCategory, newValues);

                return result != null
                ? Ok(new { response = result })
                : BadRequest(new { error = "Sorry, the category could not be updated" });
            }

            return Forbid();
        }

        [HttpDelete("{userCategoryId}")]
        public async Task<IActionResult> DeleteUserCategory([FromRoute] Guid userCategoryId)
        {
            var toBeDeletedCategory = await _userCategoryService.GetById(userCategoryId);
            if (toBeDeletedCategory == null) return NotFound("Category is not exist");

            if (toBeDeletedCategory.RelatedToGivenUserId(User.GetAuthorizedUserId()))
            {
                return await _userCategoryService.DeleteAsync(toBeDeletedCategory)
                ? Ok()
                : BadRequest(new { error = "Sorry, the category could not be deleted" });
            }

            return Forbid();
        }
        #endregion
    }
}