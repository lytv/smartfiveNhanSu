using Application.Features.Departments.Commands;
using Application.Features.Departments.Queries;
using Application.Wrappers.Abstract;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : BaseController
    {
        private readonly IMediator _mediator;

        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IResponse> GetAllDepartments()
        {
            return await _mediator.Send(new GetAllDepartmentsQuery());
        }

        [HttpGet("{id}")]
        public async Task<IResponse> FindDepartmentById(Guid id)
        {
            return await _mediator.Send(new GetDepartmentByIdQuery(id));
        }

        [HttpPost]
        public async Task<IResponse> CreateDepartment(CreateDepartmentCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut]
        public async Task<IResponse> UpdateDepartment(UpdateDepartmentCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<IResponse> RemoveDepartment(Guid id)
        {
            return await _mediator.Send(new RemoveDepartmentCommand(id));
        }
    }
}
