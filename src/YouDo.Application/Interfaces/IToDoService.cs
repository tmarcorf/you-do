using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouDo.Application.DTOs;
using YouDo.Core.Entities;

namespace YouDo.Application.Interfaces
{
    public interface IToDoService
    {
        Task<IEnumerable<ToDoDTO>> GetAllFromUserAsync(Guid userId);

        Task<IEnumerable<ToDoDTO>> GetAllFromUserWithSpecifiedCreationDateAsync(Guid userId, DateTime creationDate);

        Task<ToDoDTO> GetByIdAsync(Guid id);

        Task CreateAsync(ToDoDTO toDoDto);

        Task UpdateAsync(ToDoDTO toDoDto);

        Task<bool> DeleteAsync(Guid id);
    }
}
