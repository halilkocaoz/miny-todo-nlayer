using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
            services.AddTransient<IUserCategoryService, UserCategoryService>();
            services.AddTransient<IUserTaskService, UserTaskService>();

            services.AddTransient<IUserCategoryRepository, UserCategoryRepository>();
            services.AddTransient<IUserTaskRepository, UserTaskRepository>();

            return services;
        }
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                var securityScheme = new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement { { securityScheme, new string[] { } } });
            });
            return services;
        }
    }
}