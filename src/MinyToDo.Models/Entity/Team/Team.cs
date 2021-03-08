using System;
using System.Collections.Generic;

namespace MinyToDo.Models.Entity
{
    public class Team
    {
        public Guid Id { get; set; }
        public ICollection<TeamMember> Members { get; set; }        
    }
}