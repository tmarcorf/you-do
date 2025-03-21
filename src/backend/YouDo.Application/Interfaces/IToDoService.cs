using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouDo.Application.DTOs.ToDo;
using YouDo.Application.Results;
using YouDo.Core.Entities;

namespace YouDo.Application.Interfaces
{
    public interface IToDoService
    {
        Task<Result<IEnumerable<ToDoDTO>>> GetAllFromUserAsync(Guid userId, int skip, int take);

        Task<Result<IEnumerable<ToDoDTO>>> GetAllFromUserWithSpecifiedCreationDateAsync(Guid userId, DateTime creationDate, int skip, int take);

        Task<Result<ToDoDTO>> GetByIdAsync(Guid id);

        Task<Result<ToDoDTO>> CreateAsync(CreateToDoDTO createToDoDTO);

        Task<Result<ToDoDTO>> UpdateAsync(UpdateToDoDTO updateToDoDTO);

        Task<Result<bool>> DeleteAsync(Guid id);
    }
}
