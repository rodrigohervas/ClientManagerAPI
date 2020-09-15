using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ClientsManager.Data;
using ClientsManager.Models;
using ClientsManager.WebAPI.DTOs;
using ClientsManager.WebAPI.ValidationActionFiltersMiddleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ClientsManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeTypesController : ControllerBase
    {
        private readonly IGenericRepository<EmployeeType> _genericRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public EmployeeTypesController(IGenericRepository<EmployeeType> genericRepository, IMapper mapper, ILogger<EmployeeTypesController> logger)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _logger = logger;
        }


        /// <summary>
        /// Gets all EmployeeTypes
        /// </summary>
        /// <returns>Task<ActionResult<IEnumerable<EmployeeTypeDTO>>> - A list of all the EmployeeTypes</returns>
        // GET: api/employeetypes?pageNumber=2&pageSize=3
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeTypeDTO>>> GetAllEmployeeTypesAsync()
        {
            var employeeTypeList = await _genericRepository.GetAllAsync();

            if (!employeeTypeList.Any())
            {
                _logger.LogError($"EmployeeTypesController.GetAllEmployeeTypesAsync: No EmployeeTypes were found");
                return NotFound("No EmployeeTypes were found");
            }

            IEnumerable<EmployeeTypeDTO> employeeTypesDTO = _mapper.Map<IEnumerable<EmployeeTypeDTO>>(employeeTypeList);

            return Ok(employeeTypesDTO);
        }

        /// <summary>
        /// Gets an EmployeeType for a provided id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Task<ActionResult<EmployeeTypeDTO>> - An EmployeeType</returns>
        // GET api/employeetypes/5
        [HttpGet("{id:int}")]
        [ServiceFilter(typeof(IdValidator))]
        public async Task<ActionResult<EmployeeTypeDTO>> GetEmployeeTypeByIdAsync(int id)
        {
            var employeeType = await _genericRepository.GetOneByAsync(et => et.Id == id);

            if (employeeType is null)
            {
                _logger.LogError($"EmployeeTypesController.GetEmployeeTypeByIdAsync: No data was found for the id {id}");
                return NotFound("No data was found");
            }

            EmployeeTypeDTO employeeTypeDTO = _mapper.Map<EmployeeTypeDTO>(employeeType);

            return Ok(employeeTypeDTO);
        }

        /// <summary>
        /// Creates an EmployeeType
        /// </summary>
        /// <param name="employeeType"> The EmployeeType object to create</param>
        /// <returns>Task<ActionResult<EmployeeTypeDTO>> - The created EmployeeType</returns>
        // POST api/employeetypes
        [HttpPost]
        [ServiceFilter(typeof(EmployeeTypeValidationFilter))]
        public async Task<ActionResult<EmployeeTypeDTO>> AddEmployeeType(EmployeeType employeeType)
        {
            var addResult = await _genericRepository.AddTAsync(employeeType);

            if (addResult == 0)
            {
                _logger.LogError($"EmployeeTypesController.AddEmployeeType: No EmployeeType was created for employeeType {employeeType}");
                return NotFound("No EmployeeType was created");
            }

            var newEmployeeType = await _genericRepository.GetOneByAsync(et => et.Description == employeeType.Description);

            var employeeTypeDTO = _mapper.Map<EmployeeTypeDTO>(newEmployeeType);

            return Created("", employeeTypeDTO);
        }


        /// <summary>
        /// Updates an existing EmployeeType
        /// </summary>
        /// <param name="employeeType">The EmployeeType object to update</param>
        /// <returns>Task<ActionResult<EmployeeTypeDTO>> - The updated EmployeeType</returns>
        // PUT/PATCH api/employeetypes/5
        [HttpPut("{id:int}")]
        [HttpPatch("{id:int}")]
        [ServiceFilter(typeof(EmployeeTypeValidationFilter))]
        public async Task<ActionResult<EmployeeTypeDTO>> UpdateEmployeeType(EmployeeType employeeType)
        {
            var employeeTypeResult = await _genericRepository.GetOneByAsync(et => et.Description == employeeType.Description &&
                                                                          et.Id == employeeType.Id);

            if (employeeTypeResult is null)
            {
                _logger.LogError($"EmployeeTypesController.UpdateEmployeeType: No EmployeeType was updated for employeeType {employeeType}");
                return NotFound("No EmployeeType was updated");
            }
            
            var updateResult = await _genericRepository.UpdateTAsync(employeeType);
            
            var employeeTypeDTO = _mapper.Map<EmployeeTypeDTO>(employeeType);

            return Ok(employeeTypeDTO);
        }


        /// <summary>
        /// Deletes an existing EmployeeType
        /// </summary>
        /// <param name="id">The if of the EmployeeType object to delete</param>
        /// <returns>Task<ActionResult<int>> - The number of EmployeeTypes deleted</returns>
        // DELETE api/employeetypes/5
        [HttpDelete("{id:int}")]
        [ServiceFilter(typeof(IdValidator))]
        public async Task<ActionResult<int>> DeleteEmployeeType(int id)
        {
            var employeeType = await _genericRepository.GetOneByAsync(et => et.Id == id);

            if (employeeType is null)
            {
                _logger.LogError($"EmployeeTypesController.DeleteEmployeeType: No EmployeeType was found for the provided id {id}");
                return NotFound("No EmployeeType was found for the provided id");
            }

            var deleteResult = await _genericRepository.DeleteTAsync(employeeType);

            return Ok(deleteResult);
        }
    }
}
