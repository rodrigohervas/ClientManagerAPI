using System.Net.Http.Headers;
using ClientsManager.Models;
using ClientsManager.WebAPI;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;

namespace ClientsManager.Tests.IntegrationTests
{
    public class ClientsIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;
        private readonly CustomWebApplicationFactory<Startup> _factory;

        //url
        private static string url = "/api/clients";
        
        //paging parameters
        private static int pageNumber = 1;
        private static int pageSize = 5;
        public static IEnumerable<object[]> PagingParameters =>
        new List<object[]>
        {
            new object[] { url, pageNumber, pageSize }
        };

        public ClientsIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            //_httpClient = _factory.CreateClient(new WebApplicationFactoryClientOptions { });
        }

        //Task<ActionResult<IEnumerable<ClientDTO>>> GetAllClientsAsync([FromQuery] QueryStringParameters parameters)
        [Theory]
        [MemberData(nameof(PagingParameters))]
        public async Task GetAllClientsAsync_Returns_All_Clients_Paged(string url, int pageNumber, int pageSize)
        {
            //Arrange
            var client = _factory.CreateClient();
                      

            //Act - send GET Request from testClient to API
            string uri = $"{url}?pageNumber={pageNumber}&pageSize={pageSize}";
            var clients = await client.GetAsync(uri);

            var serializedResponse = await clients.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<IEnumerable<Client>>(serializedResponse);

            //Assert
            Assert.Contains(actual, cl => cl.Id == 1);
            Assert.Contains(actual, cl => cl.Id == 2);
            Assert.Contains(actual, cl => cl.Id == 3);
            Assert.Contains(actual, cl => cl.Id == 6);
            Assert.Contains(actual, cl => cl.Name == "Test Company 1");
            Assert.Contains(actual, cl => cl.Name == "Test Company 2");
        }

        //public async Task<IEnumerable<BillableActivity>> GetBillableActivitiesByEmployeeIdAsync(int employee_id)


        //GetClientByIdAsync([FromRoute] int id)
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetClientByIdAsync_Returns_Client_For_Id(int id)
        {
            //Arrange
            var client = _factory.CreateClient();

            //Act - send GET Request from testClient to API
            string uri = $"{url}/{id}";
            var clients = await client.GetAsync(uri);

            var serializedResponse = await clients.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<IEnumerable<Client>>(serializedResponse);

            //Assert
            Assert.Contains(actual, cl => cl.Id == 1);
            Assert.Contains(actual, cl => cl.Id == 2);
            Assert.Contains(actual, cl => cl.Id == 3);
            Assert.Contains(actual, cl => cl.Id == 6);
            Assert.Contains(actual, cl => cl.Name == "Test Company 1");
            Assert.Contains(actual, cl => cl.Name == "Test Company 2");
        }


        //public async Task<BillableActivity> GetBillableActivityByIdAsync(int id)

    }
}
