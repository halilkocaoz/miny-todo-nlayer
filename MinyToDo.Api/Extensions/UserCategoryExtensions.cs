using System;
using MinyToDo.Entity.Models;

namespace MinyToDo.Api.Extensions
{
    public static class UserCategoryExtensions
    {
        public static bool RelatedToGivenUserId(this UserCategory userCategory, Guid userId) => userCategory?.ApplicationUserId == userId;
    }
}