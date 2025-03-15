using Microsoft.AspNetCore.Mvc;
using YouDo.API.Extensions;
using YouDo.Application.Interfaces;
using YouDo.Application.DTOs.Authenticate;

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
                return BadRequest("Invalid data");
            }

            var createResult = await _authenticateService.RegisterUser(createUserDTO, createUserDTO.Password);

            if (!createResult.IsSuccess)
            {
                return BadRequest(createResult);
            }

            return Ok($"User {createUserDTO.Email} was successfully created");
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserDTO loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest("Invalid data");
            }

            var result = await _authenticateService.Authenticate(loginModel.Email, loginModel.Password);

            if (!result.IsSuccess)
            {
                return NotFound(result.Error.Message);
            }

            return Ok(result.Data);
        }
    }
}
