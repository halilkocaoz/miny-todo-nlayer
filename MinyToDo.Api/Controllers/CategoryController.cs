using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinyToDo.Abstract.Services;
using MinyToDo.Api.Extensions;

namespace MinyToDo.Api.Controllers
{
    [Authorize]
    public class CategoryController : ApiController
    {
        IUserCategoryService _userCategoryService;
        public CategoryController(IUserCategoryService userCategoryService)
        {
            _userCategoryService = userCategoryService;
        }

        [HttpGet("User")]
        public async Task<IActionResult> GetAllCategoriesForAuthorizedUser()
        {
            var result = await _userCategoryService.GetAllByUserId(User.GetUserId());
            return result?.ToList().Count > 0 ? Ok(new { response = result }) : NoContent();

        }
    }
}