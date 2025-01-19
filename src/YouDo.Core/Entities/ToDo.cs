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

        public User User { get; set; }
    }
}
