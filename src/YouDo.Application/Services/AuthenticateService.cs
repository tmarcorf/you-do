using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using YouDo.Application.DTOs;
using YouDo.Application.Interfaces;
using YouDo.Core.Entities;

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

        public async Task<UserTokenDTO> Authenticate(string email, string password)
        {
            var authenticationResult = await _signInManager.PasswordSignInAsync(email, password, false, false);

            if (!authenticationResult.Succeeded) return null;

            return GenerateToken(email);
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

        private UserTokenDTO GenerateToken(string email)
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

            return new UserTokenDTO()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
