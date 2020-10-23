using ClientsManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.Tests.TestData
{
    public class EmployeeTypesData
    {
        public static IEnumerable<EmployeeType> getTestEmployeeTypes()
        {
            return new List<EmployeeType>()
            {
                new EmployeeType
                {
                    Id = 1,
                    Description = "Base Test Level"
                },
                new EmployeeType
                {
                    Id = 2,
                    Description = "Manager test Level"
                }
            };
        }
    }
}
