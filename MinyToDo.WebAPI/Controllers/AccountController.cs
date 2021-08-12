using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MinyToDo.WebAPI.Extensions;
using MinyToDo.Models.Entity;

namespace MinyToDo.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer"), Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        public AccountController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
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
            var authorizedUser = await userManager.FindByIdAsync(User.GetAuthorizedUserId().ToString());
            var result = await userManager.ChangePasswordAsync(authorizedUser, value.Current, value.New);
            return result.Succeeded ? NoContent() : BadRequest(result.Errors);
        }
    }
}