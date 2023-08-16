using Application.Constants;
using Application.Exceptions;
using Application.Helpers;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Wrappers.Abstract;
using Application.Wrappers.Concrete;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Application.Features.Users.Commands
{
    public class RegisterCommand : IRequest<IResponse>
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateOnly Birthdate { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string EmployeeCode { get; set; }
        public string DepartmentCode { get; set; }
        public string EmployeeTypeCode { get; set; }
        public int TenantId { get; set; } = 1;

        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, IResponse>
        {
            private readonly IUserRepository _userRepository;
            private readonly IDepartmentRepository _departmentRepository;
            private readonly IEmployeeTypeRepository _employeeTypeRepository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IEmailService _emailService;
            private readonly IMapper _mapper;

            public RegisterCommandHandler(
                IUserRepository userRepository,
                IDepartmentRepository departmentRepository,
                IEmployeeTypeRepository employeeTypeRepository,
                IUnitOfWork unitOfWork,
                IEmailService emailService,
                IMapper mapper
                )
            {
                _userRepository = userRepository;
                _departmentRepository = departmentRepository;
                _employeeTypeRepository = employeeTypeRepository;
                _unitOfWork = unitOfWork;
                _emailService = emailService;
                _mapper = mapper;
            }

            public async Task<IResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                var existuser = await _userRepository.GetAsync(
                    x => x.UserName == request.UserName
                    || x.Email == request.Email
                    || x.EmployeeCode == request.EmployeeCode
                    , noTracking: true);
                if (existuser?.UserName == request.UserName)
                    throw new ApiException(400, Messages.UsernameIsAlreadyExist);

                if (existuser?.Email == request.Email)
                    throw new ApiException(400, Messages.EmailIsAlreadyExist);

                if (existuser?.EmployeeCode == request.EmployeeCode)
                    throw new ApiException(400, Messages.EmployeeCodeIsAlreadyExist);
                
                var user = _mapper.Map<User>(request);

                if (request.DepartmentCode != null)
                {
                    var department = await _departmentRepository.GetAsync(d => d.DepartmentCode == request.DepartmentCode)
                        ?? throw new ApiException(400, Messages.DepartmentNotFound);
                    user.Department = department;
                }

                if (request.EmployeeTypeCode != null)
                {
                    var employeeType = await _employeeTypeRepository.GetAsync(e => e.EmployeeTypeCode == request.EmployeeTypeCode)
                        ?? throw new ApiException(400, Messages.EmployeeTypeNotFound);
                    user.EmployeeType = employeeType;
                }

                var (passwordHash, passwordSalt) = PasswordHelper.CreateHash(request.Password);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.EmailConfirmationCode = PasswordHelper.GenerateRandomString(20);
                await _userRepository.AddAsync(user);
                await _unitOfWork.SaveChangesAsync();
                //string link = "http://localhost:8080/confirmemail/" + user.EmailConfirmationCode; if u use spa you must use this link example
                string link = "http://localhost:5010/api/users/confirmemail/" + user.EmailConfirmationCode;
                await _emailService.ConfirmationMailAsync(link, request.Email);
                return new SuccessResponse(200, Messages.RegisterSuccessfully);
            }
        }
    }
}