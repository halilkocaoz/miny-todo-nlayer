using System;
using MinyToDo.Models.Entity.Base;

namespace MinyToDo.Models.Entity
{
    public class UserTask : TaskBase
    {
        public UserTask()
        {
            //do not remove this ctor, because automapper uses it.
        }
        public Guid UserCategoryId { get; set; }
    }
}