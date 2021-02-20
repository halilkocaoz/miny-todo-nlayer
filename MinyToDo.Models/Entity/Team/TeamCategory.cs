using System;
using System.Collections.Generic;
using MinyToDo.Models.Entity.Base;

namespace MinyToDo.Models.Entity
{
    public class TeamCategory : CategoryBase
    {
        public Guid TeamId { get; set; }
        public ICollection<TeamTask> Tasks { get; set; }
    }
}