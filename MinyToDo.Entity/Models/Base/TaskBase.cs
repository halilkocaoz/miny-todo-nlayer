using System;

namespace MinyToDo.Entity.Models.Base
{
    public abstract class TaskBase
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string LongDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DueDate { get; set; }
        public bool Completed { get; set; }
        // public short Priority { get; set; } // todo: refactor to enum
    }
}