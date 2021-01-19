using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MinyToDo.Abstract.Repositories;
using MinyToDo.Entity.Models;

namespace MinyToDo.Data.Concrete
{
    public class UserCategoryRepository : Repository<UserCategory>, IUserCategoryRepository
    {
        public UserCategoryRepository(MinyToDoContext context) : base(context)
        {
        }

        public async Task<IEnumerable<UserCategory>> GetAllWithTasksAsync(Expression<Func<UserCategory, bool>> predicate = null)
        {
            return predicate != null
            ? await _context.UserCategories.Where(predicate).Include(usercategory => usercategory.Tasks).ToListAsync()
            : await _context.UserCategories.Include(usercategory => usercategory.Tasks).ToListAsync();
        }
    }
}