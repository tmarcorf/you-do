using Microsoft.AspNetCore.Mvc;
using YouDo.API.Extensions;
using YouDo.Application.Interfaces;
using YouDo.Application.DTOs.Authenticate;
using YouDo.API.Responses;
using Microsoft.AspNetCore.Identity;

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
    }
}
