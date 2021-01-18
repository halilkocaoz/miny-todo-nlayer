
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MinyToDo.Api.Models.Auth;
using MinyToDo.Entity.Models;

namespace MinyToDo.Api.Controllers
{
    public class AuthController : ApiController
    {
        private UserManager<AppUser> _userManager;
        private readonly SymmetricSecurityKey _key;

        public AuthController(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]));
        }

        [HttpPost("Signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel value)
        {
            if (User.Identity.IsAuthenticated) return BadRequest();

            var newAppUser = new AppUser
            {
                UserName = value.UserName,
                Email = value.Email,
                Name = value.Name,
                Surname = value.Surname
            };
            var result = await _userManager.CreateAsync(newAppUser, value.Password);
            return Ok(result.Succeeded);
        }

        [HttpPost("Signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInModel value)
        {
            if (User.Identity.IsAuthenticated) return BadRequest();

            var appUser = value.Identifier.Contains('@')
            ? await _userManager.FindByEmailAsync(value.Identifier)
            : await _userManager.FindByNameAsync(value.Identifier);

            var isPasswordAndUserRelated = appUser != null && await _userManager.CheckPasswordAsync(appUser, value.Password);
            if (isPasswordAndUserRelated)
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.NameId, appUser.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, appUser.UserName),
                };

                var signCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

                var token = new JwtSecurityToken(
                    expires: DateTime.Now.AddDays(1),
                    claims: claims,
                    signingCredentials: signCredentials
                );
                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            return Unauthorized();
        }
    }
}