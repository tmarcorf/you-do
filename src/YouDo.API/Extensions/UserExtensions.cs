using YouDo.API.Models.Authenticate;
using YouDo.Core.Entities;

namespace YouDo.API.Extensions
{
    public static class UserExtensions
    {
        public static User ToEntity(this CreateUserModel createUserModel)
        {
            return new User
            {
                Email = createUserModel.Email,
                UserName = createUserModel.Email,
                FirstName = createUserModel.FirstName,
                LastName = createUserModel.LastName,
                Gender = createUserModel.Gender,
                DateOfBirth = createUserModel.DateOfBirth
            };
        }
    }
}
