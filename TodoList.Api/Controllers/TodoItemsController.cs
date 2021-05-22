using Microsoft.AspNetCore.Mvc;
using System;
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
            try
            {
                var tasks = await _service.GetTasks();

                return Ok(tasks);
            }
            catch (ApplicationException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet]
        [Route("find/{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            try
            {
                var task = await _service.GetTask(id);

                return Ok(task);
            }
            catch (ApplicationException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int? id, [FromBody]TodoItem todoItem)
        {
            try
            {
                await _service.UpdateTask(id, todoItem);

                return Ok(todoItem);
            }
            catch (Exceptions.DbUpdateException e)
            {
                return BadRequest(e.Message);
            }
            catch (ApplicationException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteTask(id);

                return Ok(new { message = "Deletado com sucesso!" });
            }
            catch (ApplicationException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
