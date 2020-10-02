using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirstAPI.Models;
using System.Security.Permissions;

namespace FirstAPI.Controllers
{
    [Route("todos")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        [HttpPost("list")]
        public JsonResult GetListItems()
        {
            var body = _context.TodoItems.ToList();

            JsonResult json = new JsonResult(body);

            return json;
        }

        // GET: todos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Consumes("application/x-www-form-urlencoded")]
        public IActionResult PutTodoItem([FromForm] IFormCollection data)
        {

            List<string> requestBody = new List<string>();
            //Something something = JsonConvert.DeserializeObject<Something>(data);
            foreach (var key in data.Keys)
            {
                requestBody.Add(data[key]);
            }

            string newTitle = requestBody[0];
            string idAsString = Request.Path.Value;
            string[] splitted = idAsString.Split("/");
            long ID = Convert.ToInt64(splitted[splitted.Length - 1]);

            TodoItem searchedTodoItem = _context.TodoItems.Where(item => item.Id == ID).FirstOrDefault();

            searchedTodoItem.Title = newTitle;

            if (ID != searchedTodoItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(searchedTodoItem).State = EntityState.Modified;

             _context.SaveChanges();

            return CreatedAtAction(nameof(GetTodoItem), new  { id = searchedTodoItem.Id}, searchedTodoItem);

        }

        [HttpPut("change/{id}/toggle_status")]
        [Consumes("application/x-www-form-urlencoded")]
        public IActionResult ToggleCompleted([FromForm] IFormCollection data)
        {

            string idAsString = Request.Path.Value;
            string[] splitted = idAsString.Split("/");
            long ID = Convert.ToInt64(splitted[3]);

            TodoItem searchedTodoItem = _context.TodoItems.Where(item => item.Id == ID).FirstOrDefault();

            searchedTodoItem.Completed = !searchedTodoItem.Completed;

            if (ID != searchedTodoItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(searchedTodoItem).State = EntityState.Modified;

            _context.SaveChanges();

            return CreatedAtAction(nameof(GetTodoItem), new { id = searchedTodoItem.Id }, searchedTodoItem);

        }

            // POST: api/TodoItems
            // To protect from overposting attacks, enable the specific properties you want to bind to, for
            // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
            [HttpPost("add")]
        [Consumes("application/x-www-form-urlencoded")]
        public ActionResult<TodoItem> PostTodoItem([FromForm] IFormCollection data)
        {
            TodoItem newItem;

            List<string> requestBody = new List<string>();
            //Something something = JsonConvert.DeserializeObject<Something>(data);
            foreach (var key in data.Keys)
            {
                requestBody.Add(data[key]);
            }

            newItem = new TodoItem { Title = requestBody[0], Completed = false };

            _context.TodoItems.Add(newItem);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetTodoItem), new { id = newItem.Id }, newItem);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return todoItem;
        }

        private bool TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
