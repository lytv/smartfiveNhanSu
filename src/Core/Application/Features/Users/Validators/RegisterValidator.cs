using Application.Constants;
using Application.Features.Users.Commands;
using FluentValidation;

namespace Application.Features.Users.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email is not valid");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required")
           .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                    .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
                    .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                    .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                    .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Confirm password is required").Equal(x => x.Password).WithMessage("Password and ConfirmPassword must match");
            RuleFor(x => x.Birthdate)
                .GreaterThan(DateOnly.FromDateTime(new DateTime(1900, 1, 1)))
                .WithMessage("Your Birthday have to be greater than 01/01/1900");
            RuleFor(x => x.Birthdate)
            .LessThan(DateOnly.FromDateTime(DateTime.Now))
            .WithMessage("Your Birthday have to be less than " + DateTime.Now.Year);
            RuleFor(x => x.PhoneNumber)
                .Matches(ValidatorConsts.LeadingPhoneNumberRegex)
                .WithMessage($"Your phone number must start with 090");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required");
            RuleFor(x => x.EmployeeCode).NotEmpty().WithMessage("Employee code is required");
            RuleFor(x => x.DepartmentCode).MaximumLength(ValidatorConsts.MaximumCodeLength).WithMessage($"Department code length must not exceed {ValidatorConsts.MaximumCodeLength}.");
            RuleFor(x => x.EmployeeTypeCode).MaximumLength(ValidatorConsts.MaximumCodeLength).WithMessage($"Employee type code length must not exceed {ValidatorConsts.MaximumCodeLength}.");
        }
    }
}