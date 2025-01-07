using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YouDo.API.Extensions;
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

            var createResult = await _authenticateService.RegisterUser(createUserModel.ToEntity(), createUserModel.Password);

            if (!createResult.Succeeded) return BadRequest(createResult);

            return Ok($"User {createUserModel.Email} was successfully created");
        }
    }
}
