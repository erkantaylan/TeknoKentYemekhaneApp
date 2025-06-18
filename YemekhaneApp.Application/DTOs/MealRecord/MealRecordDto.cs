using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekhaneApp.Application.DTOs.Employee;

namespace YemekhaneApp.Application.DTOs.MealRecord
{
    public class MealRecordDto
    {
        public Guid Id { get; set; } // Unique identifier for the meal record
        public Guid EmployeeId { get; set; } // Foreign key to Employee
        public DateOnly MealDate { get; set; } // Date of the meal
        public bool IsEaten { get; set; } // Indicates if the meal was eaten
        public EmployeeDto Employee { get; set; } // Navigation property to Employee details
    }
}
