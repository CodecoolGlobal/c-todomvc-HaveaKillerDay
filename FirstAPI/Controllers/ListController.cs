using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Controllers
{
    public class ListController : ControllerBase
    {
        private readonly TodoContext todoContext;

        public ListController(TodoContext context)
        {
            todoContext = context;
        }

    }
}
