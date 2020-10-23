using ClientsManager.Data;
using ClientsManager.Models;
using ClientsManager.Tests.TestData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientsManager.Tests.IntegrationTests
{
    public static class TestSeeder
    {
        public static void SeedDB(CMTestsDbContext context)
        {
            context.AddRange(AddressesData.getTestAddresses());
            context.AddRange(BillableActivitiesData.getTestBillableActivities());
            context.AddRange(ClientsData.getTestClients());
            context.AddRange(ContactsData.getTestContacts());
            context.AddRange(EmployeesData.getTestEmployees());
            context.AddRange(EmployeeTypesData.getTestEmployeeTypes());
            context.AddRange(LegalCasesData.getTestLegalCases());

            context.SaveChanges();
        }

        public static void ReSeedDB(CMTestsDbContext context)
        {
            context.RemoveRange(AddressesData.getTestAddresses());
            context.RemoveRange(BillableActivitiesData.getTestBillableActivities());
            context.RemoveRange(ClientsData.getTestClients());
            context.RemoveRange(ContactsData.getTestContacts());
            context.RemoveRange(EmployeesData.getTestEmployees());
            context.RemoveRange(EmployeeTypesData.getTestEmployeeTypes());
            context.RemoveRange(LegalCasesData.getTestLegalCases());

            context.SaveChanges();
            SeedDB(context);
        }
    }
}
