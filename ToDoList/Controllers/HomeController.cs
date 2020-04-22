using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
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
                return RedirectToAction(nameof(Index));
            }
            return View();
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
