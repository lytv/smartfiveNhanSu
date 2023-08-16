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

namespace Application.Features.Departments.Queries
{
    public class GetDepartmentByIdQuery : IRequest<DataResponse<Department>>
    {
        public Guid Id { get; set; }

        public GetDepartmentByIdQuery(Guid id)
        {
            Id = id;
        }

        public class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQuery, DataResponse<Department>>
        {
            private readonly IDepartmentRepository _departmentRepository;

            public GetDepartmentByIdQueryHandler(IDepartmentRepository departmentRepository)
            {
                _departmentRepository = departmentRepository;
            }
            public async Task<DataResponse<Department>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
            {
                var department = await _departmentRepository.GetByIdAsync(request.Id);

                if (department == null)
                {
                    throw new ApiException(404, Messages.NotFound);
                }

                return new DataResponse<Department>(department, 200);
            }
        }
    }
}
