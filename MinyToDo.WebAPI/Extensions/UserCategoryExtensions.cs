using System;
using MinyToDo.Models.Entity;

namespace MinyToDo.WebAPI.Extensions
{
    public static class UserCategoryExtensions
    {
        public static bool RelatedToGivenUserId(this UserCategory userCategory, Guid userId) => userCategory?.ApplicationUserId == userId;
    }
}