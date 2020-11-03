using ClientsManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.Tests.TestData
{
    public class ContactsData
    {
        public static IEnumerable<Contact> getTestContacts()
        {
            return new List<Contact>()
            {
                new Contact()
                {
                    Id = 1,
                    Client_Id = 4,
                    Address_Id = 2,
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
                    Address_Id = 3,
                    Name = "Dewitt Dioniso",
                    Position = "Project Manager",
                    Telephone = "943-333-2330",
                    Cellphone = "866-685-4130",
                    Email = "ddioniso1@testcompany4.com"
                },
                new Contact()
                {
                    Id = 3,
                    Client_Id = 1,
                    Address_Id = 1,
                    Name = "John Doe",
                    Position = "VP Operations",
                    Telephone = "654-654-6546",
                    Cellphone = "456-456-4564",
                    Email = "johndoe@test.com"
                }
            };
        }
    }
}
