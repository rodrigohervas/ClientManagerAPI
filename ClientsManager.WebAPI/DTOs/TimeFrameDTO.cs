using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.DTOs
{
    /// <summary>
    /// DTO class for a TimeFrame
    /// </summary>
    public class TimeFrameDTO
    {
        public int Id { get; set; }

        public int Employee_Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime Start_DateTime { get; set; }

        public DateTime Finish_DateTime { get; set; }
    }
}
