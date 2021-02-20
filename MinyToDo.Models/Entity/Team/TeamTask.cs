using System;
using MinyToDo.Models.Entity.Base;

namespace MinyToDo.Models.Entity
{
    public class TeamTask : TaskBase
    {
        public Guid TeamCategoryId { get; set; }
    }
}