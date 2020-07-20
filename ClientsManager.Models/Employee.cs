using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ClientsManager.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //FK from EmployeeType
        public int EmployeeType_Id { get; set; }

        public string Position { get; set; }



        //Prop for relationship with EmployeeType
        public EmployeeType EmployeeType { get; set; }

        //Prop for relationship with TimeFrame
        public ICollection<TimeFrame> TimeFrames { get; set; }
    }
}
