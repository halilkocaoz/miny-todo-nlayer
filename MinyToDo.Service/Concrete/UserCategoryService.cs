using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MinyToDo.Abstract.Repositories;
using MinyToDo.Abstract.Services;
using MinyToDo.Entity.DTO.Request;
using MinyToDo.Entity.Models;

namespace MinyToDo.Service.Concrete
{
    public class UserCategoryService : IUserCategoryService
    {
        private IUserCategoryRepository _userCategoryRepository;
        private ILogger _logger;

        public UserCategoryService(IUserCategoryRepository userCategoryRepository)
        {
            _userCategoryRepository = userCategoryRepository;
        }

        public async Task<UserCategory> InsertAsync(Guid appUserId, UserCategoryRequest newCategory)
        {
            var newEntity = new UserCategory(appUserId, newCategory);
            return await _userCategoryRepository.InsertAsync(newEntity);
        }

        public async Task<UserCategory> UpdateAsync(UserCategory userCategory, UserCategoryRequest newValues)
        {
            userCategory.Name = newValues.Name;
            return await _userCategoryRepository.UpdateAsync(userCategory);
        }

        public async Task<bool> DeleteAsync(UserCategory userCategory)
        {
            return await _userCategoryRepository.DeleteAsync(userCategory) > 0 ? true : false;
        }

        public async Task<IEnumerable<UserCategory>> GetAllByUserId(Guid appUserId)
        {
            return await _userCategoryRepository.GetAll(x => x.ApplicationUserId == appUserId);
        }

        public async Task<IEnumerable<UserCategory>> GetAllWithTasksByUserId(Guid appUserId)
        {
            return await _userCategoryRepository.GetAllWithTasksAsync(x => x.ApplicationUserId == appUserId);
        }

        public async Task<UserCategory> GetById(Guid categoryId)
        {
            return await _userCategoryRepository.GetById(categoryId);
        }
    }
}