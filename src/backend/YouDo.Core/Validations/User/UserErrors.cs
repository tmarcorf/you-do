using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouDo.Core.Validations.User
{
    public static class UserErrors
    {
        public static readonly Error InvalidEmail = new Error(
            "UserErrors.InvalidEmail", "Email is invalid.");

        public static readonly Error InvalidFirstName = new Error(
            "UserErrors.InvalidFirstName", "FirstName is required.");

        public static readonly Error InvalidLastName = new Error(
            "UserErrors.InvalidLastName", "LastName is required.");

        public static readonly Error InvalidDateOfBirth = new Error(
            "UserErrors.InvalidDateOfBirth", $"DateOfBirth must be greater than {DateTime.MinValue.ToString("dd/MM/yyyy")}.");

        public static readonly Error DateOfBirthGreaterThanCurrentDate = new Error(
            "UserErrors.DateOfBirthGreaterThanCurrentDate", "DateOfBirth cannot be greater than the current date.");

        public static readonly Error InvalidGender = new Error(
            "UserErrors.InvalidGender", "Gender must be one of the values (0, 1, 2).");
    }
}
