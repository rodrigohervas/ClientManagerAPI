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
using System.Threading.Tasks;
using Xunit;

namespace ClientsManager.Tests.UnitTests
{
    public class AddressesControllerUnitTests
    {
        private readonly IEnumerable<Address> _addresses;
        private readonly Mock<IGenericRepository<Address>> _mockRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddressesController> _logger;
        QueryStringParameters parameters;

        public AddressesControllerUnitTests()
        {
            //get Billable Activities test data
            _addresses = AddressesData.getTestAddresses();

            //AutoMapper Configuration
            var profiles = new AutoMapperProfiles();
            var configuration = new MapperConfiguration(config => config.AddProfile(profiles));
            _mapper = new Mapper(configuration);

            //Configure Logger Mock
            var _loggerMock = new Mock<ILogger<AddressesController>>();
            _logger = _loggerMock.Object;

            //Mock Repo initialization
            _mockRepository = new Mock<IGenericRepository<Address>>();

            //QueryStringParameters for paging
            parameters = new QueryStringParameters();
            parameters.pageNumber = 1;
            parameters.pageSize = 10;
        }

        //Task<ActionResult<IEnumerable<Address>>> GetAllAddressesAsync([FromQuery] QueryStringParameters parameters)
        [Fact]
        public async void GetAllAddressesAsync_Returns_All_Addresses()
        {
            IEnumerable<AddressDTO> expectedDtos = _mapper.Map<IEnumerable<AddressDTO>>(_addresses);

            //specify the mockRepo return
            _mockRepository.Setup(repo => repo.GetAllPagedAsync(ad => ad.Client_Id, parameters)).ReturnsAsync(_addresses);

            //instantiate the controller
            var controller = new AddressesController(_mockRepository.Object, _mapper, _logger);

            //Call the SUT method
            var result = await controller.GetAllAddressesAsync(parameters);

            //Assert the result
            Assert.NotNull(result);
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Address>>>(result);
            var objResult = Assert.IsType<OkObjectResult>(result.Result);
            var addressesList = objResult.Value;
            IEnumerable<AddressDTO> actualDtos = _mapper.Map<IEnumerable<AddressDTO>>(addressesList);

            //use FluentAssertions to compare Collections of Reference types
            actualDtos.Should().BeEquivalentTo(expectedDtos, options => options.ComparingByMembers<AddressDTO>());
        }


        //Task<ActionResult<IEnumerable<AddressDTO>>> GetAddressesByClientIdAsync([FromRoute] int client_id)
        [Theory]
        [InlineData(2)]
        public async void GetAddressesByClientIdAsync_Returns_All_Addresses_for_One_Client(int client_id)
        {
            //gets all BillableActivities for one employee
            var _addressesForClient = _addresses.Where(ad => ad.Client_Id == client_id);
            IEnumerable<AddressDTO> expectedDtos = _mapper.Map<IEnumerable<AddressDTO>>(_addressesForClient);

            //Setup repository
            _mockRepository.Setup(repo => repo.GetByAsync(co => co.Client_Id == client_id))
                                                    .ReturnsAsync(_addressesForClient);

            //instantiate System Under Test
            var controller = new AddressesController(_mockRepository.Object, _mapper, _logger); ;

            //call SUT method
            var actionResult = await controller.GetAddressesByClientIdAsync(client_id);

            //Assert results
            Assert.NotNull(actionResult);

            //Assert object in actionresult
            var result = Assert.IsType<OkObjectResult>(actionResult.Result);
            var actualAddresses = (IEnumerable<AddressDTO>)(result.Value);
            IEnumerable<AddressDTO> actualDtos = _mapper.Map<IEnumerable<AddressDTO>>(actualAddresses);

            //use FluentAssertions to compare Collections of Reference types
            actualDtos.Should().BeEquivalentTo(expectedDtos, options => options.ComparingByMembers<AddressDTO>());
        }


        //Task<ActionResult<AddressDTO>> GetAddressByIdAsync([FromRoute] int id)
        [Theory]
        [InlineData(1)]
        public async void GetAddressByIdAsync_Returns_One_Address(int id)
        {
            //get the first Contact
            var address = _addresses.FirstOrDefault<Address>();
            var expectedDto = _mapper.Map<AddressDTO>(address);

            //specify the mockRepo return
            _mockRepository.Setup(repo => repo.GetOneByAsync(co => co.Id == id)).ReturnsAsync(address);

            //instantiate the controller, and call the method
            var controller = new AddressesController(_mockRepository.Object, _mapper, _logger);

            //Call the SUT method
            var actionResult = await controller.GetAddressByIdAsync(id);

            //Assert the result
            Assert.NotNull(actionResult);

            //convert ActionResult to OkObjectResult to get its Value
            var result = Assert.IsType<OkObjectResult>(actionResult.Result);
            //get the ObjectResult.Value
            var actualDTO = result.Value as AddressDTO;

            //use FluentAssertions to compare Reference types
            actualDTO.Should().BeEquivalentTo(expectedDto, options => options.ComparingByMembers<AddressDTO>());
        }


