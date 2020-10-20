using ClientsManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientsManager.Tests.TestData
{
    public static class Data<T> where T : class
    {
        public static List<Client> GetTestData()
        {
            if (typeof(T) == typeof(Client) ) {
                return getTestClients();
            }

            if (typeof(T) == typeof(BillableActivity))
            {
                //return getTestBillableActivities();
            }

            return null;            
        }

        public static List<Client> getTestClients() {
            return new List<Client> {
                new Client()
                {
                    //Id = 1,
                    Name = "Test Company 1",
                    Description = "test Company description"
                },
                new Client()
                {
                    //Id = 2,
                    Name = "Test Company 2",
                    Description = "Test Company 2 Description",
                    Website = "https://www.testcompany2.com"
                }
            };
        }

        public static List<BillableActivity> getTestBillableActivities()
        {
            return new List<BillableActivity> {
                new BillableActivity
                {
                    //Id = 1,
                    LegalCase_Id = 1,
                    Employee_Id = 1,
                    Title = "Test BillableActivity 1",
                    Description = "Test BillableActivity 1 description",
                    Price = 120.50m,
                    Start_DateTime = new DateTime(2018, 06, 20, 9, 30, 00),
                    Finish_DateTime = new DateTime(2018, 06, 20, 16, 00, 00)
                },
                new BillableActivity
                {
                    Id = 2,
                    LegalCase_Id = 1,
                    Employee_Id = 1,
                    Title = "Test BillableActivity 2",
                    Description = "Test BillableActivity 2 description",
                    Price = 400m,
                    Start_DateTime = new DateTime(2019, 06, 21, 10, 00, 00),
                    Finish_DateTime = new DateTime(2019, 06, 21, 17, 30, 00)
                },
                new BillableActivity
                {
                    Id = 3,
                    LegalCase_Id = 2,
                    Employee_Id = 2,
                    Title = "Test BillableActivity 3",
                    Description = "Test BillableActivity 3 description",
                    Price = 300.50m,
                    Start_DateTime = new DateTime(2020, 06, 23, 8, 15, 00),
                    Finish_DateTime = new DateTime(2020, 06, 23, 13, 30, 00)
                }
            };
        }
    }
}
