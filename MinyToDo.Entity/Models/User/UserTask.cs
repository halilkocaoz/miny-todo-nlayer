using System;
using MinyToDo.Entity.Models.Base;

namespace MinyToDo.Entity.Models
{
    public class UserTask : TaskBase
    {
        public UserTask()
        {
            
        }
        public Guid UserCategoryId { get; set; }
    }
}