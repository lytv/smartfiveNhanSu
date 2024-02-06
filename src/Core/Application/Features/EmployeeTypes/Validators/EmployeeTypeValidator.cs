using Application.Constants;
using Application.Features.EmployeeTypes.Commands;
using FluentValidation;

namespace Application.Features.Departments.Validators
{
    public class CreateEmployeeTypeValidator : AbstractValidator<CreateEmployeeTypeCommand>
    {
        public CreateEmployeeTypeValidator() 
        {
            RuleFor(x => x.EmployeeTypeCode).NotEmpty().WithMessage("Employee type code is required");
            RuleFor(x => x.EmployeeTypeCode).MaximumLength(ValidatorConsts.MaximumCodeLength).WithMessage($"Employee type code length must be under {ValidatorConsts.MaximumCodeLength}.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        }
    }

    public class UpdateEmployeeTypeValidator : AbstractValidator<UpdateEmployeeTypeCommand>
    {
        public UpdateEmployeeTypeValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Employee type id is required");
            RuleFor(x => x.EmployeeTypeCode).NotEmpty().WithMessage("Employee type code is required");
            RuleFor(x => x.EmployeeTypeCode).MaximumLength(10).WithMessage($"Employee type code length must be under {ValidatorConsts.MaximumCodeLength}.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        }
    }
}
