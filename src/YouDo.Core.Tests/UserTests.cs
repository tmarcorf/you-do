using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouDo.Core.Entities;
using YouDo.Core.Enums;
using YouDo.Core.Validations.User;

namespace YouDo.Core.Tests
{
    public class UserTests
    {
        [Fact]
        public void Constructor_ShouldCreateUser_WhenValidDataProvided()
        {
            var email = "test@example.com";
            var firstName = "John";
            var lastName = "Doe";
            var dateOfBirth = new DateTime(1990, 5, 15);
            var gender = EnumGender.MALE;

            var user = new User(email, firstName, lastName, dateOfBirth, gender);

            Assert.NotNull(user);
            user.Email.Should().Be(email);
            user.FirstName.Should().Be(firstName);
            user.LastName.Should().Be(lastName);
            user.DateOfBirth.Should().Be(dateOfBirth);
            user.Gender.Should().Be(gender);
        }

        [Theory]
        [InlineData("invalid-email")]
        [InlineData("user@")]
        [InlineData("user@domain")]
        [InlineData("user@.com")]
        public void Constructor_ShouldThrowException_WhenEmailIsInvalid(string invalidEmail)
        {
            var func = () => new User(invalidEmail, "John", "Doe", DateTime.UtcNow.AddYears(-25), EnumGender.MALE);

            func.Should()
                .Throw<ArgumentException>()
                .WithMessage(UserErrors.InvalidEmail.Description);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenFirstNameIsNullOrEmpty()
        {
            var func = () => new User("test@example.com", "", "Hopper", DateTime.UtcNow.AddYears(-25), EnumGender.FEMALE);

            func.Should()
                .Throw<ArgumentException>()
                .WithMessage(UserErrors.InvalidFirstName.Description);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenLastNameIsNullOrEmpty()
        {
            var func = () => new User("test@example.com", "John", "", DateTime.UtcNow.AddYears(-25), EnumGender.FEMALE);

            func.Should()
                .Throw<ArgumentException>()
                .WithMessage(UserErrors.InvalidLastName.Description);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenDateOfBirthIsMinValue()
        {
            var func = () => new User("test@example.com", "John", "Doe", DateTime.MinValue, EnumGender.MALE);

            func.Should()
                .Throw<ArgumentException>()
                .WithMessage(UserErrors.InvalidDateOfBirth.Description);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenDateOfBirthIsInTheFuture()
        {
            var func = () => new User("test@example.com", "Grace", "Hopper", DateTime.UtcNow.AddDays(1), EnumGender.FEMALE);

            func.Should()
                .Throw<ArgumentException>()
                .WithMessage(UserErrors.DateOfBirthGreaterThanCurrentDate.Description);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(3)]
        [InlineData(99)]
        public void Constructor_ShouldThrowException_WhenGenderIsInvalid(int invalidGender)
        {
            var func = () => new User("test@example.com", "John", "Doe", DateTime.UtcNow.AddYears(-25), (EnumGender)invalidGender);

            func.Should().Throw<ArgumentException>()
                .WithMessage(UserErrors.InvalidGender.Description);
        }
    }
}
