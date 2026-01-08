using Application.Common.Results;
using Application.DTOs.User;
using Application.Services.Interfaces;
using Application.Validators;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using Web.Security;
namespace Application.Services.Implementations
{
    public class UserService : IUserService
    {

        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IValidator<UserRegisterDto> _userRegisterDtoValidator;
        private readonly IValidator<UserLoginDto> _userLoginDtoValidator;
        private readonly IValidator<UserUpdateDto> _userUpdateDtoValidator;
        public UserService(ITokenService tokenService,
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            IValidator<UserRegisterDto> userRegisterDtoValidator,
            IValidator<UserLoginDto> userLoginDtoValidator,
            IValidator<UserUpdateDto> userUpdateDtoValidator)

        {
            _tokenService = tokenService;
            _mapper = mapper;
            _userManager = userManager;
            _userRegisterDtoValidator = userRegisterDtoValidator;
            _userLoginDtoValidator = userLoginDtoValidator;
            _userUpdateDtoValidator = userUpdateDtoValidator;
        }

        public async Task<DataResult<UserRegisterResponseDto>> UserRegisterAsync(UserRegisterDto userRegisterDto)
        {
            //Validation
            var validationResult= await _userRegisterDtoValidator.ValidateAsync(userRegisterDto);

            if (!validationResult.IsValid)
            {
                var allErrors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                return DataResult<UserRegisterResponseDto>.Fail(allErrors);
            }
            //business rules check
            var emailExists = await _userManager.FindByEmailAsync(userRegisterDto.Email);
            if (emailExists != null)
                return DataResult<UserRegisterResponseDto>.Fail("Email is already taken.");

            var trimedUserName = userRegisterDto.UserName.Trim();
            var userNameExists = await _userManager.FindByNameAsync(trimedUserName);
            if (userNameExists != null)
                return DataResult<UserRegisterResponseDto>.Fail("UserName is already taken.");

            //Create User
            var newUser=_mapper.Map<ApplicationUser>(userRegisterDto);
            newUser.UserName = trimedUserName;
            var result=await _userManager.CreateAsync(newUser,userRegisterDto.Password);
            if (!result.Succeeded)
            {
                return DataResult<UserRegisterResponseDto>.Fail("Failed to create user");
            }
            //Add User role default "User"
            var roleResult = await _userManager.AddToRoleAsync(newUser, "User");
            if (!roleResult.Succeeded)
            {
                await _userManager.DeleteAsync(newUser);
                return DataResult<UserRegisterResponseDto>.Fail("An error occurred while creating the user role. The user could not be created.");
            }
            var userGetDto = _mapper.Map<UserGetDto>(newUser);
            var AccessToken = await _tokenService.GenerateToken(newUser);
            var userRegisterResponseDto=new UserRegisterResponseDto(AccessToken,userGetDto);
            return DataResult<UserRegisterResponseDto>.Ok(userRegisterResponseDto);
        }
        public async Task<DataResult<UserLoginResponseDto>> LoginAsync(UserLoginDto UserLoginDto)
        {
            //validation
            var validationResult= await _userLoginDtoValidator.ValidateAsync(UserLoginDto);
            if (validationResult.IsValid)
            {
                var allErrors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                return DataResult<UserLoginResponseDto>.Fail(allErrors);
            }
            //User Exist and password check
            var userExist= await _userManager.FindByNameAsync(UserLoginDto.UserName);
            if (userExist == null ||
                !await _userManager.CheckPasswordAsync(userExist,UserLoginDto.Password))
                return DataResult<UserLoginResponseDto>.Fail("Invalid username or password");
            //Generate the Tokens
            var accessToken = await _tokenService.GenerateToken(userExist);
            var refreshToken = await _tokenService.GenerateRefreshToken();
            using var sha256 = SHA256.Create();
            var refreshTokenHash=sha256.ComputeHash(Encoding.UTF8.GetBytes(refreshToken));
            userExist.RefreshToken = Convert.ToBase64String(refreshTokenHash);
            userExist.RefreshTokenExpiryTime = DateTime.Now.AddDays(_tokenService.JwtSettings.RefreshTokenExpiresDay);

            var result= await _userManager.UpdateAsync(userExist);
            if(!result.Succeeded)
                return DataResult<UserLoginResponseDto>.Fail("Failed to update user");
            var userGetDto = _mapper.Map<UserGetDto>(userExist);
            var userLoginResponseDto= new UserLoginResponseDto(accessToken, refreshToken,userGetDto);
            return DataResult<UserLoginResponseDto>.Ok(userLoginResponseDto);
        }

