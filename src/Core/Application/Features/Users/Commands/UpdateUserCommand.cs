using Application.Constants;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Wrappers.Abstract;
using Application.Wrappers.Concrete;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Features.Users.Commands
{
    public class UpdateUserCommand : IRequest<IResponse>
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly Birthdate { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string DepartmentCode { get; set; }
        public string EmployeeTypeCode { get; set; }
        public string EmployeeCode { get; set; }
        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, IResponse>
        {
            private readonly IUserRepository _userRepository;
            private readonly IDepartmentRepository _departmentRepository;
            private readonly IEmployeeTypeRepository _employeeTypeRepository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly IEasyCacheService _easyCacheService;

            public UpdateUserCommandHandler(
                IUserRepository userRepository,
                IDepartmentRepository departmentRepository,
                IEmployeeTypeRepository employeeTypeRepository,
                IUnitOfWork unitOfWork,
                IMapper mapper,
                IEasyCacheService easyCacheService
                )
            {
                _userRepository = userRepository;
                _departmentRepository = departmentRepository;
                _employeeTypeRepository = employeeTypeRepository;
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _easyCacheService = easyCacheService;
            }

            public async Task<IResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByIdAsync(request.UserId);
                if (user == null)
                {
                    throw new ApiException(404, Messages.UserNotFound);
                }
                var username = await _userRepository.GetAsync(x => x.UserName == request.UserName);
                if (username != null && user.UserName != request.UserName)
                {
                    throw new ApiException(400, Messages.UsernameIsAlreadyExist);
                }

                var employeeCode = await _userRepository.GetAsync(
                    d => d.EmployeeCode == request.EmployeeCode);

                if (employeeCode?.EmployeeCode != request.EmployeeCode)
                {
                    throw new ApiException(400, Messages.EmployeeCodeIsAlreadyExist);
                }

                if (request.DepartmentCode != null)
                {
                    var department = await _departmentRepository.GetAsync(d => d.DepartmentCode == request.DepartmentCode);
                    user.Department = department ?? throw new ApiException(400, Messages.DepartmentNotFound);
                }

                if (request.EmployeeTypeCode != null)
                {
                    var employeeType = await _employeeTypeRepository.GetAsync(e => e.EmployeeTypeCode == request.EmployeeTypeCode);
                    user.EmployeeType = employeeType ?? throw new ApiException(400, Messages.EmployeeTypeNotFound); ;
                }

                _mapper.Map(request, user);
                await _unitOfWork.SaveChangesAsync();
                await _easyCacheService.RemoveByPrefixAsync("GetAuthenticatedUserWithRoles");
                return new SuccessResponse(200, Messages.UpdatedSuccessfully);
            }
        }
    }
}