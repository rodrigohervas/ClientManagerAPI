using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ClientsManager.Models
{
    public class EmployeeType
    {
        public int Id { get; set; }

        public string Description { get; set; }



        //Prop for relationship with Employee
        public ICollection<Employee> Employees { get; set; }
    }
}
