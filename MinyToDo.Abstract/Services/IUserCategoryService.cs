using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MinyToDo.Entity.Models;

namespace MinyToDo.Abstract.Services
{
    public interface IUserCategoryService
    {
        // todo: change return types to data transfer object
        Task<UserCategory> InsertAsync(UserCategory userCategory);
        Task<UserCategory> UpdateAsync(UserCategory userCategory);
        Task<bool> DeleteAsync(UserCategory userCategory);

        Task<IEnumerable<UserCategory>> GetAllByUserId(Guid appUserId);
        Task<UserCategory> GetById(Guid categoryId);
    }
}