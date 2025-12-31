using GameLibrary.Entity.Entities;
using GameLibrary.Service.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Service.Mapping
{
    public static class UserMapping
    {
        public static UserDto ToDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };
        }

        public static User ToEntity(this CreateUserDto dto)
        {
            return new User
            {
                Username = dto.Username,
                Email = dto.Email
            };
        }
    }
}
