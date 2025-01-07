using YouDo.Core.Enums;

namespace YouDo.API.Models.Authenticate
{
    public class CreateUserModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public EnumGender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
