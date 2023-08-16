using Application.Features.Users.Commands;
using Application.Features.Users.Queries;
using Application.Interfaces.Repositories;
using Application.Wrappers.Abstract;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Persistence.Repositories;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;


        public UsersController(IMediator mediator, IUserRepository userRepository)
        {
            _mediator = mediator;
            _userRepository = userRepository;

        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<IResponse> GetAllUsersWithRoles()
        {
            return await _mediator.Send(new GetAllUsersWithRolesQuery());
        }


        [HttpGet("confirmemail/{code}")]
        public async Task<IResponse> ConfirmEmail(string code)
        {
            return await _mediator.Send(new ConfirmEmailCommand(code));
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("updateuserrole")]
        public async Task<IResponse> UpdateUserRole(UpdateUserRoleCommand command)
        {
            return await _mediator.Send(command);
        }

        [Authorize]
        [HttpPut]
        public async Task<IResponse> UpdateUser(UpdateUserCommand command)
        {
            command.UserId = UserId.Value;
            return await _mediator.Send(command);
        }

        [Authorize]
        [HttpPost("changepassword")]
        public async Task<IResponse> ChangePassword (ChangePasswordCommand command)
        {
            command.UserId = UserId.Value;
            return await _mediator.Send(command);
        }
        
        [Authorize]
        [HttpPost("changeemail")]
        public async Task<IResponse> ChangeEmail(ChangeEmailCommand command)
        {
            command.UserId = UserId.Value;
            return await _mediator.Send(command);
        }

        [HttpPost("forgetpassword")]
        public async Task<IResponse> ForgetPassword(ForgetPasswordCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("resetpassword/{code}/{email}")]
        public async Task<IResponse> ResetPassword(string code,string email)
        {
            return await _mediator.Send(new ResetPasswordCommand(code, email));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{userid}")]
        public async Task<IResponse> DeleteUser(Guid userid)
        {
            return await _mediator.Send(new RemoveUserCommand(userid));
        }

        [HttpPost("importexcel")]
        public async Task<IResponse> ImportExcel([FromForm] ImportExcelCommand command)
        {

            return await _mediator.Send(command);
        }

        [HttpGet("exportexcel")]
        public async Task<IActionResult> ExportExcel()
        {

            var list = await _userRepository.GetAllAsync();
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromCollection(list, true);
                package.Save();
            }

            stream.Position = 0;
            string excelName = $"UserList-{System.DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

            string fileDownloadName = excelName;

            return File(stream, "application/octet-stream", fileDownloadName);
        }
    }
}