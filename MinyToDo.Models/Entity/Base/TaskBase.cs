using System;
using MinyToDo.Models.Enums;

namespace MinyToDo.Models.Entity.Base
{
    public abstract class TaskBase
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string LongDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DueDate { get; set; }
        public bool Completed { get; set; }
        public TaskPriority Priority { get; set; }
    }
}