        //Task<ActionResult<AddressWithContactsDTO>> GetAddressByIdWithContactsAsync([FromRoute] int id)
        [Theory]
        [InlineData(1)]
        public async void GetAddressByIdWithContactsAsync_Returns_One_Address_With_Related_Contacts(int id)
        {
            //get the first Address and its contacts
            var address = _addresses.FirstOrDefault<Address>();
            address.Contacts = ContactsData.getTestContacts().Where(co => co.Address_Id == id) as ICollection<Contact>;
            var expectedDto = _mapper.Map<AddressWithContactsDTO>(address);

            //specify the mockRepo return
            _mockRepository.Setup(repo => repo.GetOneByWithRelatedDataAsync(ad => ad.Id == id, ad => ad.Contacts)).ReturnsAsync(address);

            //instantiate the controller, and call the method
            var controller = new AddressesController(_mockRepository.Object, _mapper, _logger);

            //Call the SUT method
            var actionResult = await controller.GetAddressByIdWithContactsAsync(id);

            //Assert the result
            Assert.NotNull(actionResult);

            //convert ActionResult to OkObjectResult to get its Value
            var result = Assert.IsType<OkObjectResult>(actionResult.Result);
            //get the ObjectResult.Value
            var actualDto = result.Value as AddressWithContactsDTO;

            //use FluentAssertions to compare Reference types
            actualDto.Should().BeEquivalentTo(expectedDto, options => options.ComparingByMembers<AddressDTO>());
        }


        //Task<ActionResult<AddressDTO>> AddAddressAsync([FromBody] Address address)
        [Fact]
        public async void AddAddressAsync_Creates_One_Address_And_Returns_201_And_Address_Created()
        {
            //declare address
            Address address = _addresses.First();
            AddressDTO expectedDTO = _mapper.Map<AddressDTO>(address);

            //set mockRepo return for Add action
            _mockRepository.Setup(repo => repo.AddTAsync(address)).ReturnsAsync(1);

            //set repo return for getting the newly created object
            _mockRepository.Setup(repo => repo.GetOneByAsync(ad => ad.Client_Id == address.Client_Id &&
                                                                   ad.StreetNumber == address.StreetNumber &&
                                                                   ad.City == address.City)).ReturnsAsync(address);

            //instantiate the controller, passing the repo object
            var controller = new AddressesController(_mockRepository.Object, _mapper, _logger);

            //Call the SUT method
            var actionResult = await controller.AddAddressAsync(address);

            //Get the int result from the posted ActionResult
            var createdResult = actionResult.Result as CreatedResult;
            var statusCode = createdResult.StatusCode;
            AddressDTO actualDto = createdResult.Value as AddressDTO;

            //Assert the result
            Assert.NotNull(actionResult);

            //Validate returned status code
            Assert.Equal(201, statusCode);

            //Validate the result
            actualDto.Should().BeEquivalentTo(expectedDTO, options => options.ComparingByMembers<AddressDTO>());
        }


        //Task<ActionResult<AddressDTO>> AddAddressAsync([FromBody] Address address)
        [Fact]
        public async void AddAddressAsync_Returns_NotFound_404_When_Create_With_null_Address()
        {
            //Configure Repository Mock
            _mockRepository.Setup(repo => repo.AddTAsync(null)).ReturnsAsync(0);
            string expectedMessage = "No Address was created";

            //instantiate the controller, and call the method
            var controller = new AddressesController(_mockRepository.Object, _mapper, _logger);

            //Create Custom ControllerContext and add it to Controller for logging in the Controller in case of error
            controller.ControllerContext = new ControllerContextModel();

            //Call the SUT method - returns ActionResult<BillableActivity> type
            var actionResult = await controller.AddAddressAsync(null);

            //Assert the result
            Assert.NotNull(actionResult);

            var result = actionResult.Result as NotFoundObjectResult;
            var statusCode = result.StatusCode;
            var message = result.Value;

            //Assert message
            Assert.Equal(expectedMessage, message);

            //Assert StatusCode
            Assert.Equal(404, statusCode);
        }

