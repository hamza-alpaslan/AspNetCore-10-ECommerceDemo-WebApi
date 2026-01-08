using Application.Common.Results;
using Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<DataResult<UserGetDto>> GetByIdAsync(Guid userId);
        //Task<List<UserDto>> GetAllUsersAsync();
        Task<DataResult<UserLoginResponseDto>> LoginAsync(UserLoginDto dto);
        public Task<DataResult<UserRegisterResponseDto>> UserRegisterAsync(UserRegisterDto userRegisterDto);
        Task<DataResult<UserUpdateResponseDto>> UpdateAsync(UserUpdateDto dto);
        Task<Result> DeleteAsync(Guid userId);
    }
}
