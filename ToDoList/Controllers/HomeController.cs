using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //Field to access the database anywhere in the class
        private readonly ApplicationDbContext _context;

        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateTodoList() 
        {
            //If a user is logged in, get their user id
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["UserId"] = userId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodoList(TDList list) 
        {
            if (ModelState.IsValid) 
            {
                await TDListDb.AddToDoList(_context, list);
                TempData["Message"] = $"{list.ListTitle} added successfully";

                //Redirect the user to their newly created list's details page
                return RedirectToAction(nameof(Details), new RouteValueDictionary(new { action = "Details", Id = list.ListId}));
            }
            return View();
        }

        public async Task<IActionResult> Details(int id) 
        {
            TDList list = await TDListDb.GetToDoListById(_context, id);
            ViewBag.Tasks = await TaskDb.GetAllTasksByListId(list.ListId, _context);
            ViewData["ListTitle"] = list.ListTitle;
            ViewData["ListId"] = list.ListId;
            return View();
        }

        public async Task<IActionResult> ViewCompletedTasks(int id)
        {
            TDTask task = await TaskDb.GetTaskById(id, _context);
            task.isCompleted = true;
            await TaskDb.UpdateTask(task, _context);

            return RedirectToAction(nameof(Details), new RouteValueDictionary(new { action = "Details", Id = task.ListId }));
        }

        public async Task<IActionResult> UndoCompletedTask(int id) 
        {
            TDTask task = await TaskDb.GetTaskById(id, _context);
            task.isCompleted = false;
            await TaskDb.UpdateTask(task, _context);
            return RedirectToAction(nameof(Details), new RouteValueDictionary(new { action = "Details", Id = task.ListId }));
        }

        [HttpGet]
        public IActionResult AddTask(int listId) 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTask(TDTask task) 
        {
            if (ModelState.IsValid) 
            {
                await TaskDb.AddTask(task, _context);
                TempData["Message"] = $"{task.TaskTitle} task added successfully";
                return RedirectToAction(nameof(Details), new RouteValueDictionary(new { action = "Details", Id = task.ListId}));
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditTask(int id) 
        {
            TDTask task = await TaskDb.GetTaskById(id, _context);
            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> EditTask(TDTask task) 
        {
            if (ModelState.IsValid) 
            {
                await TaskDb.UpdateTask(task, _context);
                TempData["Message"] = $"{task.TaskTitle} edited successfully";
                return RedirectToAction(nameof(Details), new RouteValueDictionary(new { action = "Details", Id = task.ListId }));
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteTask(int id)
        {
            TDTask task = await TaskDb.GetTaskById(id, _context);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost, ActionName("DeleteTask")]
        public async Task<IActionResult> DeleteTaskConfirmed(int id)
        {
            TDTask task = await TaskDb.GetTaskById(id, _context);
            int listId = task.ListId;
            await TaskDb.DeleteTask(task, _context);
            TempData["Message"] = $"{task.TaskTitle} task deleted successfully";
            return RedirectToAction(nameof(Details), new RouteValueDictionary(new { action = "Details", Id = listId }));
        }

        [HttpGet]
        public async Task<IActionResult> ViewAllToDoLists(string userId)
        {
            IdentityUser currentUser = await _userManager.FindByIdAsync(userId);
            ViewBag.UserTDLists = await TDListDb.GetAllToDoListsById(_context, userId);
            return View(currentUser);        
        }

        [HttpGet]
        public async Task<IActionResult> DeleteToDoList(int id) 
        {
            TDList list = await TDListDb.GetToDoListById(_context, id);
            if (list == null) 
            {
                return NotFound();
            }
            return View(list);
        }

        [HttpPost, ActionName("DeleteToDoList")]
        public async Task<IActionResult> DeleteToDoListConfirmed(int id) 
        {
            TDList list = await TDListDb.GetToDoListById(_context, id);
            await TDListDb.DeleteToDoList(_context, list);
            TempData["Message"] = $"{list.ListTitle} list deleted successfully";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> EditToDoList(int id, int userId)
        {
            ViewData["UserId"] = userId;
            TDList list = await TDListDb.GetToDoListById(_context, id);
            if (list == null)
            {
                return NotFound();
            }
            return View(list);
        }

        [HttpPost]
        public async Task<IActionResult> EditToDoList(TDList list)
        {
            await TDListDb.UpdateToDoList(_context, list);
            TempData["Message"] = "Name changed successfully";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
