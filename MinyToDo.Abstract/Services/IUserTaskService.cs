using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MinyToDo.Entity.DTO.Request;
using MinyToDo.Entity.Models;

namespace MinyToDo.Abstract.Services
{
    public interface IUserTaskService
    {
        // todo: change return types to data transfer object
        Task<UserTask> InsertAsync(UserTaskRequest userTask);
        Task<UserTask> UpdateAsync(UserTask userTask, UserTaskRequest newValues);
        Task<bool> DeleteAsync(UserTask userTask);

        Task<IEnumerable<UserTask>> GetAllByCategoryId(Guid categoryId);
        
        Task<UserTask> GetById(Guid userTaskId);
    }
}