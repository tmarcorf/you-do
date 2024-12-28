using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouDo.Application.DTOs;
using YouDo.Core.Entities;

namespace YouDo.Application.Extensions
{
    public static class ToDoExtensions
    {
        public static ToDoDTO ToDto(this ToDo toDo)
        {
            return new ToDoDTO
            {
                Id = toDo.Id,
                Title = toDo.Title,
                Details = toDo.Details,
                CreatedAt = toDo.CreatedAt,
                UpdatedAt = toDo.UpdatedAt,
                Completed = toDo.Completed,
                UserId = toDo.UserId,
            };
        }

        public static ToDo ToEntity(this ToDoDTO toDoDTO)
        {
            var todo = new ToDo(
                toDoDTO.Id,
                toDoDTO.UserId,
                toDoDTO.Title,
                toDoDTO.Details,
                toDoDTO.Completed);
            
            todo.CreatedAt = toDoDTO.CreatedAt;
            todo.UpdatedAt = toDoDTO.UpdatedAt;

            return todo;
        }

        public static IEnumerable<ToDo> ToEntityList(this IEnumerable<ToDoDTO> toDoDtoEntities)
        {
            return toDoDtoEntities.Select(ToEntity);
        }

        public static IEnumerable<ToDoDTO> ToDtoList(this IEnumerable<ToDo> toDoEntities)
        {
            return toDoEntities.Select(ToDto);
        }
    }
}
