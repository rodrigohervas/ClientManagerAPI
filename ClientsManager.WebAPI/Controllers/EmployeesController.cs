using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ClientsManager.Data;
using ClientsManager.Models;
using ClientsManager.WebAPI.DTOs;
using ClientsManager.WebAPI.ValidationActionFiltersMiddleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ClientsManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly IGenericRepository<Employee> _genericRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public EmployeesController(IGenericRepository<Employee> genericRepository, IMapper mapper, ILogger<EmployeesController> logger)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _logger = logger;
        }


        /// <summary>
        /// Get all Employees for the paging parameters
        /// </summary>
        /// <param name="parameters">Paging parameters</param>
        /// <returns>Task<ActionResult<IEnumerable<EmployeeDTO>>> - A list of all the Employees</returns>
        // GET: api/employees?pageNumber=2&pageSize=3
        [HttpGet]
        [ServiceFilter(typeof(QueryStringParamsValidator))]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetAllEmployeesAsync([FromQuery] QueryStringParameters parameters)
        {
            var employees = await _genericRepository.GetAllPagedAsync(em => em.EmployeeType, parameters);

            if (!employees.Any())
            {
                var logData = new
                {
                    Parameters = parameters,
                    Action = ControllerContext.ActionDescriptor.DisplayName,
                    Verb = HttpContext.Request.Method,
                    EndpointPath = HttpContext.Request.Path.Value,
                    User = HttpContext.User.Claims.First(usr => usr.Type == "preferred_username").Value
                };
                _logger.LogInformation("No Employees were found for Parameters {@parameters}. Data: {@logData}", parameters, logData);

                return NotFound("No Employees were found");
            }

            IEnumerable<EmployeeDTO> employeeList = _mapper.Map<IEnumerable<EmployeeDTO>>(employees);

            return Ok(employeeList);
        }

        /// <summary>
        /// Gets an Employee for a provided id
        /// </summary>
        /// <param name="id">int - the Employee id</param>
        /// <returns>Task<ActionResult<EmployeeDTO>> -  An Employee object</returns>
        // GET: api/employees/5
        [HttpGet("{id}")]
        [ServiceFilter(typeof(IdValidator))]
        public async Task<ActionResult<EmployeeDTO>> GetEmployeeByIdAsync([FromRoute] int id)
        {
            var employee = await _genericRepository.GetOneByWithRelatedDataAsync(employee => employee.Id == id, 
                                                                                 employee => employee.BillableActivities);

            if (employee is null)
            {
                var logData = new
                {
                    Id = id,
                    Action = ControllerContext.ActionDescriptor.DisplayName,
                    Verb = HttpContext.Request.Method,
                    EndpointPath = HttpContext.Request.Path.Value,
                    User = HttpContext.User.Claims.First(usr => usr.Type == "preferred_username").Value
                };
                _logger.LogInformation("No Employee was found for Id {id}. Data: {@logData}", id, logData);

                return NotFound("No data was found");
            }

            EmployeeWithBillableActivitiesDTO employeeTF = _mapper.Map<EmployeeWithBillableActivitiesDTO>(employee);

            return Ok(employeeTF);
        }

        /// <summary>
        /// Gets a list of Employees for a provided employee type
        /// </summary>
        /// <param name="employeeType_id">int - The EmployeeType id</param>
        /// <returns>Task<ActionResult<IEnumerable<EmployeeDTO>>> - A List of Employees</returns>
        // GET: api/employees/employeetype/1
        [HttpGet("employeetype/{employeetype_id:int}")]
        [ServiceFilter(typeof(EmployeeTypeIdValidator))]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployeesByTypeAsync([FromRoute] int employeeType_id)
        {
            var employees = await _genericRepository.GetByAsync(employee => employee.EmployeeType_Id == employeeType_id);

            if (!employees.Any())
            {
                var logData = new
                {
                    EmployeeType_id = employeeType_id,
                    Action = ControllerContext.ActionDescriptor.DisplayName,
                    Verb = HttpContext.Request.Method,
                    EndpointPath = HttpContext.Request.Path.Value,
                    User = HttpContext.User.Claims.First(usr => usr.Type == "preferred_username").Value
                };
                _logger.LogInformation("No Employees were found for EmployeeType_id {employeeType_id}. Data: {@logData}", employeeType_id, logData);

                return NotFound("No Employees were found");
            }

            IEnumerable<EmployeeDTO> employeeList = _mapper.Map<IEnumerable<EmployeeDTO>>(employees);

            return Ok(employeeList);
        }


        /// <summary>
        /// Creates an Employee
        /// </summary>
        /// <param name="employee"> Employee - An Employee object</param>
        /// <returns>Task<ActionResult<EmployeeDTO>> - The created Employee</returns>
        // POST: api/employees
        [HttpPost]
        [ServiceFilter(typeof(EmployeeValidationFilter))]
        public async Task<ActionResult<EmployeeDTO>> AddEmployeeAsync([FromBody] Employee employee)
        {
            var addResult = await _genericRepository.AddTAsync(employee);

            if (addResult == 0)
            {
                var logData = new
                {
                    Employee = employee,
                    Action = ControllerContext.ActionDescriptor.DisplayName,
                    Verb = HttpContext.Request.Method,
                    EndpointPath = HttpContext.Request.Path.Value,
                    User = HttpContext.User.Claims.First(usr => usr.Type == "preferred_username").Value
                };
                _logger.LogInformation("No Employee was created for Employee {@employee}. Data: {@logData}", employee, logData);

                return NotFound("No Employee was created");
            }

            var newEmployee = await _genericRepository.GetOneByAsync(emp =>
                                                        emp.Name == employee.Name && 
                                                        emp.Position == employee.Position);

            EmployeeDTO employeeDto = _mapper.Map<EmployeeDTO>(newEmployee);

            return Created("", employeeDto);
        }


        /// <summary>
        /// Updates an existing Employee
        /// </summary>
        /// <param name="id"> int - the Employee id</param>
        /// <param name="employee"> Employee - An Employee object</param>
        /// <returns>Task<ActionResult<EmployeeDTO>> - The updated Employee</returns>
        // PUT/PATCH: api/employees/id
        [HttpPut("{id:int}")]
        [HttpPatch("{id:int}")]
        [ServiceFilter(typeof(EmployeeValidationFilter))]
        public async Task<ActionResult<EmployeeDTO>> UpdateEmployeeAsync([FromRoute] int id, [FromBody] Employee employee)
        {
            var employeeResult = await _genericRepository.GetOneByAsync(emp => emp.Id == employee.Id);

            if (employeeResult is null)
            {
                var logData = new
                {
                    Id = id, 
                    Employee = employee,
                    Action = ControllerContext.ActionDescriptor.DisplayName,
                    Verb = HttpContext.Request.Method,
                    EndpointPath = HttpContext.Request.Path.Value,
                    User = HttpContext.User.Claims.First(usr => usr.Type == "preferred_username").Value
                };
                _logger.LogInformation("No Employee was updated for id {id} and employee {@employee}. Data: {@logData}", id, employee, logData);
                
                return NotFound("No Employee was updated");
            }

            var updateResult = await _genericRepository.UpdateTAsync(employee);

            EmployeeDTO updatedEmployeeDto = _mapper.Map<EmployeeDTO>(employee);

            return Ok(updatedEmployeeDto);
        }

        /// <summary>
        /// Deletes an existing Employee
        /// </summary>
        /// <param name="employee"> int - The Employee id</param>
        /// <returns>Task<ActionResult<int>> - The number of Employees deleted</returns>
        // DELETE: employees/id
        [HttpDelete("{id:int}")]
        [ServiceFilter(typeof(IdValidator))]
        public async Task<ActionResult<int>> DeleteEmployeeAsync([FromRoute] int id)
        {
            Employee employee = await _genericRepository.GetOneByAsync(emp => emp.Id == id);

            if (employee is null)
            {
                var logData = new
                {
                    Id = id,
                    Action = ControllerContext.ActionDescriptor.DisplayName,
                    Verb = HttpContext.Request.Method,
                    EndpointPath = HttpContext.Request.Path.Value,
                    User = HttpContext.User.Claims.First(usr => usr.Type == "preferred_username").Value
                };
                _logger.LogInformation("No Employee was found for the provided id {id}. Data: {@logData}", id, logData);

                _logger.LogError($"EmployeesController.DeleteEmployeeAsync: No Employee was found for the provided id {id}");
                return NotFound("No Employee was found for the provided id");
            }

            var deleteResult = await _genericRepository.DeleteTAsync(employee);

            return Ok(deleteResult);
        }
    }
}
