using System;

namespace MinyToDo.Entity.Models.Base
{
    public abstract class CategoryBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}