using AutoMapper;
using MinyToDo.Entity.DTO.Request;
using MinyToDo.Entity.DTO.Response;
using MinyToDo.Entity.Models;

namespace MinyToDo.Api.Helper
{
    public class AutoMappers : Profile
    {
        public AutoMappers()
        {
            // DTO to >>> Orjinal
            CreateMap<UserTaskRequest, UserTask>();
            CreateMap<SignUpRequest, AppUser>();


            
            // Orjinal to >>> DTO
            CreateMap<UserTask, UserTaskResponse>();
            CreateMap<UserCategory, UserCategoryResponse>();
        }
    }
}