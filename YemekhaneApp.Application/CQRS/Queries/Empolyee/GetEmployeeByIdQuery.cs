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
    public class GetEmployeeByIdQuery : IRequest<ServiceResponse<EmployeeDto>>
    {
        public Guid Id { get; set; }
        public GetEmployeeByIdQuery(Guid id)
        {
            Id = id;
        }

        public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, ServiceResponse<EmployeeDto>>
        {
            private readonly IUnitOfWork unitOfWork;
            private readonly IMapper _mapper;

            public GetEmployeeByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                this.unitOfWork = unitOfWork; 
                _mapper = mapper;
            }

            public async Task<ServiceResponse<EmployeeDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
            {
                var employee = await unitOfWork.GetRepository<Employee>().GetByGuidAsync(request.Id);
                if (employee == null)
                    return new ServiceResponse<EmployeeDto>("Employee not found");

                var dto = _mapper.Map<EmployeeDto>(employee);
                return new ServiceResponse<EmployeeDto>(dto);
            }
        }
    }

}
