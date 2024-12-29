namespace EmployeeAdminPortal.Models
{
    public class UpdateEmloyeeDto
    {
        public  string? Name { get; set; }
        public  string? Email { get; set; }
        public string? Phone { get; set; }

        public decimal Salary { get; set; }
    }
}
