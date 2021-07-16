using System;
using System.Collections.Generic;
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
        private readonly IUserCategoryRepository categoryRepository;
        private readonly IMapper _mapper;

        public UserCategoryService(IUserCategoryRepository userCategoryRepository, IMapper mapper)
        {
            _mapper = mapper;
            categoryRepository = userCategoryRepository;
        }

        public async Task<ApiResponse> InsertAsync(Guid appUserId, UserCategoryRequest categoryRequest)
        {
            var newCategoryResponse = _mapper.Map<UserCategoryResponse>(await categoryRepository.InsertAsync(new UserCategory(appUserId, categoryRequest)));

            return new ApiResponse(Models.Enums.ApiResponseType.Created, newCategoryResponse);
        }

        public async Task<ApiResponse> UpdateAsync(Guid appUserId, Guid toBeUpdatedCategoryId, UserCategoryRequest userCategoryRequest)
        {
            var toBeUpdatedCategory = await categoryRepository.GetById(toBeUpdatedCategoryId);
            if (toBeUpdatedCategory == null)
            {
                return new ApiResponse(Models.Enums.ApiResponseType.NotFound, "CATEGORY.NOTFOUND");
            }

            if (toBeUpdatedCategory.ApplicationUserId != appUserId)
            {
                return new ApiResponse(Models.Enums.ApiResponseType.Forbidden);
            }

            toBeUpdatedCategory.Name = userCategoryRequest.Name;
            var updatedEntity = await categoryRepository.UpdateAsync(toBeUpdatedCategory);

            return new ApiResponse(Models.Enums.ApiResponseType.NoContent);
        }

        public async Task<ApiResponse> DeleteAsync(Guid appUserId, Guid toBeDeletedCategoryId)
        {
            var toBeDeletedCategory = await categoryRepository.GetById(toBeDeletedCategoryId);
            if (toBeDeletedCategory == null)
            {
                return new ApiResponse(Models.Enums.ApiResponseType.NotFound, "CATEGORY.NOTFOUND");
            }

            if (toBeDeletedCategory.ApplicationUserId != appUserId)
            {
                return new ApiResponse(Models.Enums.ApiResponseType.Forbidden);
            }
            
            await categoryRepository.DeleteAsync(toBeDeletedCategory);
            return new ApiResponse(Models.Enums.ApiResponseType.NoContent);
        }

        public async Task<ApiResponse> GetAllWithTasksByUserIdAnd(Guid appUserId, bool withTasks)
        {
            IEnumerable<UserCategoryResponse> data = null;

            data = withTasks
                ? _mapper.Map<IEnumerable<UserCategoryResponse>>(await categoryRepository.GetAllWithTasksAsync(userCategory => userCategory.ApplicationUserId == appUserId))
                : _mapper.Map<IEnumerable<UserCategoryResponse>>(await categoryRepository.GetAll(userCategory => userCategory.ApplicationUserId == appUserId));

            return new ApiResponse(Models.Enums.ApiResponseType.Ok, data);
        }
    }
}