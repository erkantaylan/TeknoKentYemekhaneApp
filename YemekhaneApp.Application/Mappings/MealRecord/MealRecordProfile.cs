using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemekhaneApp.Application.Mappings.MealRecord
{
    public class MealRecordProfile:Profile
    {
        public MealRecordProfile()
        {
            CreateMap<Domain.Entities.MealRecord, Dtos.MealRecordDto>().ReverseMap();  
        }
    }
}
