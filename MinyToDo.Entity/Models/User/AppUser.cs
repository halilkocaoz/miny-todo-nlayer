using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MinyToDo.Entity.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime CreatedAt { get; private set; }

        public ICollection<UserCategory> Categories { get; set; }
    }
}