using AutoMapper;
using MediatR;
using OnionArchitectureDemo.Application.Interfaces;
using OnionArchitectureDemo.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekhaneApp.Application.DTOs.MealRecord;
using YemekhaneApp.Application.Interfaces;
using MealRecordEntity = YemekhaneApp.Domain.Entities.MealRecord;


namespace YemekhaneApp.Application.CQRS.Queries.MealRecord
{
    public class GetMealsByDateWithEmployeeQuery : IRequest<ServiceResponse<List<MealRecordDto>>>
    {
        public DateOnly Date { get; set; }

        public GetMealsByDateWithEmployeeQuery(DateOnly date)
        {
            Date = date;
        }

        public class GetMealsByDateWithEmployeeQueryHandler : IRequestHandler<GetMealsByDateWithEmployeeQuery, ServiceResponse<List<MealRecordDto>>>
        {
            private readonly IUnitOfWork unitOfWork;
            private readonly IMapper _mapper;

            public GetMealsByDateWithEmployeeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                this.unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<ServiceResponse<List<MealRecordDto>>> Handle(GetMealsByDateWithEmployeeQuery request, CancellationToken cancellationToken)
            {
                var records = await unitOfWork.GetRepository<MealRecordEntity>().GetAllAsync(
                    m => m.MealDate == request.Date,
                    x => x.Employee // Include Employee details    
                );
                    

                var dtoList = _mapper.Map<List<MealRecordDto>>(records);

                return new ServiceResponse<List<MealRecordDto>>(dtoList);
            }
        }
    }

}
