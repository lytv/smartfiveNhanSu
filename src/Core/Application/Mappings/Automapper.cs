using Application.Dtos;
using Application.Features.Departments.Commands;
using Application.Features.EmployeeTypes.Commands;
using Application.Features.Roles.Commands;
using Application.Features.Users.Commands;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public class Automapper : Profile
    {
        public Automapper()
        {
            CreateMap<User, UpdateUserCommand>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, RegisterCommand>().ReverseMap();
            CreateMap<Role, UpdateRoleCommand>().ReverseMap();
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<UpdateDepartmentCommand, Department>().ReverseMap();
            CreateMap<UpdateEmployeeTypeCommand, EmployeeType>().ReverseMap();
        }
    }
}
