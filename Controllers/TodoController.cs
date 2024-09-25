using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiWithSwaggerDemo.Models;
using WebApiWithSwaggerDemo.Repositories;

namespace WebApiWithSwaggerDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoRepository _repository;

        public TodoController(TodoRepository repository)
        {
            _repository = repository;
        }


        // GET: api/todo
        //[HttpGet]
        [HttpGet(Name = "GetAllTasks")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TodoTask>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<TodoTask>>> GetTasks()
        {
            var tasks = _repository.GetAll();
            if (!tasks.Any())
            {
                tasks = [];
                return Ok(tasks); // 204 No Content if there are no tasks
            }
            return Ok(tasks); // 200 OK with tasks
        }


        // GET: api/todo/{id}
        //[HttpGet("{id}")]
        [HttpGet("{id}", Name = "GetTaskById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoTask))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TodoTask> GetTask(int id)
        {
            var task = _repository.GetById(id);
            if (task == null)
            {
                return NotFound(); // 404 Not Found if the task does not exist
            }
            return Ok(task); // 200 OK with the found task
        }


        // POST: api/todo
        //[HttpPost]
        [HttpPost(Name = "CreateTask")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TodoTask))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TodoTask> CreateTask(TodoTask request)
        {
            if (request == null || string.IsNullOrEmpty(request.Name))
            {
                return BadRequest(); // 400 Bad Request if the input is invalid
            }

            var newTask = new TodoTask { Name = request.Name };
            var createdTask = _repository.Create(newTask);

            return CreatedAtAction(nameof(GetTask), new { id = newTask.Id }, newTask); // 201 Created with the new task
        }


        // PUT: api/todo/{id}/complete
        //[HttpPut("{id}/complete")]
        [HttpPut("{id}/complete", Name = "CompleteTask")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoTask))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TodoTask> CompleteTask(int id)
        {
            var task = _repository.Complete(id);
            if (task == null)
            {
                return NotFound(); // 404 Not Found if the task does not exist
            }
            return Ok(task); // 200 OK with the completed task
        }

        // DELETE: api/todo/{id}
        //[HttpDelete("{id}")]
        [HttpDelete("{id}", Name = "DeleteTask")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteTask(int id)
        {
            var deleted = _repository.Delete(id);
            if (!deleted)
            {
                return NotFound(); // 404 if the task doesn't exist
            }
            return NoContent(); // 204 No Content on successful delete
        }
    }
}
