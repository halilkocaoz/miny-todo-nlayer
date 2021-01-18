using System;

namespace MinyToDo.Entity.Models
{
    public class TeamMember
    {
        public Guid ApplicationUserId { get; set; }
        public AppUser User { get; set; }
    }
}