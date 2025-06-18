using AutoMapper;
using MediatR;
using OnionArchitectureDemo.Application.Interfaces;
using OnionArchitectureDemo.Application.Wrappers;
using System;
using System.Threading;
using System.Threading.Tasks;
using YemekhaneApp.Application.Interfaces;
using YemekhaneApp.Domain.Entities;
using MealRecordEntity = YemekhaneApp.Domain.Entities.MealRecord;
using EmployeeEntity= YemekhaneApp.Domain.Entities.Employee;


namespace YemekhaneApp.Application.CQRS.Commands.MealRecord
{
    public class CreateOrUpdateMealRecordCommand : IRequest<ServiceResponse<Guid>>
    {
        public Guid EmployeeId { get; set; }
        public DateOnly MealDate { get; set; }
        public bool IsEaten { get; set; }

        public class CreateOrUpdateMealRecordCommandHandler : IRequestHandler<CreateOrUpdateMealRecordCommand, ServiceResponse<Guid>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public CreateOrUpdateMealRecordCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<ServiceResponse<Guid>> Handle(CreateOrUpdateMealRecordCommand request, CancellationToken cancellationToken)
            {
                var mealRecordRepository = _unitOfWork.GetRepository<MealRecordEntity>();
                var employeeRepository = _unitOfWork.GetRepository<EmployeeEntity>();

                using var transaction = await _unitOfWork.BeginTransactionAsync();
                var mealRecordId= Guid.Empty;
                try
                {
                    var employee = await employeeRepository.GetByGuidAsync(request.EmployeeId);
                    if (employee == null)
                        return new ServiceResponse<Guid>("Employee not found");

                    var existingRecord = (await mealRecordRepository.GetAllAsync(
                        m => m.EmployeeId == request.EmployeeId && m.MealDate == request.MealDate)).FirstOrDefault();

                    if (existingRecord != null)
                    {
                        if (existingRecord.IsEaten != request.IsEaten)
                        {
                            if (request.IsEaten)
                            {
                                employee.TotalMealCount++;
                            }
                            else
                            {
                                if (employee.TotalMealCount > 0)
                                    employee.TotalMealCount--;
                            }
                        }

                        existingRecord.IsEaten = request.IsEaten;
                        mealRecordId= existingRecord.Id;
                        await mealRecordRepository.UpdateAsync(existingRecord);
                        await employeeRepository.UpdateAsync(employee);
                    }
                    else
                    {
                        var mealRecord = _mapper.Map<MealRecordEntity>(request);

                        if (mealRecord.IsEaten)
                            employee.TotalMealCount++;

                        mealRecordId = mealRecord.Id;

                        await mealRecordRepository.AddAsync(mealRecord);
                        await employeeRepository.UpdateAsync(employee);
                    }

                    await _unitOfWork.SaveAsync();
                    await transaction.CommitAsync();

                    return new ServiceResponse<Guid>(existingRecord?.Id ?? mealRecordId);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return new ServiceResponse<Guid>($"Error creating or updating meal record: {ex.Message}");
                }
            }
        }
    }

}
