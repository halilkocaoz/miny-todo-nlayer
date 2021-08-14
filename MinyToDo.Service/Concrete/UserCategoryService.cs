using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using MinyToDo.Abstract.Repositories;
using MinyToDo.Abstract.Services;
using MinyToDo.Models;
using MinyToDo.Models.DTO.Request;
using MinyToDo.Models.DTO.Response;
using MinyToDo.Models.Entity;

namespace MinyToDo.Service.Concrete
{
    public class UserCategoryService : IUserCategoryService
    {
        private readonly IUserCategoryRepository categoryUserRepository;
        private readonly IMapper mapper;

        public UserCategoryService(IUserCategoryRepository userCategoryRepository, IMapper mapper)
        {
            this.mapper = mapper;
            categoryUserRepository = userCategoryRepository;
        }

        public async Task<ApiResponse> InsertAsync(Guid appUserId, UserCategoryRequest categoryRequest)
        {
            var newCategoryResponse = mapper.Map<UserCategoryResponse>(await categoryUserRepository.InsertAsync(new UserCategory(appUserId, categoryRequest)));

            return new ApiResponse(Models.Enums.ApiResponseStatus.Created, newCategoryResponse);
        }

        public async Task<ApiResponse> UpdateAsync(Guid appUserId, Guid toBeUpdatedCategoryId, UserCategoryRequest userCategoryRequest)
        {
            var toBeUpdatedCategory = await categoryUserRepository.GetById(toBeUpdatedCategoryId);
            if (toBeUpdatedCategory == null)
            {
                return new ApiResponse(Models.Enums.ApiResponseStatus.NotFound, "CATEGORY.NOTFOUND");
            }

            if (toBeUpdatedCategory.ApplicationUserId != appUserId)
            {
                return new ApiResponse(Models.Enums.ApiResponseStatus.Forbidden);
            }

            toBeUpdatedCategory.Name = userCategoryRequest.Name;
            await categoryUserRepository.UpdateAsync(toBeUpdatedCategory);

            return new ApiResponse(Models.Enums.ApiResponseStatus.NoContent);
        }

        public async Task<ApiResponse> DeleteAsync(Guid appUserId, Guid toBeDeletedCategoryId)
        {
            var toBeDeletedCategory = await categoryUserRepository.GetById(toBeDeletedCategoryId);
            if (toBeDeletedCategory == null)
            {
                return new ApiResponse(Models.Enums.ApiResponseStatus.NotFound, "CATEGORY.NOTFOUND");
            }

            if (toBeDeletedCategory.ApplicationUserId != appUserId)
            {
                return new ApiResponse(Models.Enums.ApiResponseStatus.Forbidden);
            }
            
            await categoryUserRepository.DeleteAsync(toBeDeletedCategory);
            return new ApiResponse(Models.Enums.ApiResponseStatus.NoContent);
        }

        public async Task<ApiResponse> GetAllWithTasksByUserId(Guid appUserId, bool withTasks)
        {
            IEnumerable<UserCategoryResponse> data = null;
            Expression<Func<UserCategory, bool>>  whereExpression = userCategory => userCategory.ApplicationUserId == appUserId;
            
            data = withTasks
                ? mapper.Map<IEnumerable<UserCategoryResponse>>(await categoryUserRepository.GetAllWithTasksAsync(whereExpression))
                : mapper.Map<IEnumerable<UserCategoryResponse>>(await categoryUserRepository.GetAll(whereExpression));

            return new ApiResponse(Models.Enums.ApiResponseStatus.Ok, data);
        }
    }
}