using YouDo.Core.Validations;

namespace YouDo.Core.Entities
{
    public class ToDo : BaseEntity
    {
        public string Title { get; set; }

        public string Details { get; set; }

        public DateTime CreatedAt { get; set; } 

        public DateTime UpdatedAt { get; set; }

        public bool Completed { get; set; }

        public Guid UserId { get; set; }

        public ToDo(Guid id, Guid userId, string title, string details, bool completed)
        {
            ValidateDomain(id, userId, title);

            Id = id;
            UserId = userId;
            Title = title;
            Details = details;
            Completed = completed;
        }

        private void ValidateDomain(Guid id, Guid userId, string title)
        {
            DomainExceptionValidation.When(id.Equals(Guid.Empty), ToDoValidationMessages.INVALID_ID);
            DomainExceptionValidation.When(userId.Equals(Guid.Empty), ToDoValidationMessages.INVALID_USER_ID);
            DomainExceptionValidation.When(string.IsNullOrEmpty(title), ToDoValidationMessages.INVALID_TITLE);
            DomainExceptionValidation.When(title.Length < 5, ToDoValidationMessages.INVALID_TITLE_LENGTH);
        }
    }
}
