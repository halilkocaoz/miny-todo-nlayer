using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MinyToDo.Abstract.Repositories;
using MinyToDo.Abstract.Services;
using MinyToDo.Entity.DTO.Request;
using MinyToDo.Entity.DTO.Response;
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

        public async Task<UserTaskResponse> InsertAsync(UserTaskRequest newUserTaskRequest)
        {
            var newUserTask = _mapper.Map<UserTask>(newUserTaskRequest);

            newUserTask.CreatedAt = DateTime.Now;
            newUserTask.Completed = false;
            return _mapper.Map<UserTaskResponse>(await _userTaskRepository.InsertAsync(newUserTask));
        }

        public async Task<UserTaskResponse> UpdateAsync(UserTask userTask, UserTaskRequest newValues)
        {
            userTask = _mapper.Map<UserTask>(userTask);

            return _mapper.Map<UserTaskResponse>(await _userTaskRepository.UpdateAsync(userTask));
        }

        public async Task<bool> DeleteAsync(UserTask userTask)
        {
            return await _userTaskRepository.DeleteAsync(userTask) > 0 ? true : false;
        }

        public async Task<IEnumerable<UserTaskResponse>> GetAllByCategoryId(Guid categoryId)
        {
            return _mapper.Map<IEnumerable<UserTaskResponse>>(await _userTaskRepository.GetAll(task => task.UserCategoryId == categoryId));
        }

        public async Task<UserTask> GetById(Guid userTaskId)
        {
            return await _userTaskRepository.GetById(userTaskId);

        }
    }
}