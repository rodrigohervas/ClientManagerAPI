using ClientsManager.Data;
using ClientsManager.Tests.TestData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientsManager.Tests.IntegrationTests
{
    public static class BATestDbSeeder
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
