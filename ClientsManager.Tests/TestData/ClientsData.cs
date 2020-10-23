using ClientsManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.Tests.TestData
{
    public class ClientsData
    {
        public static IEnumerable<Client> getTestClients()
        {
            return new List<Client> {
                new Client()
                {
                    Id = 1,
                    Name = "Test Company 1",
                    Description = "test Company description"
                },
                new Client()
                {
                    Id = 2,
                    Name = "Test Company 2",
                    Description = "Test Company 2 Description",
                    Website = "https://www.testcompany2.com"
                },
                new Client()
                {
                    Id = 3,
                    Name = "Test Company 3",
                    Description = "Test Company 3 Description",
                    Website = "https://www.testcompany3.com"
                },
                new Client()
                {
                    Id = 4,
                    Name = "Test Company 4",
                    Description = "Test Company 4 Description",
                    Website = "https://www.testcompany4.com"
                }
            };
        }
    }
}
