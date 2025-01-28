using Microsoft.AspNetCore.Identity;
using YouDo.Application.DTOs;
using YouDo.Application.Results;
using YouDo.Core.Entities;

namespace YouDo.Application.Interfaces
{
    public interface IAuthenticateService
    {
        Task<Result<UserTokenDTO>> Authenticate(string email, string password);

        Task<IdentityResult> RegisterUser(User user, string password);

        Task Logout();
    }
}
