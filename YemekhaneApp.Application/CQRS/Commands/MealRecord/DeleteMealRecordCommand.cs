using MediatR;
using OnionArchitectureDemo.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekhaneApp.Application.Interfaces;
using MealRecordEntity = YemekhaneApp.Domain.Entities.MealRecord;

namespace YemekhaneApp.Application.CQRS.Commands.MealRecord
{
    public class DeleteMealRecordCommand : IRequest<ServiceResponse<Guid>>
    {
        public Guid Id { get; set; }
        public DeleteMealRecordCommand(Guid id)
        {
            Id = id;
        }
        public class DeleteMealRecordCommandHandler : IRequestHandler<DeleteMealRecordCommand, ServiceResponse<Guid>>
        {
            private readonly IUnitOfWork _unitOfWork;
            public DeleteMealRecordCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<ServiceResponse<Guid>> Handle(DeleteMealRecordCommand request, CancellationToken cancellationToken)
            {
                await using var transaction = await _unitOfWork.BeginTransactionAsync();
                try
                {
                    var mealRecordRepo = _unitOfWork.GetRepository<MealRecordEntity>();
                    var mealRecord = await mealRecordRepo.GetByGuidAsync(request.Id);
                    if (mealRecord == null)
                        return new ServiceResponse<Guid>("Meal record not found.");

                    // İlişkili kullanıcıyı getir
                    var employeeRepo = _unitOfWork.GetRepository<YemekhaneApp.Domain.Entities.Employee>();
                    var employee = await employeeRepo.GetByGuidAsync(mealRecord.EmployeeId);
                    if (employee == null)
                        return new ServiceResponse<Guid>("İlişkili çalışan bulunamadı.");

                    // mealCount -1
                    if (employee.TotalMealCount > 0)
                        employee.TotalMealCount--;

                    await employeeRepo.UpdateAsync(employee);
                    await mealRecordRepo.DeleteAsync(mealRecord);

                    await _unitOfWork.SaveAsync();
                    await transaction.CommitAsync();

                    return new ServiceResponse<Guid>(mealRecord.Id);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return new ServiceResponse<Guid>($"Error deleting meal record: {ex.Message}");
                }
            }
        }
    }
}
