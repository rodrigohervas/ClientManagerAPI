using ClientsManager.Data;
using ClientsManager.Tests.TestData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientsManager.Tests.IntegrationTests
{
    public static class BATestDbSeeder
    {
        public static void SeedDB(CMTestsDbContext context) {
            context.BillableActivities.AddRange(BillableActivitiesData.getTestBillableActivities());
            context.SaveChanges();
        }

        public static void ReSeedDB(CMTestsDbContext context) {
            context.BillableActivities.RemoveRange(BillableActivitiesData.getTestBillableActivities());
            SeedDB(context);
        }
    }
}
