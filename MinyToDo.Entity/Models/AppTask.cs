using System;

namespace MinyToDo.Entity.Models
{
    public class AppTask
    {
        public Guid Id { get; set; }
        public string Task { get; set; }
        public string LongDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Completed { get; set; }
    }
}