using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MinyToDo.Models.DTO.Request;
using MinyToDo.Models.DTO.Response;
using MinyToDo.Models.Entity;

namespace MinyToDo.Abstract.Services
{
    public interface IUserTaskService
    {
        Task<UserTaskResponse> InsertAsync(UserTaskRequest newTask);
        Task<UserTaskResponse> UpdateAsync(UserTask toBeUpdatedCategory, UserTaskRequest newValues);
        Task<bool> DeleteAsync(UserTask toBeDeletedCategory);

        Task<IEnumerable<UserTaskResponse>> GetAllByCategoryId(Guid categoryId);
        
        Task<UserTask> GetById(Guid userTaskId);
    }
}