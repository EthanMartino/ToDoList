using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList
{
    public class Task
    {
        public int TaskId { get; set; }

        public string TaskTitle { get; set; }

        public string Description { get; set; }

        public DateTime DueDate { get; set; }
    }
}
