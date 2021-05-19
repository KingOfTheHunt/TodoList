using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Api.Exceptions;
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
            try
            {
                return await _repository.FindAll();
            }
            catch (NotFoundException e)
            {
                throw new NotFoundException(e.Message);
            }
        }

        public async Task<TodoItem> GetTask(int? id)
        {
            if (id == null)
            {
                throw new Exception("O id não foi informado!");
            }

            var task = await _repository.Find(id.Value);

            if (task == null)
            {
                throw new NotFoundException("Não existe uma tarefa com este id!");
            }

            return task;
        }

        public async Task UpdateTask(int? id, TodoItem todoItem)
        {
            if (id == null)
            {
                throw new Exception("O id não foi informado!");
            }

            if (id != todoItem.Id)
            {
                throw new Exception("O id informado na url é diferente do id da tarefa!");
            }

            try
            {
                await _repository.Update(todoItem);
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new Exceptions.DbUpdateException(e.Message);
            }
            catch (NotFoundException e)
            {
                throw new NotFoundException(e.Message);
            }
        }
    }
}
