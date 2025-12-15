using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.User
{
    public record UserUpdateResponseDto(string AccessToken, string RefreshToken, UserGetDto UserGetDto);
}
