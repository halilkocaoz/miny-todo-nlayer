using MinyToDo.Abstract.Repositories;
using MinyToDo.Entity.Models;

namespace MinyToDo.Data.Concrete
{
    public class UserTaskRepository : Repository<UserTask>, IUserTaskRepository
    {
        public UserTaskRepository(MinyToDoContext context) : base(context)
        {
        }
    }
}