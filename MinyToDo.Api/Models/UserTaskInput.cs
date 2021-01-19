using System;
using System.ComponentModel.DataAnnotations;

namespace MinyToDo.Api.Models
{
    public class UserTaskInput
    {
        public UserTaskInput()
        {
            
        }
        
        [Required]
        public Guid? UserCategoryId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Content { get; set; }
        public string LongDescription { get; set; }
        public DateTime DueDate { get; set; } = DateTime.Now.AddDays(1);
    }
}