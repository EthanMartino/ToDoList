using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList
{
    /// <summary>
    /// Represents a single Task in a ToDoList
    /// </summary>
    public class TDTask
    {
        [Key, Required]
        /// <summary>
        /// Identity column for the Database
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// Foreign key to TDList's id property
        /// </summary>
        [Required]
        public int ListId { get; set; }

        [StringLength(50), Required, Display(Name = "Task")]
        /// <summary>
        /// The name of the Task
        /// </summary>
        public string TaskTitle { get; set; }

        [StringLength(200)]
        /// <summary>
        /// A description for the Task
        /// </summary>
        public string Description { get; set; }

        [StringLength(20), Display(Name = "Expected Completion")]
        /// <summary>
        /// The expected Date for the Task to be finished
        /// </summary>
        public string DueDate { get; set; }

        [DefaultValue(false)]
        /// <summary>
        /// Represents whether or not the Task is completed
        /// </summary>
        public bool isCompleted { get; set; }
    }
}
