using System;
using MinyToDo.Entity.Models.Base;

namespace MinyToDo.Entity.Models
{
    public class TeamTask : TaskBase
    {
        public Guid TeamCategoryId { get; set; }
    }
}