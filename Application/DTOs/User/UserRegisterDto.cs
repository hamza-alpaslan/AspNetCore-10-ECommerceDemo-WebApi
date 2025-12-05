using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.User
{
    public record UserRegisterDto(string Name,string LastName,string UserName,string? Email,string Password);
}
