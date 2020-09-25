using System;
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
    public class AddressesController : ControllerBase
    {
        private readonly IGenericRepository<Address> _genericRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public AddressesController(IGenericRepository<Address> genericRepository, IMapper mapper, ILogger<AddressesController> logger)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Gets all Addresses for the paging parameters
        /// </summary>
        /// <param name="parameters">Paging parameters</param>
        /// <returns>Task<ActionResult<IEnumerable<Address>>> - A collection of Address objects</returns>
        // GET: api/addresses
        // GET: api/addresses?pageNumber=2&pageSize=3
        [HttpGet]
        [ServiceFilter(typeof(QueryStringParamsValidator))]
        public async Task<ActionResult<IEnumerable<Address>>> GetAllAddressesAsync([FromQuery] QueryStringParameters parameters)
        {
            var addresses = await _genericRepository.GetAllPagedAsync(ad => ad.Client_Id, parameters);

            if (!addresses.Any())
            {
                var logData = new
                {
                    Parameters = parameters,
                    Action = ControllerContext.ActionDescriptor.DisplayName,
                    Verb = HttpContext.Request.Method,
                    EndpointPath = HttpContext.Request.Path.Value,
                    User = HttpContext.User.Claims.First(usr => usr.Type == "preferred_username").Value
                };
                _logger.LogError("No Addresses where found for Parameters {parameters}. Data: {@logData}", parameters, logData);

                return NotFound("No Addresses where found");
            }

            IEnumerable<AddressDTO> addressesDTO = _mapper.Map<IEnumerable<AddressDTO>>(addresses);

            return Ok(addressesDTO);
        }


        /// <summary>
        /// Gets all Addresses for a provided Client Id
        /// </summary>
        /// <param name="client_id">int - The Client_Id of the Address objects</param>
        /// <returns>Task<ActionResult<IEnumerable<AddressDTO>>> - A list of Address objects</returns>
        //GET: api/addresses/client/1
        [HttpGet("client/{client_id:int}")]
        [ServiceFilter(typeof(ClientIdValidator))]
        public async Task<ActionResult<IEnumerable<AddressDTO>>> GetAddressesByClientIdAsync([FromRoute] int client_id)
        {
            var addresses = await _genericRepository.GetByAsync(ad => ad.Client_Id == client_id);

            if (!addresses.Any())
            {
                var logData = new
                {
                    Client_Id = client_id,
                    Action = ControllerContext.ActionDescriptor.DisplayName,
                    Verb = HttpContext.Request.Method,
                    EndpointPath = HttpContext.Request.Path.Value,
                    User = HttpContext.User.Claims.First(usr => usr.Type == "preferred_username").Value
                };
                _logger.LogError("No Address was found for the client with client_id {client_id}. Data: {@logData}", client_id, logData);

                return NotFound("No data was found for the client");
            }

            IEnumerable<AddressDTO> addressesDTO = _mapper.Map<IEnumerable<AddressDTO>>(addresses);

            return Ok(addressesDTO);
        }

        /// <summary>
        /// Gets the Address for the provided Id
        /// </summary>
        /// <param name="id">int - the Address Id</param>
        /// <returns>Task<ActionResult<AddressDTO>> - an Address object</returns>
        //GET: api/addresses/1
        [HttpGet("{id:int}")]
        [ServiceFilter(typeof(IdValidator))]
        public async Task<ActionResult<AddressDTO>> GetAddressByIdAsync([FromRoute] int id)
        {
            var address = await _genericRepository.GetOneByAsync(ad => ad.Id == id);

            if (address is null)
            {
                var logData = new
                {
                    Id = id,
                    Action = ControllerContext.ActionDescriptor.DisplayName,
                    Verb = HttpContext.Request.Method,
                    EndpointPath = HttpContext.Request.Path.Value,
                    User = HttpContext.User.Claims.First(usr => usr.Type == "preferred_username").Value
                };
                _logger.LogError("No Address was found for the id {id}. Data: {@logData}", id, logData);

                return NotFound("No data was found");
            }

            AddressDTO addressDTO = _mapper.Map<AddressDTO>(address);

            return Ok(addressDTO);
        }

        /// <summary>
        /// Gets an Address for the provided Id, including its related Contact objects
        /// </summary>
        /// <param name="id">int - The Address Id</param>
        /// <returns>Task<ActionResult<AddressWithContactsDTO>> - A Contact object with its related Address object</returns>
        //GET: api/addresses/details/1
        [HttpGet("details/{id:int}")]
        [ServiceFilter(typeof(IdValidator))]
        public async Task<ActionResult<AddressWithContactsDTO>> GetAddressByIdWithContactsAsync([FromRoute] int id)
        {
            var addressWithDetails = await _genericRepository.GetOneByWithRelatedDataAsync(ad => ad.Id == id,
                                                                                   ad => ad.Contacts);

            if (addressWithDetails is null)
            {
                var logData = new
                {
                    Id = id,
                    Action = ControllerContext.ActionDescriptor.DisplayName,
                    Verb = HttpContext.Request.Method,
                    EndpointPath = HttpContext.Request.Path.Value,
                    User = HttpContext.User.Claims.First(usr => usr.Type == "preferred_username").Value
                };
                _logger.LogError("No Address was found for the id {id}. Data: {@logData}", id, logData);

                return NotFound("No address was found");
            }

            AddressWithContactsDTO addressWithContactsDTO = _mapper.Map<AddressWithContactsDTO>(addressWithDetails);

            return Ok(addressWithContactsDTO);
        }

        /// <summary>
        /// Creates an Address
        /// </summary>
        /// <param name="address">Address - The Address object to create</param>
        /// <returns>Task<ActionResult<AddressDTO>> - The Address created</returns>
        // POST api/addresses
        [HttpPost]
        [ServiceFilter(typeof(AddressValidationFilter))]
        public async Task<ActionResult<AddressDTO>> AddAddressAsync([FromBody] Address address)
        {
            int created = await _genericRepository.AddTAsync(address);

            if (created == 0)
            {
                var logData = new
                {
                    Address = address,
                    Action = ControllerContext.ActionDescriptor.DisplayName,
                    Verb = HttpContext.Request.Method,
                    EndpointPath = HttpContext.Request.Path.Value,
                    User = HttpContext.User.Claims.First(usr => usr.Type == "preferred_username").Value
                };
                _logger.LogError("No Address was created for the address {address}. Data: {@logData}", address, logData);
                
                return NotFound("No Address was created");
            }

            var newAddress = await _genericRepository.GetOneByAsync(ad =>
                                                                        ad.Client_Id == address.Client_Id &&
                                                                        ad.StreetNumber == address.StreetNumber &&
                                                                        ad.City == address.City);

            AddressDTO addressDTO = _mapper.Map<AddressDTO>(newAddress);

            return Created("", addressDTO);
        }

        /// <summary>
        /// Updates an Address for a provided Id
        /// </summary>
        /// <param name="id">int - the Address Id</param>
        /// <param name="address">Address - The Address object to modify</param>
        /// <returns>Task<ActionResult<AddressDTO>> - The Address object updated</returns>
        // PUT api/addresses/5
        // PATCH api/addresses/5
        [HttpPut("{id:int}")]
        [HttpPatch("{id:int}")]
        [ServiceFilter(typeof(AddressValidationFilter))]
        public async Task<ActionResult<AddressDTO>> UpdateAddressAsync([FromRoute] int id, [FromBody] Address address)
        {
            Address addressResult = await _genericRepository.GetOneByAsync(ad => ad.Id == address.Id);

            if (addressResult is null)
            {
                var logData = new
                {
                    Id = id,
                    NewAdress = address, 
                    Action = ControllerContext.ActionDescriptor.DisplayName,
                    Verb = HttpContext.Request.Method,
                    EndpointPath = HttpContext.Request.Path.Value,
                    User = HttpContext.User.Claims.First(usr => usr.Type == "preferred_username").Value
                };
                _logger.LogError("No Address was updated for id {id} and address {address}. Data: {@logData}", id, address, logData);
                
                return NotFound("No Address was updated");
            }

            int updated = await _genericRepository.UpdateTAsync(address);

            AddressDTO addressDTO = _mapper.Map<AddressDTO>(address);

            return Ok(addressDTO);
        }

        /// <summary>
        /// Deletes an Address object for a provided Id
        /// </summary>
        /// <param name="id">int id - the Address id</param>
        /// <returns>Task<ActionResult<int>> - The number of Address objects deleted</returns>
        // DELETE api/addresses/5
        [HttpDelete("{id:int}")]
        [ServiceFilter(typeof(IdValidator))]
        public async Task<ActionResult<int>> DeleteAddressAsync([FromRoute] int id)
        {
            Address addressResult = await _genericRepository.GetOneByAsync(ad => ad.Id == id);

            if (addressResult is null)
            {
                var logData = new
                {
                    Id = id,
                    Action = ControllerContext.ActionDescriptor.DisplayName,
                    Verb = HttpContext.Request.Method,
                    EndpointPath = HttpContext.Request.Path.Value,
                    User = HttpContext.User.Claims.First(usr => usr.Type == "preferred_username").Value
                };
                _logger.LogError("No Address was found for the id {id}. Data: {@logData}", id, logData);

                return NotFound("No Address was found");
            }

            int deletedAddresses = await _genericRepository.DeleteTAsync(addressResult);

            return Ok(deletedAddresses);
        }
    }
}
