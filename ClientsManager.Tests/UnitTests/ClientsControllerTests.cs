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
using Xunit;

namespace ClientsManager.Tests.UnitTests
{
    public class ClientsControllerTests
    {
        private readonly IEnumerable<Client> _clients;
        private readonly IEnumerable<Address> _addresses;
        private readonly IEnumerable<Contact> _contacts;
        private readonly IEnumerable<LegalCase> _legalCases;
        private readonly Mock<IGenericRepository<Client>> _mockRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ClientsController> _logger;
        QueryStringParameters parameters;

        public ClientsControllerTests()
        {
            //get Clients test data
            _clients = ClientsData.getTestClients();

            //AutoMapper Configuration
            var profiles = new AutoMapperProfiles();
            var configuration = new MapperConfiguration(config => config.AddProfile(profiles));
            _mapper = new Mapper(configuration);

            //Configure Logger Mock
            var _loggerMock = new Mock<ILogger<ClientsController>>();
            _logger = _loggerMock.Object;

            //Mock Repository initialization
            _mockRepository = new Mock<IGenericRepository<Client>>();

            //QueryStringParameters for paging
            parameters = new QueryStringParameters();
            parameters.pageNumber = 1;
            parameters.pageSize = 10;
        }

        //GetAllClientsAsync([FromQuery] QueryStringParameters parameters)
        [Fact]
        public async void GetAllClientsAsync_Returns_All_Clients()
        {
            //map Clients test data to DTOs
            var _clients = ClientsData.getTestClients();
            var expectedDTOs = _mapper.Map<IEnumerable<ClientDTO>>(_clients);

            //specify the mockRepo return
            _mockRepository.Setup(repo => repo.GetAllPagedAsync(cl => cl.Id, parameters)).ReturnsAsync(_clients);

            //instantiate the controller
            var controller = new ClientsController(_mockRepository.Object, _mapper, _logger);

            //Call the SUT method
            var result = await controller.GetAllClientsAsync(parameters);

            //Assert the result
            Assert.NotNull(result);
            var actionResult = Assert.IsType<ActionResult<IEnumerable<ClientDTO>>>(result);
            var objResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualClients = objResult.Value;
            IEnumerable<ClientDTO> dtos = _mapper.Map<IEnumerable<ClientDTO>>(actualClients);

            //use FluentAssertions to compare Collections of Reference types
            dtos.Should().BeEquivalentTo(expectedDTOs, options => options.ComparingByMembers<ClientDTO>());
        }


        //GetClientByIdAsync(int id)
        [Theory]
        [InlineData(1)]
        public async void GetClientByIdAsync_Returns_One_Client(int id)
        {
            //get the first Client and map it to its DTO
            var client = _clients.First(cl => cl.Id == id);
            var expectedClientDto = _mapper.Map<ClientDTO>(client);

            //specify the mockRepo return
            _mockRepository.Setup(repo => repo.GetOneByAsync(cl => cl.Id == id)).ReturnsAsync(client);

            //instantiate the controller, and call the method
            var controller = new ClientsController(_mockRepository.Object, _mapper, _logger);

            //Create Custom ControllerContext and add it to Controller for vars req and user in ClientsController.GetClientByIdAsync()
            controller.ControllerContext = new ControllerContextModel();

            //Call the SUT method
            var actionResult = await controller.GetClientByIdAsync(id);

            //Assert the result
            Assert.NotNull(actionResult);

            //convert ActionResult to OkObjectResult to get its Value: a Client type
            var result = Assert.IsType<OkObjectResult>(actionResult.Result);
            //get the ObjectResult.Value
            var actualClientDto = result.Value as ClientDTO;

            //use FluentAssertions to compare Reference types
            actualClientDto.Should().BeEquivalentTo(expectedClientDto, options => options.ComparingByMembers<ClientDTO>());
        }


