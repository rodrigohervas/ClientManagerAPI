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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClientsManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeTypesController : ControllerBase
    {
        private readonly IGenericRepository<EmployeeType> _genericRepository;
        private readonly IMapper _mapper;

        public EmployeeTypesController(IGenericRepository<EmployeeType> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }


        /// <summary>
        /// Gets all EmployeeTypes
        /// </summary>
        /// <returns>Task<ActionResult<IEnumerable<EmployeeTypeDTO>>> - A list of all the EmployeeTypes</returns>
        // GET: api/employeetypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeTypeDTO>>> GetAllEmployeeTypesAsync()
        {
            var employeeTypeList = await _genericRepository.GetAllAsync();

            if (!employeeTypeList.Any())
            {
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
                return NotFound("No data was found for the id");
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
            var updateResult = await _genericRepository.UpdateTAsync(employeeType);

            if (updateResult == 0)
            {
                return NotFound("No EmployeeType was updated");
            }

            var updatedEmployeeType = await _genericRepository.GetOneByAsync(et => et.Description == employeeType.Description && 
                                                                          et.Id == employeeType.Id);

            var employeeTypeDTO = _mapper.Map<EmployeeTypeDTO>(updatedEmployeeType);

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
                return NotFound("No EmployeeType was found for the provided id");
            }

            var deleteResult = await _genericRepository.DeleteTAsync(employeeType);

            return Ok(deleteResult);
        }
    }
}
