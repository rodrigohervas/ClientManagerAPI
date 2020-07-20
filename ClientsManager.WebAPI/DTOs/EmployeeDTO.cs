using ClientsManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.DTOs
{
    /// <summary>
    /// DTO class for an Employee
    /// </summary>
    public class EmployeeDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int EmployeeType_Id { get; set; }

        public string Position { get; set; }
    }
}
