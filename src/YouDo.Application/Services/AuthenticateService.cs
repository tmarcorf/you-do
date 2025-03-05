using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using YouDo.Application.DTOs.Authenticate;
using YouDo.Application.DTOs.ToDo;
using YouDo.Application.Extensions;
using YouDo.Application.Interfaces;
using YouDo.Application.Results;
using YouDo.Core.Entities;
using YouDo.Core.Enums;
using YouDo.Core.Validations.Authenticate;
using YouDo.Core.Validations.ToDo;
using YouDo.Core.Validations.User;

namespace YouDo.Application.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthenticateService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<Result<UserTokenDTO>> Authenticate(string email, string password)
        {
            var authenticationResult = await _signInManager.PasswordSignInAsync(email, password, false, false);

            if (!authenticationResult.Succeeded)
            {
                return Result<UserTokenDTO>.Failure(AuthenticateErrors.InvalidEmailOrPassword);
            }

            return GenerateToken(email);
        }

        public async Task<Result<IdentityResult>> RegisterUser(CreateUserDTO createUserDTO, string password)
        {
            var result = Validate(createUserDTO);

            if (!result.IsSuccess) return result;

            var user = createUserDTO.ToEntity();
            user.UserName = createUserDTO.Email;

            var createResult = await _userManager.CreateAsync(user, password);

            if (!createResult.Succeeded)
            {
                var errorMessages = string.Join(", ", createResult.Errors.Select(x => x.Description));

                return Result<IdentityResult>.Failure(AuthenticateErrors.CustomError(errorMessages));
            }

            return Result<IdentityResult>.Success(createResult);
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        private Result<UserTokenDTO> GenerateToken(string email)
        {
            var claims = new[]
            {
                new Claim("email", email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            
            var privateKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    Environment.GetEnvironmentVariable(
                        _configuration["Jwt:SecretKey"])));

            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.Now.AddHours(2);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
                );

            var userTokenDto = new UserTokenDTO()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };

            return Result<UserTokenDTO>.Success(userTokenDto);
        }

        private Result<IdentityResult> Validate(CreateUserDTO createUserDTO)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (!Regex.IsMatch(createUserDTO.Email, emailPattern))
            {
                return Result<IdentityResult>.Failure(UserErrors.InvalidEmail);
            }

            if (string.IsNullOrWhiteSpace(createUserDTO.FirstName))
            {
                return Result<IdentityResult>.Failure(UserErrors.InvalidFirstName);
            }

            if (string.IsNullOrWhiteSpace(createUserDTO.LastName))
            {
                return Result<IdentityResult>.Failure(UserErrors.InvalidLastName);
            }

            if (createUserDTO.DateOfBirth <= DateTime.MinValue)
            {
                return Result<IdentityResult>.Failure(UserErrors.InvalidDateOfBirth);
            }

            if (createUserDTO.DateOfBirth > DateTime.UtcNow)
            {
                return Result<IdentityResult>.Failure(UserErrors.DateOfBirthGreaterThanCurrentDate);
            }

            if (!Enum.IsDefined(typeof(EnumGender), createUserDTO.Gender))
            {
                return Result<IdentityResult>.Failure(UserErrors.InvalidGender);
            }

            return Result<IdentityResult>.Success(null);
        }
    }
}
