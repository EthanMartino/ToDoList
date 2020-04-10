using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList
{
    /// <summary>
    /// Represents a single Task in a ToDoList
    /// </summary>
    public class Task
    {
        /// <summary>
        /// Identity column for the Database
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// The name of the Task
        /// </summary>
        public string TaskTitle { get; set; }

        /// <summary>
        /// A description for the Task
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The expected Date for the Task to be finished
        /// </summary>
        public DateTime DueDate { get; set; }
    }
}
