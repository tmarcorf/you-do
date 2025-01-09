using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouDo.Core.Validations;

namespace YouDo.Application.DTOs.ToDo
{
    public class UpdateToDoDTO : CreateToDoDTO
    {
        [Required(ErrorMessage = ToDoValidationMessages.INVALID_ID)]
        public Guid Id { get; set; }

        public bool Completed { get; set; }
    }
}
