using System;
using System.Collections.Generic;

namespace MinyToDo.Entity.Models
{
    public class AppUser // todo: identityuser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime CreatedAt { get; private set; }

        public ICollection<UserCategory> Categories { get; set; }
    }
}