using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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

            return Ok(todoItem);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> Get()
        {
            var tasks = await _service.GetAll();

            return Ok(tasks);
        }
    }
}
