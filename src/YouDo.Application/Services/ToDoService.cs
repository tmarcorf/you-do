﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using YouDo.Application.DTOs.ToDo;
using YouDo.Application.Extensions;
using YouDo.Application.Interfaces;
using YouDo.Core.Entities;
using YouDo.Core.Interfaces;

namespace YouDo.Application.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepository _repository;
        private readonly UserManager<User> _userManager;

        public ToDoService(IToDoRepository repository, UserManager<User> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<IEnumerable<ToDoDTO>> GetAllFromUserAsync(Guid userId, int skip, int take)
        {
            var toDoEntities = await _repository.GetAllFromUserAsync(userId, skip, take);

            return toDoEntities.ToDtoList();
        }

        public async Task<IEnumerable<ToDoDTO>> GetAllFromUserWithSpecifiedCreationDateAsync(Guid userId, DateTime creationDate, int skip, int take)
        {
            var toDoEntities = await _repository.GetAllFromUserWithSpecifiedCreationDateAsync(userId, creationDate, skip, take);

            return toDoEntities.ToDtoList();
        }

        public async Task<ToDoDTO> GetByIdAsync(Guid id)
        {
            var toDo = await _repository.GetByIdAsync(id);

            return toDo.ToDto();
        }

        public async Task<ToDoDTO> CreateAsync(CreateToDoDTO createToDoDTO)
        {
            var toDo = createToDoDTO.ToEntity();

            toDo.Id = Guid.NewGuid();
            toDo.CreatedAt = DateTime.UtcNow.AddDays(1);
            toDo.UpdatedAt = DateTime.UtcNow.AddDays(1);
            toDo.Completed = false;

            if (!await _userManager.Users.AnyAsync(x => x.Id == toDo.UserId)) return null;

            toDo.ValidateDomain(toDo.Id, toDo.UserId, toDo.Title);
            await _repository.CreateAsync(toDo);

            return toDo.ToDto();
        }

        public async Task<ToDoDTO> UpdateAsync(UpdateToDoDTO updateToDoDTO)
        {
            if (!await _userManager.Users.AnyAsync(x => x.Id == updateToDoDTO.UserId)) return null;

            var toDo = await _repository.GetByIdAsync(updateToDoDTO.Id);

            if (toDo == null) return null;

            toDo = updateToDoDTO.ToEntity(toDo);
            toDo.UpdatedAt = DateTime.UtcNow;

            toDo.ValidateDomain(updateToDoDTO.Id, updateToDoDTO.UserId, updateToDoDTO.Title);
            await _repository.UpdateAsync(toDo);

            return toDo.ToDto();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var toDo = await _repository.GetByIdAsync(id);

            if (toDo == null) return false;

            return await _repository.DeleteAsync(toDo);
        }
    }
}
