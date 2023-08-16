using Application.Features.EmployeeTypes.Commands;
using Application.Features.EmployeeTypes.Queries;
using Application.Wrappers.Abstract;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeTypeController : BaseController
    {
        private readonly IMediator _mediator;

        public EmployeeTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IResponse> GetAllEmployeeTypes()
        {
            return await _mediator.Send(new GetAllEmployeeTypesQuery());
        }

        [HttpGet("{id}")]
        public async Task<IResponse> FindEmployeeTypeById(Guid id)
        {
            return await _mediator.Send(new GetEmployeeTypeByIdQuery(id));
        }

        [HttpPost]
        public async Task<IResponse> CreateEmployeeType(CreateEmployeeTypeCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut]
        public async Task<IResponse> UpdateEmployeeType(UpdateEmployeeTypeCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<IResponse> RemoveEmployeeType(Guid id)
        {
            return await _mediator.Send(new RemoveEmployeeTypeCommand(id));
        }
    }
}
