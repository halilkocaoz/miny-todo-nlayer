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
            var result = await _userTaskRepository.DeleteAsync(userTask);
            return result > 0 ? true : false;
        }
    }
}