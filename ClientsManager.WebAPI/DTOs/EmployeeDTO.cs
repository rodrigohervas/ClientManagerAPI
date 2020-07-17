using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //FK from EmployeeType
        public int EmployeeType_Id { get; set; }

        public string Position { get; set; }
    }
}
