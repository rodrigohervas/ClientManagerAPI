using ClientsManager.Data;
using ClientsManager.Models;
using ClientsManager.Tests.IntegrationTests;
using ClientsManager.WebAPI;
using ClientsManager.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ClientsManager.Tests.UnitTests
{
    public class TimeFrameControllerTests
    {
        private readonly IEnumerable<TimeFrame> _timeFrames;
        private readonly Mock<ITimeFrameRepository> _mockRepository;

        public TimeFrameControllerTests()
        {
            _mockRepository = new Mock<ITimeFrameRepository>();
            _timeFrames = TimeFrameData.GetTestTimeFrames();
        }

        //Task<ActionResult<IEnumerable<TimeFrame>>> GetAllTimeFramesAsync
        [Fact]
        public async void Should_Return_All_TimeFrames()
        {
            //specify the mockRepo return
            _mockRepository.Setup(repo => repo.GetAllTimeFramesAsync()).ReturnsAsync(_timeFrames);


            //instantiate the controller, and call the method
            var controller = new TimeFramesController(_mockRepository.Object);

            //Call the SUT method
            var result = await controller.GetAllTimeFramesAsync();
            
            //Assert the result
            Assert.NotNull(result);
            var actionResult = Assert.IsType<ActionResult<IEnumerable<TimeFrame>>>(result);
            var objResult = Assert.IsType<OkObjectResult>(result.Result);
            var timeFramesList = objResult.Value;
            Assert.Equal(_timeFrames, timeFramesList);
        }


        //Task<ActionResult<IEnumerable<TimeFrame>>> GetTimeFramesByEmployeeIdAsync(int employee_id)
        [Theory]
        [InlineData(2)]
        public async void Should_Return_All_TimeFrames_for_One_Employee(int employee_id)
        {
            //gets all TimeFrames for one employee
            var _timeFramesOfEmployee = _timeFrames.Where(timeframe => timeframe.Employee_Id == employee_id);

            //Setup repository
            _mockRepository.Setup(repo => repo.GetTimeFramesByEmployeeIdAsync(employee_id))
                                                    .ReturnsAsync(_timeFramesOfEmployee);

            //instantiate System Under Test
            var controller = new TimeFramesController(_mockRepository.Object); ;

            //call SUT method
            var actionResult = await controller.GetTimeFramesByEmployeeIdAsync(employee_id);

            //Assert results
            Assert.NotNull(actionResult);

            //Assert object in actionresult
            var result = Assert.IsType<OkObjectResult>(actionResult.Result);
            var actual = (IEnumerable<TimeFrame>)(result.Value);
            Assert.Equal(_timeFramesOfEmployee, actual);
        }


        //Task<ActionResult<TimeFrame>> GetTimeFrameByIdAsync(int id)
        [Theory]
        [InlineData(1)]
        public async void Should_Return_One_TimeFrame(int id)
        {
            //get the first TimeFrame
            var timeFrame = _timeFrames.FirstOrDefault<TimeFrame>();

            //specify the mockRepo return
            _mockRepository.Setup(repo => repo.GetTimeFrameByIdAsync(id)).ReturnsAsync(timeFrame);

            //instantiate the controller, and call the method
            var controller = new TimeFramesController(_mockRepository.Object);

            //Call the SUT method
            //returns ActionResult<TimeFrame> type
            var actionResult = await controller.GetTimeFrameByIdAsync(id);
            
            //Assert the result
            Assert.NotNull(actionResult);
            
            //convert ActionResult to OkObjectResult to get its Value: a TimeFrame type
            var result = Assert.IsType<OkObjectResult>(actionResult.Result);
            //get the ObjectResult.Value (the TimeFrame)
            var actual = result.Value;
            Assert.Equal(timeFrame, actual);

            var resultTimeFrame = (TimeFrame)actual;
            Assert.Equal(timeFrame.Id, resultTimeFrame.Id);
        }

        //Test public async Task<ActionResult<TimeFrame>> AddTimeFrameAsync(TimeFrame timeFrame)
        [Fact]
        public async void Should_Create_One_TimeFrame()
        {
            //declare a TimeFrame
            var newTimeFrame = new TimeFrame
            {
                Id = 1,
                Employee_Id = 1,
                Title = "timeframe 1",
                Description = "this is the timeframe 1",
                Price = 120.50m,
                Start_DateTime = new DateTime(2020, 06, 20, 9, 30, 00),
                Finish_DateTime = new DateTime(2020, 06, 20, 16, 00, 00)
            };

            //specify the mockRepo return
            _mockRepository.Setup(repo => repo.AddTimeFrameAsync(newTimeFrame)).ReturnsAsync(newTimeFrame);
            
            //instantiate the controller, and call the method
            var controller = new TimeFramesController(_mockRepository.Object);

            //Call the SUT method
            //returns ActionResult<TimeFrame> type
            var actionResult = await controller.AddTimeFrameAsync(newTimeFrame);

            //Assert the result
            Assert.NotNull(actionResult);

            //convert ActionResult to CreatedResult to get its Value: a TimeFrame type
            var result = Assert.IsType<CreatedResult>(actionResult.Result);
            //get the CreatedResult.Value (the TimeFrame)
            var actual = result.Value;
            Assert.Equal(newTimeFrame, actual);

            var resultTimeFrame = (TimeFrame)actual;
            Assert.Equal(newTimeFrame.Id, resultTimeFrame.Id);
        }


        [Fact]
        public async void Should_Return_BadRequest_When_Create_With__null_TimeFrame()
        {
            //declare a TimeFrame
            var newTimeFrame = new TimeFrame
            {
                Id = 1,
                Employee_Id = 1,
                Title = "timeframe 1",
                Description = "this is the timeframe 1",
                Price = 120.50m,
                Start_DateTime = new DateTime(2020, 06, 20, 9, 30, 00),
                Finish_DateTime = new DateTime(2020, 06, 20, 16, 00, 00)
            };

            //specify the mockRepo return
            //_mockRepository.Setup(repo => repo.AddTimeFrameAsync(newTimeFrame)).ReturnsAsync(newTimeFrame);
            _mockRepository.Setup(repo => repo.AddTimeFrameAsync(newTimeFrame)).ReturnsAsync(newTimeFrame);

            //instantiate the controller, and call the method
            var controller = new TimeFramesController(_mockRepository.Object);

            //Call the SUT method
            //returns ActionResult<TimeFrame> type
            var actionResult = await controller.AddTimeFrameAsync(new TimeFrame());

            //Assert the result
            Assert.NotNull(actionResult);

            //convert ActionResult to CreatedResult to get its Value: a TimeFrame type
            var result = Assert.IsType<BadRequestResult>(actionResult.Value);
            //get the CreatedResult.Value (the TimeFrame)
            var actual = result.StatusCode;
            //Assert.Equal(newTimeFrame.ToString(), actual);

            var resultTimeFrame = actual;
            Assert.Equal(newTimeFrame.Id, resultTimeFrame);
        }

        //Test public async Task<ActionResult<TimeFrame>> UpdateTimeFrameAsync(int id, TimeFrame timeFrame)


        //Test public async Task<ActionResult<int>> DeleteTimeFrameAsync(int id)
    }
}
