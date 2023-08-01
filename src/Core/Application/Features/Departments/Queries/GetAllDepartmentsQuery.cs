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

namespace Application.Features.Departments.Queries
{
    public class GetAllDepartmentsQuery : IRequest<IResponse>
    {
        public class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, IResponse>
        {
            private readonly IDepartmentRepository _departmentRepository;

            public GetAllDepartmentsQueryHandler(IDepartmentRepository departmentRepository)
            {
                _departmentRepository = departmentRepository;
            }

            public async Task<IResponse> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
            {
                var departments = await _departmentRepository.GetAllAsync();
                return new DataResponse<IEnumerable<Department>>(departments, 200);
            }
        }
    }
}
