using System;
using System.Collections.Generic;
using MinyToDo.Entity.Models.Base;

namespace MinyToDo.Entity.Models
{
    public class UserCategory : CategoryBase
    {
        public Guid ApplicationUserId { get; set; }
        public ICollection<UserTask> Tasks { get; set; }
    }
}