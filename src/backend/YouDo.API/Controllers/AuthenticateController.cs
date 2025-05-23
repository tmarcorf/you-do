using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YouDo.API.Responses;
using YouDo.Application.DTOs.Authenticate;
using YouDo.Application.Interfaces;

namespace YouDo.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;

        public AuthenticateController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserDTO createUserDTO)
        {
            if (createUserDTO == null)
            {
                return BadRequest(ApiResponse<string>.Failure("Invalid data"));
            }

            var createResult = await _authenticateService.RegisterUser(createUserDTO, createUserDTO.Password);

            if (!createResult.IsSuccess)
            {
                return BadRequest(ApiResponse<string>.Failure(createResult.Error.Message));
            }

            return Ok(ApiResponse<string>.Success($"User {createUserDTO.Email} was successfully created"));
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserDTO loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest(ApiResponse<UserTokenDTO>.Failure("Invalid data"));
            }

            var result = await _authenticateService.Authenticate(loginModel.Email, loginModel.Password);

            if (!result.IsSuccess)
            {
                return NotFound(ApiResponse<UserTokenDTO>.Failure(result.Error.Message, "404"));
            }

            return Ok(ApiResponse<UserTokenDTO>.Success(result.Data));
        }

        [Authorize]
        [HttpGet("user-info")]
        public async Task<ActionResult> GetUserInfo()
        {
            var userIdGuid = GetUserId();

            var result = await _authenticateService.GetUserInfo(userIdGuid);

            if (!result.IsSuccess)
            {
                return NotFound(ApiResponse<UserInfoDTO>.Failure(result.Error.Message, "404"));
            }

            return Ok(ApiResponse<UserInfoDTO>.Success(result.Data));
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
