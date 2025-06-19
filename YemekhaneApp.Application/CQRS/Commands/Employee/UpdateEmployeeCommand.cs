using AutoMapper;
using MediatR;
using OnionArchitectureDemo.Application.Wrappers;
using System;
using System.Threading;
using System.Threading.Tasks;
using YemekhaneApp.Application.Interfaces;

using EmployeeEntity = YemekhaneApp.Domain.Entities.Employee;

namespace YemekhaneApp.Application.CQRS.Commands.Employee
{
    public class UpdateEmployeeCommand : IRequest<ServiceResponse<Guid>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }

        public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, ServiceResponse<Guid>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public UpdateEmployeeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<ServiceResponse<Guid>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var employeeRepo = _unitOfWork.GetRepository<EmployeeEntity>();
                    var employee = await employeeRepo.GetByGuidAsync(request.Id);

                    if (employee == null)
                        return new ServiceResponse<Guid>("Çalışan bulunamadı.");

                    // Alanları güncelle
                    employee.Name = request.Name;
                    employee.Surname = request.Surname;
                    employee.Email = request.Email;
                    employee.PhoneNumber = request.PhoneNumber;
                    employee.IsActive = request.IsActive;

                    await employeeRepo.UpdateAsync(employee);
                    await _unitOfWork.SaveAsync();

                    return new ServiceResponse<Guid>(employee.Id);
                }
                catch (Exception ex)
                {
                    return new ServiceResponse<Guid>($"Çalışan güncellenirken hata oluştu: {ex.Message}");
                }
            }
        }
    }
}
