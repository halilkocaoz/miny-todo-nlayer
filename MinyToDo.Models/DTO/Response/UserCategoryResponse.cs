using System.Collections.Generic;
using MinyToDo.Models.Entity.Base;

namespace MinyToDo.Models.DTO.Response
{
    public class UserCategoryResponse : CategoryBase
    {
        public UserCategoryResponse()
        {
            
        }
        public ICollection<UserTaskResponse> Tasks { get; set; }
    }
}