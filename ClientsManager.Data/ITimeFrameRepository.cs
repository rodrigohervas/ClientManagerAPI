﻿using ClientsManager.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientsManager.Data
{
    /// <summary>
    /// Interface that all TimeFrameRepository classes must implement
    /// </summary>
    public interface ITimeFrameRepository
    {
        Task<IEnumerable<TimeFrame>> GetAllTimeFramesAsync();
        Task<IEnumerable<TimeFrame>> GetTimeFramesByEmployeeIdAsync(int employee_id);
        Task<TimeFrame> GetTimeFrameByIdAsync(int id);
        Task<TimeFrame> AddTimeFrameAsync(TimeFrame timeFrame);
        Task<TimeFrame> UpdateTimeFrameAsync(TimeFrame timeFrame);
        Task<int> DeleteTimeFrameAsync(int id);

    }
}
