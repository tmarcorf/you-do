using YouDo.API.Models.Authenticate;
using YouDo.Core.Entities;

namespace YouDo.API.Extensions
{
    public static class UserExtensions
    {
        public static User ToEntity(this CreateUserModel createUserModel)
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
