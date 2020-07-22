using ClientsManager.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientsManager.Tests
{
    public static class TestDbSeeder
    {
        public static void SeedDB(ClientsManagerDBContext context) {
            context.BillableActivities.AddRange(BillableActivityData.GetTestBillableActivities());
            context.SaveChanges();
        }

        public static void ReSeedDB(ClientsManagerDBContext context) {
            context.BillableActivities.RemoveRange(BillableActivityData.GetTestBillableActivities());
            SeedDB(context);
        }
    }
}
