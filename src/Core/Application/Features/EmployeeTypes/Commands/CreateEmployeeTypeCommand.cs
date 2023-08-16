using Application.Constants;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers.Abstract;
using Application.Wrappers.Concrete;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.EmployeeTypes.Commands
{
    public class CreateEmployeeTypeCommand : IRequest<IResponse>
    {
        public string EmployeeTypeCode { get; set; }
        public string Description { get; set; }
        public int TenantId { get; set; } = 1;

        public class CreateEmployeeTypeCommandHandler : IRequestHandler<CreateEmployeeTypeCommand, IResponse>
        {
            private readonly IEmployeeTypeRepository _employeeRepository;
            private readonly IUnitOfWork _unitOfWork;

            public CreateEmployeeTypeCommandHandler(IEmployeeTypeRepository employeeRepository, IUnitOfWork unitOfWork)
            {
                _employeeRepository = employeeRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<IResponse> Handle(CreateEmployeeTypeCommand request, CancellationToken cancellationToken)
            {
                var existEmployeeType = await _employeeRepository
                    .GetAsync(d => d.EmployeeTypeCode == request.EmployeeTypeCode);
                if (existEmployeeType != null) 
                {
                    throw new ApiException(400, Messages.DepartmentCodeAlreadyExist);
                }

                var newEmployeeType = await _employeeRepository
                    .AddAsync(new EmployeeType()
                    {
                        EmployeeTypeCode = request.EmployeeTypeCode,
                        Description = request.Description,
                        TenantId = request.TenantId
                    });
                await _unitOfWork.SaveChangesAsync();
                return new SuccessResponse(200, Messages.RegisterSuccessfully);
            }
        }
    }
}
