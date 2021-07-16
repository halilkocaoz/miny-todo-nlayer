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
    public class UserTaskService : IUserTaskService
    {
        private readonly IMapper _mapper;
        private readonly IUserTaskRepository taskRepository;
        private readonly IUserCategoryRepository categoryRepository;

        public UserTaskService(IMapper mapper, IUserTaskRepository userTaskRepository, IUserCategoryRepository userCategoryRepository)
        {
            _mapper = mapper;
            taskRepository = userTaskRepository;
            categoryRepository = userCategoryRepository;
        }

        public async Task<ApiResponse> InsertAsync(Guid appUserId, UserTaskRequest userTaskRequest)
        {
            if (!userTaskRequest.UserCategoryId.HasValue)
            {
                return new ApiResponse(Models.Enums.ApiResponseType.BadRequest, "CATEGORY.CANTBENULL");
            }

            var toBeRelatedCategory = await categoryRepository.GetById(userTaskRequest.UserCategoryId);
            if (toBeRelatedCategory == null)
            {
                return new ApiResponse(Models.Enums.ApiResponseType.NotFound, "CATEGORY.NOTFOUND");
            }
            if (toBeRelatedCategory.ApplicationUserId != appUserId)
            {
                return new ApiResponse(Models.Enums.ApiResponseType.Forbidden);
            }

            var newEntity = _mapper.Map<UserTask>(userTaskRequest);

            newEntity.ApplicationUserId = appUserId;
            newEntity.CreatedAt = DateTime.Now;
            await taskRepository.InsertAsync(newEntity);

            return new ApiResponse(Models.Enums.ApiResponseType.Created);
        }

        public async Task<ApiResponse> UpdateAsync(Guid appUserId, Guid toBeUpdatedTaskId, UserTaskRequest userTaskRequest)
        {
            var wantToChangeRelatedCategory = userTaskRequest.UserCategoryId.HasValue;
            if (wantToChangeRelatedCategory)
            {
                var selectedCategoryToChange = await categoryRepository.GetById(userTaskRequest.UserCategoryId);
                if (selectedCategoryToChange == null)
                {
                    return new ApiResponse(Models.Enums.ApiResponseType.NotFound, "CATEGORY.NOTFOUND");
                }
                if (selectedCategoryToChange.ApplicationUserId != appUserId)
                {
                    return new ApiResponse(Models.Enums.ApiResponseType.Forbidden);
                }
            }

            var toBeUpdatedTask = await taskRepository.GetById(toBeUpdatedTaskId);
            if (toBeUpdatedTask == null)
            {
                return new ApiResponse(Models.Enums.ApiResponseType.NotFound, "TASK.NOTFOUND");
            }

            if (toBeUpdatedTask.ApplicationUserId != appUserId)
            {
                return new ApiResponse(Models.Enums.ApiResponseType.Forbidden);
            }

            _mapper.Map(userTaskRequest, toBeUpdatedTask);
            await taskRepository.UpdateAsync(toBeUpdatedTask);

            return new ApiResponse(Models.Enums.ApiResponseType.NoContent);
        }

        public async Task<ApiResponse> DeleteAsync(Guid appUserId, Guid toBeDeletedTaskId)
        {
            var toBeDeletedTask = await taskRepository.GetById(toBeDeletedTaskId);
            if (toBeDeletedTask == null)
            {
                return new ApiResponse(Models.Enums.ApiResponseType.NotFound, "TASK.NOTFOUND");
            }
            if (toBeDeletedTask.ApplicationUserId != appUserId)
            {
                return new ApiResponse(Models.Enums.ApiResponseType.Forbidden);
            }

            await taskRepository.DeleteAsync(toBeDeletedTask);
            return new ApiResponse(Models.Enums.ApiResponseType.NoContent);
        }

        public async Task<ApiResponse> GetAllByCategoryIdAsync(Guid appUserId, Guid userCategoryId)
        {
            var userTasks = _mapper.Map<IEnumerable<UserTaskResponse>>(
                await taskRepository.GetAll(x => x.ApplicationUserId == appUserId && x.UserCategoryId == userCategoryId));

            return new ApiResponse(Models.Enums.ApiResponseType.Ok, userTasks);
        }
    }
}