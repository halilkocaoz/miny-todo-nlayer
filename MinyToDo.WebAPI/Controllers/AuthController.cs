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
    [Route("Api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUserCategoryService _userCategoryService;
        private readonly IMapper _mapper;
        public AuthController(IMapper mapper, IJwtTokenService jwtTokenService, UserManager<AppUser> userManager, IUserCategoryService userCategoryService)
        {
            _mapper = mapper;
            _jwtTokenService = jwtTokenService;
            _userManager = userManager;
            _userCategoryService = userCategoryService;
        }

        [HttpPost("Signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest value)
        {
            if (User.Identity.IsAuthenticated) return BadRequest();

            var newAppUser = _mapper.Map<AppUser>(value);

            var result = await _userManager.CreateAsync(newAppUser, value.Password);
            if (result.Succeeded)
            {
                #pragma warning disable 4014 // creating default UserCategory for new signing up user.
                _userCategoryService.InsertAsync(newAppUser.Id, new UserCategoryRequest { Name = "General" });
                #pragma warning disable 4014

                var token = _jwtTokenService.CreateToken(newAppUser);
                return Ok(new { token });
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("Signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest value)
        {
            if (User.Identity.IsAuthenticated) return BadRequest();

            var isIdentifierEmail = value.Identifier.Contains('@');

            var appUser = isIdentifierEmail
            ? await _userManager.FindByEmailAsync(value.Identifier)
            : await _userManager.FindByNameAsync(value.Identifier);

            var isPasswordRelatedToFoundUser = appUser != null && await _userManager.CheckPasswordAsync(appUser, value.Password);
            if (isPasswordRelatedToFoundUser)
            {
                var token = _jwtTokenService.CreateToken(appUser);
                return Ok(new { token });
            }
            return Unauthorized();
        }
    }
}