        public async Task<Result> DeleteAsync(Guid userId)
        {
            var user= await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return Result.Fail("User not found");
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                return Result.Fail("Failed to delete user");
            // ? how to delete user access token?????
            return Result.Ok();
        }

        public async Task<DataResult<UserGetDto>> GetByIdAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return DataResult<UserGetDto>.Fail("User Not found");
            var userGetDto=_mapper.Map<UserGetDto>(user);
            return DataResult<UserGetDto>.Ok(userGetDto);
        }

        public async Task<DataResult<UserUpdateResponseDto>> UpdateAsync(UserUpdateDto userUpdateDto)
        {
            //validation
            var validationResult = await _userUpdateDtoValidator.ValidateAsync(userUpdateDto);
            if (!validationResult.IsValid)
            {
                var allErrors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                return DataResult<UserUpdateResponseDto>.Fail(allErrors);
            }
            //business rules check
            if (userUpdateDto.Email != null && !userUpdateDto.Email.IsWhiteSpace())
            {
                var emailExists = await _userManager.FindByEmailAsync(userUpdateDto.Email);
                if (emailExists != null)
                    return DataResult<UserUpdateResponseDto>.Fail("Email is already taken.");
            }
            if (userUpdateDto.UserName != null && userUpdateDto.UserName.IsWhiteSpace())
            {
                var trimedUserName = userUpdateDto.UserName.Trim();
                var userNameExists = await _userManager.FindByNameAsync(trimedUserName);
                if (userNameExists != null)
                    return DataResult<UserUpdateResponseDto>.Fail("UserName is already taken.");
            }
            //User update
            var user=await _userManager.FindByIdAsync(userUpdateDto.Id.ToString());
            if (user == null)
                return DataResult<UserUpdateResponseDto>.Fail("User not found");
            if (userUpdateDto.Name != null)
                user.Name = userUpdateDto.Name;

            if (userUpdateDto.LastName != null)
                user.LastName = userUpdateDto.LastName;

            if (userUpdateDto.Email != null)
                user.Email = userUpdateDto.Email;

            if (userUpdateDto.UserName != null)
                user.UserName = userUpdateDto.UserName;

            // Password Update
            if (userUpdateDto.Password != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResult = await _userManager.ResetPasswordAsync(user, token, userUpdateDto.Password);

                if (!passwordResult.Succeeded)
                {
                    return DataResult<UserUpdateResponseDto>.Fail("Password update fail");
                }
            }

            // Normal update
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return DataResult<UserUpdateResponseDto>.Fail("Fail to update");

            var userGetDto=_mapper.Map<UserGetDto>(user);
            var updateResponseDto = new UserUpdateResponseDto(userGetDto);
            return DataResult<UserUpdateResponseDto>.Ok(updateResponseDto);

        }
        //check the resresh token and generate new access token
        public async Task<DataResult<string>> RefreshTokenCheckAync(string refreshToken)
        {
            using var sha256= SHA256.Create();
            var refreshTokenHash=sha256.ComputeHash(Encoding.UTF8.GetBytes(refreshToken));
            var hashedRefreshToken = Convert.ToBase64String(refreshTokenHash);

            var user= await _userManager.Users.FirstOrDefaultAsync(u=>u.RefreshToken==hashedRefreshToken);
            if (user == null) return DataResult<string>.Fail("Invalid refresh token");
            if (user.RefreshTokenExpiryTime < DateTime.Now)
                return DataResult<string>.Fail("Refresh token expired");

            var accessToken = await _tokenService.GenerateToken(user);
            return DataResult<string>.Ok(accessToken);
        }
        public async Task<Result> RefreshTokenRevoke(string refreshToken)
        {
            using var sha256 = SHA256.Create();
            var refreshTokenHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(refreshToken));
            var hashedRefreshToken = Convert.ToBase64String(refreshTokenHash);

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == hashedRefreshToken);
            if (user == null) return Result.Fail("Invalid refresh token");
            if (user.RefreshTokenExpiryTime < DateTime.Now)
                return DataResult<string>.Fail("Refresh token expired");

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;
            var result= await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return Result.Fail("Fail to update user");
            return Result.Ok();
        }
    }
}
