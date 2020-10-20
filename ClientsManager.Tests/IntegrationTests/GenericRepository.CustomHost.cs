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
using ClientsManager.Tests.TestData;
using Microsoft.Extensions.Logging;
using System.Net;

namespace ClientsManager.Tests.IntegrationTests
{
    public class GenericRepositoryCustomHost : IClassFixture<TestWebHost<Startup>>
    {
        //create an HTTP Client to make the requests
        private readonly HttpClient _client;

        private readonly TestWebHost<Startup> _host;
        private readonly IEnumerable<BillableActivity> _billableActivities;

        //url with paging
        private static string url = "/api/billableactivities";
        public static IEnumerable<object[]> PagingParameters =>
        new List<object[]>
        {
            //pageNumber = 1
            //pageSize = 10
            new object[] { url, 1, 10 }
        };

        public GenericRepositoryCustomHost(TestWebHost<Startup> host)
        {
            _host = host;
            _client = host.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
            _billableActivities = BillableActivityData.GetTestBillableActivities();
        }

        

        //public async Task<IEnumerable<BillableActivity>> GetAllBillableActivitiesAsync()
        [Theory]
        [MemberData(nameof(PagingParameters))]
        public async Task GetAllBillableActivitiesAsync_Returns_All_BillableActivities_Paged(string url, int pageNumber, int pageSize)
        {
            string uri = "/";
            var response = await _client.GetAsync(url);
            //var response = await _client.GetAsync($"{url}?pageNumber={pageNumber}&pageSize={pageSize}");
            var expected = _billableActivities;

            var serializedResponse = await response.Content.ReadAsStringAsync();
            var billableActivitiesResult = JsonConvert.DeserializeObject<IEnumerable<BillableActivity>>(serializedResponse);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.Contains(billableActivitiesResult, ba => ba.Id == 1);
            Assert.Contains(billableActivitiesResult, ba => ba.Id == 2);
            Assert.Contains(billableActivitiesResult, ba => ba.Id == 3);
            Assert.Contains(billableActivitiesResult, ba => ba.LegalCase_Id == 1);
            Assert.Contains(billableActivitiesResult, ba => ba.LegalCase_Id == 2);
            Assert.Contains(billableActivitiesResult, ba => ba.Employee_Id == 1);
            Assert.Contains(billableActivitiesResult, ba => ba.Employee_Id == 2);
        }

        //public async Task<IEnumerable<BillableActivity>> GetBillableActivitiesByEmployeeIdAsync(int employee_id)


        //public async Task<BillableActivity> GetBillableActivityByIdAsync(int id)


    }
}
