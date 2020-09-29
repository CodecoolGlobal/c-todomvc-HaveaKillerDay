using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FirstAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {

        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        [HttpPost]
        public void AddToDo()
        {

        }
    }
}
