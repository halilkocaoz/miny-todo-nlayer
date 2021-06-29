using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MinyToDo.Abstract.Repositories;
using MinyToDo.Abstract.Services;
using MinyToDo.Models.DTO.Request;
using MinyToDo.Models.DTO.Response;
using MinyToDo.Models.Entity;

namespace MinyToDo.Service.Concrete
{
    public class UserTaskService : IUserTaskService
    {
        private readonly IMapper _mapper;
        private readonly IUserTaskRepository _userTaskRepository;

        public UserTaskService(IUserTaskRepository userTaskRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userTaskRepository = userTaskRepository;
        }

        public async Task<UserTaskResponse> InsertAsync(UserTaskRequest newTask)
        {
            var newUserTask = _mapper.Map<UserTask>(newTask);
            newUserTask.CreatedAt = DateTime.Now;
            return _mapper.Map<UserTaskResponse>(await _userTaskRepository.InsertAsync(newUserTask));
        }

        public async Task<UserTaskResponse> UpdateAsync(UserTask toBeUpdatedCategory, UserTaskRequest newValues)
        {
            _mapper.Map(newValues, toBeUpdatedCategory);
            return _mapper.Map<UserTaskResponse>(await _userTaskRepository.UpdateAsync(toBeUpdatedCategory));
        }

        public async Task<bool> DeleteAsync(UserTask toBeDeletedCategory)
        {
            return await _userTaskRepository.DeleteAsync(toBeDeletedCategory) > 0 ? true : false;
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