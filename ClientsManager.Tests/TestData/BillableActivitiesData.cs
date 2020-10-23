using ClientsManager.Models;
using ClientsManager.WebAPI.DTOs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClientsManager.Tests.TestData
{
    public static class BillableActivitiesData
    { 
        public static IEnumerable<BillableActivity> getTestBillableActivities()
        {
            return new List<BillableActivity> {
                new BillableActivity
                {
                    Id = 1,
                    LegalCase_Id = 1, 
                    Employee_Id = 1,
                    Title = "Test BillableActivity from James",
                    Description = "Test James did this BillableActivity",
                    Price = 120.50m,
                    Start_DateTime = new DateTime(2020, 06, 20, 9, 30, 00),
                    Finish_DateTime = new DateTime(2020, 06, 20, 16, 00, 00)
                },
                new BillableActivity
                {
                    Id = 2,
                    LegalCase_Id = 1, 
                    Employee_Id = 1,
                    Title = "Test Mary's BillableActivity from October",
                    Description = "Test in October Mary did this BillableActivity",
                    Price = 400m,
                    Start_DateTime = new DateTime(2020, 06, 21, 10, 00, 00),
                    Finish_DateTime = new DateTime(2020, 06, 21, 17, 30, 00)
                },
                new BillableActivity
                {
                    Id = 3,
                    LegalCase_Id = 2, 
                    Employee_Id = 2,
                    Title = "Test BillableActivity 3",
                    Description = "Test this is the BillableActivity 3",
                    Price = 300.50m,
                    Start_DateTime = new DateTime(2020, 06, 23, 8, 15, 00),
                    Finish_DateTime = new DateTime(2020, 06, 23, 13, 30, 00)
                }
            };
        }
    }
}
