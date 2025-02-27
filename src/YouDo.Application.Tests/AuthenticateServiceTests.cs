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

namespace YouDo.Application.Tests
{
    public class AuthenticateServiceTests
    {
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly Mock<SignInManager<User>> _mockSignInManager;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly AuthenticateService _authenticateService;

        public AuthenticateServiceTests()
        {
            // Mock do UserManager
            var userStoreMock = new Mock<IUserStore<User>>();
            _mockUserManager = new Mock<UserManager<User>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

            // Mock das dependências do SignInManager
            var contextAccessorMock = new Mock<IHttpContextAccessor>();
            var userPrincipalFactoryMock = new Mock<IUserClaimsPrincipalFactory<User>>();
            var identityOptionsMock = new Mock<IOptions<IdentityOptions>>();
            var loggerMock = new Mock<ILogger<SignInManager<User>>>();
            var schemeProviderMock = new Mock<IAuthenticationSchemeProvider>();
            var userConfirmationMock = new Mock<IUserConfirmation<User>>();

            // Criando uma instância real do SignInManager com os mocks
            _mockSignInManager = new Mock<SignInManager<User>>(
                _mockUserManager.Object
            );

            // Mock do IConfiguration
            _mockConfiguration = new Mock<IConfiguration>();

            // Instância do serviço
            _authenticateService = new AuthenticateService(
                _mockUserManager.Object,
                _mockSignInManager.Object,
                _mockConfiguration.Object
            );
        }

        [Fact]
        public async Task Authenticate_InvalidCredentials_ReturnsFailure()
        {
            // Arrange
            var email = "test@example.com";
            var password = "wrongpassword";

            _mockSignInManager
                .Setup(sm => sm.PasswordSignInAsync(email, password, false, false))
                .ReturnsAsync(SignInResult.Failed);

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

            _mockSignInManager
                .Setup(sm => sm.PasswordSignInAsync(email, password, false, false))
                .ReturnsAsync(SignInResult.Success);

            _mockConfiguration
                .Setup(c => c["Jwt:SecretKey"])
                .Returns("SuperSecretKey1234567890");
            _mockConfiguration
                .Setup(c => c["Jwt:Issuer"])
                .Returns("TestIssuer");
            _mockConfiguration
                .Setup(c => c["Jwt:Audience"])
                .Returns("TestAudience");

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
            // Arrange
            _mockSignInManager
                .Setup(sm => sm.SignOutAsync())
                .Returns(Task.CompletedTask);

            // Act
            await _authenticateService.Logout();

            // Assert
            _mockSignInManager.Verify(sm => sm.SignOutAsync(), Times.Once);
        }

        //[Fact]
        //public void GenerateToken_ValidEmail_ReturnsToken()
        //{
        //    // Arrange
        //    var email = "test@example.com";

        //    _mockConfiguration
        //        .Setup(c => c["Jwt:SecretKey"])
        //        .Returns("SuperSecretKey1234567890");
        //    _mockConfiguration
        //        .Setup(c => c["Jwt:Issuer"])
        //        .Returns("TestIssuer");
        //    _mockConfiguration
        //        .Setup(c => c["Jwt:Audience"])
        //        .Returns("TestAudience");

        //    // Act
        //    var result = _authenticateService.GenerateToken(email);

        //    // Assert
        //    Assert.True(result.IsSuccess);
        //    Assert.NotNull(result.Data.Token);
        //    Assert.True(result.Data.Expiration > DateTime.Now);
        //}
    }
}
