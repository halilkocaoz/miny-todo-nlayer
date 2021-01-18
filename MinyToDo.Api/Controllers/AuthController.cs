
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MinyToDo.Api.Models.Auth;
using MinyToDo.Api.Services.Abstract;
using MinyToDo.Entity.Models;

namespace MinyToDo.Api.Controllers
{
    public class AuthController : ApiController
    {
        private UserManager<AppUser> _userManager;
        private IJwtTokenService _jwtTokenService;

        public AuthController(IJwtTokenService jwtTokenService, UserManager<AppUser> userManager)
        {
            _jwtTokenService = jwtTokenService;
            _userManager = userManager;
        }

        [HttpPost("Signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel value)
        {
            if (User.Identity.IsAuthenticated) return BadRequest();

            var newAppUser = new AppUser // todo: automapper
            {
                UserName = value.UserName,
                Email = value.Email,
                Name = value.Name,
                Surname = value.Surname
            };

            var result = await _userManager.CreateAsync(newAppUser, value.Password);
            if (result.Succeeded)
            {
                var token = await _jwtTokenService.CreateTokenAsync(newAppUser);
                return Ok(new { token });
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("Signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInModel value)
        {
            if (User.Identity.IsAuthenticated) return BadRequest();
            
            var isIdentifierEmail = value.Identifier.Contains('@');

            var appUser = isIdentifierEmail
            ? await _userManager.FindByEmailAsync(value.Identifier)
            : await _userManager.FindByNameAsync(value.Identifier);

            var isPasswordRelatedToFoundUser = appUser != null && await _userManager.CheckPasswordAsync(appUser, value.Password);
            if (isPasswordRelatedToFoundUser)
            {
                var token = await _jwtTokenService.CreateTokenAsync(appUser);
                return Ok(new { token });
            }
            return Unauthorized();
        }
    }
}