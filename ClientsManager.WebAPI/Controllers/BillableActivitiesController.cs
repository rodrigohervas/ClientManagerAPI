using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// <summary>
    /// BillableActivities controller class
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BillableActivitiesController : ControllerBase
    {
        /// <summary>
        /// IGenericRepository<BillableActivity> for DI
        /// </summary>
        private readonly IGenericRepository<BillableActivity> _genericRepository;
        private readonly IMapper _mapper;


        /// <summary>
        /// DI constructor injection of Respository
        /// </summary>
        /// <param name="genericRepository"> GenericRepository object</param>
        /// <param name="mapper">AutoMapper Imapper object</param>
        public BillableActivitiesController(IGenericRepository<BillableActivity> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }


        /// <summary>
        /// Async returns a list of all the available BillableActivities
        /// </summary>
        /// <returns>Task<ActionResult<IEnumerable<BillableActivity>>> - A list of all the BillableActivities</returns>
        //GET: api/billableactivities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BillableActivity>>> GetAllBillableActivitiesAsync()
        {
            try
            {
                var billableActivities = await _genericRepository.GetAllAsync();

                if (!billableActivities.Any())
                {
                    return NotFound("No billable activities were found");
                }

                IEnumerable<BillableActivityDTO> billableActivitiesDTO = _mapper.Map<IEnumerable<BillableActivityDTO>>(billableActivities);

                return Ok(billableActivitiesDTO);
            }
            catch
            {
                throw new Exception("Couldn't find BillableActivities");
            }
        }

        /// <summary>
        /// Async returns the number of billable activities in the DB
        /// </summary>
        /// <returns>Task<ActionResult<int>> - The total number of BillableActivities</returns>
        //GET: api/billableactivities/count/all
        [HttpGet("count/all")]
        public async Task<ActionResult<int>> CountBillableActivities()
        {
            var billableActivitiesNumber = await _genericRepository.CountAsync();

            if (billableActivitiesNumber == 0)
            {
                return NotFound("No billable activities available");
            }

            return Ok(billableActivitiesNumber);
        }

        /// <summary>
        /// Async returns a list of BillableActivities for a provided employee_id
        /// </summary>
        /// <param name="employee_id">Integer - BillableActivity employee_id identifier in DB</param>
        /// <returns>Task<ActionResult<IEnumerable<BillableActivity>>> - A list of BillableActivities</returns>
        //GET: api/billableactivities/employee/1
        [HttpGet("employees/{employee_id:int}")]
        [ServiceFilter(typeof(EmployeeIdValidator))]
        public async Task<ActionResult<IEnumerable<BillableActivityDTO>>> GetBillableActivitiesByEmployeeIdAsync(int employee_id)
        {
            var billableActivities = await _genericRepository.GetByAsync(ba => ba.Employee_Id == employee_id);

            if (!billableActivities.Any())
            {
                return NotFound("No data was found for the employee");
            }

            IEnumerable<BillableActivityDTO> billableActivitiesDTO = _mapper.Map<IEnumerable<BillableActivityDTO>>(billableActivities);

            return Ok(billableActivitiesDTO);
        }

        /// <summary>
        /// Async returns a list of BillableActivities for a provided case_id
        /// </summary>
        /// <param name="legalCase_id">Integer - BillableActivity case_id identifier in DB</param>
        /// <returns>Task<ActionResult<IEnumerable<BillableActivity>>> - A list of BillableActivities</returns>
        //GET: api/billableactivities/case/1
        [HttpGet("case/{legalCase_id:int}")]
        [ServiceFilter(typeof(LegalCaseIdValidator))]
        public async Task<ActionResult<IEnumerable<BillableActivityDTO>>> GetBillableActivitiesByLegalCaseIdAsync(int legalCase_id)
        {
            var billableActivities = await _genericRepository.GetByAsync(ba => ba.LegalCase_Id == legalCase_id);

            if (!billableActivities.Any())
            {
                return NotFound("No data was found for the case");
            }

            IEnumerable<BillableActivityDTO> billableActivitiesDTO = _mapper.Map<IEnumerable<BillableActivityDTO>>(billableActivities);

            return Ok(billableActivitiesDTO);
        }

        /// <summary>
        /// Async returns a BillableActivity for a provided id
        /// </summary>
        /// <param name="id">Integer - BillableActivity id identifier in DB</param>
        /// <returns>Task<ActionResult<BillableActivity>> - A BillableActivity corresponding to the BillableActivity id</returns>
        //GET: api/billableactivity/4
        [HttpGet("{id:int}")]
        [ServiceFilter(typeof(IdValidator))]
        public async Task<ActionResult<BillableActivityDTO>> GetBillableActivityByIdAsync(int id)
        {
            var billableActivity = await _genericRepository.GetOneByAsync(ba => ba.Id == id);

            if (billableActivity is null) {
                return NotFound("No data was found for the id");
            }

            BillableActivityDTO billableActivityDTO = _mapper.Map<BillableActivityDTO>(billableActivity);

            return Ok(billableActivityDTO);
        }

        /// <summary>
        /// Async Creates a BillableActivity
        /// </summary>
        /// <param name="billableActivity">BillableActivity - A BillableActivity object</param>
        /// <returns>Task<ActionResult<BillableActivity>> - The BillableActivity created</returns>
        [HttpPost()]
        [ServiceFilter(typeof(BillableActivityValidationFilter))]
        public async Task<ActionResult<BillableActivityDTO>> AddBillableActivityAsync(BillableActivity billableActivity)
        {
            int created = await _genericRepository.AddTAsync(billableActivity);

            if (created == 0)
            {
                return NotFound("No Billable Activity was created");
            }

            var newBillableActivity = await _genericRepository.GetOneByAsync(ba =>
                                    ba.Title == billableActivity.Title &&
                                    ba.Employee_Id == billableActivity.Employee_Id && 
                                    ba.LegalCase_Id == billableActivity.LegalCase_Id);

            BillableActivityDTO billableActivityDTO = _mapper.Map<BillableActivityDTO>(newBillableActivity);

            return Created("", billableActivityDTO);
        }

        /// <summary>
        /// Async Updates an existing BillableActivity
        /// </summary>
        /// <param name>int - The BillableActivity id</param>
        /// <param name="billableActivity">A BillableActivity object</param>
        /// Task<ActionResult<BillableActivity>> - The updated BillableActivity</returns>
        [HttpPatch("{id:int}")]
        [HttpPut("{id:int}")]
        [ServiceFilter(typeof(BillableActivityValidationFilter))]
        public async Task<ActionResult<BillableActivityDTO>> UpdateBillableActivityAsync(int id, BillableActivity billableActivity)
        {
            var billableActivityResult = await _genericRepository.GetOneByAsync(ba => ba.Id == billableActivity.Id);

            if (billableActivityResult is null)
            {
                return NotFound("No Billable Activity was updated");
            }
            
            int updated = await _genericRepository.UpdateTAsync(billableActivity);
            
            BillableActivityDTO billableActivityDTO = _mapper.Map<BillableActivityDTO>(billableActivity);

            return Ok(billableActivityDTO);
        }

        /// <summary>
        /// Async Deletes an existing BillableActivity
        /// </summary>
        /// <param name="id">int - The BillableActivity Id</param>
        /// <returns>Task<ActionResult<int>> - The number of BillableActivities deleted</returns>
        [HttpDelete("{id:int}")]
        [ServiceFilter(typeof(IdValidator))]
        public async Task<ActionResult<int>> DeleteBillableActivityAsync(int id)
        {
            BillableActivity billableActivity = await _genericRepository.GetOneByAsync(tf => tf.Id == id);

            if (billableActivity is null)
            {
                return NotFound("No data was found for the id");
            }

            var billableActivitiesDeleted = await _genericRepository.DeleteTAsync(billableActivity);

            return Ok(billableActivitiesDeleted);
        }
    }
}
