using Microsoft.AspNetCore.Mvc;
using YouDo.API.Extensions;
using YouDo.API.Models.Authenticate;
using YouDo.Application.DTOs;
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

        [HttpPost("create-user")]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserModel createUserModel)
        {
            if (createUserModel == null) return BadRequest("Invalid data");

            var createResult = await _authenticateService.RegisterUser(createUserModel.ToEntity(), createUserModel.Password);

            if (!createResult.Succeeded) return BadRequest(createResult);

            return Ok($"User {createUserModel.Email} was successfully created");
        }

        [HttpPost("login-user")]
        public async Task<ActionResult> Login(LoginModel loginModel)
        {
            if (loginModel == null) return BadRequest("Invalid data");

            var result = await _authenticateService.Authenticate(loginModel.Email, loginModel.Password);

            if (!result.IsSuccess) return NotFound(result.Error.Description);

            return Ok(result.Data);
        }
    }
}
