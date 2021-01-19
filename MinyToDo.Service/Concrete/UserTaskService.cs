using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MinyToDo.Abstract.Repositories;
using MinyToDo.Abstract.Services;
using MinyToDo.Entity.Models;

namespace MinyToDo.Service.Concrete
{
    public class UserTaskService : IUserTaskService
    {
        private IUserTaskRepository _userTaskRepository;

        public UserTaskService(IUserTaskRepository userTaskRepository)
        {
            _userTaskRepository = userTaskRepository;
        }

        public async Task<UserTask> InsertAsync(UserTask userTask)
        {
            return await _userTaskRepository.InsertAsync(userTask);
        }

        public async Task<UserTask> UpdateAsync(UserTask userTask)
        {
            return await _userTaskRepository.UpdateAsync(userTask);
        }

        public async Task<bool> DeleteAsync(UserTask userTask)
        {
            return await _userTaskRepository.DeleteAsync(userTask) > 0 ? true : false;
        }

        public async Task<IEnumerable<UserTask>> GetAllByCategoryId(Guid categoryId)
        {
            return await _userTaskRepository.GetAll(task => task.UserCategoryId == categoryId);
        }

        public async Task<UserTask> GetById(Guid userTaskId)
        {
            return await _userTaskRepository.GetById(userTaskId);

        }
    }
}