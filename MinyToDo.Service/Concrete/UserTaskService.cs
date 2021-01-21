using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MinyToDo.Abstract.Repositories;
using MinyToDo.Abstract.Services;
using MinyToDo.Entity.DTO.Request;
using MinyToDo.Entity.Models;

namespace MinyToDo.Service.Concrete
{
    public class UserTaskService : IUserTaskService
    {
        private IMapper _mapper;
        private IUserTaskRepository _userTaskRepository;

        public UserTaskService(IUserTaskRepository userTaskRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userTaskRepository = userTaskRepository;
        }

        public async Task<UserTask> InsertAsync(UserTaskRequest newUserTaskRequest)
        {
            var newUserTask = _mapper.Map<UserTask>(newUserTaskRequest);

            newUserTask.CreatedAt = DateTime.Now;
            newUserTask.Completed = false;
            return await _userTaskRepository.InsertAsync(newUserTask);
        }

        public async Task<UserTask> UpdateAsync(UserTask userTask, UserTaskRequest newValues)
        {
            userTask = _mapper.Map<UserTask>(userTask);
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