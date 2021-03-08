using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MinyToDo.Api.Services.Abstract;
using MinyToDo.Models.Entity;

namespace MinyToDo.Api.Services.Concrete
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly SymmetricSecurityKey _key;
        public JwtTokenService(IConfiguration configuration)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]));
        }

        public async Task<string> CreateTokenAsync(AppUser appUser)
        {
            await Task.Yield();
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
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}