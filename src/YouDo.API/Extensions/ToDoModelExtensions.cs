using YouDo.API.Models.ToDo;
using YouDo.Application.DTOs;

namespace YouDo.API.Extensions
{
    public static class ToDoModelExtensions
    {
        public static ToDoDTO ToDto(this CreateToDoModel createTodoModel)
        {
            return new ToDoDTO
            {
                Title = createTodoModel.Title,
                Details = createTodoModel.Details,
                UserId = createTodoModel.UserId
            };
        }

        public static ToDoDTO ToDto(this UpdateToDoModel updateTodoModel)
        {
            return new ToDoDTO
            {
                Id = updateTodoModel.Id,
                Title = updateTodoModel.Title,
                Details = updateTodoModel.Details,
                Completed = updateTodoModel.Completed,
                UserId = updateTodoModel.UserId
            };
        }
    }
}
