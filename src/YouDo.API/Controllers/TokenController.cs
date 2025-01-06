using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YouDo.API.Models;
using YouDo.Core.Account;

namespace YouDo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly IConfiguration _configuration;

        public TokenController(IAuthenticateService authenticateService, IConfiguration configuration)
        {
            _authenticateService = authenticateService;
            _configuration = configuration;
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserModel createUserModel)
        {
            if (createUserModel == null) return BadRequest("Invalid data");

            var successfullyCreated = await _authenticateService.RegisterUser(createUserModel.Email, createUserModel.Password);

            if (!successfullyCreated)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt");

                return BadRequest(ModelState);
            }

            return Ok($"User {createUserModel.Email} was successfully created");
        }
    }
}
