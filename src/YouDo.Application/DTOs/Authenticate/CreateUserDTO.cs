﻿using YouDo.Core.Enums;

namespace YouDo.Application.DTOs.Authenticate
{
    public class CreateUserDTO
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public EnumGender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
