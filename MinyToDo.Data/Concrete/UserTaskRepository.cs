using MinyToDo.Abstract.Repositories;
using MinyToDo.Models.Entity;

namespace MinyToDo.Data.Concrete
{
    public class UserTaskRepository : Repository<UserTask>, IUserTaskRepository
    {
        public UserTaskRepository(MinyToDoContext context) : base(context)
        {
        }
    }
}