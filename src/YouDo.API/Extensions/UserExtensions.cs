using YouDo.Application.DTOs.Authenticate;
using YouDo.Core.Entities;

namespace YouDo.API.Extensions
{
    public static class UserExtensions
    {
        public static User ToEntity(this CreateUserDTO createUserModel)
        {
            return new User(
                createUserModel.Email,
                createUserModel.FirstName,
                createUserModel.LastName,
                createUserModel.DateOfBirth,
                createUserModel.Gender);
        }
    }
}
