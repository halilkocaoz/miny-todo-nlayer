using System;
using System.Threading.Tasks;
using MinyToDo.Models;
using MinyToDo.Models.DTO.Request;

namespace MinyToDo.Abstract.Services
{
    public interface IUserCategoryService
    {
        Task<ApiResponse> InsertAsync(Guid appUserId, UserCategoryRequest categoryRequest);
        Task<ApiResponse> UpdateAsync(Guid appUserId, Guid toBeUpdatedCategoryId, UserCategoryRequest categoryRequest);
        Task<ApiResponse> DeleteAsync(Guid appUserId, Guid toBeDeletedCategoryId);
        Task<ApiResponse> GetAllWithTasksByUserId(Guid appUserId, bool withTasks);
    }
}