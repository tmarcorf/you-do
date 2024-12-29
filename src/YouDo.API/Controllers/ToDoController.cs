using Microsoft.AspNetCore.Mvc;
using YouDo.Application.DTOs;
using YouDo.Application.Interfaces;

namespace YouDo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService _service;

        public ToDoController(IToDoService service)
        {
            _service = service;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<ToDoDTO>>> GetAllFromUser(string userId)
        {
            Guid userIdGuid;
            if (!Guid.TryParse(userId, out userIdGuid)) return NotFound("ToDos not found");

            var toDos = await _service.GetAllFromUserAsync(userIdGuid);

            return Ok(toDos);
        }

        [HttpGet("{userId}/{creationDate}")]
        public async Task<ActionResult<IEnumerable<ToDoDTO>>> GetAllFromUserWithSpecifiedCreationDate(string userId, DateTime creationDate)
        {
            Guid userIdGuid;
            if (!Guid.TryParse(userId, out userIdGuid)) return NotFound("ToDos not found");

            var toDos = await _service.GetAllFromUserWithSpecifiedCreationDateAsync(userIdGuid, creationDate);

            return Ok(toDos);
        }

        [HttpGet("specific/{id}")]
        public async Task<ActionResult<ToDoDTO>> GetById(string id)
        {
            Guid idGuid;
            if (!Guid.TryParse(id, out idGuid)) return NotFound("ToDo not found");

            var toDo = await _service.GetByIdAsync(idGuid);

            if (toDo == null) return NotFound("ToDo not found");

            return Ok(toDo);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ToDoDTO toDoDto)
        {
            if (toDoDto == null) return BadRequest("Invalid data");

            await _service.CreateAsync(toDoDto);

            return Created();
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] ToDoDTO toDoDto)
        {
            if (toDoDto == null) return BadRequest("Invalid data");

            await _service.UpdateAsync(toDoDto);

            return Ok(toDoDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ToDoDTO>> Delete(string id)
        {
            Guid idGuid;
            if (!Guid.TryParse(id, out idGuid)) return NotFound("ToDo not found");

            var toDo = await _service.GetByIdAsync(idGuid);

            if (toDo == null) return NotFound("ToDo not found");

            await _service.DeleteAsync(idGuid);

            return Ok(toDo);
        }
    }
}
