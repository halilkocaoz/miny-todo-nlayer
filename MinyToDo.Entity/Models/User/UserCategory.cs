using System;
using System.Collections.Generic;
using MinyToDo.Entity.DTO.Request;
using MinyToDo.Entity.Models.Base;

namespace MinyToDo.Entity.Models
{
    public class UserCategory : CategoryBase
    {
        public UserCategory()
        {
            
        }
        public UserCategory(Guid appUserId, UserCategoryRequest userCategoryRequest)
        {
            Id = Guid.NewGuid();
            ApplicationUserId = appUserId;
            Name = userCategoryRequest.Name;
        }

        public Guid ApplicationUserId { get; set; }
        public ICollection<UserTask> Tasks { get; set; }
    }
}