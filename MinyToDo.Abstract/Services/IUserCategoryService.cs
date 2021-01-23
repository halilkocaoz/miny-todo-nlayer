using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MinyToDo.Entity.DTO.Request;
using MinyToDo.Entity.DTO.Response;
using MinyToDo.Entity.Models;

namespace MinyToDo.Abstract.Services
{
    public interface IUserCategoryService
    {
        Task<UserCategoryResponse> InsertAsync(Guid appUserId, UserCategoryRequest newCategory);
        Task<UserCategoryResponse> UpdateAsync(UserCategory toBeUpdatedCategory, UserCategoryRequest newValues);
        Task<bool> DeleteAsync(UserCategory toBeDeletedCategory);

        Task<IEnumerable<UserCategoryResponse>> GetAllByUserId(Guid appUserId);
        Task<IEnumerable<UserCategoryResponse>> GetAllWithTasksByUserId(Guid appUserId);

        Task<UserCategory> GetById(Guid categoryId);
    }
}