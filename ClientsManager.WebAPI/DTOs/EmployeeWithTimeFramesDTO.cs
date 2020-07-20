using ClientsManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.DTOs
{
    /// <summary>
    /// DTO class for an Employee with TimeFrames
    /// </summary>
    public class EmployeeWithTimeFramesDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int EmployeeType_Id { get; set; }

        public string Position { get; set; }

        public ICollection<TimeFrameDTO> TimeFrames { get; set; }
    }
}
