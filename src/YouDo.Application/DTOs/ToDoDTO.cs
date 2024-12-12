using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouDo.Core.Validations;

namespace YouDo.Application.DTOs
{
    public class ToDoDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = ToDoValidationMessages.INVALID_TITLE)]
        [MinLength(5, ErrorMessage = ToDoValidationMessages.INVALID_TITLE_LENGTH)]
        [MaxLength(100, ErrorMessage = ToDoValidationMessages.INVALID_TITLE_MAX_LENGTH)]
        public string Title { get; private set; }

        [MaxLength(500, ErrorMessage = ToDoValidationMessages.INVALID_DETAILS_MAX_LENGTH)]
        public string Details { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public bool Completed { get; private set; }

        [Required(ErrorMessage = ToDoValidationMessages.INVALID_USER_ID)]
        public Guid UserId { get; set; }
    }
}
