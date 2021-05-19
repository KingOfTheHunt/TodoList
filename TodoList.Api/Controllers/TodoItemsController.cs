using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoList.Api.Models;
using TodoList.Api.Models.ViewModel.TodoItem;
using TodoList.Api.Services;

namespace TodoList.Api.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoItemService _service;

        public TodoItemsController(TodoItemService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody]TodoItemViewModelInput todoItem)
        {
            await _service.Insert(todoItem);

            return Created("", todoItem);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> Get()
        {
            var tasks = await _service.GetTasks();

            return Ok(tasks);
        }

        [HttpGet]
        [Route("find/{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            var task = await _service.GetTask(id);

            if (task == null)
            {
                return NotFound("A tarefa não foi encontrada!");
            }

            return Ok(task);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int? id, [FromBody]TodoItem todoItem)
        {
            try
            {
                await _service.UpdateTask(todoItem);

                return Ok(todoItem);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
