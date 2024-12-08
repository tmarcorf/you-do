using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouDo.Core.Entities;
using YouDo.Core.Interfaces;
using YouDo.Infraestructure.Data.Context;
using YouDo.Infraestructure.Data.Identity;

namespace YouDo.Infraestructure.Data.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;


        public ToDoRepository(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IEnumerable<ToDo>> GetAllFromUserAsync(Guid userId)
        {
            if (userId.Equals(Guid.Empty)) return Enumerable.Empty<ToDo>();

            var userExist = await _userManager.FindByIdAsync(userId.ToString()) != null;

            if (!userExist) return Enumerable.Empty<ToDo>();

            var toDoList = await _context
                .ToDos
                .Where(x => x.UserId == userId)
                .ToListAsync();

            return toDoList;
        }

        public async Task<IEnumerable<ToDo>> GetAllFromUserWithSpecifiedCreationDateAsync(Guid userId, DateTime creationDate)
        {
            if (userId.Equals(Guid.Empty)) return Enumerable.Empty<ToDo>();

            var userExist = await _userManager.FindByIdAsync(userId.ToString()) != null;

            if (!userExist) return Enumerable.Empty<ToDo>();

            var toDoList = await _context
                .ToDos
                .Where(x => x.UserId == userId && 
                            x.CreatedAt.Date == creationDate.Date)
                .ToListAsync();

            return toDoList;
        }

        public async Task<ToDo> GetByIdAsync(Guid id)
        {
            var toDo = await _context
                .ToDos
                .FirstOrDefaultAsync(x => x.Id == id);

            return toDo;
        }

        public async Task<ToDo> CreateAsync(ToDo toDo)
        {
            _context.ToDos.Add(toDo);
            await _context.SaveChangesAsync();

            return toDo;
        }
        
        public async Task<ToDo> UpdateAsync(ToDo toDo)
        {
            _context.ToDos.Update(toDo);
            await _context.SaveChangesAsync();

            return toDo;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var toDo = await _context.ToDos.FirstOrDefaultAsync(x => x.Id == id);

            if (toDo == null) return false;

            _context.ToDos.Remove(toDo);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
