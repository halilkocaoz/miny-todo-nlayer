using System;
using AutoMapper;
using MinyToDo.Entity.DTO.Request;
using MinyToDo.Entity.DTO.Response;
using MinyToDo.Entity.Models;

namespace MinyToDo.Api.Helpers
{
    public class AutoMappers : Profile
    {
        public AutoMappers()
        {
            // DTO(Request) to >>> Original
            CreateMap<UserTaskRequest, UserTask>().ForMember(
            dest => dest.UserCategoryId, option =>
            {
                option.Condition(
                   source => source.UserCategoryId != null && source.UserCategoryId != Guid.Empty
                );
                option.MapFrom(source => source.UserCategoryId);
            });

            CreateMap<SignUpRequest, AppUser>();



            // Original to >>> DTO(Response)
            CreateMap<UserTask, UserTaskResponse>();
            CreateMap<UserCategory, UserCategoryResponse>();
        }
    }
}