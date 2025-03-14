﻿using YouDo.Core.Validations.ToDo;

namespace YouDo.Core.Entities
{
    public class ToDo : BaseEntity
    {
        private const int TITLE_MIN_LENGTH = 5;
        private const int TITLE_MAX_LENGTH = 100;
        private const int DETAILS_MAX_VALUE = 500;

        public ToDo(string title, string details)
        {
            Title = title;
            Details = details;

            Validate();
        }

        public string Title { get; private set; }

        public string Details { get; private set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool Completed { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public void Update(string title, string details, bool completed)
        {
            Title = title;
            Details = details;
            Completed = completed;

            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                throw new ArgumentException(ToDoErrors.InvalidTitle.Message);
            }

            if (Title.Length < TITLE_MIN_LENGTH)
            {
                throw new ArgumentException(ToDoErrors.InvalidTitleLength.Message);
            }

            if (Title.Length > TITLE_MAX_LENGTH)
            {
                throw new ArgumentException(ToDoErrors.InvalidTitleMaxLength.Message);
            }

            if (Details?.Length > DETAILS_MAX_VALUE)
            {
                throw new ArgumentException(ToDoErrors.InvalidDetailsMaxLength.Message);
            }
        }
    }
}
