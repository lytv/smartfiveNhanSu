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
            RuleFor(x => x.DepartmentCode).MaximumLength(10).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }

    public class UpdateDepartmentValidator : AbstractValidator<UpdateDepartmentCommand>
    {
        public UpdateDepartmentValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.DepartmentCode).MaximumLength(10).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
