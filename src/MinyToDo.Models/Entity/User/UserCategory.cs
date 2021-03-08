using System;
using System.Collections.Generic;
using MinyToDo.Models.DTO.Request;
using MinyToDo.Models.Entity.Base;

namespace MinyToDo.Models.Entity
{
    public class UserCategory : CategoryBase
    { 
        public UserCategory()
        {
            //do not remove this ctor, because automapper uses it.
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