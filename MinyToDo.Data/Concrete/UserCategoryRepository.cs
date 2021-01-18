using MinyToDo.Abstract.Repositories;
using MinyToDo.Entity.Models;

namespace MinyToDo.Data.Concrete
{
    public class UserCategoryRepository : Repository<UserCategory>, IUserCategoryRepository
    {
        public UserCategoryRepository(MinyToDoContext context) : base(context)
        {
        }
    }
}