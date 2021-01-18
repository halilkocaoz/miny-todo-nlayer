using System;
using System.Collections.Generic;

namespace MinyToDo.Entity.Models
{
    public class UserCategory
    {
        public Guid Id { get; set; }
        public Guid ApplicationUserId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<AppTask> Tasks { get; set; }
    }
}