        //GetClientByIdWithLegalCasesAsync([FromRoute] int id)
        [Theory]
        [InlineData(4)] //Client with id 4 has related LegalCases
        public async void GetClientByIdWithLegalCasesAsync_Returns_One_Client_With_Related_LegalCases(int id)
        {
            //get the Client with id 4 (that has related LegalCases) and map it to its DTO
            var client = _clients.First<Client>(cl => cl.Id == id);
            client.LegalCases = LegalCasesData.getTestLegalCases() as ICollection<LegalCase>;
            var expectedClientDto = _mapper.Map<ClientWithLegalCasesDTO>(client);

            //specify the mockRepo return
            _mockRepository.Setup(repo => repo.GetOneByWithRelatedDataAsync(cl => cl.Id == id, cl => cl.LegalCases)).ReturnsAsync(client);

            //instantiate the controller, and call the method
            var controller = new ClientsController(_mockRepository.Object, _mapper, _logger);

            //Call the SUT method
            var actionResult = await controller.GetClientByIdWithLegalCasesAsync(id);

            //Assert the result
            Assert.NotNull(actionResult);

            //convert ActionResult to OkObjectResult to get its Value: a Client type
            var result = Assert.IsType<OkObjectResult>(actionResult.Result);
            //get the ObjectResult.Value
            var actualClientDto = result.Value as ClientWithLegalCasesDTO;

            //use FluentAssertions to compare Reference types
            actualClientDto.Should().BeEquivalentTo(expectedClientDto, options => options.ComparingByMembers<ClientWithLegalCasesDTO>());
        }


        //GetClientByIdWithAddressesAsync(int id)
        [Theory]
        [InlineData(4)] //Client with id 4 has related Addresses
        public async void GetClientByIdWithAddressesAsync_Returns_One_Client_With_Related_Addresses(int id)
        {
            //get the Client with id 4 (that has related Addresses) and map it to its DTO
            var client = _clients.First<Client>(cl => cl.Id == id);
            client.Addresses = AddressesData.getTestAddresses() as ICollection<Address>;            
            var expectedClientDto = _mapper.Map<ClientWithAddressesDTO>(client);

            //specify the mockRepo return
            _mockRepository.Setup(repo => repo.GetOneByWithRelatedDataAsync(cl => cl.Id == id, cl => cl.Addresses)).ReturnsAsync(client);

            //instantiate the controller, and call the method
            var controller = new ClientsController(_mockRepository.Object, _mapper, _logger);

            //Call the SUT method
            var actionResult = await controller.GetClientByIdWithAddressesAsync(id);

            //Assert the result
            Assert.NotNull(actionResult);

            //convert ActionResult to OkObjectResult to get its Value: a Client type
            var result = Assert.IsType<OkObjectResult>(actionResult.Result);
            //get the ObjectResult.Value
            var actualClientDto = result.Value as ClientWithAddressesDTO;

            //use FluentAssertions to compare Reference types
            actualClientDto.Should().BeEquivalentTo(expectedClientDto, options => options.ComparingByMembers<ClientWithAddressesDTO>());
        }



        //GetClientByIdWithContactsAsync(int id)
        [Theory]
        [InlineData(4)] //Client with id 4 has related Contacts
        public async void GetClientByIdWithContactsAsync_Returns_One_Client_With_Related_Contacts(int id)
        {
            //get the Client with id 4 (that has related Contacts) and map it to its DTO
            var client = _clients.First<Client>(cl => cl.Id == id);
            client.Contacts = ContactsData.getTestContacts() as ICollection<Contact>;
            var expectedClientDto = _mapper.Map<ClientWithContactsDTO>(client);

            //specify the mockRepo return
            _mockRepository.Setup(repo => repo.GetOneByWithRelatedDataAsync(cl => cl.Id == id, cl => cl.Contacts)).ReturnsAsync(client);

            //instantiate the controller, and call the method
            var controller = new ClientsController(_mockRepository.Object, _mapper, _logger);

            //Call the SUT method
            var actionResult = await controller.GetClientByIdWithContactsAsync(id);

            //Assert the result
            Assert.NotNull(actionResult);

            //convert ActionResult to OkObjectResult to get its Value: a Client type
            var result = Assert.IsType<OkObjectResult>(actionResult.Result);
            //get the ObjectResult.Value
            var actualClientDto = result.Value as ClientWithContactsDTO;

            //use FluentAssertions to compare Reference types
            actualClientDto.Should().BeEquivalentTo(expectedClientDto, options => options.ComparingByMembers<ClientWithContactsDTO>());
        }


