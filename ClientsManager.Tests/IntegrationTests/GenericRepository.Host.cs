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

        public GenericRepositoryHost(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        //public async Task<IEnumerable<BillableActivity>> GetAllBillableActivitiesAsync()
        [Theory]
        [InlineData("/api/billableactivities")]
        public async Task Should_Get_All_BillableActivities(string url)
        {
            //Arrange
            var httpClient = _factory.CreateClient();

            //Act
            var billableActivities = await httpClient.GetAsync(url);

            var serializedResponse = await billableActivities.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<IEnumerable<BillableActivity>>(serializedResponse);

            //Assert
            Assert.Contains(actual, ba => ba.Id == 1);
            Assert.Contains(actual, ba => ba.Id == 2);
            Assert.Contains(actual, ba => ba.Id == 3);
            Assert.Contains(actual, ba => ba.Id == 6);
            Assert.Contains(actual, ba => ba.Case_Id == 1);
            Assert.Contains(actual, ba => ba.Case_Id == 2);
            Assert.Contains(actual, ba => ba.Employee_Id == 1);
            Assert.Contains(actual, ba => ba.Employee_Id == 2);
        }

        //public async Task<IEnumerable<BillableActivity>> GetBillableActivitiesByEmployeeIdAsync(int employee_id)


        //public async Task<BillableActivity> GetBillableActivityByIdAsync(int id)

    }
}
