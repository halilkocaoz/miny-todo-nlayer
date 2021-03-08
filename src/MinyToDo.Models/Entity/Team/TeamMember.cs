using System;

namespace MinyToDo.Models.Entity
{
    public class TeamMember
    {
        public Guid ApplicationUserId { get; set; }
        public AppUser User { get; set; }
    }
}