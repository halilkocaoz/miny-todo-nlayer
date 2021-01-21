using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MinyToDo.Entity.DTO.Request;
using MinyToDo.Entity.Models;

namespace MinyToDo.Abstract.Services
{
    public interface IUserCategoryService
    {
        // todo: change return types to data transfer object
        Task<UserCategory> InsertAsync(Guid appUserId, UserCategoryRequest newCategory);
        Task<UserCategory> UpdateAsync(UserCategory userCategory, UserCategoryRequest newValues);
        Task<bool> DeleteAsync(UserCategory userCategory);

        Task<IEnumerable<UserCategory>> GetAllByUserId(Guid appUserId);
        Task<IEnumerable<UserCategory>> GetAllWithTasksByUserId(Guid appUserId);

        Task<UserCategory> GetById(Guid categoryId);
    }
}