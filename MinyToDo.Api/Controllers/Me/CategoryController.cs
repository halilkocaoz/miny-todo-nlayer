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
    public class CategoryController : ApiController
    {
        IUserCategoryService _userCategoryService;
        public CategoryController(IUserCategoryService userCategoryService)
        {
            _userCategoryService = userCategoryService;
        }
        private bool categoryRelatedToAuthorizedUser(UserCategory userCategory)
                => userCategory?.ApplicationUserId == User.GetAuthorizedUserId();

        #region read
        [HttpGet]
        public async Task<IActionResult> GetAllCategoriesForAuthorizedUser([FromQuery] bool withTasks = false)
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
            : BadRequest(new { error = "Sorry, the category could not add" });
        }

        [HttpPut("{userCategoryId}")]
        public async Task<IActionResult> UpdateUserCategory(Guid userCategoryId, UserCategoryRequest newValues)
        {
            var toBeUpdatedCategory = await _userCategoryService.GetById(userCategoryId);
            if (toBeUpdatedCategory == null) NoContent();

            if (categoryRelatedToAuthorizedUser(toBeUpdatedCategory))
            {
                var result = await _userCategoryService.UpdateAsync(toBeUpdatedCategory, newValues);

                return result != null
                ? Ok(new { response = result })
                : BadRequest(new { error = "Sorry, the category could not update" });
            }

            return Forbid();
        }

        [HttpDelete("{userCategoryId}")]
        public async Task<IActionResult> DeleteUserCategory(Guid userCategoryId)
        {
            var toBeDeletedCategory = await _userCategoryService.GetById(userCategoryId);
            if (toBeDeletedCategory == null) return NoContent();
            if (categoryRelatedToAuthorizedUser(toBeDeletedCategory))
            {
                var result = await _userCategoryService.DeleteAsync(toBeDeletedCategory);

                return result
                ? Ok()
                : BadRequest(new { error = "Sorry, the category could not delete" });
            }

            return Forbid();
        }
        #endregion
    }
}