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
        public UserService(ITokenService tokenService,
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            IValidator<UserRegisterDto> userRegisterDtoValidator)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _userManager = userManager;
            _userRegisterDtoValidator = userRegisterDtoValidator;
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
                var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                return DataResult<UserRegisterResponseDto>.Fail(errors);
            }

            var AccessToken = await _tokenService.GenerateToken(newUser);
            var userRegisterResponseDto=new UserRegisterResponseDto(AccessToken);
            return DataResult<UserRegisterResponseDto>.Ok(userRegisterResponseDto);
        }
        public async Task<UserLoginResponseDto> LoginAsync(UserLoginDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<UserGetDto?> GetByIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }


        public async Task<UserUpdateResponseDto> UpdateAsync(UserUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
