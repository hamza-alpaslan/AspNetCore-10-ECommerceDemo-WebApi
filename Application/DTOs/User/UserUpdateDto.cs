using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.User
{
    public record UserUpdateDto(string Id,string Name, string LastName, string? Email, string UserName,string Password);

}