        //AddClientAsync(Client Client)
        [Theory]
        [InlineData(2)]
        public async void AddClientAsync_Creates_One_Client_Returns_201_And_Client_Created(int id)
        {
            //get a Client and set expected DTO
            var expectedClient = ClientsData.getTestClients().First<Client>(cl => cl.Id == id);
            ClientDTO expectedDTO = _mapper.Map<ClientDTO>(expectedClient);

            //set mockRepo return for Add action
            _mockRepository.Setup(repo => repo.AddTAsync(expectedClient)).ReturnsAsync(1);

            //set repo return for getting the newly created object
            _mockRepository.Setup(repo => repo.GetOneByAsync(cl => 
                                                    cl.Name == expectedClient.Name &&
                                                    cl.Description == expectedClient.Description &&
                                                    cl.Website == expectedClient.Website))
                                              .ReturnsAsync(expectedClient);

            //instantiate the controller, passing the repo object
            var controller = new ClientsController(_mockRepository.Object, _mapper, _logger);

            //Call the SUT method - returns ActionResult<Client> type
            var actionResult = await controller.AddClientAsync(expectedClient);

            //Get the int result from the posted ActionResult
            var createdResult = actionResult.Result as CreatedResult;
            var statusCode = createdResult.StatusCode;
            ClientDTO actualDTO = createdResult.Value as ClientDTO;

            //Assert the result
            Assert.NotNull(actionResult);

            //Validate the return is 1 Client created
            Assert.Equal(201, statusCode);

            //Validate the actual Client
            actualDTO.Should().BeEquivalentTo(expectedDTO, options => options.ComparingByMembers<ClientDTO>());
        }


        //AddClientAsync(0)
        [Fact]
        public async void AddClientAsync_Returns_NotFound_404_When_Create_With_null_Client()
        {
            //Configure Repository Mock
            _mockRepository.Setup(repo => repo.AddTAsync(null)).ReturnsAsync(0);

            //instantiate the controller, and call the method
            var controller = new ClientsController(_mockRepository.Object, _mapper, _logger);

            //Create Custom ControllerContext and add it to Controller for logging in the Controller in case of error
            controller.ControllerContext = new ControllerContextModel();

            //Call the SUT method - returns ActionResult<Client> type
            var actionResult = await controller.AddClientAsync(null); // (new Client());

            //Assert the result
            Assert.NotNull(actionResult);

            var result = actionResult.Result as NotFoundObjectResult;
            var statusCode = result.StatusCode;
            var message = result.Value;

            //Assert message
            Assert.Equal("No Client was created", message);

            //Assert StatusCode
            Assert.Equal(404, statusCode);
        }

        //UpdateClientAsync(int id, Client Client)
        [Theory]
        [InlineData(2)]
        public async void UpdateClientAsync_Updates_One_Client_Returns_200_And_Client_Updated(int id)
        {
            //declare a Client
            var expectedClient = ClientsData.getTestClients().First<Client>(cl => cl.Id == id);
            ClientDTO expectedDTO = _mapper.Map<ClientDTO>(expectedClient);

            //set repo return for getting the object to update
            _mockRepository.Setup(repo => repo.GetOneByAsync(cl => cl.Id == expectedClient.Id))
                           .ReturnsAsync(expectedClient);

            //set mockRepo return for Update action
            _mockRepository.Setup(repo => repo.UpdateTAsync(expectedClient)).ReturnsAsync(1);

            //instantiate the controller, passing the repo object
            var controller = new ClientsController(_mockRepository.Object, _mapper, _logger);

            //Call the SUT method - returns ActionResult<Client> type
            var actionResult = await controller.UpdateClientAsync(expectedClient.Id, expectedClient);

            //Get the int result from the posted ActionResult
            var okObjectResult = actionResult.Result as OkObjectResult;
            var statusCode = okObjectResult.StatusCode;
            ClientDTO actualDTO = okObjectResult.Value as ClientDTO;

            //Assert the result
            Assert.NotNull(actionResult);

            //Validate StatusCode
            Assert.Equal(200, statusCode);

            //Validate the actual Client
            actualDTO.Should().BeEquivalentTo(expectedDTO, options => options.ComparingByMembers<ClientDTO>());
        }

