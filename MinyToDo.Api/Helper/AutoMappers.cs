using AutoMapper;
using MinyToDo.Api.Models;
using MinyToDo.Api.Models.Auth;
using MinyToDo.Entity.Models;

namespace MinyToDo.Api.Helper
{
    public class AutoMappers : Profile
    {
        public AutoMappers()
        {
            CreateMap<UserTaskInput, UserTask>();
            CreateMap<SignUpModel, AppUser>();
        }
    }
}