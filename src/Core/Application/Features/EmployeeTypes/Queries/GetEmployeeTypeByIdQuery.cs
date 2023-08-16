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

namespace Application.Features.EmployeeTypes.Queries
{
    public class GetEmployeeTypeByIdQuery : IRequest<DataResponse<EmployeeType>>
    {
        public Guid Id { get; set; }

        public GetEmployeeTypeByIdQuery(Guid id)
        {
            Id = id;
        }

        public class GetEmployeeTypeByIdQueryHandler : IRequestHandler<GetEmployeeTypeByIdQuery, DataResponse<EmployeeType>>
        {
            private readonly IEmployeeTypeRepository _employeeRepository;

            public GetEmployeeTypeByIdQueryHandler(IEmployeeTypeRepository employeeRepository)
            {
                _employeeRepository = employeeRepository;
            }

            public async Task<DataResponse<EmployeeType>> Handle(GetEmployeeTypeByIdQuery request, CancellationToken cancellationToken)
            {
                var employeeType = await _employeeRepository.GetByIdAsync(request.Id)
                    ?? throw new ApiException(404, Messages.NotFound);

                return new DataResponse<EmployeeType>(employeeType, 200);
            }
        }
    }
}
