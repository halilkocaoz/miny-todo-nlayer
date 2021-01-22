using System.Collections.Generic;
using MinyToDo.Entity.Models;
using MinyToDo.Entity.Models.Base;

namespace MinyToDo.Entity.DTO.Response
{
    public class UserCategoryResponse : CategoryBase
    {
        public UserCategoryResponse()
        {
            
        }
        public ICollection<UserTask> Tasks { get; set; }
    }
}