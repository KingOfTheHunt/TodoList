using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Api.Data;
using TodoList.Api.Models;

namespace TodoList.Api.Repositories
{
    public class TodoItemRepository : IRepository<TodoItem>
    {
        private readonly TodoListDbContext _context;

        public TodoItemRepository(TodoListDbContext context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            var task = await Find(id);
            _context.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task<TodoItem> Find(int id)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);

            return task;
        }

        public async Task<List<TodoItem>> FindAll()
        {
            var tasks = await _context.Tasks.ToListAsync();

            return tasks;
        }

        public async Task Save(TodoItem t)
        {
            _context.Add(t);
            await _context.SaveChangesAsync();
        }

        public async Task Update(TodoItem t)
        {
            try
            {
                var hasAny = await _context.Tasks.AnyAsync(x => x.Id == t.Id);

                if (hasAny == false)
                {
                    return;
                }

                _context.Update(t);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
