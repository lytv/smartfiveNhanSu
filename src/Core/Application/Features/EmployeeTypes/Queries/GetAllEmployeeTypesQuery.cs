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
    public class GetAllEmployeeTypesQuery : IRequest<IResponse>
    {
        public class GetAllEmployeeTypesQueryHandler : IRequestHandler<GetAllEmployeeTypesQuery, IResponse>
        {
            private readonly IEmployeeTypeRepository _employeeRepository;

            public GetAllEmployeeTypesQueryHandler(IEmployeeTypeRepository employeeRepositor)
            {
                _employeeRepository = employeeRepositor;
            }

            public async Task<IResponse> Handle(GetAllEmployeeTypesQuery request, CancellationToken cancellationToken)
            {
                var employeeTypes = await _employeeRepository.GetAllAsync();
                return new DataResponse<IEnumerable<EmployeeType>>(employeeTypes, 200);
            }
        }
    }
}
