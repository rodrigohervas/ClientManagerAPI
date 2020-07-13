using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ClientsManager.Data;
using ClientsManager.Models;
using ClientsManager.WebAPI.ValidationActionFiltersMiddleware;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientsManager.WebAPI.Controllers
{
    /// <summary>
    /// TimeFrames controller class
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TimeFramesController : ControllerBase
    {
        /// <summary>
        /// ITimeFrameRepository for DI
        /// </summary>
        private ITimeFrameRepository _timeFrameRepository = null;

        /// <summary>
        /// DI constructor injection of Respository
        /// </summary>
        /// <param name="timeFrameRepository">ITimeFrameRepository type</param>
        public TimeFramesController(ITimeFrameRepository timeFrameRepository)
        {
            _timeFrameRepository = timeFrameRepository;
        }

        /// <summary>
        /// Async returns a list of all the available TimeFrames
        /// </summary>
        /// <returns>IEnumerable<TimeFrame></returns>
        //GET: api/TimeFrames
        [HttpGet]
        //public async Task<IActionResult> GetAllTimeFramesAsync() 
        public async Task<ActionResult<IEnumerable<TimeFrame>>> GetAllTimeFramesAsync()
        {
            try
            {
                var timeFrames = await _timeFrameRepository.GetAllTimeFramesAsync();

                if (!timeFrames.Any())
                {
                    return NotFound("No data was found");
                }

                return Ok(timeFrames);
            }
            catch
            {
                throw new Exception("Couldn't find TimeFrames");
            }
        }

        /// <summary>
        /// Async returns a list of TimeFrames for a provided employee_id
        /// </summary>
        /// <param name="employee_id">Integer - TimeFrame employee_id identifier in DB</param>
        /// <returns>IEnumerable<TimeFrame></returns>
        //GET: api/TimeFrames/employee/1
        [HttpGet("employee/{employee_id}")]

        public async Task<ActionResult<IEnumerable<TimeFrame>>> GetTimeFramesByEmployeeIdAsync(int employee_id)
        {
            try
            {
                if (employee_id == 0)
                {
                    return BadRequest("Employee_Id is mandatory");
                }

                var timeFrames = await _timeFrameRepository.GetTimeFramesByEmployeeIdAsync(employee_id);

                if (!timeFrames.Any())
                {
                    return NotFound("No data was found for the employee");
                }

                return Ok(timeFrames);
            }
            catch (Exception ex)
            {
                throw new Exception("There are no TimeFrames for the Employee_Id ");
            }
        }

        /// <summary>
        /// Async returns a TimeFrame for a provided id
        /// </summary>
        /// <param name="id">Integer - TimeFrame id identifier in DB</param>
        /// <returns>TimeFrame</returns>
        //GET: api/TimeFrames/4
        [HttpGet("{id}")]
        [ServiceFilter(typeof(IdValidator))]
        public async Task<ActionResult<TimeFrame>> GetTimeFrameByIdAsync(int id)
        {
            var timeFrame = await _timeFrameRepository.GetTimeFrameByIdAsync(id);

            if (timeFrame is null) {
                return NotFound("No data was found for the id");
            }

            return Ok(timeFrame);
        }

        /// <summary>
        /// Creates a TimeFrame
        /// </summary>
        /// <param name="timeFrame">A TimeFrame object</param>
        /// <returns>Task<ActionResult<TimeFrame>> - The TimeFrame created</returns>
        [HttpPost()]
        [ServiceFilter(typeof(TimeFrameValidationFilter))]
        public async Task<ActionResult<TimeFrame>> AddTimeFrameAsync(TimeFrame timeFrame)
        {
            var newTimeframe = await _timeFrameRepository.AddTimeFrameAsync(timeFrame);

            return Created("", newTimeframe);
        }

        /// <summary>
        /// Updates an existing TimeFrame
        /// </summary>
        /// <param name="timeFrame">A TimeFrame object</param>
        /// <returns>Task<ActionResult<TimeFrame>> - The updated TimeFrame</returns>
        [HttpPatch("{id}")]
        [HttpPut("{id}")]
        [ServiceFilter(typeof(TimeFrameValidationFilter))]
        public async Task<ActionResult<TimeFrame>> UpdateTimeFrameAsync(int id, TimeFrame timeFrame)
        {
            var updatedTimeFrame = await _timeFrameRepository.UpdateTimeFrameAsync(timeFrame);

            return Ok(updatedTimeFrame);
        }

        /// <summary>
        /// Deletes a TimeFrame
        /// </summary>
        /// <param name="id">int - The TimeFrame Id</param>
        /// <returns>Task<ActionResult<int>> - The number of TimeFrames deleted</returns>
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(IdValidator))]
        public async Task<ActionResult<int>> DeleteTimeFrameAsync(int id)
        {            
            var timeFramesDeleted = await _timeFrameRepository.DeleteTimeFrameAsync(id);

            return Ok(timeFramesDeleted + " TimeFrames deleted");
        }
    }
}
