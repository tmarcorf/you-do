using Microsoft.AspNetCore.Identity;
using YouDo.Application.DTOs.Authenticate;
using YouDo.Application.Results;
using YouDo.Core.Entities;

namespace YouDo.Application.Interfaces
{
    public interface IAuthenticateService
    {
        Task<Result<UserTokenDTO>> Authenticate(string email, string password);

        Task<Result<IdentityResult>> RegisterUser(CreateUserDTO user, string password);

        Task Logout();
    }
}
