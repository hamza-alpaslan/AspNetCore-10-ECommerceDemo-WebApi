using Application.DTOs.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validators
{
    public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
    {
        public UserUpdateDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("User Id is required.");

            When(x => x.Name != null, () =>
            {
                RuleFor(x => x.Name!)
                    .NotEmpty()
                    .MinimumLength(2);
            });

            When(x => x.LastName != null, () =>
            {
                RuleFor(x => x.LastName!)
                    .NotEmpty()
                    .MinimumLength(2);
            });

            When(x => x.Email != null, () =>
            {
                RuleFor(x => x.Email!)
                    .EmailAddress().WithMessage("Email format is invalid.");
            });

            When(x => x.UserName != null, () =>
            {
                RuleFor(x => x.UserName!)
                    .MinimumLength(3);
            });

            When(x => x.Password != null, () =>
            {
                RuleFor(x => x.Password!)
                    .MinimumLength(6)
                    .WithMessage("Password must be at least 6 characters.");
            });
        }
    }
}
