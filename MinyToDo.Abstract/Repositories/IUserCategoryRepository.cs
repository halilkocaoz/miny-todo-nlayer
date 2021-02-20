using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MinyToDo.Models.Entity;

namespace MinyToDo.Abstract.Repositories
{
    public interface IUserCategoryRepository : IRepository<UserCategory>
    {
        Task<IEnumerable<UserCategory>> GetAllWithTasksAsync(Expression<Func<UserCategory, bool>> predicate);
    }
}