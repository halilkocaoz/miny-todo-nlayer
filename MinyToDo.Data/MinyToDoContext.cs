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
        public MinyToDoContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<AppUser> ApplicationUsers { get; set; }
        public virtual DbSet<UserCategory> UserCategories { get; set; }
        public virtual DbSet<UserTask> UserTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>().HasKey(p => p.Id);

            modelBuilder.Entity<AppUser>().HasIndex(e => e.UserName)
               .HasDatabaseName("Unique_UserName")
               .IsUnique();

            modelBuilder.Entity<AppUser>().HasIndex(e => e.Email)
                .HasDatabaseName("Unique_Email")
                .IsUnique();

            //

            modelBuilder.Entity<UserCategory>().HasKey(p => p.Id);

            modelBuilder.Entity<UserCategory>().HasOne<AppUser>()
                .WithMany(appUser => appUser.Categories)
                .HasForeignKey(k => k.ApplicationUserId);

            //

            modelBuilder.Entity<UserTask>().HasKey(p => p.Id);

            modelBuilder.Entity<UserTask>().HasOne<UserCategory>()
                .WithMany(userCategory => userCategory.Tasks)
                .HasForeignKey(key => key.UserCategoryId);

            base.OnModelCreating(modelBuilder);
        }
    }
}