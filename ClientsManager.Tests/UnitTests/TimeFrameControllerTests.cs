using AutoMapper;
using ClientsManager.Data;
using ClientsManager.Models;
using ClientsManager.Tests.IntegrationTests;
using ClientsManager.WebAPI;
using ClientsManager.WebAPI.AutoMapperProfiles;
using ClientsManager.WebAPI.Controllers;
using ClientsManager.WebAPI.DTOs;
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
        private readonly Mock<IGenericRepository<TimeFrame>> _mockRepository;
        private readonly IMapper _mapper;
        

        public TimeFrameControllerTests()
        {
            _mockRepository = new Mock<IGenericRepository<TimeFrame>>();
            _timeFrames = TimeFrameData.GetTestTimeFrames();

            //AutoMapper Configuration
            var profiles = new AutoMapperProfiles();
            var configuration = new MapperConfiguration(config => config.AddProfile(profiles));
            _mapper = new Mapper(configuration);
        }

        //Task<ActionResult<IEnumerable<TimeFrame>>> GetAllTimeFramesAsync
        [Fact]
        public async void Should_Return_All_TimeFrames()
        {
            //specify the mockRepo return
            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(_timeFrames);

            //instantiate the controller, and call the method
            //var controller = new TimeFramesController(_mockRepository.Object, _mapper.Object);
            var controller = new TimeFramesController(_mockRepository.Object, _mapper);

            //Call the SUT method
            var result = await controller.GetAllTimeFramesAsync();
            
            //Assert the result
            Assert.NotNull(result);
            var actionResult = Assert.IsType<ActionResult<IEnumerable<TimeFrame>>>(result);
            var objResult = Assert.IsType<OkObjectResult>(result.Result);
            var timeFramesList = objResult.Value;
            IEnumerable<TimeFrame> dtos = _mapper.Map<IEnumerable<TimeFrame>>(timeFramesList);

            var expected = _timeFrames.FirstOrDefault();
            var actual = dtos.FirstOrDefault();

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Title, actual.Title);
            Assert.Equal(expected.Description, actual.Description);
            Assert.Equal(expected.Price, actual.Price);
            Assert.Equal(expected.Start_DateTime, actual.Start_DateTime);
            Assert.Equal(expected.Finish_DateTime, actual.Finish_DateTime);

            Assert.Equal(_timeFrames.Count(), dtos.Count());

            //TODO: add FluentAssertions package to assert collections
        }


        //Task<ActionResult<IEnumerable<TimeFrame>>> GetTimeFramesByEmployeeIdAsync(int employee_id)
        [Theory]
        [InlineData(2)]
        public async void Should_Return_All_TimeFrames_for_One_Employee(int employee_id)
        {
            //gets all TimeFrames for one employee
            var _timeFramesOfEmployee = _timeFrames.Where(timeframe => timeframe.Employee_Id == employee_id);

            //Setup repository
            _mockRepository.Setup(repo => repo.GetByAsync(tf => tf.Employee_Id == employee_id))
                                                    .ReturnsAsync(_timeFramesOfEmployee);

            //instantiate System Under Test
            var controller = new TimeFramesController(_mockRepository.Object, _mapper); ;

            //call SUT method
            var actionResult = await controller.GetTimeFramesByEmployeeIdAsync(employee_id);

            //Assert results
            Assert.NotNull(actionResult);

            //Assert object in actionresult
            var result = Assert.IsType<OkObjectResult>(actionResult.Result);
            var actualTimeFrames = (IEnumerable<TimeFrameDTO>)(result.Value);
            IEnumerable<TimeFrame> dtos = _mapper.Map<IEnumerable<TimeFrame>>(actualTimeFrames);

            var expected = _timeFramesOfEmployee.FirstOrDefault();
            var actual = dtos.FirstOrDefault();

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Title, actual.Title);
            Assert.Equal(expected.Description, actual.Description);
            Assert.Equal(expected.Price, actual.Price);
            Assert.Equal(expected.Start_DateTime, actual.Start_DateTime);
            Assert.Equal(expected.Finish_DateTime, actual.Finish_DateTime);

            Assert.Equal(_timeFramesOfEmployee.Count(), dtos.Count());

            //TODO: add FluentAssertions package to assert collections
        }


        //Task<ActionResult<TimeFrame>> GetTimeFrameByIdAsync(int id)
        [Theory]
        [InlineData(1)]
        public async void Should_Return_One_TimeFrame(int id)
        {
            //get the first TimeFrame
            var timeFrame = _timeFrames.FirstOrDefault<TimeFrame>();

            //specify the mockRepo return
            _mockRepository.Setup(repo => repo.GetOneByAsync(tf => tf.Id == id)).ReturnsAsync(timeFrame);

            //instantiate the controller, and call the method
            var controller = new TimeFramesController(_mockRepository.Object, _mapper);

            //Call the SUT method
            //returns ActionResult<TimeFrame> type
            var actionResult = await controller.GetTimeFrameByIdAsync(id);
            
            //Assert the result
            Assert.NotNull(actionResult);
            
            //convert ActionResult to OkObjectResult to get its Value: a TimeFrame type
            var result = Assert.IsType<OkObjectResult>(actionResult.Result);
            //get the ObjectResult.Value (the TimeFrame)
            var actualTimeFrame = result.Value;
            
            TimeFrame actual = _mapper.Map<TimeFrame>(actualTimeFrame);

            Assert.Equal(timeFrame.Id, actual.Id);
            Assert.Equal(timeFrame.Title, actual.Title);
            Assert.Equal(timeFrame.Description, actual.Description);
            Assert.Equal(timeFrame.Price, actual.Price);
            Assert.Equal(timeFrame.Start_DateTime, actual.Start_DateTime);
            Assert.Equal(timeFrame.Finish_DateTime, actual.Finish_DateTime);
        }

        //Test public async Task<ActionResult<TimeFrame>> AddTimeFrameAsync(TimeFrame timeFrame)
        [Fact]
        public async void Should_Create_One_TimeFrame_201()
        {
            //declare a TimeFrame
            var expectedTimeFrame = new TimeFrame
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
            _mockRepository.Setup(repo => repo.AddTAsync(expectedTimeFrame)).ReturnsAsync(1);


            //instantiate the controller, passing the repo object
            var controller = new TimeFramesController(_mockRepository.Object, _mapper);

            //Call the SUT method
            //returns ActionResult<TimeFrame> type
            var actionResult = await controller.AddTimeFrameAsync(expectedTimeFrame);

            var test = actionResult.Result;
            
            //Assert the result
            Assert.NotNull(actionResult);

            //Get the int result from the posted ActionResult
            var result = (CreatedResult)actionResult.Result;
            var statusCode = result.StatusCode;

            //Validate the return is 1 TimeFrame created
            Assert.Equal(201, statusCode);
        }


        [Fact]
        public async void Should_Return_NotFound_404_When_Create_With_null_TimeFrame()
        {
            //specify the mockRepo return
            _mockRepository.Setup(repo => repo.AddTAsync(null)).ReturnsAsync(0);

            //instantiate the controller, and call the method
            var controller = new TimeFramesController(_mockRepository.Object, _mapper);

            //Call the SUT method
            //returns ActionResult<TimeFrame> type
            var actionResult = await controller.AddTimeFrameAsync(new TimeFrame());

            //Assert the result
            Assert.NotNull(actionResult);

            var result = actionResult.Result as NotFoundObjectResult;
            var statusCode = result.StatusCode;
            var message = result.Value;
            
            //Assert message
            Assert.Equal("No TimeFrame was created", message);

            //Assert StatusCode
            Assert.Equal(404, statusCode);
        }

        //Test public async Task<ActionResult<TimeFrame>> UpdateTimeFrameAsync(int id, TimeFrame timeFrame)


        //Test public async Task<ActionResult<int>> DeleteTimeFrameAsync(int id)
    }
}
