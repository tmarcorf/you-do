using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;
using YouDo.Core.Enums;
using YouDo.Core.Validations.User;

namespace YouDo.Core.Entities
{
    public class User : IdentityUser<Guid>
    {
        public User(string email, string firstName, string lastName, DateTime dateOfBirth, EnumGender gender)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;

            Validate();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public EnumGender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public IEnumerable<ToDo> ToDos { get; set; }

        private void Validate()
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (!Regex.IsMatch(Email, emailPattern))
                throw new ArgumentException(UserErrors.InvalidEmail.Message);

            if (string.IsNullOrWhiteSpace(FirstName))
                throw new ArgumentException(UserErrors.InvalidFirstName.Message);

            if (string.IsNullOrWhiteSpace(LastName))
                throw new ArgumentException(UserErrors.InvalidLastName.Message);

            if (DateOfBirth <= DateTime.MinValue)
                throw new ArgumentException(UserErrors.InvalidDateOfBirth.Message);

            if (DateOfBirth > DateTime.UtcNow)
                throw new ArgumentException(UserErrors.DateOfBirthGreaterThanCurrentDate.Message);

            if (!Enum.IsDefined(typeof(EnumGender), Gender))
                throw new ArgumentException(UserErrors.InvalidGender.Message);
        }
    }
}
