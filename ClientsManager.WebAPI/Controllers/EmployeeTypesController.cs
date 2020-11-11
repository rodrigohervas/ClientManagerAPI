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
    [Produces("application/json")]
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
        /// <returns>Task&lt;ActionResult&lt;IEnumerable&lt;EmployeeTypeDTO&gt;&gt;&gt; - A list of all the EmployeeTypes</returns>
        /// <![CDATA[ <returns>Task<ActionResult<IEnumerable<EmployeeTypeDTO>>> - A list of all the EmployeeTypes</returns> ]]>
        // GET: api/employeetypes
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<EmployeeTypeDTO>>> GetAllEmployeeTypesAsync()
        {
            var employeeTypeList = await _genericRepository.GetAllAsync();

            if (!employeeTypeList.Any())
            {
                var logData = new
                {
                    Action = ControllerContext.ActionDescriptor.DisplayName,
                    Verb = HttpContext.Request.Method,
                    EndpointPath = HttpContext.Request.Path.Value,
                    User = HttpContext.User.Claims.First(usr => usr.Type == "preferred_username").Value
                };
                _logger.LogInformation("No EmployeeTypes were found. Data: {@logData}", logData);

                return NotFound("No EmployeeTypes were found");
            }

            IEnumerable<EmployeeTypeDTO> employeeTypesDTO = _mapper.Map<IEnumerable<EmployeeTypeDTO>>(employeeTypeList);

            return Ok(employeeTypesDTO);
        }

        /// <summary>
        /// Gets an EmployeeType for a provided id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Task&lt;ActionResult&lt;EmployeeTypeDTO&gt;&gt; - An EmployeeType</returns>
        /// <![CDATA[ <returns>Task<ActionResult<EmployeeTypeDTO>> - An EmployeeType</returns> ]]>
        // GET api/employeetypes/5
        [HttpGet("{id:int}")]
        [ServiceFilter(typeof(IdValidator))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmployeeTypeDTO>> GetEmployeeTypeByIdAsync([FromRoute] int id)
        {
            var employeeType = await _genericRepository.GetOneByAsync(et => et.Id == id);

            if (employeeType is null)
            {
                var logData = new
                {
                    Id = id,
                    Action = ControllerContext.ActionDescriptor.DisplayName,
                    Verb = HttpContext.Request.Method,
                    EndpointPath = HttpContext.Request.Path.Value,
                    User = HttpContext.User.Claims.First(usr => usr.Type == "preferred_username").Value
                };
                _logger.LogInformation("No EmployeeTypes were found for the id {id}. Data: {@logData}", id, logData);

                return NotFound("No data was found");
            }

            EmployeeTypeDTO employeeTypeDTO = _mapper.Map<EmployeeTypeDTO>(employeeType);

            return Ok(employeeTypeDTO);
        }

        /// <summary>
        /// Creates an EmployeeType
        /// </summary>
        /// <param name="employeeType"> The EmployeeType object to create</param>
        /// <returns>Task&lt;ActionResult&lt;EmployeeTypeDTO&gt;&gt; - The created EmployeeType</returns>
        /// <![CDATA[ <returns>Task<ActionResult<EmployeeTypeDTO>> - The created EmployeeType</returns> ]]>
        // POST api/employeetypes
        [HttpPost]
        [ServiceFilter(typeof(EmployeeTypeValidationFilter))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmployeeTypeDTO>> AddEmployeeType([FromBody] EmployeeType employeeType)
        {
            var addResult = await _genericRepository.AddTAsync(employeeType);

            if (addResult == 0)
            {
                var logData = new
                {
                    EmployeeType = employeeType,
                    Action = ControllerContext.ActionDescriptor.DisplayName,
                    Verb = HttpContext.Request.Method,
                    EndpointPath = HttpContext.Request.Path.Value,
                    User = HttpContext.User.Claims.First(usr => usr.Type == "preferred_username").Value
                };
                _logger.LogInformation("No EmployeeType was created for the employeeType {@employeeType}. Data: {@logData}", employeeType, logData);
                
                return NotFound("No EmployeeType was created");
            }

            var newEmployeeType = await _genericRepository.GetOneByAsync(et => et.Description == employeeType.Description);

            var employeeTypeDTO = _mapper.Map<EmployeeTypeDTO>(newEmployeeType);

            return Created("", employeeTypeDTO);
        }


        /// <summary>
        /// Updates an existing EmployeeType
        /// </summary>
        /// <param name="id">int - the employeeType id</param>
        /// <param name="employeeType">The EmployeeType object to update</param>
        /// <returns>Task&lt;ActionResult&lt;EmployeeTypeDTO&gt;&gt; - The updated EmployeeType</returns>
        /// <![CDATA[ <returns>Task<ActionResult<EmployeeTypeDTO>> - The updated EmployeeType</returns> ]]>
        // PUT/PATCH api/employeetypes/5
        [HttpPut("{id:int}")]
        [HttpPatch("{id:int}")]
        [ServiceFilter(typeof(EmployeeTypeValidationFilter))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmployeeTypeDTO>> UpdateEmployeeType([FromRoute] int id, [FromBody] EmployeeType employeeType)
        {
            var employeeTypeResult = await _genericRepository.GetOneByAsync(et => et.Description == employeeType.Description &&
                                                                          et.Id == employeeType.Id);

            if (employeeTypeResult is null)
            {
                var logData = new
                {
                    Id = id, 
                    EmployeeType = employeeType,
                    Action = ControllerContext.ActionDescriptor.DisplayName,
                    Verb = HttpContext.Request.Method,
                    EndpointPath = HttpContext.Request.Path.Value,
                    User = HttpContext.User.Claims.First(usr => usr.Type == "preferred_username").Value
                };
                _logger.LogInformation("No EmployeeType was updated for the Id {id} and employeeType {@employeeType}. Data: {@logData}", id, employeeType, logData);
                
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
        /// <returns>Task&lt;ActionResult&lt;int&gt;&gt; - The number of EmployeeTypes deleted</returns>
        /// <![CDATA[ <returns>Task<ActionResult<int>> - The number of EmployeeTypes deleted</returns> ]]>
        // DELETE api/employeetypes/5
        [HttpDelete("{id:int}")]
        [ServiceFilter(typeof(IdValidator))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> DeleteEmployeeType([FromRoute] int id)
        {
            var employeeType = await _genericRepository.GetOneByAsync(et => et.Id == id);

            if (employeeType is null)
            {
                var logData = new
                {
                    Id = id,
                    Action = ControllerContext.ActionDescriptor.DisplayName,
                    Verb = HttpContext.Request.Method,
                    EndpointPath = HttpContext.Request.Path.Value,
                    User = HttpContext.User.Claims.First(usr => usr.Type == "preferred_username").Value
                };
                _logger.LogInformation("No EmployeeType was found for the Id {id}. Data: {@logData}", id, logData);
                _logger.LogError($"EmployeeTypesController.DeleteEmployeeType: No EmployeeType was found for the provided id {id}");
                return NotFound("No EmployeeType was found for the provided id");
            }

            var deleteResult = await _genericRepository.DeleteTAsync(employeeType);

            return Ok(deleteResult);
        }
    }
}
