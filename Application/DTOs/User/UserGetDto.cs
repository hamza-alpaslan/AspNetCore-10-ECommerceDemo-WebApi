using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.User
{
    public record UserGetDto(string Id,string Name, string LastName, string UserName,string? Email);

}
