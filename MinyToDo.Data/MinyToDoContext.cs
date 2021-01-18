using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MinyToDo.Entity.Models;
using System;
using Microsoft.AspNetCore.Identity;

namespace MinyToDo.Data
{
    public class AppRole : IdentityRole<Guid>
    {
    }
    
    public class MinyToDoContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public MinyToDoContext(DbContextOptions options) : base(options) { { { } } }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}