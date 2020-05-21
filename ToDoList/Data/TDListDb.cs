using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList.Data
{
    public class TDListDb
    {
        /// <summary>
        /// Gets all of the todo lists out of the database
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task<List<TDList>> GetAllToDoListsById(ApplicationDbContext context, string userId) 
        {
            List<TDList> lists = await (from l in context.TDLists
                                        where l.UserId == userId
                                        orderby l.ListId ascending
                                        select l).ToListAsync();
            return lists;
        }

        /// <summary>
        /// Gets a single todo list by id
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<TDList> GetToDoListById(ApplicationDbContext context, int id) 
        {
            TDList list = await (from l in context.TDLists
                                 where l.ListId == id
                                 select l).SingleOrDefaultAsync();
            return list;
        }

        /// <summary>
        /// Adds a todo list to the database
        /// </summary>
        /// <param name="context"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static async Task<TDList> AddToDoList(ApplicationDbContext context, TDList list) 
        {
            await context.AddAsync(list);
            await context.SaveChangesAsync();
            return list;
        }

        /// <summary>
        /// Updates an existing todo list from the database
        /// </summary>
        /// <param name="context"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static async Task<TDList> UpdateToDoList(ApplicationDbContext context, TDList list) 
        {
            await context.AddAsync(list);
            context.Entry(list).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return list;
        }

        /// <summary>
        /// Deletes an existing todo list from the database 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static async Task DeleteToDoList(ApplicationDbContext context, TDList list) 
        {
            await context.AddAsync(list);
            context.Entry(list).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }

        public static async Task DeleteToDoListWithoutAUserId(ApplicationDbContext context) 
        {
            List<TDList> lists = await (from l in context.TDLists
                                        where l.UserId == null
                                        select l).ToListAsync();

            foreach (TDList item in lists) 
            {
                await context.AddAsync(item);
                context.Entry(item).State = EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }
    }
}
