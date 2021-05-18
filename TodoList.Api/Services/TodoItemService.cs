using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Api.Models;
using TodoList.Api.Models.ViewModel.TodoItem;
using TodoList.Api.Repositories;

namespace TodoList.Api.Services
{
    public class TodoItemService
    {
        private readonly TodoItemRepository _repository;

        public TodoItemService(TodoItemRepository repository)
        {
            _repository = repository;
        }

        public async Task Insert(TodoItemViewModelInput item)
        {
            var todoItem = new TodoItem
            {
                Name = item.Name,
                IsCompleted = item.IsCompleted
            };

            await _repository.Save(todoItem);
        }

        public async Task<List<TodoItem>> GetAll()
        {
            return await _repository.FindAll();
        }
    }
}
