using Application.Common.Results;
using Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserGetDto?> GetByIdAsync(Guid userId);
        //Task<List<UserDto>> GetAllUsersAsync();
        Task<UserLoginResponseDto> LoginAsync(UserLoginDto dto);
        public Task<DataResult<UserRegisterResponseDto>> UserRegisterAsync(UserRegisterDto userRegisterDto);
        Task<UserUpdateResponseDto> UpdateAsync(UserUpdateDto dto);
        Task<bool> DeleteAsync(string userId);
    }
}
