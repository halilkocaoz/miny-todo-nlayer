using System;
using AutoMapper;
using MinyToDo.Models.DTO.Request;
using MinyToDo.Models.DTO.Response;
using MinyToDo.Models.Entity;

namespace MinyToDo.Api.Helpers
{
    public class AutoMappers : Profile
    {
        public AutoMappers()
        {
            #region DTO(Request) to >>> Original
            // 
            CreateMap<UserTaskRequest, UserTask>().ForMember(
            dest => dest.UserCategoryId, option =>
            {
                option.Condition(
                   source => source.UserCategoryId != null && source.UserCategoryId != Guid.Empty
                );
                option.MapFrom(source => source.UserCategoryId);
            });

            CreateMap<SignUpRequest, AppUser>();
            #endregion

            #region Original to >>> DTO(Response)
            CreateMap<UserTask, UserTaskResponse>();
            CreateMap<UserCategory, UserCategoryResponse>();
            #endregion
        }
    }
}