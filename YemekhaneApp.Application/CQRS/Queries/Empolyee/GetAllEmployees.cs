using AutoMapper;
using MediatR;
using OnionArchitectureDemo.Application.Interfaces;
using OnionArchitectureDemo.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekhaneApp.Application.DTOs.Employee;
using YemekhaneApp.Application.Interfaces;
using YemekhaneApp.Domain.Entities;

namespace YemekhaneApp.Application.CQRS.Queries.Empolyee
{
    public class GetAllEmployees: IRequest<ServiceResponse<List<EmployeeDto>>>
    {
        public GetAllEmployees()
        {
        }

        public class GetAllEmployeesHandler: IRequestHandler<GetAllEmployees, ServiceResponse<List<EmployeeDto>>>
        {
            private readonly IUnitOfWork unitOfWork;
            private readonly IMapper _mapper;
            public GetAllEmployeesHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                this.unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<ServiceResponse<List<EmployeeDto>>> Handle(GetAllEmployees request, CancellationToken cancellationToken)
            {
                var employees = await unitOfWork.GetRepository<Employee>().GetAllAsync();
                if (employees == null || !employees.Any())
                    return new ServiceResponse<List<EmployeeDto>>("Kayıt bulunamadı");

                var dtoList = _mapper.Map<List<EmployeeDto>>(employees);
                return new ServiceResponse<List<EmployeeDto>>(dtoList);
            }
        }
    }
    
}
