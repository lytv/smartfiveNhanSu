using Application.Constants;
using Application.Features.Users.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User name is required");
            RuleFor(x => x.Birthdate)
                .GreaterThan(DateOnly.FromDateTime(new DateTime(1900, 1, 1)))
                .WithMessage("Your Birthday have to be greater than 01/01/1900");
            RuleFor(x => x.Birthdate)
            .LessThan(DateOnly.FromDateTime(DateTime.Now))
            .WithMessage("Your Birthday have to be less than " + DateTime.Now);
            RuleFor(x => x.PhoneNumber)
                .Matches(ValidatorConsts.LeadingPhoneNumberRegex)
                .WithMessage("Your phone number must start with 090");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required");
            RuleFor(x => x.EmployeeCode).NotEmpty().WithMessage("Employee code is required");
            RuleFor(x => x.DepartmentCode).MaximumLength(ValidatorConsts.MaximumCodeLength).WithMessage($"Department code length must be under {ValidatorConsts.MaximumCodeLength}");
            RuleFor(x => x.EmployeeTypeCode).MaximumLength(ValidatorConsts.MaximumCodeLength).WithMessage($"Employee type code length must be under {ValidatorConsts.MaximumCodeLength}");
        }
    }
}
