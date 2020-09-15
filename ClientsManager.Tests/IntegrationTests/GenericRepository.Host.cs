using ClientsManager.Models;
using ClientsManager.WebAPI;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ClientsManager.Tests.IntegrationTests
{
    public class GenericRepositoryHost : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        //url with paging
        private static string url = "/api/billableactivities";
        public static IEnumerable<object[]> PagingParameters =>
        new List<object[]>
        {
            //pageNumber = 1
            //pageSize = 10
            new object[] { url, 1, 10 }
        };

        public GenericRepositoryHost(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        //public async Task<IEnumerable<BillableActivity>> GetAllBillableActivitiesAsync()
        [Theory]
        [MemberData(nameof(PagingParameters))]
        public async Task GetAllBillableActivitiesAsync_Returns_All_BillableActivities_Paged(string url, int pageNumber, int pageSize)
        {
            //Arrange
            var httpClient = _factory.CreateClient();

            //Act
            var billableActivities = await httpClient.GetAsync($"{url}?pageNumber={pageNumber}&pageSize={pageSize}");

            var serializedResponse = await billableActivities.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<IEnumerable<BillableActivity>>(serializedResponse);

            //Assert
            Assert.Contains(actual, ba => ba.Id == 1);
            Assert.Contains(actual, ba => ba.Id == 2);
            Assert.Contains(actual, ba => ba.Id == 3);
            Assert.Contains(actual, ba => ba.Id == 6);
            Assert.Contains(actual, ba => ba.LegalCase_Id == 1);
            Assert.Contains(actual, ba => ba.LegalCase_Id == 2);
            Assert.Contains(actual, ba => ba.Employee_Id == 1);
            Assert.Contains(actual, ba => ba.Employee_Id == 2);
        }

        //public async Task<IEnumerable<BillableActivity>> GetBillableActivitiesByEmployeeIdAsync(int employee_id)


        //public async Task<BillableActivity> GetBillableActivityByIdAsync(int id)

    }
}
