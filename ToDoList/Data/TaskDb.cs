﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Data
{
    /// <summary>
    /// Database Helper class for the Task class
    /// </summary>
    public static class TaskDb
    {
        /// <summary>
        /// Gets all TDTasks from the database
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task<List<TDTask>> GetAllTasks(ApplicationDbContext context)
        {
            List<TDTask> tasks =
                 await (from t in context.TDTasks
                        orderby t.TaskId ascending
                        select t).ToListAsync();

            return tasks;
        }

        /// <summary>
        /// Gets a single TDTask from the database based on the given TaskId
        /// </summary>
        /// <param name="id"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task<TDTask> GetTaskById(int id, ApplicationDbContext context)
        {
            TDTask task =
                await (from t in context.TDTasks
                       where t.TaskId == id
                       select t).SingleOrDefaultAsync();

            return task;
        }

        /// <summary>
        /// Adds the given TDTask to the Database
        /// </summary>
        /// <param name="task"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task<TDTask> AddTask(TDTask task, ApplicationDbContext context)
        {
            await context.AddAsync(task);
            await context.SaveChangesAsync();
            return task;
        }

        /// <summary>
        /// Updates the given TDTask in the database
        /// </summary>
        /// <param name="task"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task<TDTask> UpdateTask(TDTask task, ApplicationDbContext context)
        {
            await context.AddAsync(task);
            context.Entry(task).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return task;
        }

        /// <summary>
        /// Deletes the given TDTask from the Database
        /// </summary>
        /// <param name="task"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task DeleteTask(TDTask task, ApplicationDbContext context)
        {
            await context.AddAsync(task);
            context.Entry(task).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }
    }
}