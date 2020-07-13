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

namespace ClientsManager.Tests.IntegrationTests
{
    public class TimeFrameRepositoryCustomHost : IClassFixture<TestWebHost<Startup>>
    {
        //create an HTTP Client to make the requests
        private readonly HttpClient _client;

        private readonly TestWebHost<Startup> _host;
        private readonly IEnumerable<TimeFrame> _timeFrames;

        public TimeFrameRepositoryCustomHost(TestWebHost<Startup> host)
        {
            //_timeFrameRepo = timeFrameRepo;
            _host = host;
            _client = host.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
            _timeFrames = TimeFrameData.GetTestTimeFrames();
        }

        //public async Task<IEnumerable<TimeFrame>> GetAllTimeFramesAsync()
        [Theory]
        [InlineData("/api/timeframes")]
        public async Task Should_Return__List_Of_TimeFrames(string url)
        {
            var actual = await _client.GetAsync(url);
            var expected = _timeFrames;

            var result = await actual.Content.ReadAsStringAsync();
            var timeframes = JsonConvert.DeserializeObject<IEnumerable<TimeFrame>>(result);

            Assert.Contains(timeframes, tf => tf.Id == 1);
            Assert.Contains(timeframes, tf => tf.Id == 2);
            Assert.Contains(timeframes, tf => tf.Id == 3);
            Assert.Contains(timeframes, tf => tf.Employee_Id == 1);
            Assert.Contains(timeframes, tf => tf.Employee_Id == 2);
        }

        //public async Task<IEnumerable<TimeFrame>> GetTimeFramesByEmployeeIdAsync(int employee_id)


        //public async Task<TimeFrame> GetTimeFrameByIdAsync(int id)


    }
}
