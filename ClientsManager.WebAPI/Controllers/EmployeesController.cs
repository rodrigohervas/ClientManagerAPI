using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ClientsManager.Data;
using ClientsManager.Models;
using ClientsManager.WebAPI.DTOs;
using ClientsManager.WebAPI.ValidationActionFiltersMiddleware;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientsManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IGenericRepository<Employee> _genericRepository;
        private readonly IMapper _mapper;

        public EmployeesController(IGenericRepository<Employee> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }


        // GET: api/employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployeesAsync()
        {
            var employees = await _genericRepository.GetAllAsync();

            if (!employees.Any())
            {
                return NotFound("No Employees were found");
            }

            IEnumerable<EmployeeDTO> employeeList = _mapper.Map<IEnumerable<EmployeeDTO>>(employees);

            return Ok(employeeList);
        }

        // GET: api/employees/5
        [HttpGet("{id}")]
        [ServiceFilter(typeof(IdValidator))]
        public async Task<ActionResult<Employee>> GetEmployeeByIdAsync(int id)
        {
            var employee = await _genericRepository.GetOneByWithRelatedDataAsync(employee => employee.Id == id, 
                                                                                 employee => employee.TimeFrames);

            if (employee is null)
            {
                return NotFound("No data was found for the id");
            }

            return Ok(employee);
        }

        // GET: api/employees/employeetype/1
        [HttpGet("/employeetype/{employeetype_id}")]
        [ServiceFilter(typeof(IdValidator))]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesByTypeAsync(int employeetype_id)
        {
            var employees = await _genericRepository.GetByAsync(employee => employee.EmployeeType_Id == employeetype_id);

            if (!employees.Any())
            {
                return NotFound("No Employees were found");
            }

            return Ok(employees);
        }


        // POST: api/employees/
        [HttpPost("{Employee}")]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(EmployeeValidationFilter))]
        public async Task<ActionResult<Employee>> AddEmployeeAsync(Employee employee)
        {
            var addResult = await _genericRepository.AddTAsync(employee);

            if (addResult == 0)
            {
                return NotFound("No Employee was created");
            }

            var newEmployee = await _genericRepository.GetOneByAsync(emp =>
                                                        emp.Name == employee.Name && 
                                                        emp.Position == employee.Position);

            return Created("", newEmployee);
        }

        
        // PUT/PATCH: api/employees/id
        [HttpPut("{id}")]
        [HttpPatch("{id}")]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(EmployeeValidationFilter))]
        public async Task<ActionResult<Employee>> UpdateEmployeeAsync(int id, Employee employee)
        {
            var updateResult = await _genericRepository.UpdateTAsync(employee);

            if (updateResult == 0)
            {
                return NotFound("No Employee was updated");
            }

            var updatedEmployee = await _genericRepository.GetOneByAsync(emp => emp.Id == employee.Id);

            return Ok(updatedEmployee);
        }

        
        // DELETE: employees/id
        [HttpDelete("{id}")]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(IdValidator))]
        public async Task<ActionResult<Employee>> Delete(int id)
        {
            Employee employee = await _genericRepository.GetOneByAsync(emp => emp.Id == id);

            if (employee is null)
            {
                return NotFound("No Employee was found for the provided id");
            }

            var deleteResult = await _genericRepository.DeleteTAsync(employee);

            return Ok(deleteResult);
        }
    }
}
