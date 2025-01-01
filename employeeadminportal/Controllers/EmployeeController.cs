using employeeadminportal.Data;
using employeeadminportal.Models;
using employeeadminportal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace employeeadminportal.Controllers
{
    //localhost:xxxx/api/
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]

        public IActionResult GetAllemployees()
        {
           var allemployess= dbContext.Employees.ToList();

            return Ok(allemployess);

        }


        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeebyid(Guid id) 
        {
            var employee =dbContext.Employees.Find(id);


            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(employee);
            }
        }

        [HttpPost] 

        public IActionResult AddEmployee(AddEmployeeDto addemployeedto)
        {
            var employeeentity = new Employee()
            {
                Name = addemployeedto.Name,
                Email = addemployeedto.Email,
                Phone = addemployeedto.Phone,
                salary = addemployeedto.salary
            };

            dbContext.Employees.Add(employeeentity);
            dbContext.SaveChanges();
            return Ok(employeeentity);
        }

        [HttpPut]
        [Route("{id:guid}")]

        public  IActionResult updateemployee(Guid id,updatemployeedto updatemployeedto)
        {

            var employee = dbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }


            employee.Name = updatemployeedto.Name;
            employee.Email = updatemployeedto.Email;
            employee.Phone = updatemployeedto.Phone;
            employee.salary = updatemployeedto.salary;
            dbContext.SaveChanges(); return Ok(employee);


        }

        [HttpDelete]
        [Route("{id:guid}")]

        public IActionResult deleteemployee(Guid id)
        {
            var employee=dbContext.Employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            dbContext.Employees.Remove(employee);

            dbContext.SaveChanges(); return Ok();
        }
    }
}
