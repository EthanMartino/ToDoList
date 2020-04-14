﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Models
{
    /// <summary>
    /// Represents a complete ToDo List
    /// </summary>
    public class TDList
    {
        /// <summary>
        /// Unique id for the todo list
        /// </summary>
        [Key, Required]
        public int ListId { get; set; }

        /// <summary>
        /// Foreign Key to IdentityUser Id
        /// Note: Since this is a froeign key from an Identity, 
        /// it auto creates the migration with this property as a foreign key.
        /// No need to do the modelBuilder to make it a foreign key.
        /// </summary>
        [Required]
        public IdentityUser Id { get; set; }

        /// <summary>
        /// Foreign key to Task model
        /// </summary>
        [Required]
        public int TaskId { get; set; }

        /// <summary>
        /// Title/Category of the todo list
        /// </summary>
        [Required, StringLength(50), Display(Name = "ToDo List Title: ")]
        public string ListTitle { get; set; }
    }
}
