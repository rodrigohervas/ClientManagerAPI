using ClientsManager.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientsManager.Tests
{
    public static class TestDbSeeder
    {
        public static void SeedDB(ClientsManagerDBContext context) {
            context.TimeFrames.AddRange(TimeFrameData.GetTestTimeFrames());
            context.SaveChanges();
        }

        public static void ReSeedDB(ClientsManagerDBContext context) {
            context.TimeFrames.RemoveRange(TimeFrameData.GetTestTimeFrames());
            SeedDB(context);
        }
    }
}
