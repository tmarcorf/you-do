using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouDo.Core.Validations.ToDo
{
    public static class ToDoErrors
    {
        public static readonly Error InvalidId = new Error(
            "ToDoErrors.InvalidId", "Invalid Id. Id is required.");

        public static readonly Error InvalidUserId = new Error(
            "ToDoErrors.InvalidUserId", "Invalid userId. userId is required.");

        public static readonly Error InvalidTitle = new Error(
            "ToDoErrors.InvalidTitle", "Invalid title. Title is required.");

        public static readonly Error InvalidTitleLength = new Error(
            "ToDoErrors.InvalidTitleLength", "Too short title. Mininum 5 characters is required.");

        public static readonly Error InvalidTitleMaxLength = new Error(
            "ToDoErrors.InvalidTitleMaxLength", "Too long title. Maximum 100 characters is required.");

        public static readonly Error InvalidDetailsMaxLength = new Error(
            "ToDoErrors.InvalidDetailsMaxLength", "Too long details. Maximum 500 characters is required.");

        public static readonly Error InvalidCreationDate = new Error(
            "ToDoErrors.InvalidCreationDate", $"Invalid creation date. Creation date must be greater than {DateTime.MinValue.ToString("dd/MM/yyyy")}.");
    }
}
