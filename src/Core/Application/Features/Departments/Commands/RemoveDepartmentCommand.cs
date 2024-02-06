using Application.Constants;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers.Abstract;
using Application.Wrappers.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Departments.Commands
{
    public class RemoveDepartmentCommand : IRequest<IResponse>
    {
        public Guid Id;

        public RemoveDepartmentCommand(Guid id)
        {
            Id = id;
        }

        public class RemoveDepartmentCommandHandler : IRequestHandler<RemoveDepartmentCommand, IResponse>
        {
            private readonly IDepartmentRepository _departmentRepository;
            private readonly IUnitOfWork _unitOfWork;

            public RemoveDepartmentCommandHandler(IDepartmentRepository departmentRepository, IUnitOfWork unitOfWork)
            {
                _departmentRepository = departmentRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<IResponse> Handle(RemoveDepartmentCommand request, CancellationToken cancellationToken)
            {
                var existDepartment = await _departmentRepository.GetByIdAsync(request.Id)
                    ?? throw new ApiException(404, Messages.DepartmentNotFound);

                _departmentRepository.Remove(existDepartment);
                await _unitOfWork.SaveChangesAsync();

                return new SuccessResponse(200, Messages.DeletedSuccessfully);
            }
        }
    }
}
