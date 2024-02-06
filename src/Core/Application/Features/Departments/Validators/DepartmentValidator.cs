using Application.Constants;
using Application.Features.Departments.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Departments.Validators
{
    public class CreateDepartmentValidator : AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentValidator() 
        {
            RuleFor(x => x.DepartmentCode).NotEmpty().WithMessage("Department code is required");
            RuleFor(x => x.DepartmentCode).MaximumLength(ValidatorConsts.MaximumCodeLength).WithMessage($"Department code length must be under {ValidatorConsts.MaximumCodeLength}.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        }
    }

    public class UpdateDepartmentValidator : AbstractValidator<UpdateDepartmentCommand>
    {
        public UpdateDepartmentValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Department id is required");
            RuleFor(x => x.DepartmentCode).NotEmpty().WithMessage("Department code is required");
            RuleFor(x => x.DepartmentCode).MaximumLength(ValidatorConsts.MaximumCodeLength).WithMessage($"Department code length must be under {ValidatorConsts.MaximumCodeLength}.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        }
    }
}
