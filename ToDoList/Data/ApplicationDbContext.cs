using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TDList> TDLists { get; set; }
        public DbSet<TDTask> TDTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TDList>() //Dependent entity
                        .HasOne<TDTask>() //Parent Entity
                        .WithMany()
                        .HasForeignKey(task => task.TaskId); //The foreign key that you want to set up as from the Task.cs class
        }
    }
}
