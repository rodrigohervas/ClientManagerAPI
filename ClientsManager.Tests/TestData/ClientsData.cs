using ClientsManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.Tests.TestData
{
    public class ClientsData
    {
        public static IEnumerable<Client> GetTestClients()
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
                    Website = "https://www.testcompany3.com", 
                    LegalCases = new List<LegalCase>() 
                    {
                        new LegalCase() {
                            Id = 1,
                            Client_Id = 3,
                            Title = "Contract write-up",
                            Description = "Leasingh contract model",
                            TrustFund = 5500
                        },
                        new LegalCase() {
                            Id = 2,
                            Client_Id = 3,
                            Title = "Deposition",
                            Description = "N case deposition for Mr. Doe",
                            TrustFund = 4850
                        },
                        new LegalCase() {
                            Id = 3,
                            Client_Id = 3,
                            Title = "leasing agreement",
                            Description = "leasing agreement review",
                            TrustFund = 955
                        }
                    },
                    Addresses = new List<Address>()
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
                    },
                    Contacts = new List<Contact>()
                    {
                        new Contact()
                        {
                            Id = 1,
                            Client_Id = 4,
                            Address_Id = 1,
                            Name = "Myca Coslett",
                            Position = "Legal Assistant",
                            Telephone = "792-438-4570",
                            Cellphone = "572-634-0767",
                            Email = "mcoslett0@testcompany4.com"
                        },
                        new Contact()
                        {
                            Id = 2,
                            Client_Id = 4,
                            Address_Id = 2,
                            Name = "Dewitt Dioniso",
                            Position = "Project Manager",
                            Telephone = "943-333-2330",
                            Cellphone = "866-685-4130",
                            Email = "ddioniso1@testcompany4.com"
                        }
                    }
                }
            };
        }
    }
}
