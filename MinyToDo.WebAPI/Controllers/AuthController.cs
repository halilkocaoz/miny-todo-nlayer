using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MinyToDo.Abstract.Services;
using MinyToDo.Models.DTO.Request;
using MinyToDo.WebAPI.Services.Abstract;
using MinyToDo.Models.Entity;

namespace MinyToDo.WebAPI.Controllers
{
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IJwtTokenService jwtTokenService;
        private readonly IUserCategoryService userCategoryService;
        private readonly IMapper mapper;
        public AuthController(IMapper mapper, IJwtTokenService jwtTokenService, UserManager<AppUser> userManager, IUserCategoryService userCategoryService)
        {
            this.mapper = mapper;
            this.jwtTokenService = jwtTokenService;
            this.userManager = userManager;
            this.userCategoryService = userCategoryService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest value)
        {
            if (User.Identity.IsAuthenticated) return BadRequest();

            var newAppUser = mapper.Map<AppUser>(value);

            var result = await userManager.CreateAsync(newAppUser, value.Password);
            if (result.Succeeded)
            {
                #pragma warning disable 4014 // creating default UserCategory for new signing up user.
                userCategoryService.InsertAsync(newAppUser.Id, new UserCategoryRequest { Name = "General" });
                #pragma warning disable 4014

                var token = jwtTokenService.CreateToken(newAppUser);
                return Ok(new { token });
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest value)
        {
            if (User.Identity.IsAuthenticated) return BadRequest();

            var isIdentifierEmail = value.Identifier.Contains('@');

            var appUser = isIdentifierEmail
            ? await userManager.FindByEmailAsync(value.Identifier)
            : await userManager.FindByNameAsync(value.Identifier);

            var isPasswordRelatedToFoundUser = appUser != null && await userManager.CheckPasswordAsync(appUser, value.Password);
            if (isPasswordRelatedToFoundUser)
            {
                var token = jwtTokenService.CreateToken(appUser);
                return Ok(new { token });
            }
            return Unauthorized();
        }
    }
}