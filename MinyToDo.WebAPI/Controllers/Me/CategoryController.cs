using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinyToDo.Abstract.Services;
using MinyToDo.WebAPI.Extensions;
using MinyToDo.Models.DTO.Request;

namespace MinyToDo.WebAPI.Controllers.Me
{
    [Authorize(AuthenticationSchemes = "Bearer"), ApiController, Route("me/categories")]
    public class CategoryController : BaseController
    {
        private readonly IUserCategoryService categoryUserService;
        public CategoryController(IUserCategoryService userCategoryService)
        {
            categoryUserService = userCategoryService;
        }

        #region read
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            return ApiReturn(await categoryUserService.GetAllWithTasksByUserId(User.GetAuthorizedUserId(), false));
        }
        [HttpGet("tasks")]
        public async Task<IActionResult> GetAllCategoriesWithTasks()
        {
            return ApiReturn(await categoryUserService.GetAllWithTasksByUserId(User.GetAuthorizedUserId(), true));
        }
        #endregion

        #region create - update - delete
        [HttpPost]
        public async Task<IActionResult> CreateUserCategory([FromBody] UserCategoryRequest userCategoryRequest)
        {
            return ApiReturn(await categoryUserService.InsertAsync(User.GetAuthorizedUserId(), userCategoryRequest));
        }

        [HttpPut("{userCategoryId}")]
        public async Task<IActionResult> UpdateUserCategory([FromRoute] Guid userCategoryId, [FromBody] UserCategoryRequest userCategoryRequest)
        {
            return ApiReturn(await categoryUserService.UpdateAsync(User.GetAuthorizedUserId(), userCategoryId, userCategoryRequest));
        }

        [HttpDelete("{userCategoryId}")]
        public async Task<IActionResult> DeleteUserCategory([FromRoute] Guid userCategoryId)
        {
            return ApiReturn(await categoryUserService.DeleteAsync(User.GetAuthorizedUserId(), userCategoryId));
        }
        #endregion
    }
}