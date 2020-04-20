using Microsoft.AspNetCore.Identity;
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
        /// Keeps track of the logged in user's id
        /// </summary>
        [StringLength(450)]
        public string UserId { get; set; }

        /// <summary>
        /// Title/Category of the todo list
        /// </summary>
        [Required, StringLength(50), Display(Name = "ToDo List Title: ")]
        public string ListTitle { get; set; }
    }
}
