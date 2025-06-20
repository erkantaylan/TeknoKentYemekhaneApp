using WebUI.Models.Employee;

namespace WebUI.Models.MealRecord
{
    public class MealRecordViewModel
    {
        public Guid Id { get; set; } // Unique identifier for the meal record
        public Guid EmployeeId { get; set; } // Foreign key to Employee
        public DateOnly MealDate { get; set; } // Date of the meal
        public bool IsEaten { get; set; } // Indicates if the meal was eaten
        public EmployeeViewModel Employee { get; set; } // Navigation property to Employee details
    }
}
