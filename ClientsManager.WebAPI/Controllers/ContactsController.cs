using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ClientsManager.Data;
using ClientsManager.Models;
using ClientsManager.WebAPI.DTOs;
using ClientsManager.WebAPI.ValidationActionFiltersMiddleware;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientsManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IGenericRepository<Contact> _genericRepository;
        private readonly IMapper _mapper;

        public ContactsController(IGenericRepository<Contact> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all Contacts for the paging parameters
        /// </summary>
        /// <param name="parameters">Paging parameters</param>
        /// <returns>Task<ActionResult<IEnumerable<Contact>>> - A collection of Contact objects</returns>
        // GET: api/contacts
        // GET: api/contacts?pageNumber=2&pageSize=3
        [HttpGet]
        [ServiceFilter(typeof(QueryStringParamsValidator))]
        public async Task<ActionResult<IEnumerable<Contact>>> GetAllContactsAsync([FromQuery] QueryStringParameters parameters)
        {
            var contacts = await _genericRepository.GetAllPagedAsync(co => co.Client_Id, parameters);

            if (!contacts.Any())
            {
                return NotFound("No Contacts where found");
            }

            IEnumerable<ContactDTO> contactsDTO = _mapper.Map<IEnumerable<ContactDTO>>(contacts);

            return Ok(contactsDTO);
        }


        /// <summary>
        /// Gets all Contacts for a provided Client Id
        /// </summary>
        /// <param name="client_id">int - The Client_Id of the Contact objects</param>
        /// <returns>Task<ActionResult<IEnumerable<ContactDTO>>> - A list of Contact objects</returns>
        //GET: api/contacts/client/1
        [HttpGet("client/{client_id:int}")]
        [ServiceFilter(typeof(ClientIdValidator))]
        public async Task<ActionResult<IEnumerable<ContactDTO>>> GetContactsByClientIdAsync([FromRoute] int client_id)
        {
            var contacts = await _genericRepository.GetByAsync(co => co.Client_Id == client_id);

            if (!contacts.Any())
            {
                return NotFound("No data was found for the client");
            }

            IEnumerable<ContactDTO> contactsDTO = _mapper.Map<IEnumerable<ContactDTO>>(contacts);

            return Ok(contactsDTO);
        }

        /// <summary>
        /// Gets the Contact for the provided Id
        /// </summary>
        /// <param name="id">int - the Contact Id</param>
        /// <returns>Task<ActionResult<ContactDTO>> - a Contact object</returns>
        //GET: api/contacts/1
        [HttpGet("{id:int}")]
        [ServiceFilter(typeof(IdValidator))]
        public async Task<ActionResult<ContactDTO>> GetContactByIdAsync([FromRoute] int id)
        {
            var contact = await _genericRepository.GetOneByAsync(co => co.Id == id);

            if (contact is null)
            {
                return NotFound("No data was found");
            }

            ContactDTO contactDTO = _mapper.Map<ContactDTO>(contact);

            return Ok(contactDTO);
        }

        /// <summary>
        /// Gets a Contact for the provided Id, including its related Address object
        /// </summary>
        /// <param name="id">int - The Contact Id</param>
        /// <returns>Task<ActionResult<ContactWithAddressDTO>> - A Contact object with its related Address object</returns>
        //GET: api/contacts/details/1
        [HttpGet("details/{id:int}")]
        [ServiceFilter(typeof(IdValidator))]
        public async Task<ActionResult<ContactWithAddressDTO>> GetContactByIdWithDetailsAsync([FromRoute] int id)
        {
            var contactWithDetails = await _genericRepository.GetOneByWithRelatedDataAsync(co => co.Id == id,
                                                                                   co => co.Address);

            if (contactWithDetails is null)
            {
                return NotFound("No data was found");
            }

            ContactWithAddressDTO contactWithDetailsDTO = _mapper.Map<ContactWithAddressDTO>(contactWithDetails);

            return Ok(contactWithDetailsDTO);
        }

        /// <summary>
        /// Creates a Contact
        /// </summary>
        /// <param name="contact">Contact - The Contact object to create</param>
        /// <returns>Task<ActionResult<ContactDTO>> - The Contact created</returns>
        // POST api/contacts
        [HttpPost]
        [ServiceFilter(typeof(ContactValidationFilter))]
        public async Task<ActionResult<ContactDTO>> AddContactAsync([FromBody] Contact contact)
        {
            int created = await _genericRepository.AddTAsync(contact);

            if (created == 0)
            {
                return NotFound("No Contact was created");
            }

            var newContact = await _genericRepository.GetOneByAsync(co =>
                                                                        co.Client_Id == contact.Client_Id &&
                                                                        co.Name == contact.Name && 
                                                                        co.Position == contact.Position);

            ContactDTO contactDTO = _mapper.Map<ContactDTO>(newContact);

            return Created("", contactDTO);
        }

        /// <summary>
        /// Updates a Contact for a provided Id
        /// </summary>
        /// <param name="id">int - the Contact Id</param>
        /// <param name="contact">Contact - The Contact object to modify</param>
        /// <returns>Task<ActionResult<ContactDTO>> - The Contact object updated</returns>
        // PUT api/contacts/5
        // PATCH api/contacts/5
        [HttpPut("{id:int}")]
        [HttpPatch("{id:int}")]
        [ServiceFilter(typeof(ContactValidationFilter))]
        public async Task<ActionResult<ContactDTO>> UpdateContactAsync([FromRoute] int id, [FromBody] Contact contact)
        {
            Contact contactResult = await _genericRepository.GetOneByAsync(co => co.Id == contact.Id);

            if (contactResult is null)
            {
                return NotFound("No Contact was updated");
            }

            int updated = await _genericRepository.UpdateTAsync(contact);

            ContactDTO contactDTO = _mapper.Map<ContactDTO>(contact);

            return Ok(contactDTO);
        }

        /// <summary>
        /// Deletes a Contact object for a provided Id
        /// </summary>
        /// <param name="id">int id - the Contact id</param>
        /// <returns>Task<ActionResult<int>> - The number of Contact objects deleted</returns>
        // DELETE api/contacts/5
        [HttpDelete("{id:int}")]
        [ServiceFilter(typeof(IdValidator))]
        public async Task<ActionResult<int>> DeleteContactAsync([FromRoute] int id)
        {
            Contact contactResult = await _genericRepository.GetOneByAsync(co => co.Id == id);

            if (contactResult is null)
            {
                return NotFound("No Contact was found");
            }

            int deletedContacts = await _genericRepository.DeleteTAsync(contactResult);

            return Ok(deletedContacts);
        }
    }
}
