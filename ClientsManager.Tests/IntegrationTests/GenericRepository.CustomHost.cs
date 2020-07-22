using System;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClientsManager.Data;
using ClientsManager.Models;
using ClientsManager.WebAPI;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ClientsManager.WebAPI.DTOs;

namespace ClientsManager.Tests.IntegrationTests
{
    public class GenericRepositoryCustomHost : IClassFixture<TestWebHost<Startup>>
    {
        //create an HTTP Client to make the requests
        private readonly HttpClient _client;

        private readonly TestWebHost<Startup> _host;
        private readonly IEnumerable<BillableActivity> _billableActivities;

        public GenericRepositoryCustomHost(TestWebHost<Startup> host)
        {
            _host = host;
            _client = host.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
            _billableActivities = BillableActivityData.GetTestBillableActivities();
        }

        //public async Task<IEnumerable<BillableActivity>> GetAllBillableActivitiesAsync()
        [Theory]
        [InlineData("/api/billableactivities")]
        public async Task Should_Return__List_Of_BillableActivities(string url)
        {
            var actual = await _client.GetAsync(url);
            var expected = _billableActivities;

            var result = await actual.Content.ReadAsStringAsync();
            var billableActivities = JsonConvert.DeserializeObject<IEnumerable<BillableActivity>>(result);

            Assert.Contains(billableActivities, ba => ba.Id == 1);
            Assert.Contains(billableActivities, ba => ba.Id == 2);
            Assert.Contains(billableActivities, ba => ba.Id == 3);
            Assert.Contains(billableActivities, ba => ba.Case_Id == 1);
            Assert.Contains(billableActivities, ba => ba.Case_Id == 2);
            Assert.Contains(billableActivities, ba => ba.Employee_Id == 1);
            Assert.Contains(billableActivities, ba => ba.Employee_Id == 2);
        }

        //public async Task<IEnumerable<BillableActivity>> GetBillableActivitiesByEmployeeIdAsync(int employee_id)


        //public async Task<BillableActivity> GetBillableActivityByIdAsync(int id)


    }
}
