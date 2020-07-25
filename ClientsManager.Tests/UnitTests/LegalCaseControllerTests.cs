using AutoMapper;
using ClientsManager.Data;
using ClientsManager.Models;
using ClientsManager.Tests.TestData;
using ClientsManager.WebAPI.AutoMapperProfiles;
using ClientsManager.WebAPI.Controllers;
using ClientsManager.WebAPI.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
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

        public LegalCaseControllerTests()
        {
            //AutoMapper Configuration
            var profiles = new AutoMapperProfiles();
            var configuration = new MapperConfiguration(config => config.AddProfile(profiles));
            _mapper = new Mapper(configuration);

            //Repo and Controller initialization
            _mockRepository = new Mock<IGenericRepository<LegalCase>>();
            _controller = new LegalCasesController(_mockRepository.Object, _mapper);

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


        //Task<ActionResult<LegalCaseDTO>> AddLegalCaseAsync([FromBody] LegalCase legalCase)


        //Task<ActionResult<LegalCaseDTO>> UpdateLegalCaseAsync([FromRoute] int id, [FromBody] LegalCase legalCase)


        //Task<ActionResult<int>> DeleteLegalCaseAsync([FromRoute] int id)
    }
}
