using System;
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
            CreateMap<UserTaskRequest, UserTask>().ForMember(
            dest => dest.UserCategoryId, option =>
            {
                option.Condition(
                   source => source.UserCategoryId != null && source.UserCategoryId != Guid.Empty
                );
                option.MapFrom(source => source.UserCategoryId);
            });

            CreateMap<SignUpRequest, AppUser>();



            // Orjinal to >>> DTO
            CreateMap<UserTask, UserTaskResponse>();
            CreateMap<UserCategory, UserCategoryResponse>();
        }
    }
}