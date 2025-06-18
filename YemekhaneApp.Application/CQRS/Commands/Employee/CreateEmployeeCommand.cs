using AutoMapper;
using MediatR;
using OnionArchitectureDemo.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekhaneApp.Application.Interfaces;

using EmployeeEntity = YemekhaneApp.Domain.Entities.Employee;

namespace YemekhaneApp.Application.CQRS.Commands.Employee
{
    public class CreateEmployeeCommand : IRequest<ServiceResponse<Guid>>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; } = true;

        public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, ServiceResponse<Guid>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public CreateEmployeeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            
            public async Task<ServiceResponse<Guid>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    // Employee entity'sine map et
                    var employee = _mapper.Map<EmployeeEntity>(request);

                    // Başlangıç değerleri kontrol edilebilir (IsActive ve TotalMealCount zaten default)

                    // Repository ile ekle
                    var employeeRepo = _unitOfWork.GetRepository<EmployeeEntity>();
                    await employeeRepo.AddAsync(employee);

                    // Değişiklikleri kaydet
                    await _unitOfWork.SaveAsync();

                    return new ServiceResponse<Guid>(employee.Id);
                }
                catch (Exception ex)
                {
                    return new ServiceResponse<Guid>($"Error creating employee: {ex.Message}");
                }
            }
        }
    }
}
