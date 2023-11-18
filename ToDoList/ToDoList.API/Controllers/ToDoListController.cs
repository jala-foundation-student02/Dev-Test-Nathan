using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.API.Models;
using ToDoList.API.Persistance;

namespace ToDoList.API.Controllers
{
    [Route("api/to-do-list-challenge")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly ToDoListDbContext _context;

        public ToDoListController(ToDoListDbContext context)
        {
            _context = context;
        }

        

        //GET ALL TO-DO LISTS THAT WERE NOT DELETED
        [HttpGet]
        public IActionResult GetAll()
        {
            var toDoLists = _context.ToDoLists.Where(td => !td.IsDeleted).ToList();

            if(toDoLists == null)
            {
                return NotFound();
            }

            return Ok(toDoLists);
        }

        

        //GET TO-DO LIST BY CATEGORY
        [HttpGet("/search-by-category/{category}")]
        public IActionResult GetByCategory(string category)
        {
            var toDoLists = _context.ToDoLists.Where(td => td.TaskCategory.ToLower() == category.ToLower() && !td.IsDeleted).ToList();

            if (toDoLists == null)
            {
                return NotFound();
            }

            return Ok(toDoLists);
        }

        //GET TO-DO LIST BY STATUS
        [HttpGet("/search-by-status/{status}")]
        public IActionResult GetByStatus(string status)
        {
            var toDoLists = _context.ToDoLists.Where(td => td.Status.ToLower() == status.ToLower() && !td.IsDeleted).ToList();

            if(toDoLists == null)
            {
                return NotFound();
            }

            return Ok(toDoLists);
        }

        //GET TO-DO LIST BY ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var toDoList = _context.ToDoLists.SingleOrDefault(td => td.Id == id);

            if(toDoList == null)
            {
                return NotFound();
            }

            return Ok(toDoList);
        }

        [HttpPost]
        public IActionResult Post(ToDoListModel toDoList)
        {
            _context.Add(toDoList);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = toDoList.Id }, toDoList);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, ToDoListModel input)
        {
            var toDoList = _context.ToDoLists.SingleOrDefault(td => td.Id == id);

            if(toDoList == null)
            {
                return NotFound();
            }

            toDoList.Update(input.TaskTitle, input.TaskCategory, input.TaskDescription, input.TaskDeadline);
            _context.ToDoLists.Update(toDoList);
            _context.SaveChanges();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var toDoList = _context.ToDoLists.SingleOrDefault(td => td.Id == id);

            if(toDoList == null)
            {
                return NotFound();
            }

            toDoList.Delete();
            _context.SaveChanges();

            return NoContent();
        }

        [HttpGet("{id}/complete-task")]
        public IActionResult CompleteTask(int id)
        {
            var toDoList = _context.ToDoLists.SingleOrDefault(td => td.Id == id);

            if(toDoList == null)
            {
                return NotFound();
            }

            toDoList.Completed();
            _context.SaveChanges();

            return NoContent();
        }

        


    }
}
