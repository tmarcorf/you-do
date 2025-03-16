using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YouDo.API.Responses;
using YouDo.Application.DTOs.ToDo;
using YouDo.Application.Interfaces;

namespace YouDo.API.Controllers
{
    [Route("api/todo")]
    [ApiController]
    [Authorize]
    public class ToDoController : ControllerBase
    {
        private const int DEFAULT_SKIP = 0;
        private const int DEFAULT_TAKE = 10;
        private const int LIMIT_TAKE = 30;
        private readonly IToDoService _service;

        public ToDoController(IToDoService service)
        {
            _service = service;
        }

        [HttpGet("{skip}/{take}")]
        public async Task<ActionResult> GetAllFromUser(int skip = DEFAULT_SKIP, int take = DEFAULT_TAKE)
        {
            if (take > LIMIT_TAKE)
            {
                return BadRequest(ApiResponse<IEnumerable<ToDoDTO>>.Failure($"The limit of items per page is {LIMIT_TAKE}"));
            }

            var userIdGuid = GetUserId();
            var result = await _service.GetAllFromUserAsync(userIdGuid, skip, take);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse<IEnumerable<ToDoDTO>>.Failure(result.Error.Message));
            }

            return Ok(ApiResponse<IEnumerable<ToDoDTO>>.Success(result.Data));
        }

        [HttpGet("{creationDate}/{skip}/{take}")]
        public async Task<ActionResult> GetAllFromUserWithSpecifiedCreationDate(DateTime creationDate, int skip = DEFAULT_SKIP, int take = DEFAULT_TAKE)
        {
            if (take > LIMIT_TAKE)
            {
                return BadRequest(ApiResponse<IEnumerable<ToDoDTO>>.Failure($"The limit of items per page is {LIMIT_TAKE}"));
            }

            var userIdGuid = GetUserId();

            var result = await _service.GetAllFromUserWithSpecifiedCreationDateAsync(userIdGuid, creationDate, skip, take);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse<IEnumerable<ToDoDTO>>.Failure(result.Error.Message));
            }

            return Ok(ApiResponse<IEnumerable<ToDoDTO>>.Success(result.Data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            Guid idGuid;
            if (!Guid.TryParse(id, out idGuid))
            {
                return BadRequest(ApiResponse<ToDoDTO>.Failure("Invalid Id. Id is required"));
            }

            var result = await _service.GetByIdAsync(idGuid);

            if (!result.IsSuccess)
            {
                return NotFound(ApiResponse<ToDoDTO>.Failure(result.Error.Message, "404"));
            }

            return Ok(ApiResponse<ToDoDTO>.Success(result.Data));
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] CreateToDoDTO createToDoModel)
        {
            if (createToDoModel == null)
            {
                return BadRequest(ApiResponse<ToDoDTO>.Failure("Invalid data"));
            }

            createToDoModel.UserId = GetUserId();
            var result = await _service.CreateAsync(createToDoModel);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse<ToDoDTO>.Failure(result.Error.Message));
            }

            return Ok(ApiResponse<ToDoDTO>.Success(result.Data));
        }

        [HttpPut("update")]
        public async Task<ActionResult> Update([FromBody] UpdateToDoDTO updateTodoModel)
        {
            if (updateTodoModel == null)
            {
                return BadRequest(ApiResponse<ToDoDTO>.Failure("Invalid data"));
            }

            updateTodoModel.UserId = GetUserId();
            var result = await _service.UpdateAsync(updateTodoModel);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse<ToDoDTO>.Failure(result.Error.Message));
            }

            return Ok(ApiResponse<ToDoDTO>.Success(result.Data));
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            Guid idGuid;
            if (!Guid.TryParse(id, out idGuid))
            {
                return BadRequest(ApiResponse<bool>.Failure("Invalid Id. Id is required"));
            }

            var result = await _service.DeleteAsync(idGuid);

            if (!result.IsSuccess)
            {
                return NotFound(ApiResponse<bool>.Failure(result.Error.Message, "404"));
            }

            return Ok(ApiResponse<bool>.Success(result.Data));
        }

        private Guid GetUserId()
        {
            Guid userId;
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out userId))
            {
                throw new UnauthorizedAccessException("Not authorized user");
            }

            return userId;
        }
    }
}
