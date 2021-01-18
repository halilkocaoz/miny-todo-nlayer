
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MinyToDo.Abstract.Services;
using MinyToDo.Entity.Models;

namespace MinyToDo.Api.Controllers
{
    public class AuthController : ApiController
    {
        private UserManager<AppUser> _userManager;

        [HttpPost("Signup")]
        public async Task<IActionResult> Signup([FromBody] string value)
        {
            return Ok();
        }

        [HttpPost("Signin")]
        public async Task<IActionResult> Signin([FromBody] string value)
        {
            return Ok();
        }
    }
}