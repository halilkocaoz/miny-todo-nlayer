using AutoMapper;
using MinyToDo.Entity.DTO.Request;
using MinyToDo.Entity.Models;

namespace MinyToDo.Api.Helper
{
    public class AutoMappers : Profile
    {
        public AutoMappers()
        {
            CreateMap<UserTaskRequest, UserTask>();
            CreateMap<SignUpRequest, AppUser>();
        }
    }
}