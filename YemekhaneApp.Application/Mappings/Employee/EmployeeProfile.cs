using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekhaneApp.Application.CQRS.Commands.Employee;
using  YemekhaneApp.Application.DTOs.Employee;
using YemekhaneApp.Application.DTOs.Employee;
using EmployeeEntity = YemekhaneApp.Domain.Entities.Employee;

namespace YemekhaneApp.Application.Mappings.Employee
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeEntity, EmployeeDto>().ReverseMap();
            CreateMap<CreateEmployeeCommand, EmployeeEntity>().ReverseMap();
        }
    }
}
