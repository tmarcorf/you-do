﻿using YouDo.Core.Validations.ToDo;

namespace YouDo.Core.Entities
{
    public class ToDo : BaseEntity
    {
        private const int TITLE_MIN_LENGTH = 5;
        private const int TITLE_MAX_LENGTH = 100;
        private const int DESCRIPTION_MAX_VALUE = 500;

        public ToDo(string title, string details, Guid userId)
        {
            Title = title;
            Details = details;
            UserId = userId;

            Validate();
        }

        public string Title { get; private set; }

        public string Details { get; private set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool Completed { get; set; }

        public Guid UserId { get; private set; }

        public User User { get; set; }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Title))
                throw new ArgumentException(ToDoErrors.InvalidTitle.Description);

            if (Title.Length < TITLE_MIN_LENGTH)
                throw new ArgumentException(ToDoErrors.InvalidTitleLength.Description);

            if (Title.Length > TITLE_MAX_LENGTH)
                throw new ArgumentException(ToDoErrors.InvalidTitleMaxLength.Description);

            if (Details?.Length > 500)
                throw new ArgumentException(ToDoErrors.InvalidDetailsMaxLength.Description);

            if (UserId == Guid.Empty)
                throw new ArgumentException(ToDoErrors.InvalidUserId.Description);
        }
    }
}
