using Microsoft.AspNetCore.Identity;
using YouDo.Core.Account;

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

        public async Task<bool> RegisterUser(string email, string password)
        {
            var applicationUser = new User
            {
                UserName = email,
                Email = email,
            };

            var createResult = await _userManager.CreateAsync(applicationUser, password);

            return createResult.Succeeded;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
