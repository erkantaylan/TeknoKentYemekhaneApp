using MediatR;
using OnionArchitectureDemo.Application.Wrappers;
using System;
using System.Threading;
using System.Threading.Tasks;
using YemekhaneApp.Application.Interfaces;

using EmployeeEntity = YemekhaneApp.Domain.Entities.Employee;

namespace YemekhaneApp.Application.CQRS.Commands.Employee
{
    public class DeleteEmployeeCommand : IRequest<ServiceResponse<Guid>>
    {
        public Guid Id { get; set; }

        public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, ServiceResponse<Guid>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public DeleteEmployeeCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<ServiceResponse<Guid>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var employeeRepo = _unitOfWork.GetRepository<EmployeeEntity>();
                    var employee = await employeeRepo.GetByGuidAsync(request.Id);

                    if (employee == null)
                        return new ServiceResponse<Guid>("Çalışan bulunamadı.");

                    await employeeRepo.DeleteAsync(employee);
                    await _unitOfWork.SaveAsync();

                    return new ServiceResponse<Guid>(employee.Id);
                }
                catch (Exception ex)
                {
                    return new ServiceResponse<Guid>($"Çalışan silinirken hata oluştu: {ex.Message}");
                }
            }
        }
    }
}