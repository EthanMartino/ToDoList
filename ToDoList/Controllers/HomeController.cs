using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
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
            ViewData["ListTitle"] = list.ListTitle;
            return View();
        }

        [HttpGet]
        public IActionResult AddTask() 
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> AddTask(TDTask task) 
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<IActionResult> ViewAllToDoLists(string userId)
        {
            IdentityUser currentUser = await _userManager.FindByIdAsync(userId);
            ViewBag.UserTDLists = await TDListDb.GetAllToDoListsById(_context, userId);
            return View(currentUser);        
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
