using System.ComponentModel.DataAnnotations;
using YouDo.Core.Validations;

namespace YouDo.Application.DTOs.ToDo
{
    public class CreateToDoDTO
    {
        [Required(ErrorMessage = ToDoValidationMessages.INVALID_TITLE)]
        [MinLength(5, ErrorMessage = ToDoValidationMessages.INVALID_TITLE_LENGTH)]
        [MaxLength(100, ErrorMessage = ToDoValidationMessages.INVALID_TITLE_MAX_LENGTH)]
        public string Title { get; set; }

        [MaxLength(500, ErrorMessage = ToDoValidationMessages.INVALID_DETAILS_MAX_LENGTH)]
        public string Details { get; set; }

        [Required(ErrorMessage = ToDoValidationMessages.INVALID_USER_ID)]
        public Guid UserId { get; set; }
    }
}
