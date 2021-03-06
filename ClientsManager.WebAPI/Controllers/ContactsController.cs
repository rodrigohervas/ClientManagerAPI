﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ClientsManager.Data;
using ClientsManager.Models;
using ClientsManager.WebAPI.DTOs;
using ClientsManager.WebAPI.ValidationActionFiltersMiddleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ClientsManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    public class ContactsController : ControllerBase
    {
        private readonly IGenericRepository<Contact> _genericRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;


        public ContactsController(IGenericRepository<Contact> genericRepository, IMapper mapper, ILogger<ContactsController> logger)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _logger = logger;
        }


        /// <summary>
        /// Gets all Contacts for the paging parameters
        /// </summary>
        /// <param name="parameters">Paging parameters</param>
        /// <returns>Task&lt;ActionResult&lt;IEnumerable&lt;Contact&gt;&gt;&gt; - A collection of Contact objects</returns>
        /// <![CDATA[<returns>Task<ActionResult<IEnumerable<Contact>>> - A collection of Contact objects</returns>]]>
        // GET: api/contacts
        // GET: api/contacts?pageNumber=2&pageSize=3
        [HttpGet]
        [ServiceFilter(typeof(QueryStringParamsValidator))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Contact>>> GetAllContactsAsync([FromQuery] QueryStringParameters parameters)
        {
            var contacts = await _genericRepository.GetAllPagedAsync(co => co.Client_Id, parameters);

            if (!contacts.Any())
            {
                var logData = new
                {
                    Parameters = parameters,
                    Action = ControllerContext.ActionDescriptor.DisplayName,
                    Verb = HttpContext.Request.Method,
                    EndpointPath = HttpContext.Request.Path.Value,
                    User = HttpContext.User.Claims.First(usr => usr.Type == "preferred_username").Value
                };
                _logger.LogInformation("No Contacts were found for Parameters {parameters}. Data: {@logData}", parameters, logData);

                return NotFound("No Contacts were found");
            }

            IEnumerable<ContactDTO> contactsDTO = _mapper.Map<IEnumerable<ContactDTO>>(contacts);

            return Ok(contactsDTO);
        }


        /// <summary>
        /// Gets all Contacts for a provided Client Id
        /// </summary>
        /// <param name="client_id">int - The Client_Id of the Contact objects</param>
        /// <returns>Task&lt;ActionResult&lt;IEnumerable&lt;ContactDTO&gt;&gt;&gt; - A list of Contact objects</returns>
        /// <![CDATA[ <returns>Task<ActionResult<IEnumerable<ContactDTO>>> - A list of Contact objects</returns> ]]>
        //GET: api/contacts/client/1
        [HttpGet("client/{client_id:int}")]
        [ServiceFilter(typeof(ClientIdValidator))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ContactDTO>>> GetContactsByClientIdAsync([FromRoute] int client_id)
        {
            var contacts = await _genericRepository.GetByAsync(co => co.Client_Id == client_id);

            if (!contacts.Any())
            {
                var logData = new
                {
                    Client_Id = client_id,
                    Action = ControllerContext.ActionDescriptor.DisplayName,
                    Verb = HttpContext.Request.Method,
                    EndpointPath = HttpContext.Request.Path.Value,
                    User = HttpContext.User.Claims.First(usr => usr.Type == "preferred_username").Value
                };
                _logger.LogInformation("No Contacts were found for Client_Id {client_id}. Data: {@logData}", client_id, logData);

                return NotFound("No contacts were found for the client");
            }

            IEnumerable<ContactDTO> contactsDTO = _mapper.Map<IEnumerable<ContactDTO>>(contacts);

            return Ok(contactsDTO);
        }

        /// <summary>
        /// Gets the Contact for the provided Id
        /// </summary>
        /// <param name="id">int - the Contact Id</param>
        /// <returns>Task&lt;ActionResult&lt;ContactDTO&gt;&gt; - a Contact object</returns>
        /// <![CDATA[ <returns>Task<ActionResult<ContactDTO>> - a Contact object</returns> ]]>
        //GET: api/contacts/1
        [HttpGet("{id:int}")]
        [ServiceFilter(typeof(IdValidator))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ContactDTO>> GetContactByIdAsync([FromRoute] int id)
        {
            var contact = await _genericRepository.GetOneByAsync(co => co.Id == id);

            if (contact is null)
            {
                var logData = new
                {
                    Id = id,
                    Action = ControllerContext.ActionDescriptor.DisplayName,
                    Verb = HttpContext.Request.Method,
                    EndpointPath = HttpContext.Request.Path.Value,
                    User = HttpContext.User.Claims.First(usr => usr.Type == "preferred_username").Value
                };
                _logger.LogInformation("No Contact was found for Id {id}. Data: {@logData}", id, logData);

                return NotFound("No data was found");
            }

            ContactDTO contactDTO = _mapper.Map<ContactDTO>(contact);

            return Ok(contactDTO);
        }

        /// <summary>
        /// Gets a Contact for the provided Id, including its related Address object
        /// </summary>
        /// <param name="id">int - The Contact Id</param>
        /// <returns>Task&lt;ActionResult&lt;ContactWithAddressDTO&gt;&gt; - A Contact object with its related Address object</returns>
        /// <![CDATA[ <returns>Task<ActionResult<ContactWithAddressDTO>> - A Contact object with its related Address object</returns> ]]>
        //GET: api/contacts/details/1
        [HttpGet("details/{id:int}")]
        [ServiceFilter(typeof(IdValidator))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ContactWithAddressDTO>> GetContactByIdWithDetailsAsync([FromRoute] int id)
        {
            var contactWithDetails = await _genericRepository.GetOneByWithRelatedDataAsync(co => co.Id == id,
                                                                                   co => co.Address);

            if (contactWithDetails is null)
            {
                var logData = new
                {
                    Id = id,
                    Action = ControllerContext.ActionDescriptor.DisplayName,
                    Verb = HttpContext.Request.Method,
                    EndpointPath = HttpContext.Request.Path.Value,
                    User = HttpContext.User.Claims.First(usr => usr.Type == "preferred_username").Value
                };
                _logger.LogInformation("No Contact was found for Id {id}. Data: {@logData}", id, logData);

                _logger.LogInformation($"ContactsController.GetContactByIdWithDetailsAsync: No data was found for id {id}");
                return NotFound("No data was found");
            }

            ContactWithAddressDTO contactWithDetailsDTO = _mapper.Map<ContactWithAddressDTO>(contactWithDetails);

            return Ok(contactWithDetailsDTO);
        }

        /// <summary>
        /// Creates a Contact
        /// </summary>
        /// <param name="contact">Contact - The Contact object to create</param>
        /// <returns>Task&lt;ActionResult&lt;ContactDTO&gt;&gt; - The Contact created</returns>
        /// <![CDATA[ <returns>Task<ActionResult<ContactDTO>> - The Contact created</returns> ]]>
        // POST api/contacts
        [HttpPost]
        [ServiceFilter(typeof(ContactValidationFilter))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ContactDTO>> AddContactAsync([FromBody] Contact contact)
        {
            int created = await _genericRepository.AddTAsync(contact);

            if (created == 0)
            {
                var logData = new
                {
                    Contact = contact,
                    Action = ControllerContext.ActionDescriptor.DisplayName,
                    Verb = HttpContext.Request.Method,
                    EndpointPath = HttpContext.Request.Path.Value,
                    User = HttpContext.User.Claims.First(usr => usr.Type == "preferred_username").Value
                };
                _logger.LogInformation("No Contact was created for Contact {@contact}. Data: {@logData}", contact, logData);

                return NotFound("No Contact was created");
            }

            _logger.LogInformation("Contact was created for Contact {@contact}.", contact);

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
        /// <returns>Task&lt;ActionResult&lt;ContactDTO&gt;&gt; - The Contact object updated</returns>
        /// <![CDATA[ <returns>Task<ActionResult<ContactDTO>> - The Contact object updated</returns> ]]>
        // PUT api/contacts/5
        // PATCH api/contacts/5
        [HttpPut("{id:int}")]
        [HttpPatch("{id:int}")]
        [ServiceFilter(typeof(ContactValidationFilter))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ContactDTO>> UpdateContactAsync([FromRoute] int id, [FromBody] Contact contact)
        {
            Contact contactResult = await _genericRepository.GetOneByAsync(co => co.Id == contact.Id);

            if (contactResult is null)
            {
                var logData = new
                {
                    Id = id, 
                    Contact = contact,
                    Action = ControllerContext.ActionDescriptor.DisplayName,
                    Verb = HttpContext.Request.Method,
                    EndpointPath = HttpContext.Request.Path.Value,
                    User = HttpContext.User.Claims.First(usr => usr.Type == "preferred_username").Value
                };
                _logger.LogInformation("No Contact was updated for Id {id} and  Contact {@contact}. Data: {@logData}", id, contact, logData);

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
        /// <returns>Task&lt;ActionResult&lt;int&gt;&gt; - The number of Contact objects deleted</returns>
        /// <![CDATA[ <returns>Task<ActionResult<int>> - The number of Contact objects deleted</returns> ]]>
        // DELETE api/contacts/5
        [HttpDelete("{id:int}")]
        [ServiceFilter(typeof(IdValidator))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> DeleteContactAsync([FromRoute] int id)
        {
            Contact contactResult = await _genericRepository.GetOneByAsync(co => co.Id == id);

            if (contactResult is null)
            {
                var logData = new
                {
                    Id = id,
                    Action = ControllerContext.ActionDescriptor.DisplayName,
                    Verb = HttpContext.Request.Method,
                    EndpointPath = HttpContext.Request.Path.Value,
                    User = HttpContext.User.Claims.First(usr => usr.Type == "preferred_username").Value
                };
                _logger.LogInformation("No Contact was found for Id {id}. Data: {@logData}", id, logData);

                _logger.LogInformation($"ContactsController.DeleteContactAsync: No Contact was found for id {id}");
                return NotFound("No Contact was found");
            }

            int deletedContacts = await _genericRepository.DeleteTAsync(contactResult);

            return Ok(deletedContacts);
        }
    }
}
