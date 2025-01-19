using System.ComponentModel.DataAnnotations;
using YouDo.Core.Validations;

namespace YouDo.Application.DTOs.ToDo
{
    public class CreateToDoDTO
    {
        public string Title { get; set; }

        public string Details { get; set; }

        public Guid UserId { get; set; }
    }
}
