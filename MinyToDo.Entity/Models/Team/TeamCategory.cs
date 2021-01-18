using System;
using System.Collections.Generic;
using MinyToDo.Entity.Models.Base;

namespace MinyToDo.Entity.Models
{
    public class TeamCategory : CategoryBase
    {
        public Guid TeamId { get; set; }
        public ICollection<TeamTask> Tasks { get; set; }
    }
}