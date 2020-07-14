using ClientsManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClientsManager.Data
{
    /// <summary>
    /// ITimeFrameRepository implementation class
    /// </summary>
    public class TimeFrameRepository : ITimeFrameRepository
    {
        /// <summary>
        /// DbContext for DI
        /// </summary>
        private ClientsManagerDBContext _context;

        /// <summary>
        /// DbContext DI constructor injected
        /// </summary>
        /// <param name="context">ClientsManagerDBContext</param>
        public TimeFrameRepository(ClientsManagerDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Empty constructor for testing
        /// </summary>
        //public TimeFrameRepository() {}

        /// <summary>
        /// Gets all timeframes
        /// </summary>
        /// <returns> Task<IEnumerable<TimeFrame>> </returns>
        public async Task<IEnumerable<TimeFrame>> GetAllTimeFramesAsync()
        {
            return await _context.TimeFrames.ToListAsync();
        }

        /// <summary>
        /// Gets all timeframes for an employee_id
        /// </summary>
        /// <param name="employee_id">int</param>
        /// <returns> Task<IEnumerable<TimeFrame>> </returns>
        public async Task<IEnumerable<TimeFrame>> GetTimeFramesByEmployeeIdAsync(int employee_id)
        {
            return await _context.TimeFrames
                                   .Where(timeFrame => timeFrame.Employee_Id == employee_id)
                                   .ToListAsync();
        }

        /// <summary>
        /// Gets a timeframe for an id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Task<TimeFrame> </returns>
        public async Task<TimeFrame> GetTimeFrameByIdAsync(int id)
        {
            return await _context.TimeFrames.FindAsync(id);
        }

        public async Task<IEnumerable<TimeFrame>> GetByAsync(Expression<Func<TimeFrame, bool>> searchCriteria)
        {
            return await _context.TimeFrames.Where(searchCriteria).ToListAsync();
        }

        /// <summary>
        /// Adds a TimeFrame to the DB
        /// </summary>
        /// <param name="timeFrame">the TimeFrame object to add</param>
        /// <returns>Task<TimeFrame> - The TimeFrame created</returns>
        public async Task<TimeFrame> AddTimeFrameAsync(TimeFrame timeFrame)
        {
            if (timeFrame is null) 
            {
                throw new Exception("The TimeFrame is mandatory");
            }

            _context.TimeFrames.Add(timeFrame);
            await _context.SaveChangesAsync();
            return await _context.TimeFrames.FirstAsync(timeframe => timeframe.Title == timeFrame.Title);
        }

        /// <summary>
        /// Updates an existing TimeFrame
        /// </summary>
        /// <param name="timeFrame">the TimeFrame object to update</param>
        /// <returns>Task<TimeFrame> - The TimeFrame updated</returns>
        public async Task<TimeFrame> UpdateTimeFrameAsync(TimeFrame timeFrame)
        {
            if (timeFrame is null)
            {
                throw new Exception("The TimeFrame is mandatory");
            }

            _context.TimeFrames.Update(timeFrame);
            await _context.SaveChangesAsync();
            return timeFrame;
        }

        /// <summary>
        /// Deletes an existing TimeFrame
        /// </summary>
        /// <param name="id">The id of the TimeFrame to remove</param>
        /// <returns>Task<int> - the number of timeFrames deleted</returns>
        public async Task<int> DeleteTimeFrameAsync(int id)
        {
            if (id == 0)
            {
                throw new Exception("The TimeFrame is mandatory");
            }

            var timeFrame = _context.TimeFrames.First(tf => tf.Id == id);

            if (timeFrame is null)
            {
                throw new Exception("There's no TimeFrame for the Id");
            }

            _context.TimeFrames.Remove(timeFrame);
            return await _context.SaveChangesAsync();
        }
    }
}
