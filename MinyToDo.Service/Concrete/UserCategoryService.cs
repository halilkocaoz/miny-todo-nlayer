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
        private readonly IUserCategoryRepository _userCategoryRepository;
        private readonly IMapper _mapper;

        public UserCategoryService(IUserCategoryRepository userCategoryRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userCategoryRepository = userCategoryRepository;
        }

        public async Task<ApiResponse> InsertAsync(Guid appUserId, UserCategoryRequest categoryRequest)
        {
            var newCategoryResponse = _mapper.Map<UserCategoryResponse>(await _userCategoryRepository.InsertAsync(new UserCategory(appUserId, categoryRequest)));
            if (newCategoryResponse == null)
            {
                return new ApiResponse(Models.Enums.ApiResponseType.BadRequest, "CREATING.CATEGORY.FAILED");
            }

            return new ApiResponse(Models.Enums.ApiResponseType.Created, newCategoryResponse);
        }

        public async Task<ApiResponse> UpdateAsync(Guid appUserId, Guid toBeUpdatedCategoryId, UserCategoryRequest userCategoryRequest)
        {
            var toBeUpdatedCategory = await _userCategoryRepository.GetById(toBeUpdatedCategoryId);
            if (toBeUpdatedCategory == null)
            {
                return new ApiResponse(Models.Enums.ApiResponseType.NotFound, "UPDATING.CATEGORY.NOTFOUND");
            }

            if (toBeUpdatedCategory.ApplicationUserId == appUserId) //todo: refactor
            {
                toBeUpdatedCategory.Name = userCategoryRequest.Name;
                var updatedEntity = await _userCategoryRepository.UpdateAsync(toBeUpdatedCategory);

                if (updatedEntity == null)
                {
                    return new ApiResponse(Models.Enums.ApiResponseType.BadRequest, "UPDATING.CATEGORY.FAILED");
                }

                return new ApiResponse(Models.Enums.ApiResponseType.NoContent);
            }

            return new ApiResponse(Models.Enums.ApiResponseType.Forbidden);
        }

        public async Task<ApiResponse> DeleteAsync(Guid appUserId, Guid toBeDeletedCategoryId)
        {
            var toBeDeletedCategory = await _userCategoryRepository.GetById(toBeDeletedCategoryId);
            if (toBeDeletedCategory == null)
            {
                return new ApiResponse(Models.Enums.ApiResponseType.NotFound, "DELETING.CATEGORY.NOTFOUND");
            }

            if (toBeDeletedCategory.ApplicationUserId == appUserId) //todo: refactor
            {

                if (await _userCategoryRepository.DeleteAsync(toBeDeletedCategory) != 1)
                {
                    return new ApiResponse(Models.Enums.ApiResponseType.BadRequest, "DELETING.CATEGORY.FAILED");
                }

                return new ApiResponse(Models.Enums.ApiResponseType.NoContent);
            }

            return new ApiResponse(Models.Enums.ApiResponseType.Forbidden);
        }

        public async Task<ApiResponse> GetAllWithTasksByUserIdAnd(Guid appUserId, bool withTasks)
        {
            IEnumerable<UserCategoryResponse> data = null;

            data = withTasks
                ? _mapper.Map<IEnumerable<UserCategoryResponse>>(await _userCategoryRepository.GetAllWithTasksAsync(userCategory => userCategory.ApplicationUserId == appUserId))
                : _mapper.Map<IEnumerable<UserCategoryResponse>>(await _userCategoryRepository.GetAll(userCategory => userCategory.ApplicationUserId == appUserId));

            return new ApiResponse(Models.Enums.ApiResponseType.Ok, data);
        }
    }
}