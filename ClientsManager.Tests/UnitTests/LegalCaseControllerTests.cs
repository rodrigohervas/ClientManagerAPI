using AutoMapper;
using ClientsManager.Data;
using ClientsManager.Models;
using ClientsManager.Tests.Models;
using ClientsManager.Tests.TestData;
using ClientsManager.WebAPI.AutoMapperProfiles;
using ClientsManager.WebAPI.Controllers;
using ClientsManager.WebAPI.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
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
    public class LegalCaseControllerTests
    {
        private readonly Mock<IGenericRepository<LegalCase>> _mockRepository;
        private readonly LegalCasesController _controller;
        private readonly IMapper _mapper;
        private readonly IEnumerable<LegalCase> _legalCases;
        private readonly ILogger<LegalCasesController> _logger;

        public LegalCaseControllerTests()
        {
            //AutoMapper Configuration
            var profiles = new AutoMapperProfiles();
            var configuration = new MapperConfiguration(config => config.AddProfile(profiles));
            _mapper = new Mapper(configuration);

            //Configure Logger Mock
            var _loggerMock = new Mock<ILogger<LegalCasesController>>();
            _logger = _loggerMock.Object;

            //Repo and Controller initialization
            _mockRepository = new Mock<IGenericRepository<LegalCase>>();
            _controller = new LegalCasesController(_mockRepository.Object, _mapper, _logger);

            //Create Custom ControllerContext and add it to Controller for logging in the Controller in case of error
            _controller.ControllerContext = new ControllerContextModel();

            //Load LegalCases Test Data
            _legalCases = LegalCasesData.GetLegalCases();
        }

        //GetAllLegalCasesAsync([FromQuery] QueryStringParameters parameters)1
        [Theory]
        [ClassData(typeof(QueryStringParametersData))]
        public async void GetAllLegalCasesAsync_Returns_LegalCases_Paged(QueryStringParameters parameters)
        {
            //get the expected LegalCases
            IEnumerable<LegalCase> expectedLegalCases = _legalCases
                                        .OrderBy(lc => lc.Client_Id)
                                        .Skip((parameters.pageNumber - 1) * parameters.pageSize)
                                        .Take(parameters.pageSize);
            IEnumerable<LegalCaseDTO> expectedLegalCasesDTO = _mapper.Map<IEnumerable<LegalCaseDTO>>(expectedLegalCases);


            //configure the Repo return
            _mockRepository.Setup(repo => repo.GetAllPagedAsync(lc => lc.Client_Id, parameters)).ReturnsAsync(expectedLegalCases);

            //call the controller method
            var actionResult = await _controller.GetAllLegalCasesAsync(parameters);

            //Get the LegalCases form the ActionResult returned
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            IEnumerable<LegalCaseDTO> actualLegalCasesDTO = okObjectResult.Value as IEnumerable<LegalCaseDTO>;
            int? statusCode = okObjectResult.StatusCode;

            //Assertions
            Assert.Equal(200, statusCode);

            Assert.IsType<List<LegalCaseDTO>>(actualLegalCasesDTO);

            //use FluentAssertions to compare Collections of Reference types
            actualLegalCasesDTO.Should().BeEquivalentTo(expectedLegalCasesDTO, options => options.ComparingByMembers<LegalCaseDTO>());
        }

        //GetLegalCasesByClientIdAsync(int client_id)
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void GetLegalCasesByClientIdAsync_Returns_LegalCasesForAClientId(int client_id)
        {
            //get expected LegalCases
            IEnumerable<LegalCase> expectedLegalCases = _legalCases.Where(lc => lc.Client_Id == client_id);
            IEnumerable<LegalCaseDTO> expectedCasesDTO = _mapper.Map<IEnumerable<LegalCaseDTO>>(expectedLegalCases);

            //configure the Repo return
            _mockRepository.Setup(repo => repo.GetByAsync(lc => lc.Client_Id == client_id)).ReturnsAsync(expectedLegalCases);

            //call the controller method
            var actionResult = await _controller.GetLegalCasesByClientIdAsync(client_id);

            //Get the LegalCases from the ActionResult returned
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            IEnumerable<LegalCaseDTO> actualLegalCasesDTO = okObjectResult.Value as IEnumerable<LegalCaseDTO>;
            int? statusCode = okObjectResult.StatusCode;

            //Assertions
            Assert.Equal(200, statusCode);

            Assert.IsType<List<LegalCaseDTO>>(actualLegalCasesDTO);

            //use FluentAssertions to compare Collections of Reference types
            actualLegalCasesDTO.Should().BeEquivalentTo(expectedCasesDTO, options => options.ComparingByMembers<LegalCaseDTO>());
        }

        //GetLegalCasesByClientIdAsync(int client_id)
        [Theory]
        [InlineData(15)]
        public async void GetLegalCasesByClientIdAsync_Returns_NotFound_For_Wrong_ClientId(int client_id)
        {
            //get expected LegalCases
            IEnumerable<LegalCase> expectedLegalCases = new List<LegalCase>();

            //configure the Repo return
            _mockRepository.Setup(repo => repo.GetByAsync(lc => lc.Client_Id == client_id)).ReturnsAsync(expectedLegalCases);

            //call the controller method
            var actionResult = await _controller.GetLegalCasesByClientIdAsync(client_id);

            //Get the Message from the ActionResult returned
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            string responseMessage = notFoundObjectResult.Value.ToString();
            int? statusCode = notFoundObjectResult.StatusCode;

            //Assertions
            Assert.Equal(404, statusCode);

            responseMessage.Should().Be("No data was found for the client");
        }


        //GetLegalCaseByIdAsync([FromRoute] int id)
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void GetLegalCaseByIdAsync_Returns_LegalCaseForAnId(int id)
        {
            //get expected LegalCases
            LegalCase expectedLegalCase = _legalCases.Where(lc => lc.Id == id).First();
            LegalCaseDTO expectedCaseDTO = _mapper.Map<LegalCaseDTO>(expectedLegalCase);

            //configure the Repo return
            _mockRepository.Setup(repo => repo.GetOneByAsync(lc => lc.Id == id)).ReturnsAsync(expectedLegalCase);

            //call the controller method
            var actionResult = await _controller.GetLegalCaseByIdAsync(id);

            //Get the LegalCase from the ActionResult returned
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            LegalCaseDTO actualLegalCaseDTO = okObjectResult.Value as LegalCaseDTO;
            int? statusCode = okObjectResult.StatusCode;

            //Assertions
            Assert.Equal(200, statusCode);

            Assert.IsType<LegalCaseDTO>(actualLegalCaseDTO);

            //use FluentAssertions to compare Reference types
            actualLegalCaseDTO.Should().BeEquivalentTo(expectedCaseDTO, options => options.ComparingByMembers<LegalCaseDTO>());
        }


        //GetLegalCaseByIdWithDetailsAsync(FromRoute] int id)
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void GetLegalCaseByIdWithDetailsAsync_Returns_LegalCaseWithDetails(int id)
        {
            //get expected LegalCase
            LegalCase expectedLegalCase = _legalCases.Where(lc => lc.Id == id).First();
            LegalCaseWithBillableActivitiesDTO expectedCaseDTO = _mapper.Map<LegalCaseWithBillableActivitiesDTO>(expectedLegalCase);

            //configure the Repo return
            _mockRepository.Setup(repo => repo.GetOneByWithRelatedDataAsync(lc => lc.Id == id, lc => lc.BillableActivities)).ReturnsAsync(expectedLegalCase);

            //call the controller method
            var actionResult = await _controller.GetLegalCaseByIdWithDetailsAsync(id);

            //Get the LegalCase from the ActionResult returned
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            LegalCaseWithBillableActivitiesDTO actualLegalCaseDTO = okObjectResult.Value as LegalCaseWithBillableActivitiesDTO;
            int? statusCode = okObjectResult.StatusCode;

            //Assertions
            Assert.Equal(200, statusCode);

            Assert.IsType<LegalCaseWithBillableActivitiesDTO>(actualLegalCaseDTO);

            //use FluentAssertions to compare Reference types
            actualLegalCaseDTO.Should().BeEquivalentTo(expectedCaseDTO, options => options.ComparingByMembers<LegalCaseWithBillableActivitiesDTO>());
        }


        //GetLegalCaseByIdWithDetailsAsync(FromRoute] int id)
        [Theory]
        [InlineData(15)]
        public async void GetLegalCaseByIdWithDetailsAsync_Returns_NotFound_For_Wrong_Id(int id)
        {
            //get expected LegalCase
            LegalCase expectedLegalCase = null;

            //configure the Repo return
            _mockRepository.Setup(repo => repo.GetOneByWithRelatedDataAsync(lc => lc.Id == id, lc => lc.BillableActivities)).ReturnsAsync(expectedLegalCase);

            //call the controller method
            var actionResult = await _controller.GetLegalCaseByIdWithDetailsAsync(id);

            //Get the Message from the ActionResult returned
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            string responseMessage = notFoundObjectResult.Value.ToString();
            int? statusCode = notFoundObjectResult.StatusCode;

            //Assertions
            Assert.Equal(404, statusCode);

            responseMessage.Should().Be("No data was found");
        }


        //AddLegalCaseAsync([FromBody] LegalCase legalCase)
        [Fact]
        public async void AddLegalCaseAsync_Creates_And_Returns_New_LegalCase()
        {
            //get expected LegalCase
            LegalCase legalCase = _legalCases.FirstOrDefault();
            LegalCaseDTO expectedLegalCaseDTO = _mapper.Map<LegalCaseDTO>(legalCase);

            //configure Repo return of Adding the LegalCase
            _mockRepository.Setup(repo => repo.AddTAsync(legalCase)).ReturnsAsync(1);

            //configure Repo return of getting the newly created LegalCase
            _mockRepository.Setup(repo => repo.GetOneByAsync(lc => lc.Client_Id == legalCase.Client_Id &&
                                                                   lc.Title == legalCase.Title))
                                              .ReturnsAsync(legalCase);

            //call the controller method
            var actionResult = await _controller.AddLegalCaseAsync(legalCase);

            //Get the LegalCase from the ActionResult returned
            var createdResult = Assert.IsType<CreatedResult>(actionResult.Result);
            LegalCaseDTO actualLegalCaseDTO = createdResult.Value as LegalCaseDTO;
            int? statusCode = createdResult.StatusCode;

            //Assertions
            Assert.Equal(201, statusCode);

            Assert.IsType<LegalCaseDTO>(actualLegalCaseDTO);

            //use FluentAssertions to compare Reference types
            actualLegalCaseDTO.Should().BeEquivalentTo(expectedLegalCaseDTO, options => options.ComparingByMembers<LegalCaseDTO>());
        }


        //AddLegalCaseAsync([FromBody] LegalCase legalCase)
        [Fact]
        public async void AddLegalCaseAsync_Returns_NotFound_404_When_Create_With_null_LegalCase()
        {
            //get expected LegalCase
            LegalCase legalCase = null;

            //configure Repo return of Adding the LegalCase
            _mockRepository.Setup(repo => repo.AddTAsync(legalCase)).ReturnsAsync(0);

            //call the controller method
            var actionResult = await _controller.AddLegalCaseAsync(legalCase);

            //Get the LegalCase from the ActionResult returned
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            string responseMessage = (string)notFoundResult.Value;
            int? statusCode = notFoundResult.StatusCode;

            //Assertions
            Assert.IsType<int>(statusCode);
            Assert.Equal(404, statusCode);

            Assert.IsType<string>(responseMessage);
            Assert.Equal("No Legal Case was created", responseMessage);
        }


        //UpdateLegalCaseAsync([FromRoute] int id, [FromBody] LegalCase legalCase)
        [Fact]
        public async void UpdateLegalCaseAsync_Updates_Existing_LegalCase_And_Returns_Updated_LegalCase()
        {
            //get expected LegalCase
            LegalCase legalCase = new LegalCase()
            {
                Id = 1,
                Client_Id = 2,
                Title = "Title for Case Updated",
                Description = "Description for Case Updated",
                TrustFund = 9999.99m
            };
            LegalCaseDTO expectedLegalCaseDTO = _mapper.Map<LegalCaseDTO>(legalCase);

            //configure Repo return of getting the LegalCase to be updated
            _mockRepository.Setup(repo => repo.GetOneByAsync(lc => lc.Id == legalCase.Id)).ReturnsAsync(legalCase);

            //configure Repo return of the Update action
            _mockRepository.Setup(repo => repo.UpdateTAsync(legalCase)).ReturnsAsync(1);

            //call the controller method
            var actionResult = await _controller.UpdateLegalCaseAsync(legalCase.Id, legalCase);

            //Get the LegalCase from the ActionResult returned
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            LegalCaseDTO actualLegalCaseDTO = okObjectResult.Value as LegalCaseDTO;
            int? statusCode = okObjectResult.StatusCode;

            //Assertions
            Assert.Equal(200, statusCode);

            Assert.IsType<LegalCaseDTO>(actualLegalCaseDTO);

            //use FluentAssertions to compare Reference types
            actualLegalCaseDTO.Should().BeEquivalentTo(expectedLegalCaseDTO, options => options.ComparingByMembers<LegalCaseDTO>());
        }

        //UpdateLegalCaseAsync([FromRoute] int id, [FromBody] LegalCase legalCase)
        [Fact]
        public async void UpdateLegalCaseAsync_Returns_NotFound_404_When_Update_With_null_LegalCase()
        {
            //get expected LegalCase
            LegalCase legalCase = null;

            //configure Repo return of Adding the LegalCase
            _mockRepository.Setup(repo => repo.UpdateTAsync(legalCase)).ReturnsAsync(0);

            //call the controller method
            var actionResult = await _controller.UpdateLegalCaseAsync(1, legalCase);

            //Get the LegalCase from the ActionResult returned
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            string responseMessage = (string)notFoundResult.Value;
            int? statusCode = notFoundResult.StatusCode;

            //Assertions
            Assert.IsType<int>(statusCode);
            Assert.Equal(404, statusCode);

            Assert.IsType<string>(responseMessage);
            Assert.Equal("No Legal Case was updated", responseMessage);
        }

        //DeleteLegalCaseAsync([FromRoute] int id)
        [Theory]
        [InlineData(1)]
        public async void DeleteLegalCaseAsync_Deletes_A_LegalCase_And_Returns_Number_Of_Deletions(int id)
        {
            //get expected LegalCase
            //LegalCase legalCase = _legalCases.FirstOrDefault();
            LegalCase legalCase = new LegalCase()
            {
                Id = 1,
                Client_Id = 1,
                Title = "Title for Case Delete",
                Description = "Description for Case Delete",
                TrustFund = 7777.33m
            };
            
            //configure Repo return of getting the LegalCase to be deleted
            _mockRepository.Setup(repo => repo.GetOneByAsync(lc => lc.Id == id)).ReturnsAsync(legalCase);

            //configure Repo return of the Delete action
            _mockRepository.Setup(repo => repo.DeleteTAsync(legalCase)).ReturnsAsync(1);

            //call the controller method
            //var actionResult = await _controller.DeleteLegalCaseAsync(legalCase.Id);
            var actionResult = await _controller.DeleteLegalCaseAsync(legalCase.Id);

            //Get the number of deleted LegalCases from the ActionResult returned
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            int actualLegalCasesDeletedNumber = (int)okObjectResult.Value;
            int? statusCode = okObjectResult.StatusCode;

            //Assertions
            Assert.Equal(200, statusCode);

            Assert.IsType<int>(actualLegalCasesDeletedNumber);

            //use FluentAssertions to compare Reference types
            Assert.Equal(1, actualLegalCasesDeletedNumber);
        }

        //DeleteLegalCaseAsync([FromRoute] int id)
        [Theory]
        [InlineData(0)]
        public async void DeleteLegalCaseAsync_Returns_NotFound_404_When_Delete_With_non_existent_LegalCase(int id)
        {
            //get expected LegalCase
            LegalCase legalCase = null;

            //configure Repo return of getting the LegalCase to be deleted
            _mockRepository.Setup(repo => repo.GetOneByAsync(lc => lc.Id == id)).ReturnsAsync(legalCase);

            //configure Repo return of Adding the LegalCase
            _mockRepository.Setup(repo => repo.DeleteTAsync(legalCase)).ReturnsAsync(0);

            //call the controller method            
            var actionResult = await _controller.DeleteLegalCaseAsync(id);

            //Get the LegalCase from the ActionResult returned
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            string responseMessage = (string)notFoundResult.Value;
            int? statusCode = notFoundResult.StatusCode;

            //Assertions
            Assert.IsType<int>(statusCode);
            Assert.Equal(404, statusCode);

            Assert.IsType<string>(responseMessage);
            Assert.Equal("No Legal Case was found", responseMessage);
        }
    }
}
