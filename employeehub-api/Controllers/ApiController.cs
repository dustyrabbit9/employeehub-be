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

        [HttpGet("employee/getAllEmployees")]
        public IActionResult getAllEmployees()
        {
            return Ok(dbContext.Employee.ToList());
        }

        [HttpGet("department/getAllDepartments")]
        public IActionResult getAllDepartments()
        {
            return Ok(dbContext.Department.ToList());
        }

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

        [HttpGet]
        [Route("employee/getEmployeeByDepartment/{departmentName}")]
        public async Task<IActionResult> getEmployeeByDepartment([FromRoute] string departmentName)
        {
            var employees = await dbContext.Employee.Where(e => e.departmentName == departmentName).ToListAsync();

            if (employees == null || employees.Count == 0)
            {
                return NotFound();
            }

            return Ok(employees);
        }


        [HttpPost("employee/addEmployee")]
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
                employee.departmentName = updateEmployeeRequest.departmentName;

                await dbContext.SaveChangesAsync();
                return Ok(employee);
       
            }

            return NotFound();
        }

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

            return NotFound();
        }

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

            return NotFound();
        }

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

            return NotFound();
        }

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

            return NotFound();
        }



    }
}
