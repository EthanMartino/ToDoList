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

            modelBuilder.Entity<TDTask>() //Dependent entity
                        .HasOne<TDList>() //Parent Entity
                        .WithMany()
                        .HasForeignKey(list => list.ListId); //The foreign key that you want to set up as from the TDList.cs class
        }
    }
}
