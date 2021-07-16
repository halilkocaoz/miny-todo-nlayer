using System;
using System.Threading.Tasks;
using MinyToDo.Models;
using MinyToDo.Models.DTO.Request;

namespace MinyToDo.Abstract.Services
{
    public interface IUserTaskService
    {
        Task<ApiResponse> InsertAsync(Guid appUserId, UserTaskRequest userTaskRequest);
        Task<ApiResponse> UpdateAsync(Guid appUserId, Guid toBeUpdatedTaskId, UserTaskRequest userTaskRequest);
        Task<ApiResponse> DeleteAsync(Guid appUserId, Guid toBeDeletedTaskId);
        Task<ApiResponse> GetAllByCategoryIdAsync(Guid appUserId, Guid userCategoryId);
    }
}