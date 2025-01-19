using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using YouDo.Application.DTOs.ToDo;
using YouDo.Application.Extensions;
using YouDo.Application.Interfaces;
using YouDo.Application.Results;
using YouDo.Application.Results.ToDo;
using YouDo.Core.Entities;
using YouDo.Core.Interfaces;

namespace YouDo.Application.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepository _repository;
        private readonly UserManager<User> _userManager;
        private const int TITLE_MIN_LENGTH = 5;
        private const int TITLE_MAX_LENGTH = 100;
        private const int DETAILS_MAX_LENGTH = 500;

        public ToDoService(IToDoRepository repository, UserManager<User> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<Result<IEnumerable<ToDoDTO>>> GetAllFromUserAsync(Guid userId, int skip, int take)
        {
            if (userId.Equals(Guid.Empty))
            {
                return Result<IEnumerable<ToDoDTO>>.Failure(ToDoErrors.InvalidUserId);
            }

            var toDoEntities = await _repository.GetAllFromUserAsync(userId, skip, take);

            return Result<IEnumerable<ToDoDTO>>.Success(toDoEntities.ToDtoList());
        }

        public async Task<Result<IEnumerable<ToDoDTO>>> GetAllFromUserWithSpecifiedCreationDateAsync(Guid userId, DateTime creationDate, int skip, int take)
        {
            if (userId.Equals(Guid.Empty))
            {
                return Result<IEnumerable<ToDoDTO>>.Failure(ToDoErrors.InvalidUserId);
            }

            if (creationDate.Equals(DateTime.MinValue))
            {
                return Result<IEnumerable<ToDoDTO>>.Failure(ToDoErrors.InvalidCreationDate);
            }

            var toDoEntities = await _repository.GetAllFromUserWithSpecifiedCreationDateAsync(userId, creationDate, skip, take);

            return Result<IEnumerable<ToDoDTO>>.Success(toDoEntities.ToDtoList());
        }

        public async Task<Result<ToDoDTO>> GetByIdAsync(Guid id)
        {
            if (id.Equals(Guid.Empty))
            {
                return Result<ToDoDTO>.Failure(ToDoErrors.InvalidId);
            }

            var toDo = await _repository.GetByIdAsync(id);

            if (toDo == null)
            {
                return Result<ToDoDTO>.Failure(ToDoErrors.InvalidId);
            }

            return Result<ToDoDTO>.Success(toDo.ToDto());
        }

        public async Task<Result<ToDoDTO>> CreateAsync(CreateToDoDTO createToDoDTO)
        {
            var toDo = createToDoDTO.ToEntity();
            toDo.Id = Guid.NewGuid();
            toDo.CreatedAt = DateTime.UtcNow;
            toDo.UpdatedAt = DateTime.UtcNow;
            toDo.Completed = false;

            var result = GetOperationResult(toDo);

            if (result.IsSuccess) await _repository.CreateAsync(toDo);

            return result;
        }

        public async Task<Result<ToDoDTO>> UpdateAsync(UpdateToDoDTO updateToDoDTO)
        {
            var toDo = await _repository.GetByIdAsync(updateToDoDTO.Id);

            if (toDo == null) return Result<ToDoDTO>.Failure(ToDoErrors.InvalidId);
            
            toDo = updateToDoDTO.ToEntity(toDo);
            toDo.UpdatedAt = DateTime.UtcNow;

            var result = GetOperationResult(toDo);

            if (result.IsSuccess) await _repository.UpdateAsync(toDo);

            return result;
        }

        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            var toDo = await _repository.GetByIdAsync(id);

            if (toDo == null) return Result<bool>.Failure(ToDoErrors.InvalidId);

            var deleted = await _repository.DeleteAsync(toDo);

            return Result<bool>.Success(deleted);

        }

        private Result<ToDoDTO> GetOperationResult(ToDo toDo)
        {
            if (!_userManager.Users.AnyAsync(x => x.Id == toDo.UserId).Result)
            {
                return Result<ToDoDTO>.Failure(ToDoErrors.InvalidUserId);
            }

            if (string.IsNullOrEmpty(toDo.Title))
            {
                return Result<ToDoDTO>.Failure(ToDoErrors.InvalidTitle);
            }

            if (toDo.Title.Length < TITLE_MIN_LENGTH)
            {
                return Result<ToDoDTO>.Failure(ToDoErrors.InvalidTitleLength);
            }

            if (toDo.Title.Length > TITLE_MAX_LENGTH)
            {
                return Result<ToDoDTO>.Failure(ToDoErrors.InvalidTitleMaxLength);
            }

            if (!string.IsNullOrEmpty(toDo.Details) &&
                toDo.Details.Length > DETAILS_MAX_LENGTH)
            {
                return Result<ToDoDTO>.Failure(ToDoErrors.InvalidDetailsMaxLength);
            }

            return Result<ToDoDTO>.Success(toDo.ToDto());
        }
    }
}
