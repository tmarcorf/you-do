using YouDo.Application.DTOs;
using YouDo.Application.Extensions;
using YouDo.Application.Interfaces;
using YouDo.Core.Interfaces;

namespace YouDo.Application.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepository _repository;

        public ToDoService(IToDoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ToDoDTO>> GetAllFromUserAsync(Guid userId)
        {
            var toDoEntities = await _repository.GetAllFromUserAsync(userId);

            return toDoEntities.ToDtoList();
        }

        public async Task<IEnumerable<ToDoDTO>> GetAllFromUserWithSpecifiedCreationDateAsync(Guid userId, DateTime creationDate)
        {
            var toDoEntities = await _repository.GetAllFromUserWithSpecifiedCreationDateAsync(userId, creationDate);

            return toDoEntities.ToDtoList();
        }

        public async Task<ToDoDTO> GetByIdAsync(Guid id)
        {
            var toDo = await _repository.GetByIdAsync(id);

            return toDo.ToDto();
        }

        public async Task CreateAsync(ToDoDTO toDoDto)
        {
            toDoDto.Id = Guid.NewGuid();
            toDoDto.CreatedAt = DateTime.Now;
            var toDoEntity = toDoDto.ToEntity();

            await _repository.CreateAsync(toDoEntity);
        }

        public async Task UpdateAsync(ToDoDTO toDoDto)
        {
            toDoDto.UpdatedAt = DateTime.Now;
            var toDoEntity = toDoDto.ToEntity();

            await _repository.UpdateAsync(toDoEntity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
