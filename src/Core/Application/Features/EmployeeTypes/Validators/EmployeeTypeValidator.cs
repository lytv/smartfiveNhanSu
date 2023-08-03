using Application.Features.EmployeeTypes.Commands;
using FluentValidation;

namespace Application.Features.Departments.Validators
{
    public class CreateEmployeeTypeValidator : AbstractValidator<CreateEmployeeTypeCommand>
    {
        public CreateEmployeeTypeValidator() 
        {
            RuleFor(x => x.EmployeeTypeCode).MaximumLength(10).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }

    public class UpdateEmployeeTypeValidator : AbstractValidator<UpdateEmployeeTypeCommand>
    {
        public UpdateEmployeeTypeValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.EmployeeTypeCode).MaximumLength(10).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
