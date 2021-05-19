using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<List<TodoItem>> GetTasks()
        {
            return await _repository.FindAll();
        }

        public async Task<TodoItem> GetTask(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var task = await _repository.Find(id.Value);

            if (task == null)
            {
                return null;
            }

            return task;
        }

        public async Task UpdateTask(int? id, TodoItem todoItem)
        {
            if (id == null)
            {
            }

            try
            {
                await _repository.Update(todoItem);
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