        //UpdateClientAsync(int id, Client Client)
        [Theory]
        [InlineData(0)]
        public async void UpdateClientAsync_Returns_NotFound_404_When_Update_With_null_Client(int id)
        {
            //declare a Client
            Client expectedClient = null;
            //expected return error message
            string expectedResponseMessage = "No Client was updated";

            ///set mockRepo return for Update action
            _mockRepository.Setup(repo => repo.UpdateTAsync(expectedClient)).ReturnsAsync(id);

            //instantiate the controller, and call the method
            var controller = new ClientsController(_mockRepository.Object, _mapper, _logger);

            //Create Custom ControllerContext and add it to Controller for logging in the Controller in case of error
            controller.ControllerContext = new ControllerContextModel();

            //Call the SUT method
            //returns ActionResult<Client> type
            var actionResult = await controller.UpdateClientAsync(0, expectedClient);

            //Get the int result from the posted ActionResult
            var notFoundObjectResult = actionResult.Result as NotFoundObjectResult;
            var statusCode = notFoundObjectResult.StatusCode;
            string actualResponseMessage = (string)notFoundObjectResult.Value;

            //Assert the result
            Assert.NotNull(actionResult);

            //Validate the return status code
            Assert.Equal(404, statusCode);

            //Validate the actual Client
            Assert.Equal(expectedResponseMessage, actualResponseMessage);
        }

        //DeleteClientAsync(int id)
        [Theory]
        [InlineData(1)]
        public async void DeleteClientAsync_Deletes_One_Client_And_Returns_Number_Of_Deletions(int id)
        {
            //declare a Client
            var expectedClient = ClientsData.getTestClients().First(cl => cl.Id == id);
            ClientDTO expectedDTO = _mapper.Map<ClientDTO>(expectedClient);

            //set repo return for getting the object to delete
            _mockRepository.Setup(repo => repo.GetOneByAsync(cl => cl.Id == id))
                           .ReturnsAsync(expectedClient);

            //set mockRepo return for Delete action
            _mockRepository.Setup(repo => repo.DeleteTAsync(expectedClient)).ReturnsAsync(1);

            //instantiate the controller, passing the repo object
            var controller = new ClientsController(_mockRepository.Object, _mapper, _logger);

            //Call the controller method
            var actionResult = await controller.DeleteClientAsync(id);

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


        //DeleteClientAsync(int id)
        [Theory]
        [InlineData(0)]
        public async void DeleteClientAsync_Returns_NotFound_404_When_Delete_With_null_Client(int id)
        {
            //declare a Client
            Client expectedClient = null;

            //response error message:
            string expectedResponseMessage = "No Client was found";

            //set repo return for getting the object to delete
            _mockRepository.Setup(repo => repo.GetOneByAsync(ba => ba.Id == id))
                           .ReturnsAsync(expectedClient);

            //set mockRepo return for Delete action
            _mockRepository.Setup(repo => repo.DeleteTAsync(expectedClient)).ReturnsAsync(0);

            //instantiate the controller, and call the method
            var controller = new ClientsController(_mockRepository.Object, _mapper, _logger);

            //Create Custom ControllerContext and add it to Controller for logging in the Controller in case of error
            controller.ControllerContext = new ControllerContextModel();

            //Call the controller method
            var actionResult = await controller.DeleteClientAsync(id);

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
