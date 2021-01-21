using System;
using System.ComponentModel.DataAnnotations;

namespace MinyToDo.Entity.DTO.Request
{
    public class UserTaskRequest
    {
        public UserTaskRequest()
        {

        }

        [Required]
        public Guid? UserCategoryId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Content { get; set; }
        public string LongDescription { get; set; }
        public DateTime DueDate { get; set; } = DateTime.Now.AddDays(1);
        public bool Completed { get; set; } = false;
    }
}