namespace WebUI.Models.Employee
{
    public class EmployeeUpdateViewModel
    {   
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; } = true;
        public int TotalMealCount { get; set; }
    }
}
