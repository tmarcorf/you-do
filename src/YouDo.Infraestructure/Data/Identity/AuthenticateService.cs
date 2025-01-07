using Microsoft.AspNetCore.Identity;
using YouDo.Core.Account;
using YouDo.Core.Entities;

namespace YouDo.Infraestructure.Data.Identity
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthenticateService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> Authenticate(string email, string password)
        {
            var authenticationResult = await _signInManager.PasswordSignInAsync(email, password, false, false);

            return authenticationResult.Succeeded;
        }

        public async Task<IdentityResult> RegisterUser(User user, string password)
        {
            var createResult = await _userManager.CreateAsync(user, password);

            return createResult;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
