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

        Task<ToDo> CreateAsync(Guid id);

        Task<ToDo> UpdateAsync(Guid id);

        Task<bool> DeleteAsync(Guid id);
    }
}
