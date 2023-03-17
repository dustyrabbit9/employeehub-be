using employeehub_api.Data;
using employeehub_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        }
        // Retrieve all Employees 
        [HttpGet("employee/getAllEmployees")]
        public IActionResult getAllEmployees()
        {
            return Ok(dbContext.Employee.ToList());
        }
        
        // Retrieve all Departments 
        [HttpGet("department/getAllDepartments")]
        public IActionResult getAllDepartments()
        {
            return Ok(dbContext.Department.ToList());
        }

        // Retrieve specific Employee using employee Id
        [HttpGet]
        [Route("employee/getOneEmployee/{id:guid}")]
        public async Task<IActionResult> getOneEmployee([FromRoute] Guid id)
        {
            var employee = await dbContext.Employee.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // Retrieve specific Department using Department ID
        [HttpGet]
        [Route("employee/getOneDepartment/{id:guid}")]
        public async Task<IActionResult> getOneDepartment([FromRoute] Guid id)
        {
            var department = await dbContext.Department.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        // Retrieve all Employees relating to a Department using departmentId
        [HttpGet]
        [Route("employee/getEmployeeByDepartmentId/{id:guid}")]
        public async Task<IActionResult> getEmployeeByDepartment([FromRoute] Guid id)
        {
            var employees = await dbContext.Employee.Where(e => e.departmentId == id).ToListAsync();

            if (employees == null || employees.Count == 0)
            {
                return NotFound();
            }

            return Ok(employees);
        }

        // Add an Employee 
        [HttpPost("employee/addEmployee")]
        public async Task<IActionResult> addEmployee(AddEmployeeRequest addEmployeeRequest)
        {
            // Check if entered department exists
            var department = await dbContext.Department.FindAsync(addEmployeeRequest.DepartmentId);

            if (department == null)
            {
                return NotFound("Department Not Found");
            }

            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                firstName = addEmployeeRequest.firstName,
                lastName = addEmployeeRequest.lastName,
                email = addEmployeeRequest.email,
                salary = addEmployeeRequest.salary,
                dob = addEmployeeRequest.dob,
                departmentId = addEmployeeRequest.DepartmentId
            };

            employee.age = (int)((DateTime.Now - employee.dob).TotalDays / 365.242199);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dbContext.Employee.AddAsync(employee);
            await dbContext.SaveChangesAsync();

            return Ok(employee);
        }


        // Add a Department 
        [HttpPost("department/addDepartment")]

        public async Task<IActionResult> addDepartment(AddDepartmentRequest addDepartmentRequest)
        {
            var department = new Department()
            {
                Id = Guid.NewGuid(),
                departmentName = addDepartmentRequest.departmentName
            };

            await dbContext.Department.AddAsync(department);
            await dbContext.SaveChangesAsync();

            return Ok(department);
        }

        // Update an Employee based on employee Id
        [HttpPut]
        [Route("employee/updateEmployee/{id:guid}")]

        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, UpdateEmployeeRequest updateEmployeeRequest)
        {
            var employee = dbContext.Employee.Find(id);

            if (employee != null)
            {
                employee.firstName = updateEmployeeRequest.firstName;
                employee.lastName = updateEmployeeRequest.lastName;
                employee.email = updateEmployeeRequest.email;
                employee.salary = updateEmployeeRequest.salary;
                employee.dob = updateEmployeeRequest.dob;
                //employee.departmentName = updateEmployeeRequest.departmentName;

                employee.age = (int)((DateTime.Now - employee.dob).TotalDays / 365.242199);

                await dbContext.SaveChangesAsync();
                return Ok(employee);
       
            }

            return NotFound("Employee Not Found");
        }

        // Update a Department based on Department Id
        [HttpPut]
        [Route("department/updateDepartment/{id:guid}")]

        public async Task<IActionResult> UpdateDepartment([FromRoute] Guid id, UpdateDepartmentRequest updateDepartmentRequest)
        {
            var department = dbContext.Department.Find(id);

            if (department != null)
            {
                department.departmentName = updateDepartmentRequest.departmentName;

                await dbContext.SaveChangesAsync();
                return Ok(department);

            }

            return NotFound("Department Not Found");
        }

        // Update a Department based on departmentName
        [HttpPut]
        [Route("department/updateDepartment/{departmentName}")]
        public async Task<IActionResult> UpdateDepartment([FromRoute] string departmentName, UpdateDepartmentRequest updateDepartmentRequest)
        {
            var department = await dbContext.Department.FirstOrDefaultAsync(d => d.departmentName == departmentName);

            if (department != null)
            {
                department.departmentName = updateDepartmentRequest.departmentName;

                await dbContext.SaveChangesAsync();
                return Ok(department);
            }

            return NotFound("Department Not Found");
        }

        // Delete an Employee using employeeId
        [HttpDelete]
        [Route("employee/deleteEmployee/{id:guid}")]
        public async Task<IActionResult> deleteEmployee(Guid id)
        {
            var employee = await dbContext.Employee.FindAsync(id);
            if (employee != null)
            {
                dbContext.Remove(employee);
                await dbContext.SaveChangesAsync();

                return Ok(employee);
            }

            return NotFound("Employee Not Found");
        }

        // Delete a Department using departmentId
        [HttpDelete]
        [Route("department/deleteDepartment/{id:guid}")]
        public async Task<IActionResult> deleteDepartment(Guid id)
        {
            var department = await dbContext.Department.FindAsync(id);
            if (department != null)
            {
                dbContext.Remove(department);
                await dbContext.SaveChangesAsync();

                return Ok(department);
            }

            return NotFound("Department Not Found");
        }



    }
}
