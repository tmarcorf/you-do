using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouDo.Application.DTOs.Authenticate;
using YouDo.Core.Entities;

namespace YouDo.Application.Extensions
{
    public static class UserExtensions
    {
        public static User ToEntity(this CreateUserDTO createUserDTO)
        {
            return new User(
                createUserDTO.Email,
                createUserDTO.FirstName,
                createUserDTO.LastName,
                createUserDTO.DateOfBirth,
                createUserDTO.Gender);
        }

        public static UserInfoDTO ToUserInfoDTO(this User user)
        {
            return new UserInfoDTO
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
    }
}
