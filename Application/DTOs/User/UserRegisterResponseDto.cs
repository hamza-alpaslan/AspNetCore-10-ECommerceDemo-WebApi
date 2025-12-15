using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.User
{
    public record UserRegisterResponseDto(string AccessToken,UserGetDto UserGetDto);
}
