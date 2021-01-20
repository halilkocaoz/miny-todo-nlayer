using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MinyToDo.Api.Extensions;
using MinyToDo.Entity.Models;

namespace MinyToDo.Api.Controllers
{
    [Authorize]
    public class AccountController : ApiController
    {
        private UserManager<AppUser> _userManager;
        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public class Password
        {
            [Required]
            public string Current { get; set; }
            [Required]
            public string New { get; set; }
        }

        [HttpPut("password")]
        public async Task<IActionResult> PasswordChange([FromBody] Password value)
        {
            var authorizedUser = await _userManager.FindByIdAsync(User.GetUserId().ToString());
            var result = await _userManager.ChangePasswordAsync(authorizedUser, value.Current, value.New);
            return result.Succeeded ? Ok("Changed") : BadRequest(result.Errors);
        }
    }
}