using System;
using System.ComponentModel.DataAnnotations;

namespace MinyToDo.Models.DTO.Request
{
    public class UserTaskRequest
    {
        public UserTaskRequest()
        {

        }

        public Guid? UserCategoryId { get; set; } = null;
        [Required]
        [MaxLength(200)]
        public string Content { get; set; }
        public string LongDescription { get; set; }
        public DateTime DueDate { get; set; } = DateTime.Now.AddDays(1);
        public bool Completed { get; set; }
    }
}