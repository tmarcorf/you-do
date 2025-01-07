using Microsoft.AspNetCore.Identity;
using YouDo.Core.Entities;

namespace YouDo.Core.Account
{
    public interface IAuthenticateService
    {
        Task<bool> Authenticate(string email, string password);

        Task<IdentityResult> RegisterUser(User user, string password);

        Task Logout();
    }
}
