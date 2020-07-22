using ClientsManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.DTOs
{
    /// <summary>
    /// DTO class for an Employee with BillableActivities
    /// </summary>
    public class EmployeeWithBillableActivitiesDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int EmployeeType_Id { get; set; }

        public string Position { get; set; }

        public ICollection<BillableActivityDTO> BillableActivities { get; set; }
    }
}
