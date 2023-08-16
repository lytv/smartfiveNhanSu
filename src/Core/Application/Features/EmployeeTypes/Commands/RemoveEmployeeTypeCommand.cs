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

namespace Application.Features.EmployeeTypes.Commands
{
    public class RemoveEmployeeTypeCommand : IRequest<IResponse>
    {
        public Guid Id;

        public RemoveEmployeeTypeCommand(Guid id)
        {
            Id = id;
        }

        public class RemoveEmployeeTypeCommandHandler : IRequestHandler<RemoveEmployeeTypeCommand, IResponse>
        {
            private readonly IEmployeeTypeRepository _employeeRepository;
            private readonly IUnitOfWork _unitOfWork;

            public RemoveEmployeeTypeCommandHandler(IEmployeeTypeRepository employeeRepository, IUnitOfWork unitOfWork)
            {
                _employeeRepository = employeeRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<IResponse> Handle(RemoveEmployeeTypeCommand request, CancellationToken cancellationToken)
            {
                var existEmployeeType = await _employeeRepository.GetByIdAsync(request.Id)
                    ?? throw new ApiException(404, Messages.EmployeeTypeNotFound);
                _employeeRepository.Remove(existEmployeeType);
                await _unitOfWork.SaveChangesAsync();

                return new SuccessResponse(200, Messages.DeletedSuccessfully);
            }
        }
    }
}
