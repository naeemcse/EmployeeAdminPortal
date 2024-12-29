using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    // localhost:port/api/employees
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<EmployeesController> logger;
        public EmployeesController(ApplicationDbContext dbContext, ILogger<EmployeesController> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var allEmployees = dbContext.Employees.ToList();

            return Ok(allEmployees);
        }

        // Single person details
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeeById( Guid id)
        {
         
            logger.LogInformation("Fetching employee with ID: {Id}", id);
            var employee = dbContext.Employees.Find(id);
            if (employee is null)
            {
                logger.LogWarning("Employee with ID: {Id} not found", id);
                return NotFound();
            }
            return Ok(employee);
        }

        // Insert a employee into database
        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto employee )
        {
            var employeeEntity = new Employee()
            {
                Name = employee.Name,
                Email= employee.Email,
                Phone = employee.Phone,
                Salary = employee.Salary
            };

            dbContext.Employees.Add(employeeEntity);
            dbContext.SaveChanges();

            return Ok(employeeEntity);
        }
        // Update 
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id,UpdateEmloyeeDto? updateEmloyeeDto)
        {
            var employee = dbContext.Employees.Find(id);

            if (employee is null)
            {
                logger.LogWarning("Employee with ID: {Id} not found", id);
                return NotFound();
            }
            employee.Name = updateEmloyeeDto?.Name == null ? employee.Name : updateEmloyeeDto.Name;
            employee.Email = updateEmloyeeDto?.Email == null ? employee.Email: updateEmloyeeDto.Email;
            employee.Phone = updateEmloyeeDto?.Phone == null ? employee.Phone : updateEmloyeeDto.Phone;
            employee.Salary = updateEmloyeeDto?.Salary== null ? employee.Salary : updateEmloyeeDto.Salary;

            dbContext.SaveChanges();
            return Ok(employee);
        }
        // Delete 
        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employee = dbContext.Employees.Find(id);

            if (employee is null)
            {
                logger.LogWarning("Employee with ID: {Id} not found", id);
                return NotFound();
            }

            dbContext.Employees.Remove(employee);
            dbContext.SaveChanges();
            return Ok();

        }

    }
}
