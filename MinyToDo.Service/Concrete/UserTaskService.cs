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
        private readonly IMapper mapper;
        private readonly IUserTaskRepository taskUserRepository;
        private readonly IUserCategoryRepository categoryUserRepository;

        public UserTaskService(IMapper mapper, IUserTaskRepository userTaskRepository, IUserCategoryRepository userCategoryRepository)
        {
            this.mapper = mapper;
            taskUserRepository = userTaskRepository;
            categoryUserRepository = userCategoryRepository;
        }

        public async Task<ApiResponse> InsertAsync(Guid appUserId, UserTaskRequest userTaskRequest)
        {
            if (!userTaskRequest.UserCategoryId.HasValue)
            {
                return new ApiResponse(Models.Enums.ApiResponseStatus.BadRequest, "CATEGORY.CANTBENULL");
            }

            var toBeRelatedCategory = await categoryUserRepository.GetById(userTaskRequest.UserCategoryId);
            if (toBeRelatedCategory == null)
            {
                return new ApiResponse(Models.Enums.ApiResponseStatus.NotFound, "CATEGORY.NOTFOUND");
            }
            if (toBeRelatedCategory.ApplicationUserId != appUserId)
            {
                return new ApiResponse(Models.Enums.ApiResponseStatus.Forbidden);
            }

            var newEntity = mapper.Map<UserTask>(userTaskRequest);

            newEntity.ApplicationUserId = appUserId;
            newEntity.CreatedAt = DateTime.Now;
            await taskUserRepository.InsertAsync(newEntity);

            return new ApiResponse(Models.Enums.ApiResponseStatus.Created);
        }

        public async Task<ApiResponse> UpdateAsync(Guid appUserId, Guid toBeUpdatedTaskId, UserTaskRequest userTaskRequest)
        {
            var wantToChangeRelatedCategory = userTaskRequest.UserCategoryId.HasValue;
            if (wantToChangeRelatedCategory)
            {
                var selectedCategoryToChange = await categoryUserRepository.GetById(userTaskRequest.UserCategoryId);
                if (selectedCategoryToChange == null)
                {
                    return new ApiResponse(Models.Enums.ApiResponseStatus.NotFound, "CATEGORY.NOTFOUND");
                }
                if (selectedCategoryToChange.ApplicationUserId != appUserId)
                {
                    return new ApiResponse(Models.Enums.ApiResponseStatus.Forbidden);
                }
            }

            var toBeUpdatedTask = await taskUserRepository.GetById(toBeUpdatedTaskId);
            if (toBeUpdatedTask == null)
            {
                return new ApiResponse(Models.Enums.ApiResponseStatus.NotFound, "TASK.NOTFOUND");
            }

            if (toBeUpdatedTask.ApplicationUserId != appUserId)
            {
                return new ApiResponse(Models.Enums.ApiResponseStatus.Forbidden);
            }

            mapper.Map(userTaskRequest, toBeUpdatedTask);
            await taskUserRepository.UpdateAsync(toBeUpdatedTask);

            return new ApiResponse(Models.Enums.ApiResponseStatus.NoContent);
        }

        public async Task<ApiResponse> DeleteAsync(Guid appUserId, Guid toBeDeletedTaskId)
        {
            var toBeDeletedTask = await taskUserRepository.GetById(toBeDeletedTaskId);
            if (toBeDeletedTask == null)
            {
                return new ApiResponse(Models.Enums.ApiResponseStatus.NotFound, "TASK.NOTFOUND");
            }
            if (toBeDeletedTask.ApplicationUserId != appUserId)
            {
                return new ApiResponse(Models.Enums.ApiResponseStatus.Forbidden);
            }

            await taskUserRepository.DeleteAsync(toBeDeletedTask);
            return new ApiResponse(Models.Enums.ApiResponseStatus.NoContent);
        }

        public async Task<ApiResponse> GetAllByCategoryIdAsync(Guid appUserId, Guid userCategoryId)
        {
            var userTasks = mapper.Map<IEnumerable<UserTaskResponse>>(
                await taskUserRepository.GetAll(x => x.ApplicationUserId == appUserId && x.UserCategoryId == userCategoryId));

            return new ApiResponse(Models.Enums.ApiResponseStatus.Ok, userTasks);
        }
    }
}