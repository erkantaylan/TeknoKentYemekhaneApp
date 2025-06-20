using WebUI.Models.Employee;

namespace WebUI.Models.MealRecord
{
    public class MealRecordUpdateViewModel
    {
        public Guid Id { get; set; } 
        public Guid EmployeeId { get; set; } 
        public DateOnly MealDate { get; set; } 
        public bool IsEaten { get; set; } 
        
    }
}
