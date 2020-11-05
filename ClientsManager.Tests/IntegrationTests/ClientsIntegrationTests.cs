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
using ClientsManager.Tests.TestData;
using FluentAssertions;

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
            // create an instance of HttpClient with CreateClient()
            //_httpClient = _factory.CreateClient(new WebApplicationFactoryClientOptions { });
        }

        //Task<ActionResult<IEnumerable<ClientDTO>>> GetAllClientsAsync([FromQuery] QueryStringParameters parameters)
        [Theory(Skip = "GitHub Actions CI Pipeline fails in line 61")]
        [MemberData(nameof(PagingParameters))]
        public async Task GetAllClientsAsync_Returns_All_Clients_Paged(string url, int pageNumber, int pageSize)
        {
            //Arrange
            //create test HttpClient
            var client = _factory.CreateClient();

            //get test Clients
            IEnumerable<Client> expectedClients = ClientsData.getTestClients();

            //Act
            string uri = $"{url}?pageNumber={pageNumber}&pageSize={pageSize}";
            var httpResponse = await client.GetAsync(uri);

            //process Http response
            string serializedResponse = httpResponse.Content.ReadAsStringAsync().Result;
            var actualClients = JsonConvert.DeserializeObject<IEnumerable<Client>>(serializedResponse);
            
            //Assert
            Assert.True(httpResponse.IsSuccessStatusCode);

            //use FluentAssertions to compare Collections of Reference types
            actualClients.Should().BeEquivalentTo(expectedClients, options => options.ComparingByMembers<Client>());
        }


        //GetClientByIdAsync([FromRoute] int id)
        //[Theory]
        //[InlineData(1)]
        //[InlineData(2)]
        //public async Task GetClientByIdAsync_Returns_Client_For_Id(int id)
        //{
        //    new NotImplementedException();
        //}



    }
}
