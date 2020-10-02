using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FirstAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstAPI.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {

        private readonly TodoContext todoContext;

        public HomeController(TodoContext todo)
        {
            todoContext = todo;
        }

        [HttpGet]
        public ViewResult Home()
        {
            List<TodoItem> items = todoContext.TodoItems.ToListAsync().Result;

            //ViewBag.Items = items;
            //ViewBag.Title = "Todos";

            return View("Home");
        }
    }
}