        //Task<ActionResult<AddressDTO>> UpdateAddressAsync([FromRoute] int id, [FromBody] Address address)
        [Fact]
        public async void UpdateAddressAsync_Updates_One_Address_And_Returns_200_And_Address_Updated()
        {
            //declare a Contact
            Address address = _addresses.FirstOrDefault();
            AddressDTO expectedDto = _mapper.Map<AddressDTO>(address);

            //set repo return for getting the object to update
            _mockRepository.Setup(repo => repo.GetOneByAsync(ad => ad.Id == address.Id)).ReturnsAsync(address);

            //set mockRepo return for Update action
            _mockRepository.Setup(repo => repo.UpdateTAsync(address)).ReturnsAsync(1);

            //instantiate the controller, passing the repo object
            var controller = new AddressesController(_mockRepository.Object, _mapper, _logger);

            //Call the SUT method
            //returns ActionResult<BillableActivity> type
            var actionResult = await controller.UpdateAddressAsync(address.Id, address);

            //Get the int result from the posted ActionResult
            var okObjectResult = actionResult.Result as OkObjectResult;
            var statusCode = okObjectResult.StatusCode;
            AddressDTO dto = okObjectResult.Value as AddressDTO;

            //Assert the result
            Assert.NotNull(actionResult);

            //Validate StatusCode
            Assert.Equal(200, statusCode);

            //Validate the actual BillableActivity
            dto.Should().BeEquivalentTo(expectedDto, options => options.ComparingByMembers<AddressDTO>());
        }

        //Task<ActionResult<AddressDTO>> UpdateAddressAsync([FromRoute] int id, [FromBody] Address address)
        [Fact]
        public async void UpdateAddressAsync_Returns_NotFound_404_When_Update_With_null_Address()
        {
            //expected return error message
            string expectedResponseMessage = "No Address was updated";

            ///set mockRepo return for Update action
            _mockRepository.Setup(repo => repo.UpdateTAsync(null)).ReturnsAsync(0);

            //instantiate the controller, and call the method
            var controller = new AddressesController(_mockRepository.Object, _mapper, _logger);

            //Create Custom ControllerContext and add it to Controller for logging in the Controller in case of error
            controller.ControllerContext = new ControllerContextModel();

            //Call the SUT method
            var actionResult = await controller.UpdateAddressAsync(1, null);

            //Get the int result from the posted ActionResult
            var notFoundObjectResult = actionResult.Result as NotFoundObjectResult;
            var statusCode = notFoundObjectResult.StatusCode;
            string actualResponseMessage = (string)notFoundObjectResult.Value;

            //Assert the result
            Assert.NotNull(actionResult);

            //Validate the return status code
            Assert.Equal(404, statusCode);

            //Validate the result
            Assert.Equal(expectedResponseMessage, actualResponseMessage);
        }

        //Task<ActionResult<int>> DeleteAddressAsync([FromRoute] int id)
        [Theory]
        [InlineData(1)]
        public async void DeleteAddressAsync_Deletes_One_Address_And_Returns_Number_Of_Deletions(int id)
        {
            //declare an Address
            Address address = _addresses.FirstOrDefault();

            //set repo return for getting the object to delete
            _mockRepository.Setup(repo => repo.GetOneByAsync(ad => ad.Id == id))
                           .ReturnsAsync(address);

            //set mockRepo return for Delete action
            _mockRepository.Setup(repo => repo.DeleteTAsync(address)).ReturnsAsync(1);

            //instantiate the controller, passing the repo object
            var controller = new AddressesController(_mockRepository.Object, _mapper, _logger);

            //Call the controller method
            var actionResult = await controller.DeleteAddressAsync(id);

            //Get the int result
            var okObjectResult = actionResult.Result as OkObjectResult;
            var statusCode = okObjectResult.StatusCode;
            int actualDeleted = (int)okObjectResult.Value;

            //Assert the result
            Assert.NotNull(actionResult);

            //Validate StatusCode
            Assert.Equal(200, statusCode);

            //Validate the number of Addresses deleted
            Assert.Equal(1, actualDeleted);
        }


        //Task<ActionResult<int>> DeleteAddressAsync([FromRoute] int id)
        [Theory]
        [InlineData(0)]
        [InlineData(99)]
        public async void DeleteAddressAsync_Returns_NotFound_404_When_Delete_With_non_existent_Id(int id)
        {
            //declare null address
            Address address = null;

            //response error message:
            string expectedResponseMessage = "No Address was found";

            //set repo return for getting the object to delete
            _mockRepository.Setup(repo => repo.GetOneByAsync(ad => ad.Id == id)).ReturnsAsync(address);

            //set mockRepo return for Delete action
            _mockRepository.Setup(repo => repo.DeleteTAsync(null)).ReturnsAsync(0);

            //instantiate the controller, and call the method
            var controller = new AddressesController(_mockRepository.Object, _mapper, _logger);

            //Create Custom ControllerContext and add it to Controller for logging in the Controller in case of error
            controller.ControllerContext = new ControllerContextModel();

            //Call the controller method
            var actionResult = await controller.DeleteAddressAsync(id);

            //Get the int result
            var notFoundObjectResult = actionResult.Result as NotFoundObjectResult;
            var statusCode = notFoundObjectResult.StatusCode;
            string actualResponseMessage = (string)notFoundObjectResult.Value;

            //Assert the result
            Assert.NotNull(actionResult);

            //Validate StatusCode
            Assert.Equal(404, statusCode);

            //Validate result
            Assert.Equal(expectedResponseMessage, actualResponseMessage);
        }
    }
}

