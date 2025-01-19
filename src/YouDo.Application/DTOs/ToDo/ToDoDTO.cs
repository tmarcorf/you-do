using System.ComponentModel.DataAnnotations;
using YouDo.Core.Validations;

namespace YouDo.Application.DTOs.ToDo
{
    public class ToDoDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Details { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool Completed { get; set; }

        public Guid UserId { get; set; }
    }
}
