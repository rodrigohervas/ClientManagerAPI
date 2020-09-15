using AutoMapper;
using ClientsManager.Data;
using ClientsManager.Models;
using ClientsManager.Tests.IntegrationTests;
using ClientsManager.Tests.TestData;
using ClientsManager.WebAPI;
using ClientsManager.WebAPI.AutoMapperProfiles;
using ClientsManager.WebAPI.Controllers;
using ClientsManager.WebAPI.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ClientsManager.Tests.UnitTests
{
    public class BillableActivityControllerTests
    {
        private readonly IEnumerable<BillableActivity> _billableActivities;
        private readonly Mock<IGenericRepository<BillableActivity>> _mockRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BillableActivitiesController> _logger;
        QueryStringParameters parameters;

        public BillableActivityControllerTests()
        {
            _mockRepository = new Mock<IGenericRepository<BillableActivity>>();
            _billableActivities = BillableActivityData.GetTestBillableActivities();

            //AutoMapper Configuration
            var profiles = new AutoMapperProfiles();
            var configuration = new MapperConfiguration(config => config.AddProfile(profiles));
            _mapper = new Mapper(configuration);
            _logger = new Logger<BillableActivitiesController>(new LoggerFactory());

            //QueryStringParameters for paging
            parameters = new QueryStringParameters();
            parameters.pageNumber = 1;
            parameters.pageSize = 10;
        }

        //GetAllBillableActivitiesAsync
        [Fact]
        public async void GetAllAsync_Returns_All_BillableActivities()
        {
            //specify the mockRepo return
            //_mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(_billableActivities);
            _mockRepository.Setup(repo => repo.GetAllPagedAsync(ba => ba.LegalCase_Id, parameters)).ReturnsAsync(_billableActivities);

            //instantiate the controller, and call the method
            var controller = new BillableActivitiesController(_mockRepository.Object, _mapper, _logger);

            //Call the SUT method
            var result = await controller.GetAllBillableActivitiesAsync(parameters);

            //Assert the result
            Assert.NotNull(result);
            var actionResult = Assert.IsType<ActionResult<IEnumerable<BillableActivity>>>(result);
            var objResult = Assert.IsType<OkObjectResult>(result.Result);
            var billableActivitiesList = objResult.Value;
            IEnumerable<BillableActivity> dtos = _mapper.Map<IEnumerable<BillableActivity>>(billableActivitiesList);

            var expected = _billableActivities.FirstOrDefault();
            var actual = dtos.FirstOrDefault();

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.LegalCase_Id, actual.LegalCase_Id);
            Assert.Equal(expected.Employee_Id, actual.Employee_Id);
            Assert.Equal(expected.Title, actual.Title);
            Assert.Equal(expected.Description, actual.Description);
            Assert.Equal(expected.Price, actual.Price);
            Assert.Equal(expected.Start_DateTime, actual.Start_DateTime);
            Assert.Equal(expected.Finish_DateTime, actual.Finish_DateTime);

            Assert.Equal(_billableActivities.Count(), dtos.Count());

            //TODO: add FluentAssertions package to assert collections
        }


        //GetBillableActivitiesByEmployeeIdAsync(int employee_id)
        [Theory]
        [InlineData(2)]
        public async void GetBillableActivitiesByEmployeeIdAsync_Returns_All_BillableActivities_for_One_Employee(int employee_id)
        {
            //gets all BillableActivities for one employee
            var _billableActivitiesOfEmployee = _billableActivities.Where(ba => ba.Employee_Id == employee_id);

            //Setup repository
            _mockRepository.Setup(repo => repo.GetByAsync(ba => ba.Employee_Id == employee_id))
                                                    .ReturnsAsync(_billableActivitiesOfEmployee);

            //instantiate System Under Test
            var controller = new BillableActivitiesController(_mockRepository.Object, _mapper, _logger); ;

            //call SUT method
            var actionResult = await controller.GetBillableActivitiesByEmployeeIdAsync(employee_id);

            //Assert results
            Assert.NotNull(actionResult);

            //Assert object in actionresult
            var result = Assert.IsType<OkObjectResult>(actionResult.Result);
            var actualbillableActivities = (IEnumerable<BillableActivityDTO>)(result.Value);
            IEnumerable<BillableActivity> dtos = _mapper.Map<IEnumerable<BillableActivity>>(actualbillableActivities);

            var expected = _billableActivitiesOfEmployee.FirstOrDefault();
            var actual = dtos.FirstOrDefault();

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.LegalCase_Id, actual.LegalCase_Id);
            Assert.Equal(expected.Employee_Id, actual.Employee_Id);
            Assert.Equal(expected.Title, actual.Title);
            Assert.Equal(expected.Description, actual.Description);
            Assert.Equal(expected.Price, actual.Price);
            Assert.Equal(expected.Start_DateTime, actual.Start_DateTime);
            Assert.Equal(expected.Finish_DateTime, actual.Finish_DateTime);

            Assert.Equal(_billableActivitiesOfEmployee.Count(), dtos.Count());

            //TODO: add FluentAssertions package to assert collections
        }


        //GetBillableActivityByIdAsync(int id)
        [Theory]
        [InlineData(1)]
        public async void GetBillableActivityByIdAsync_Returns_One_BillableActivity(int id)
        {
            //get the first TimeFrame
            var billableActivity = _billableActivities.FirstOrDefault<BillableActivity>();

            //specify the mockRepo return
            _mockRepository.Setup(repo => repo.GetOneByAsync(tf => tf.Id == id)).ReturnsAsync(billableActivity);

            //instantiate the controller, and call the method
            var controller = new BillableActivitiesController(_mockRepository.Object, _mapper, _logger);

            //Call the SUT method
            //returns ActionResult<TimeFrame> type
            var actionResult = await controller.GetBillableActivityByIdAsync(id);

            //Assert the result
            Assert.NotNull(actionResult);

            //convert ActionResult to OkObjectResult to get its Value: a BillableActivity type
            var result = Assert.IsType<OkObjectResult>(actionResult.Result);
            //get the ObjectResult.Value (the TimeFrame)
            var actualBillableActivity = result.Value;

            BillableActivity actual = _mapper.Map<BillableActivity>(actualBillableActivity);

            Assert.Equal(billableActivity.Id, actual.Id);
            Assert.Equal(billableActivity.LegalCase_Id, actual.LegalCase_Id);
            Assert.Equal(billableActivity.Employee_Id, actual.Employee_Id);
            Assert.Equal(billableActivity.Title, actual.Title);
            Assert.Equal(billableActivity.Description, actual.Description);
            Assert.Equal(billableActivity.Price, actual.Price);
            Assert.Equal(billableActivity.Start_DateTime, actual.Start_DateTime);
            Assert.Equal(billableActivity.Finish_DateTime, actual.Finish_DateTime);
        }

        //AddBillableActivityAsync(BillableActivity billableActivity)
        [Fact]
        public async void AddBillableActivityAsync_Creates_One_BillableActivity_Returns_201_And_BA_Created()
        {
            //declare a BillableActivity
            var expectedBillableActivity = new BillableActivity
            {
                Id = 1,
                LegalCase_Id = 1,
                Employee_Id = 1,
                Title = "Billable Activity 1",
                Description = "this is the Billable Activity 1",
                Price = 120.50m,
                Start_DateTime = new DateTime(2020, 06, 20, 9, 30, 00),
                Finish_DateTime = new DateTime(2020, 06, 20, 16, 00, 00)
            };
            BillableActivityDTO expectedBaDTO = _mapper.Map<BillableActivityDTO>(expectedBillableActivity);

            //set mockRepo return for Add action
            _mockRepository.Setup(repo => repo.AddTAsync(expectedBillableActivity)).ReturnsAsync(1);

            //set repo return for getting the newly created object
            _mockRepository.Setup(repo => repo.GetOneByAsync(ba =>
                                    ba.Title == expectedBillableActivity.Title &&
                                    ba.Employee_Id == expectedBillableActivity.Employee_Id &&
                                    ba.LegalCase_Id == expectedBillableActivity.LegalCase_Id))
                            .ReturnsAsync(expectedBillableActivity);

            //instantiate the controller, passing the repo object
            var controller = new BillableActivitiesController(_mockRepository.Object, _mapper, _logger);

            //Call the SUT method
            //returns ActionResult<BillableActivity> type
            var actionResult = await controller.AddBillableActivityAsync(expectedBillableActivity);

            //Get the int result from the posted ActionResult
            var createdResult = actionResult.Result as CreatedResult;
            var statusCode = createdResult.StatusCode;
            BillableActivityDTO actualBaDTO = createdResult.Value as BillableActivityDTO;

            //Assert the result
            Assert.NotNull(actionResult);

            //Validate the return is 1 BillableActivity created
            Assert.Equal(201, statusCode);

            //Validate the actual BillableActivity
            actualBaDTO.Should().BeEquivalentTo(expectedBaDTO, options => options.ComparingByMembers<BillableActivityDTO>());
        }


        //AddBillableActivityAsync(0)
        [Fact]
        public async void AddBillableActivityAsync_Returns_NotFound_404_When_Create_With_null_BillableActivity()
        {
            //specify the mockRepo return
            _mockRepository.Setup(repo => repo.AddTAsync(null)).ReturnsAsync(0);

            //instantiate the controller, and call the method
            var controller = new BillableActivitiesController(_mockRepository.Object, _mapper, _logger);

            //Call the SUT method
            //returns ActionResult<BillableActivity> type
            var actionResult = await controller.AddBillableActivityAsync(new BillableActivity());

            //Assert the result
            Assert.NotNull(actionResult);

            var result = actionResult.Result as NotFoundObjectResult;
            var statusCode = result.StatusCode;
            var message = result.Value;

            //Assert message
            Assert.Equal("No Billable Activity was created", message);

            //Assert StatusCode
            Assert.Equal(404, statusCode);
        }

        //UpdateBillableActivityAsync(int id, BillableActivity billableActivity)
        [Fact]
        public async void UpdateBillableActivityAsync_Updates_One_BillableActivity_Returns_200_And_BillableActivity_Updated()
        {
            //declare a BillableActivity
            var expectedBillableActivity = new BillableActivity
            {
                Id = 1,
                LegalCase_Id = 1,
                Employee_Id = 1,
                Title = "Billable Activity 1",
                Description = "this is the Billable Activity 1",
                Price = 120.50m,
                Start_DateTime = new DateTime(2020, 06, 20, 9, 30, 00),
                Finish_DateTime = new DateTime(2020, 06, 20, 16, 00, 00)
            };
            BillableActivityDTO expectedBaDTO = _mapper.Map<BillableActivityDTO>(expectedBillableActivity);

            //set repo return for getting the object to update
            _mockRepository.Setup(repo => repo.GetOneByAsync(ba => ba.Id == expectedBillableActivity.Id))
                           .ReturnsAsync(expectedBillableActivity);

            //set mockRepo return for Update action
            _mockRepository.Setup(repo => repo.UpdateTAsync(expectedBillableActivity)).ReturnsAsync(1);

            //instantiate the controller, passing the repo object
            var controller = new BillableActivitiesController(_mockRepository.Object, _mapper, _logger);

            //Call the SUT method
            //returns ActionResult<BillableActivity> type
            var actionResult = await controller.UpdateBillableActivityAsync(expectedBillableActivity.Id, expectedBillableActivity);

            //Get the int result from the posted ActionResult
            var okObjectResult = actionResult.Result as OkObjectResult;
            var statusCode = okObjectResult.StatusCode;
            BillableActivityDTO actualBaDTO = okObjectResult.Value as BillableActivityDTO;

            //Assert the result
            Assert.NotNull(actionResult);

            //Validate StatusCode
            Assert.Equal(200, statusCode);

            //Validate the actual BillableActivity
            actualBaDTO.Should().BeEquivalentTo(expectedBaDTO, options => options.ComparingByMembers<BillableActivityDTO>());
        }

        //update error
        //UpdateBillableActivityAsync(int id, BillableActivity billableActivity)
        [Fact]
        public async void UpdateBillableActivityAsync_Returns_NotFound_404_When_Update_With_null_BillableActivity()
        {
            //declare a BillableActivity
            BillableActivity expectedBillableActivity = null;
            //
            string expectedResponseMessage = "No Billable Activity was updated";

            ///set mockRepo return for Update action
            _mockRepository.Setup(repo => repo.UpdateTAsync(expectedBillableActivity)).ReturnsAsync(0);

            //instantiate the controller, passing the repo object
            var controller = new BillableActivitiesController(_mockRepository.Object, _mapper, _logger);

            //Call the SUT method
            //returns ActionResult<BillableActivity> type
            var actionResult = await controller.UpdateBillableActivityAsync(1, expectedBillableActivity);

            //Get the int result from the posted ActionResult
            var notFoundObjectResult = actionResult.Result as NotFoundObjectResult;
            var statusCode = notFoundObjectResult.StatusCode;
            string actualResponseMessage = (string)notFoundObjectResult.Value;

            //Assert the result
            Assert.NotNull(actionResult);

            //Validate the return status code
            Assert.Equal(404, statusCode);

            //Validate the actual BillableActivity
            Assert.Equal(expectedResponseMessage, actualResponseMessage);
        }

        //DeleteBillableActivityAsync(int id)
        [Theory]
        [InlineData(1)]
        public async void DeleteBillableActivityAsync_Deletes_One_BillableActivity_And_Returns_Number_Of_Deletions(int id)
        {
            //declare a BillableActivity
            var expectedBillableActivity = new BillableActivity
            {
                Id = 1,
                LegalCase_Id = 1,
                Employee_Id = 1,
                Title = "Billable Activity 1",
                Description = "this is the Billable Activity 1",
                Price = 120.50m,
                Start_DateTime = new DateTime(2020, 06, 20, 9, 30, 00),
                Finish_DateTime = new DateTime(2020, 06, 20, 16, 00, 00)
            };
            BillableActivityDTO expectedBaDTO = _mapper.Map<BillableActivityDTO>(expectedBillableActivity);

            //set repo return for getting the object to delete
            _mockRepository.Setup(repo => repo.GetOneByAsync(ba => ba.Id == id))
                           .ReturnsAsync(expectedBillableActivity);

            //set mockRepo return for Delete action
            _mockRepository.Setup(repo => repo.DeleteTAsync(expectedBillableActivity)).ReturnsAsync(1);

            //instantiate the controller, passing the repo object
            var controller = new BillableActivitiesController(_mockRepository.Object, _mapper, _logger);

            //Call the controller method
            var actionResult = await controller.DeleteBillableActivityAsync(id);

            //Get the int result
            var okObjectResult = actionResult.Result as OkObjectResult;
            var statusCode = okObjectResult.StatusCode;
            int actualDeleted = (int)okObjectResult.Value;

            //Assert the result
            Assert.NotNull(actionResult);

            //Validate StatusCode
            Assert.Equal(200, statusCode);

            //Validate the number of BAs deleted
            Assert.Equal(1, actualDeleted);
        }


        //delete error
        //DeleteBillableActivityAsync(int id)
        [Theory]
        [InlineData(0)]
        public async void DeleteBillableActivityAsync_Returns_NotFound_404_When_Delete_With_null_BillableActivity(int id)
        {
            //declare a BillableActivity
            BillableActivity expectedBillableActivity = null;

            //response error message:
            string expectedResponseMessage = "No data was found for the id";

            //set repo return for getting the object to delete
            _mockRepository.Setup(repo => repo.GetOneByAsync(ba => ba.Id == id))
                           .ReturnsAsync(expectedBillableActivity);

            //set mockRepo return for Delete action
            _mockRepository.Setup(repo => repo.DeleteTAsync(expectedBillableActivity)).ReturnsAsync(0);

            //instantiate the controller, passing the repo object
            var controller = new BillableActivitiesController(_mockRepository.Object, _mapper, _logger);

            //Call the controller method
            var actionResult = await controller.DeleteBillableActivityAsync(id);

            //Get the int result
            var notFoundObjectResult = actionResult.Result as NotFoundObjectResult;
            var statusCode = notFoundObjectResult.StatusCode;
            string actualResponseMessage = (string)notFoundObjectResult.Value;

            //Assert the result
            Assert.NotNull(actionResult);

            //Validate StatusCode
            Assert.Equal(404, statusCode);

            //Validate the number of BAs deleted
            Assert.Equal(expectedResponseMessage, actualResponseMessage);
        }

    }
}
