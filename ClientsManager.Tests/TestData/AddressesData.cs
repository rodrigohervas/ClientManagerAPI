using ClientsManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.Tests.TestData
{
    public class AddressesData
    {
        public static IEnumerable<Address> getTestAddresses()
        {
            return new List<Address>()
            {
                new Address()
                {
                    Id = 1,
                    Client_Id = 4,
                    StreetNumber = "44 4th Drive",
                    City = "Silver Spring",
                    StateProvince = "Maryland",
                    ZipCode = "20811",
                    Country = "United States"
                },
                new Address()
                {
                    Id = 2,
                    Client_Id = 4,
                    StreetNumber = "4444 Mayer Point",
                    City = "Saint Joseph",
                    StateProvince = "Missouri",
                    ZipCode = "20812",
                    Country = "United States"
                }
            };
        }
    }
}
