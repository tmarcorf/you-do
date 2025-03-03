using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using YouDo.Application.DTOs.Authenticate;
using YouDo.Application.Services;
using YouDo.Core.Entities;
using YouDo.Core.Enums;
using YouDo.Core.Validations.Authenticate;
using YouDo.Core.Validations.User;

namespace YouDo.Application.Tests.AuthenticateTests
{
    public class AuthenticateServiceTests
    {
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly FakeSignInManager _fakeSignInManager;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly AuthenticateService _authenticateService;

        public AuthenticateServiceTests()
        {
            var userStoreMock = new Mock<IUserStore<User>>();
            _mockUserManager = new Mock<UserManager<User>>(
                userStoreMock.Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<User>>().Object,
                new IUserValidator<User>[0],
                new IPasswordValidator<User>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<User>>>().Object
            );

            _fakeSignInManager = new FakeSignInManager(_mockUserManager.Object);
            _mockConfiguration = new Mock<IConfiguration>();

            _authenticateService = new AuthenticateService(
                _mockUserManager.Object,
                _fakeSignInManager,
                _mockConfiguration.Object
            );
        }

        [Fact]
        public async Task Authenticate_InvalidCredentials_ReturnsFailure()
        {
            // Arrange
            var email = "test@example.com";
            var password = "wrongpassword";

            _fakeSignInManager.SetPasswordSignInResult(SignInResult.Failed);

            // Act
            var result = await _authenticateService.Authenticate(email, password);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(AuthenticateErrors.InvalidEmailOrPassword, result.Error);
        }

        [Fact]
        public async Task Authenticate_ValidCredentials_ReturnsToken()
        {
            // Arrange
            var email = "test@example.com";
            var password = "correctpassword";

            _fakeSignInManager.SetPasswordSignInResult(SignInResult.Success);

            _mockConfiguration.Setup(c => c["Jwt:SecretKey"]).Returns("SECRETKEYTEST");
            _mockConfiguration.Setup(c => c["Jwt:Issuer"]).Returns("TestIssuer");
            _mockConfiguration.Setup(c => c["Jwt:Audience"]).Returns("TestAudience");

            // Act
            var result = await _authenticateService.Authenticate(email, password);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data.Token);
            Assert.True(result.Data.Expiration > DateTime.Now);
        }

        [Fact]
        public async Task RegisterUser_InvalidEmail_ReturnsFailure()
        {
            // Arrange
            var createUserDTO = new CreateUserDTO
            {
                Email = "invalid-email",
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                Gender = EnumGender.MALE
            };
            var password = "password";

            // Act
            var result = await _authenticateService.RegisterUser(createUserDTO, password);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(UserErrors.InvalidEmail, result.Error);
        }

        [Fact]
        public async Task RegisterUser_ValidInput_ReturnsSuccess()
        {
            // Arrange
            var createUserDTO = new CreateUserDTO
            {
                Email = "test@example.com",
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                Gender = EnumGender.MALE
            };
            var password = "password";

            _mockUserManager
                .Setup(um => um.CreateAsync(It.IsAny<User>(), password))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _authenticateService.RegisterUser(createUserDTO, password);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task RegisterUser_CreationFails_ReturnsFailure()
        {
            // Arrange
            var createUserDTO = new CreateUserDTO
            {
                Email = "test@example.com",
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                Gender = EnumGender.MALE
            };
            var password = "password";

            var identityErrors = new List<IdentityError>
        {
            new IdentityError { Description = "Error 1" },
            new IdentityError { Description = "Error 2" }
        };

            _mockUserManager
                .Setup(um => um.CreateAsync(It.IsAny<User>(), password))
                .ReturnsAsync(IdentityResult.Failed(identityErrors.ToArray()));

            // Act
            var result = await _authenticateService.RegisterUser(createUserDTO, password);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Error 1", result.Error.Message);
            Assert.Contains("Error 2", result.Error.Message);
        }

        [Fact]
        public async Task Logout_CallsSignOutAsync()
        {
            // Act
            await _authenticateService.Logout();

            // Assert
            Assert.True(_fakeSignInManager.SignOutCalled);
        }
    }
}
