using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTodo.Services
{
    public class TodoItemsService : ITodoItemService
    {
        private readonly ApplicationDbContext _context;

        public TodoItemsService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<bool> AddItemAsync(NewTodoItem newItem, ApplicationUser currentUser)
        {
            var entity = new TodoItem
            {
                Id = Guid.NewGuid(),
                IsDone = false,
                Title = newItem.Title,
                DueAt = newItem.DeadLine ?? DateTimeOffset.Now.AddDays(3),
                OwnerId = currentUser.Id,
            };

            _context.Items.Add(entity);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<IEnumerable<TodoItem>> GetIncompleteItemsAsync()
        {
            return await _context.Items.Where(i => !i.IsDone).ToArrayAsync();
        }

        public async Task<IEnumerable<TodoItem>> GetIncompleteItemsAsync(ApplicationUser currentUser)
        {
            return await _context.Items.Where(i => !i.IsDone && i.OwnerId == currentUser.Id).ToArrayAsync();
        }

        public async Task<bool> MarkDoneAsync(Guid id, ApplicationUser currentUser)
        {
            var item = await _context.Items.Where(i => i.Id == id && i.OwnerId == currentUser.Id).SingleOrDefaultAsync();

            if (item == null)
                return false;

            item.IsDone = true;
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}