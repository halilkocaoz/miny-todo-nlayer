using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MinyToDo.Entity.DTO.Request;
using MinyToDo.Entity.DTO.Response;
using MinyToDo.Entity.Models;

namespace MinyToDo.Abstract.Services
{
    public interface IUserTaskService
    {
        // todo: change return types to data transfer object
        Task<UserTaskResponse> InsertAsync(UserTaskRequest userTask);
        Task<UserTaskResponse> UpdateAsync(UserTask userTask, UserTaskRequest newValues);
        Task<bool> DeleteAsync(UserTask userTask);

        Task<IEnumerable<UserTaskResponse>> GetAllByCategoryId(Guid categoryId);
        
        Task<UserTask> GetById(Guid userTaskId);
    }
}