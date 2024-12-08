using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouDo.Core.Entities;

namespace YouDo.Core.Interfaces
{
    public interface IToDoRepository
    {
        Task<IEnumerable<ToDo>> GetAllFromUserAsync(Guid userId);

        Task<IEnumerable<ToDo>> GetAllFromUserWithSpecifiedCreationDateAsync(Guid userId, DateTime creationDate);

        Task<ToDo> GetByIdAsync(Guid id);

        Task<ToDo> CreateAsync(ToDo toDo);

        Task<ToDo> UpdateAsync(ToDo toDo);

        Task<bool> DeleteAsync(Guid id);
    }
}
