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

namespace Application.Features.Departments.Commands
{
    public class CreateDepartmentCommand : IRequest<IResponse>
    {
        public string DepartmentCode { get; set; }
        public string Description { get; set; }
        public int TenantId { get; set; } = 1;

        public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, IResponse>
        {
            private readonly IDepartmentRepository _departmentRepository;
            private readonly IUnitOfWork _unitOfWork;

            public CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository, IUnitOfWork unitOfWork)
            {
                _departmentRepository = departmentRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<IResponse> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
            {
                var existDepartment = await _departmentRepository
                    .GetAsync(d => d.DepartmentCode == request.DepartmentCode);

                if (existDepartment != null) 
                {
                    throw new ApiException(400, Messages.DepartmentCodeAlreadyExist);
                }

                var newDepartment = await _departmentRepository
                    .AddAsync(new Department()
                    {
                        DepartmentCode = request.DepartmentCode,
                        Description = request.Description,
                        TenantId = request.TenantId
                    });
                await _unitOfWork.SaveChangesAsync();
                return new SuccessResponse(200, Messages.RegisterSuccessfully);
            }
        }
    }
}
