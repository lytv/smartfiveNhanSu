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

namespace Application.Features.Departments.Commands
{
    public class UpdateDepartmentCommand : IRequest<IResponse>
    {
        public Guid Id { get; set; }
        public string DepartmentCode { get; set; }
        public string Description { get; set; }

        public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, IResponse>
        {
            private readonly IDepartmentRepository _departmentRepository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public UpdateDepartmentCommandHandler(IDepartmentRepository departmentRepository, IUnitOfWork unitOfWork, IMapper mapper)
            {
                _departmentRepository = departmentRepository;
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<IResponse> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
            {
                var existDepartment = await _departmentRepository.GetByIdAsync(request.Id)
                    ?? throw new ApiException(404, Messages.DepartmentNotFound);

                var existCode = await _departmentRepository.GetAsync(
                    d => d.DepartmentCode == request.DepartmentCode);

                if (existCode != null
                    && existCode.DepartmentCode != existDepartment.DepartmentCode)
                {
                    throw new ApiException(400, Messages.DepartmentCodeAlreadyExist);
                }

                if (existDepartment.Description == request.Description)
                {
                    throw new ApiException(304, Messages.DepartmentDescriptionNotChange);
                }

                _mapper.Map(request, existDepartment);
                await _unitOfWork.SaveChangesAsync();

                return new SuccessResponse(200, Messages.UpdatedSuccessfully);
            }
        }
    }
}
