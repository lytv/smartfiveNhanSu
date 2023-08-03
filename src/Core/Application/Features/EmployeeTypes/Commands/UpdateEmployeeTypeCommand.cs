using Application.Constants;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers.Abstract;
using Application.Wrappers.Concrete;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.EmployeeTypes.Commands
{
    public class UpdateEmployeeTypeCommand : IRequest<IResponse>
    {
        public Guid Id { get; set; }
        public string EmployeeTypeCode { get; set; }
        public string Description { get; set; }

        public class UpdateEmployeeTypeCommandHandler : IRequestHandler<UpdateEmployeeTypeCommand, IResponse>
        {
            private readonly IEmployeeTypeRepository _employeeRepository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public UpdateEmployeeTypeCommandHandler(IEmployeeTypeRepository employeeRepository, IUnitOfWork unitOfWork, IMapper mapper)
            {
                _employeeRepository = employeeRepository;
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<IResponse> Handle(UpdateEmployeeTypeCommand request, CancellationToken cancellationToken)
            {
                var existEmployeeType = await _employeeRepository.GetByIdAsync(request.Id)
                    ?? throw new ApiException(404, Messages.EmployeeTypeNotFound);

                var existCode = await _employeeRepository.GetAsync(
                    d => d.EmployeeTypeCode == request.EmployeeTypeCode);

                if (existCode != null
                    && existCode.EmployeeTypeCode != existEmployeeType.EmployeeTypeCode)
                {
                    throw new ApiException(400, Messages.EmployeeTypeCodeAlreadyExist);
                }

                if (existEmployeeType.Description == request.Description)
                {
                    throw new ApiException(304, Messages.EmployeeTypeDescriptionNotChange);
                }

                _mapper.Map(request, existEmployeeType);
                await _unitOfWork.SaveChangesAsync();

                return new SuccessResponse(200, Messages.UpdatedSuccessfully);
            }
        }
    }
}
