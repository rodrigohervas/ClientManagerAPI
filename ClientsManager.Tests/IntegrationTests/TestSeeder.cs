using ClientsManager.Data;
using ClientsManager.Tests.TestData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientsManager.Tests.IntegrationTests
{
    public static class TestSeeder<T> where T: class
    {
        public static void SeedDB(TestDBContext context)
        {
            context.AddRange(Data<T>.GetTestData());
            context.SaveChanges();
        }

        public static void ReSeedDB(TestDBContext context)
        {
            context.RemoveRange(Data<T>.GetTestData());
            SeedDB(context);
        }
    }
}
