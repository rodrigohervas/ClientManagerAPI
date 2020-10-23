using ClientsManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.Tests.TestData
{
    public class EmployeesData
    {
        public static IEnumerable<Employee> getTestEmployees()
        {
            return new List<Employee>()
            {
                new Employee
                {
                    Id = 1,
                    Name = "Test Employee 1",
                    EmployeeType_Id = 1,
                    Position = "Software Engineer"
                },
                new Employee
                {
                    Id = 2,
                    Name = "Test Employee 2",
                    EmployeeType_Id = 1,
                    Position = "DevOps Engineer"
                },
                new Employee
                {
                    Id = 3,
                    Name = "Test Employee 3",
                    EmployeeType_Id = 2,
                    Position = "Engineering Senior Manager"
                }
            };
        }
    }
}
