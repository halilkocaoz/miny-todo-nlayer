using System.ComponentModel.DataAnnotations;

namespace MinyToDo.Models.DTO.Request
{
    public class UserCategoryRequest
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}