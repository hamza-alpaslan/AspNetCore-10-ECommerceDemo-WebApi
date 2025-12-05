using Application.DTOs.User;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class SilUserService
    {
        public List<ApplicationUser>? UserList = new List<ApplicationUser>{
            new ApplicationUser(){UserName="HamzaPro",PasswordHash="12345",Name="Hmz",LastName="Alp",Role="Admin"},
            new ApplicationUser(){UserName="HamzaNoob",PasswordHash="1234",Name="HmzN",LastName="AlpN",Role="User"}
        };
        public ApplicationUser? UserExistensControl(UserLoginDto userLoginDto)
        {
            return UserList
                .FirstOrDefault(x =>
                x.UserName.ToLower() == userLoginDto.UserName.ToLower()
                && x.PasswordHash == userLoginDto.Password);

        }
    }
}
