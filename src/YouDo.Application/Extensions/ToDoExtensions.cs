using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using YouDo.Application.DTOs.ToDo;
using YouDo.Core.Entities;

namespace YouDo.Application.Extensions
{
    public static class ToDoExtensions
    {
        public static ToDoDTO ToDto(this ToDo toDo)
        {
            if (toDo is null)
            {
                return null;
            }

            return new ToDoDTO
            {
                Id = toDo.Id,
                Title = toDo.Title,
                Details = toDo.Details,
                CreatedAt = toDo.CreatedAt,
                UpdatedAt = toDo.UpdatedAt,
                Completed = toDo.Completed
            };
        }

        public static ToDo ToEntity(this ToDoDTO toDoDTO)
        {
            if (toDoDTO is null) return null;

            var toDo = new ToDo(toDoDTO.Title, toDoDTO.Details);
            toDo.Id = toDoDTO.Id;
            toDo.CreatedAt = toDoDTO.CreatedAt;
            toDo.UpdatedAt = toDoDTO.UpdatedAt;
            toDo.Completed = toDoDTO.Completed;

            return toDo;
        }

        public static IEnumerable<ToDo> ToEntityList(this IEnumerable<ToDoDTO> toDoDtoEntities)
        {
            if (toDoDtoEntities is null)
            {
                return Enumerable.Empty<ToDo>();
            }

            return toDoDtoEntities.Select(ToEntity);
        }

        public static IEnumerable<ToDoDTO> ToDtoList(this IEnumerable<ToDo> toDoEntities)
        {
            if (toDoEntities is null)
            {
                return Enumerable.Empty<ToDoDTO>();
            }

            return toDoEntities.Select(ToDto);
        }

        public static ToDo ToEntity(this CreateToDoDTO createToDoDTO)
        {
            var toDo = new ToDo(createToDoDTO.Title, createToDoDTO.Details);
            toDo.UserId = createToDoDTO.UserId;

            return toDo;
        }

        public static ToDoDTO ToDto(this CreateToDoDTO createToDoDTO)
        {
            return new ToDoDTO
            {
                Title = createToDoDTO.Title,
                Details = createToDoDTO.Details
            };
        }

        public static ToDo ToEntity(this UpdateToDoDTO updateToDoDTO)
        {
            var toDo = new ToDo(updateToDoDTO.Title, updateToDoDTO.Details);
            toDo.Id = updateToDoDTO.Id;
            toDo.UserId = updateToDoDTO.UserId;
            toDo.Completed = updateToDoDTO.Completed;

            return toDo;
        }
    }
}
