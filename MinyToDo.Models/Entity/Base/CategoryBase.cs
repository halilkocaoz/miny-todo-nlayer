using System;

namespace MinyToDo.Models.Entity.Base
{
    public abstract class CategoryBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}