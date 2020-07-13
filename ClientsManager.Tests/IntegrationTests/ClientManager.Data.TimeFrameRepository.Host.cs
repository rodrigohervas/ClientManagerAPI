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
    public class TimeFrameRepositoryHost : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public TimeFrameRepositoryHost(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        //public async Task<IEnumerable<TimeFrame>> GetAllTimeFramesAsync()
        [Theory]
        [InlineData("/api/timeframes")]
        public async Task Should_Get_All_TimeFrames(string url)
        {
            //Arrange
            var httpClient = _factory.CreateClient();

            //Act
            var timeFrames = await httpClient.GetAsync(url);

            var serializedResponse = await timeFrames.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<IEnumerable<TimeFrame>>(serializedResponse);

            //Assert
            Assert.Contains(actual, tf => tf.Id == 1);
            Assert.Contains(actual, tf => tf.Id == 2);
            Assert.Contains(actual, tf => tf.Id == 3);
            Assert.Contains(actual, tf => tf.Id == 6);
            Assert.Contains(actual, tf => tf.Employee_Id == 1);
            Assert.Contains(actual, tf => tf.Employee_Id == 2);
        }

        //public async Task<IEnumerable<TimeFrame>> GetTimeFramesByEmployeeIdAsync(int employee_id)


        //public async Task<TimeFrame> GetTimeFrameByIdAsync(int id)
        
    }
}
