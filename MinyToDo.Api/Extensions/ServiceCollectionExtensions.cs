using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MinyToDo.Abstract.Repositories;
using MinyToDo.Abstract.Services;
using MinyToDo.Api.Services.Abstract;
using MinyToDo.Api.Services.Concrete;
using MinyToDo.Data;
using MinyToDo.Data.Concrete;
using MinyToDo.Entity.Models;
using MinyToDo.Service.Concrete;

namespace MinyToDo.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentities(this IServiceCollection services, IConfiguration configuration)
        {   
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            services.AddIdentityCore<AppUser>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 4;
                options.Password.RequireDigit = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            })
            .AddRoles<AppRole>()
            .AddEntityFrameworkStores<MinyToDoContext>();

            return services;
        }
        public static IServiceCollection AddDepencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IUserCategoryService, UserCategoryService>();
            services.AddScoped<IUserTaskService, UserTaskService>();

            services.AddScoped<IUserCategoryRepository, UserCategoryRepository>();
            services.AddScoped<IUserTaskRepository, UserTaskRepository>();

            return services;
        }
    }
}