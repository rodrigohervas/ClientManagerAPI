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
        private IGenericRepository<TimeFrame> _genericRepository;

        /// <summary>
        /// DI constructor injection of Respository
        /// </summary>
        /// <param name="timeFrameRepository">ITimeFrameRepository type</param>
        public TimeFramesController(ITimeFrameRepository timeFrameRepository, IGenericRepository<TimeFrame> genericRepository)
        {
            _timeFrameRepository = timeFrameRepository;
            _genericRepository = genericRepository;

        }

        /// <summary>
        /// Async returns a list of all the available TimeFrames
        /// </summary>
        /// <returns>Task<ActionResult<IEnumerable<TimeFrame>>> - A list of all the TimeFrames</returns>
        //GET: api/TimeFrames
        [HttpGet]
        //public async Task<IActionResult> GetAllTimeFramesAsync() 
        public async Task<ActionResult<IEnumerable<TimeFrame>>> GetAllTimeFramesAsync()
        {
            try
            {
                var timeFrames = await _genericRepository.GetAllAsync();

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
        /// Async returns the number of timeframes in the DB
        /// </summary>
        /// <returns>Task<ActionResult<int>> - The total number of TimeFrames</returns>
        //GET: api/timeframes/count/all
        [HttpGet("count/all")]
        public async Task<ActionResult<int>> CountTimeFrames()
        {
            var timeFrames = await _genericRepository.CountAsync();

            if (timeFrames == 0)
            {
                return NotFound("No TimeFrames available");
            }

            return Ok(timeFrames);
        }

        /// <summary>
        /// Async returns a list of TimeFrames for a provided employee_id
        /// </summary>
        /// <param name="employee_id">Integer - TimeFrame employee_id identifier in DB</param>
        /// <returns>Task<ActionResult<IEnumerable<TimeFrame>>> - A list of TimeFrames</returns>
        //GET: api/TimeFrames/employee/1
        [HttpGet("employees/{employee_id}")]
        [ServiceFilter(typeof(EmployeeIdValidator))]
        public async Task<ActionResult<IEnumerable<TimeFrame>>> GetTimeFramesByEmployeeIdAsync(int employee_id)
        {
            var timeFrames = await _genericRepository.GetByAsync(tf => tf.Employee_Id == employee_id);

            if (!timeFrames.Any())
            {
                return NotFound("No data was found for the employee");
            }

            return Ok(timeFrames);
        }

        /// <summary>
        /// Async returns a TimeFrame for a provided id
        /// </summary>
        /// <param name="id">Integer - TimeFrame id identifier in DB</param>
        /// <returns>Task<ActionResult<TimeFrame>> - A TimeFrame corresponding to the TimeFrame id</returns>
        //GET: api/TimeFrames/4
        [HttpGet("{id}")]
        [ServiceFilter(typeof(IdValidator))]
        public async Task<ActionResult<TimeFrame>> GetTimeFrameByIdAsync(int id)
        {
            var timeFrame = await _genericRepository.GetOneByAsync(tf => tf.Id == id);

            if (timeFrame is null) {
                return NotFound("No data was found for the id");
            }

            return Ok(timeFrame);
        }

        /// <summary>
        /// Async Creates a TimeFrame
        /// </summary>
        /// <param name="timeFrame">A TimeFrame object</param>
        /// <returns>Task<ActionResult<TimeFrame>> - The TimeFrame created</returns>
        [HttpPost()]
        [ServiceFilter(typeof(TimeFrameValidationFilter))]
        public async Task<ActionResult<TimeFrame>> AddTimeFrameAsync(TimeFrame timeFrame)
        {
            int created = await _genericRepository.AddTAsync(timeFrame);

            if (created == 0)
            {
                return NotFound("No TimeFrame was created");
            }

            var newTimeFrame = await _genericRepository.GetOneByAsync(tf =>
                                    tf.Title == timeFrame.Title &&
                                    tf.Employee_Id == timeFrame.Employee_Id);

            return Created("", newTimeFrame);
        }

        /// <summary>
        /// Async Updates an existing TimeFrame
        /// </summary>
        /// <param name="timeFrame">A TimeFrame object</param>
        /// Task<ActionResult<TimeFrame>> - The updated TimeFrame</returns>
        [HttpPatch("{id}")]
        [HttpPut("{id}")]
        [ServiceFilter(typeof(TimeFrameValidationFilter))]
        public async Task<ActionResult<TimeFrame>> UpdateTimeFrameAsync(int id, TimeFrame timeFrame)
        {
            int updated = await _genericRepository.UpdateTAsync(timeFrame);

            if (updated == 0)
            {
                return NotFound("No TimeFrame was updated");
            }

            var updatedTimeFrame = _genericRepository.GetOneByAsync(tf => tf.Id == timeFrame.Id);

            return Ok(updatedTimeFrame);
        }

        /// <summary>
        /// Async Deletes an existing TimeFrame
        /// </summary>
        /// <param name="id">int - The TimeFrame Id</param>
        /// <returns>Task<ActionResult<int>> - The number of TimeFrames deleted</returns>
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(IdValidator))]
        public async Task<ActionResult<int>> DeleteTimeFrameAsync(int id)
        {
            TimeFrame timeFrame = await _genericRepository.GetOneByAsync(tf => tf.Id == id);

            if (timeFrame is null)
            {
                return NotFound("No data was found for the id");
            }

            var timeFramesDeleted = await _genericRepository.DeleteTAsync(timeFrame);

            return Ok(timeFramesDeleted);
        }
    }
}
