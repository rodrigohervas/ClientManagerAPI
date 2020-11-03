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
    public class ContactsControllerUnitTests
    {
        private readonly IEnumerable<Contact> _contacts;
        private readonly Mock<IGenericRepository<Contact>> _mockRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ContactsController> _logger;
        QueryStringParameters parameters;

        public ContactsControllerUnitTests()
        {
            //get Billable Activities test data
            _contacts = ContactsData.getTestContacts();
            
            //AutoMapper Configuration
            var profiles = new AutoMapperProfiles();
            var configuration = new MapperConfiguration(config => config.AddProfile(profiles));
            _mapper = new Mapper(configuration);

            //Configure Logger Mock
            var _loggerMock = new Mock<ILogger<ContactsController>>();
            _logger = _loggerMock.Object;

            //Mock Repo initialization
            _mockRepository = new Mock<IGenericRepository<Contact>>();

            //QueryStringParameters for paging
            parameters = new QueryStringParameters();
            parameters.pageNumber = 1;
            parameters.pageSize = 10;
        }

        //Task<ActionResult<IEnumerable<Contact>>> GetAllContactsAsync([FromQuery] QueryStringParameters parameters)
        [Fact]
        public async void GetAllContactsAsync_Returns_All_Contacts()
        {
            IEnumerable<ContactDTO> expectedDtos = _mapper.Map<IEnumerable<ContactDTO>>(_contacts);
            //specify the mockRepo return
            _mockRepository.Setup(repo => repo.GetAllPagedAsync(co => co.Client_Id, parameters)).ReturnsAsync(_contacts);

            //instantiate the controller
            var controller = new ContactsController(_mockRepository.Object, _mapper, _logger);

            //Call the SUT method
            var result = await controller.GetAllContactsAsync(parameters);

            //Assert the result
            Assert.NotNull(result);
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Contact>>>(result);
            var objResult = Assert.IsType<OkObjectResult>(result.Result);
            var contactsList = objResult.Value;
            IEnumerable<ContactDTO> actualDtos = _mapper.Map<IEnumerable<ContactDTO>>(contactsList);

            //use FluentAssertions to compare Collections of Reference types
            actualDtos.Should().BeEquivalentTo(expectedDtos, options => options.ComparingByMembers<ContactDTO>());
        }


        //Task<ActionResult<IEnumerable<ContactDTO>>> GetContactsByClientIdAsync([FromRoute] int client_id)
        [Theory]
        [InlineData(2)]
        public async void GetContactsByClientIdAsync_Returns_All_Contacts_for_One_Client(int client_id)
        {
            //gets all BillableActivities for one employee
            var _contactsForClient = _contacts.Where(co => co.Client_Id == client_id);
            IEnumerable<ContactDTO> expectedDtos = _mapper.Map<IEnumerable<ContactDTO>>(_contactsForClient);
            
            //Setup repository
            _mockRepository.Setup(repo => repo.GetByAsync(co => co.Client_Id == client_id))
                                                    .ReturnsAsync(_contactsForClient);

            //instantiate System Under Test
            var controller = new ContactsController(_mockRepository.Object, _mapper, _logger); ;

            //call SUT method
            var actionResult = await controller.GetContactsByClientIdAsync(client_id);

            //Assert results
            Assert.NotNull(actionResult);

            //Assert object in actionresult
            var result = Assert.IsType<OkObjectResult>(actionResult.Result);
            var actualContacts = (IEnumerable<ContactDTO>)(result.Value);
            IEnumerable<ContactDTO> dtos = _mapper.Map<IEnumerable<ContactDTO>>(actualContacts);

            //use FluentAssertions to compare Collections of Reference types
            dtos.Should().BeEquivalentTo(expectedDtos, options => options.ComparingByMembers<ContactDTO>());
        }


        //Task<ActionResult<ContactDTO>> GetContactByIdAsync([FromRoute] int id)
        [Theory]
        [InlineData(1)]
        public async void GetContactByIdAsync_Returns_One_Contact(int id)
        {
            //get the first Contact
            var contact = _contacts.FirstOrDefault<Contact>();
            var expectedContactDto = _mapper.Map<ContactDTO>(contact);

            //specify the mockRepo return
            _mockRepository.Setup(repo => repo.GetOneByAsync(co => co.Id == id)).ReturnsAsync(contact);

            //instantiate the controller, and call the method
            var controller = new ContactsController(_mockRepository.Object, _mapper, _logger);

            //Call the SUT method
            var actionResult = await controller.GetContactByIdAsync(id);

            //Assert the result
            Assert.NotNull(actionResult);

            //convert ActionResult to OkObjectResult to get its Value
            var result = Assert.IsType<OkObjectResult>(actionResult.Result);
            //get the ObjectResult.Value
            var actualContactDto = result.Value as ContactDTO;

            //use FluentAssertions to compare Reference types
            actualContactDto.Should().BeEquivalentTo(expectedContactDto, options => options.ComparingByMembers<ContactDTO>());
        }


        //Task<ActionResult<ContactWithAddressDTO>> GetContactByIdWithDetailsAsync([FromRoute] int id)
        [Theory]
        [InlineData(1)]
        public async void GetContactByIdWithDetailsAsync_Returns_One_Contact_With_Related_Addresses(int id)
        {
            //get the first Contact and its address
            var contact = _contacts.FirstOrDefault<Contact>();
            contact.Address = AddressesData.getTestAddresses().First(ad => ad.Client_Id == id);
            var expectedContactDto = _mapper.Map<ContactWithAddressDTO>(contact);

            //specify the mockRepo return
            _mockRepository.Setup(repo => repo.GetOneByWithRelatedDataAsync(co => co.Id == id, co => co.Address)).ReturnsAsync(contact);

            //instantiate the controller, and call the method
            var controller = new ContactsController(_mockRepository.Object, _mapper, _logger);

            //Call the SUT method
            var actionResult = await controller.GetContactByIdWithDetailsAsync(id);

            //Assert the result
            Assert.NotNull(actionResult);

            //convert ActionResult to OkObjectResult to get its Value
            var result = Assert.IsType<OkObjectResult>(actionResult.Result);
            //get the ObjectResult.Value
            var actualContactDto = result.Value as ContactWithAddressDTO;

            //use FluentAssertions to compare Reference types
            actualContactDto.Should().BeEquivalentTo(expectedContactDto, options => options.ComparingByMembers<ContactDTO>());
        }


        //Task<ActionResult<ContactDTO>> AddContactAsync([FromBody] Contact contact)
        [Fact]
        public async void AddContactAsync_Creates_One_Contact_And_Returns_201_And_Contact_Created()
        {
            //declare a contact
            Contact contact = _contacts.First();
            ContactDTO expectedDTO = _mapper.Map<ContactDTO>(contact);
            
            //set mockRepo return for Add action
            _mockRepository.Setup(repo => repo.AddTAsync(contact)).ReturnsAsync(1);

            //set repo return for getting the newly created object
            _mockRepository.Setup(repo => repo.GetOneByAsync(co => co.Client_Id == contact.Client_Id &&
                                                             co.Name == contact.Name &&
                                                             co.Position == contact.Position)).ReturnsAsync(contact);

            //instantiate the controller, passing the repo object
            var controller = new ContactsController(_mockRepository.Object, _mapper, _logger);

            //Call the SUT method
            var actionResult = await controller.AddContactAsync(contact);

            //Get the int result from the posted ActionResult
            var createdResult = actionResult.Result as CreatedResult;
            var statusCode = createdResult.StatusCode;
            ContactDTO actualDto = createdResult.Value as ContactDTO;

            //Assert the result
            Assert.NotNull(actionResult);

            //Validate returned status code
            Assert.Equal(201, statusCode);

            //Validate the result
            actualDto.Should().BeEquivalentTo(expectedDTO, options => options.ComparingByMembers<ContactDTO>());
        }


        //Task<ActionResult<ContactDTO>> AddContactAsync([FromBody] Contact contact)
        [Fact]
        public async void AddContactAsync_Returns_NotFound_404_When_Create_With_null_Contact()
        {
            //Configure Repository Mock
            _mockRepository.Setup(repo => repo.AddTAsync(null)).ReturnsAsync(0);
            string expectedMessage = "No Contact was created";

            //instantiate the controller, and call the method
            var controller = new ContactsController(_mockRepository.Object, _mapper, _logger);

            //Create Custom ControllerContext and add it to Controller for logging in the Controller in case of error
            controller.ControllerContext = new ControllerContextModel();

            //Call the SUT method - returns ActionResult<BillableActivity> type
            var actionResult = await controller.AddContactAsync(new Contact());

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

        //Task<ActionResult<ContactDTO>> UpdateContactAsync([FromRoute] int id, [FromBody] Contact contact)
        [Fact]
        public async void UpdateContactAsync_Updates_One_Contact_And_Returns_200_And_Contact_Updated()
        {
            //declare a Contact
            Contact contact = _contacts.FirstOrDefault();
            ContactDTO expectedDto = _mapper.Map<ContactDTO>(contact);

            //set repo return for getting the object to update
            _mockRepository.Setup(repo => repo.GetOneByAsync(co => co.Id == contact.Id)).ReturnsAsync(contact);

            //set mockRepo return for Update action
            _mockRepository.Setup(repo => repo.UpdateTAsync(contact)).ReturnsAsync(1);

            //instantiate the controller, passing the repo object
            var controller = new ContactsController(_mockRepository.Object, _mapper, _logger);

            //Call the SUT method
            //returns ActionResult<BillableActivity> type
            var actionResult = await controller.UpdateContactAsync(contact.Id, contact);

            //Get the int result from the posted ActionResult
            var okObjectResult = actionResult.Result as OkObjectResult;
            var statusCode = okObjectResult.StatusCode;
            ContactDTO dto = okObjectResult.Value as ContactDTO;

            //Assert the result
            Assert.NotNull(actionResult);

            //Validate StatusCode
            Assert.Equal(200, statusCode);

            //Validate the actual BillableActivity
            dto.Should().BeEquivalentTo(expectedDto, options => options.ComparingByMembers<ContactDTO>());
        }

        //Task<ActionResult<ContactDTO>> UpdateContactAsync([FromRoute] int id, [FromBody] Contact contact)
        [Fact]
        public async void UpdateContactAsync_Returns_NotFound_404_When_Update_With_null_Contact()
        {
            //declare a null Contact
            Contact contact = null;
            //expected return error message
            string expectedResponseMessage = "No Contact was updated";

            ///set mockRepo return for Update action
            _mockRepository.Setup(repo => repo.UpdateTAsync(contact)).ReturnsAsync(0);

            //instantiate the controller, and call the method
            var controller = new ContactsController(_mockRepository.Object, _mapper, _logger);

            //Create Custom ControllerContext and add it to Controller for logging in the Controller in case of error
            controller.ControllerContext = new ControllerContextModel();

            //Call the SUT method
            var actionResult = await controller.UpdateContactAsync(1, contact);

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

        
        //Task<ActionResult<int>> DeleteContactAsync([FromRoute] int id)
        [Theory]
        [InlineData(1)]
        public async void DeleteContactAsync_Deletes_One_Contact_And_Returns_Number_Of_Deletions(int id)
        {
            //declare a BillableActivity
            Contact contact = _contacts.FirstOrDefault();

            //set repo return for getting the object to delete
            _mockRepository.Setup(repo => repo.GetOneByAsync(ba => ba.Id == id))
                           .ReturnsAsync(contact);

            //set mockRepo return for Delete action
            _mockRepository.Setup(repo => repo.DeleteTAsync(contact)).ReturnsAsync(1);

            //instantiate the controller, passing the repo object
            var controller = new ContactsController(_mockRepository.Object, _mapper, _logger);

            //Call the controller method
            var actionResult = await controller.DeleteContactAsync(id);

            //Get the int result
            var okObjectResult = actionResult.Result as OkObjectResult;
            var statusCode = okObjectResult.StatusCode;
            int actualDeleted = (int)okObjectResult.Value;

            //Assert the result
            Assert.NotNull(actionResult);

            //Validate StatusCode
            Assert.Equal(200, statusCode);

            //Validate the number of Contacts deleted
            Assert.Equal(1, actualDeleted);
        }


        //Task<ActionResult<int>> DeleteContactAsync([FromRoute] int id)
        [Theory]
        [InlineData(0)]
        [InlineData(99)]
        public async void DeleteContactAsync_Returns_NotFound_404_When_Delete_With_non_existent_Id(int id)
        {
            //declare a null Contact
            Contact contact = null;

            //response error message:
            string expectedResponseMessage = "No Contact was found";

            //set repo return for getting the object to delete
            _mockRepository.Setup(repo => repo.GetOneByAsync(ba => ba.Id == id))
                           .ReturnsAsync(contact);

            //set mockRepo return for Delete action
            _mockRepository.Setup(repo => repo.DeleteTAsync(contact)).ReturnsAsync(0);

            //instantiate the controller, and call the method
            var controller = new ContactsController(_mockRepository.Object, _mapper, _logger);

            //Create Custom ControllerContext and add it to Controller for logging in the Controller in case of error
            controller.ControllerContext = new ControllerContextModel();

            //Call the controller method
            var actionResult = await controller.DeleteContactAsync(id);

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
