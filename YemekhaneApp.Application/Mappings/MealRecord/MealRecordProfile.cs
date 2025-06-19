using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekhaneApp.Application.CQRS.Commands.MealRecord;
using YemekhaneApp.Application.DTOs.MealRecord;

namespace YemekhaneApp.Application.Mappings.MealRecord
{
    public class MealRecordProfile:Profile
    {
        public MealRecordProfile()
        {
            CreateMap<Domain.Entities.MealRecord, MealRecordDto>().ReverseMap();  
            CreateMap<Domain.Entities.MealRecord, CreateMealRecordCommand>().ReverseMap();
        }
    }
}
