using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouDo.Core.Validations;

namespace YouDo.Core.Entities
{
    public class ToDo : BaseEntity
    {
        public string Title { get; private set; }

        public string Details { get; set; }

        public DateTime CreatedAt { get; set; } 

        public DateTime UpdatedAt { get; set; }

        public bool Completed { get; set; }

        public ToDo(Guid id, string title)
        {
            ValidateDomain(id, title);

            Id = id;
            Title = title;
        }

        public void Update(Guid id, string title)
        {
            ValidateDomain(id, title);

            Id = id;
            Title = title;
        }

        private void ValidateDomain(Guid id, string title)
        {
            DomainExceptionValidation.When(id.Equals(Guid.Empty), ToDoValidationMessages.INVALID_ID);
            DomainExceptionValidation.When(string.IsNullOrEmpty(title), ToDoValidationMessages.INVALID_TITLE);
            DomainExceptionValidation.When(title.Length < 5, ToDoValidationMessages.INVALID_TITLE_LENGTH);
        }
    }
}
