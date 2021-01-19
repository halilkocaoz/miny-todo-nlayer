using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MinyToDo.Entity.Models;

namespace MinyToDo.Abstract.Services
{
    public interface IUserTaskService
    {
        // todo: change return types to data transfer object
        Task<UserTask> InsertAsync(UserTask userTask);
        Task<UserTask> UpdateAsync(UserTask userTask);
        Task<bool> DeleteAsync(UserTask userTask);

        Task<IEnumerable<UserTask>> GetAllByCategoryId(Guid categoryId);

    }
}