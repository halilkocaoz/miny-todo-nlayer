using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinyToDo.Abstract.Services;
using MinyToDo.Api.Extensions;
using MinyToDo.Entity.Models;

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
        
        public class CategoryInput
        {
            [Required]
            [MinLength(3)]
            [MaxLength(30)]
            public string Name { get; set; }
        }

        [HttpPost("User")]
        public async Task<IActionResult> UserCategoryCreate([FromBody] CategoryInput value)
        {
            var newUserCategory = new UserCategory(User.GetUserId(), value.Name);
            var result = await _userCategoryService.InsertAsync(newUserCategory);

            return result != null
            ? Created("", new { response = result })
            : StatusCode(500, new { error = "Sorry, the category could not add" });
        }
    }
}