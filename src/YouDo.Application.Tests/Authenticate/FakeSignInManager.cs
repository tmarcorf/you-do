using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouDo.Core.Entities;

namespace YouDo.Application.Tests.Authenticate
{
    public class FakeSignInManager : SignInManager<User>
    {
        private SignInResult _passwordSignInResult = SignInResult.Failed;
        public bool SignOutCalled { get; private set; }

        public FakeSignInManager(UserManager<User> userManager)
            : base(userManager,
                   new Mock<IHttpContextAccessor>().Object,
                   new Mock<IUserClaimsPrincipalFactory<User>>().Object,
                   new Mock<IOptions<IdentityOptions>>().Object,
                   new Mock<ILogger<SignInManager<User>>>().Object,
                   new Mock<IAuthenticationSchemeProvider>().Object)
        { }

        public void SetPasswordSignInResult(SignInResult result)
        {
            _passwordSignInResult = result;
        }

        public override Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return Task.FromResult(_passwordSignInResult);
        }

        public override Task SignOutAsync()
        {
            SignOutCalled = true;
            return Task.CompletedTask;
        }
    }
}
