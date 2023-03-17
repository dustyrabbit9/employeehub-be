using employeehub_api.Data;
using employeehub_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace employeehub_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class EmployeeHubController : Controller
    {
        private readonly APIDbContext dbContext;

        public EmployeeHubController(APIDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbContext = dbContext;
        }

        [HttpGet(Name = "getAllEmployees")]
        public IActionResult getAllEmployees()
        {
            return Ok(dbContext.Employee.ToList());

        }

        [HttpPost(Name = "addEmployee")]
        public async Task<IActionResult> addEmployee(AddEmployeeRequest addEmployeeRequest) 
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                firstName = addEmployeeRequest.firstName,
                lastName = addEmployeeRequest.lastName,
                email = addEmployeeRequest.email,
                salary = addEmployeeRequest.salary,
                dob = addEmployeeRequest.dob,
                departmentName = addEmployeeRequest.departmentName
            };

            await dbContext.Employee.AddAsync(employee);
            await dbContext.SaveChangesAsync();

            return Ok(employee);
        }
    }
}
