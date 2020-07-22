using ClientsManager.Models;
using ClientsManager.WebAPI.DTOs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClientsManager.Tests
{
    public static class BillableActivityData
    { 
        public static IEnumerable<BillableActivity> GetTestBillableActivities()
        {
            return new List<BillableActivity> {
                new BillableActivity
                {
                    Id = 1,
                    Case_Id = 1, 
                    Employee_Id = 1,
                    Title = "BillableActivity 1",
                    Description = "this is the BillableActivity 1",
                    Price = 120.50m,
                    Start_DateTime = new DateTime(2020, 06, 20, 9, 30, 00),
                    Finish_DateTime = new DateTime(2020, 06, 20, 16, 00, 00)
                },
                new BillableActivity
                {
                    Id = 2, 
                    Case_Id = 1, 
                    Employee_Id = 1,
                    Title = "BillableActivity 2",
                    Description = "this is the BillableActivity 2",
                    Price = 400m,
                    Start_DateTime = new DateTime(2020, 06, 21, 10, 00, 00),
                    Finish_DateTime = new DateTime(2020, 06, 21, 17, 30, 00)
                },
                new BillableActivity
                {
                    Id = 3,
                    Case_Id = 2, 
                    Employee_Id = 2,
                    Title = "BillableActivity 3",
                    Description = "this is the BillableActivity 3",
                    Price = 300.50m,
                    Start_DateTime = new DateTime(2020, 06, 23, 8, 15, 00),
                    Finish_DateTime = new DateTime(2020, 06, 23, 13, 30, 00)
                }
            };
        }
    }
}
