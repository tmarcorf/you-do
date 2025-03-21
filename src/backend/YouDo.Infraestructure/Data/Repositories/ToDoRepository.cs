﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using YouDo.Core.Entities;
using YouDo.Core.Interfaces;
using YouDo.Infraestructure.Data.Context;

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

        public async Task<IEnumerable<ToDo>> GetAllFromUserAsync(Guid userId, int skip, int take)
        {
            if (userId.Equals(Guid.Empty))
            {
                return Enumerable.Empty<ToDo>();
            }

            var toDoList = await _context
                .ToDos
                .Where(x => x.UserId == userId)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return toDoList;
        }

        public async Task<IEnumerable<ToDo>> GetAllFromUserWithSpecifiedCreationDateAsync(Guid userId, DateTime creationDate, int skip, int take)
        {
            if (userId.Equals(Guid.Empty))
            {
                return Enumerable.Empty<ToDo>();
            }

            var toDoList = await _context
                .ToDos
                .Where(x => x.UserId == userId &&
                            x.CreatedAt.Date == creationDate.Date)
                .Skip(skip)
                .Take(take)
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

        public async Task<bool> DeleteAsync(ToDo toDo)
        {
            _context.ToDos.Remove(toDo);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
