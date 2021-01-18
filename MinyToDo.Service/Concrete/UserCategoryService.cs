using System.Threading.Tasks;
using MinyToDo.Abstract.Repositories;
using MinyToDo.Abstract.Services;
using MinyToDo.Entity.Models;

namespace MinyToDo.Service.Concrete
{
    public class UserCategoryService : IUserCategoryService
    {
        private IUserCategoryRepository _userCategoryRepository;

        public UserCategoryService(IUserCategoryRepository userCategoryRepository)
        {
            _userCategoryRepository = userCategoryRepository;
        }

        public async Task<UserCategory> InsertAsync(UserCategory userCategory)
        {
            return await _userCategoryRepository.InsertAsync(userCategory);
        }

        public async Task<UserCategory> UpdateAsync(UserCategory userCategory)
        {
            return await _userCategoryRepository.UpdateAsync(userCategory);
        }
        
        public async Task<bool> DeleteAsync(UserCategory userCategory)
        {
            var result = await _userCategoryRepository.DeleteAsync(userCategory);
            return result > 0 ? true : false;
        }
    }
}