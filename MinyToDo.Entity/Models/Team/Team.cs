using System;
using System.Collections.Generic;

namespace MinyToDo.Entity.Models
{
    public class Team
    {
        public Guid Id { get; set; }
        public ICollection<TeamMember> Members { get; set; }        
    }
}