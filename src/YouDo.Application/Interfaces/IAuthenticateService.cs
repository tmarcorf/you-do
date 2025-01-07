using Microsoft.AspNetCore.Identity;
using YouDo.Application.DTOs;
using YouDo.Core.Entities;

namespace YouDo.Application.Interfaces
{
    public interface IAuthenticateService
    {
        Task<UserTokenDTO> Authenticate(string email, string password);

        Task<IdentityResult> RegisterUser(User user, string password);

        Task Logout();
    }
}
