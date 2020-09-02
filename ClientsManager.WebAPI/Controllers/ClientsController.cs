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
    public class ClientsController : ControllerBase
    {
        private readonly IGenericRepository<Client> _genericRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ClientsController> _logger;

        public ClientsController(IGenericRepository<Client> genericRepository, IMapper mapper, ILogger<ClientsController> logger)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Gets all Clients for the paging parameters
        /// </summary>
        /// <param name="parameters">Paging parameters</param>
        /// <returns>Task<ActionResult<IEnumerable<ClientDTO>>> - A collection of Client objects</returns>
        // GET: api/clients
        // GET: api/clients?pageNumber=2&pageSize=3
        [HttpGet]
        [ServiceFilter(typeof(QueryStringParamsValidator))]
        public async Task<ActionResult<IEnumerable<ClientDTO>>> GetAllClientsAsync([FromQuery] QueryStringParameters parameters)
        {
            IEnumerable<Client> clients = await _genericRepository.GetAllPagedAsync(cl => cl.Id, parameters);

            if (!clients.Any())
            {
                _logger.LogError("ClientsController.GetAllClientsAync - No Clients where Found");
                return NotFound("No Clients where found");
                
            }

            IEnumerable<ClientDTO> clientsDTO = _mapper.Map<IEnumerable<ClientDTO>>(clients);

            return Ok(clientsDTO);
        }


        /// <summary>
        /// Gets the Client for the provided Id
        /// </summary>
        /// <param name="id">int - the Client Id</param>
        /// <returns>Task<ActionResult<ClientDTO>> - a Client object</returns>
        //GET: api/clients/1
        [HttpGet("{id:int}")]
        [ServiceFilter(typeof(IdValidator))]
        public async Task<ActionResult<ClientDTO>> GetClientByIdAsync([FromRoute] int id)
        {
            Client client = await _genericRepository.GetOneByAsync(cl => cl.Id == id);

            if (client is null)
            {
                _logger.LogError("ClientsController.GetClientByIdAsync - No data was found");
                return NotFound("No data was found");
            }

            ClientDTO clientDTO = _mapper.Map<ClientDTO>(client);

            return Ok(clientDTO);
        }

        /// <summary>
        /// Gets a Client for the provided Id, including its related LegalCase objects
        /// </summary>
        /// <param name="id">int - The Client Id</param>
        /// <returns>Task<ActionResult<ClientWithLegalCasesDTO>> - A Client object with its related LegalCase objects</returns>
        //GET: api/clients/legalcases/1
        [HttpGet("legalcases/{id:int}")]
        [ServiceFilter(typeof(IdValidator))]
        public async Task<ActionResult<ClientWithLegalCasesDTO>> GetClientByIdWithLegalCasesAsync([FromRoute] int id)
        {
            Client clientWithDetails = await _genericRepository.GetOneByWithRelatedDataAsync(cl => cl.Id == id,
                                                                                   cl => cl.LegalCases);

            if (clientWithDetails is null)
            {
                return NotFound("No data was found");
            }

            ClientWithLegalCasesDTO clientWithLegalCasesDTO = _mapper.Map<ClientWithLegalCasesDTO>(clientWithDetails);

            return Ok(clientWithLegalCasesDTO);
        }

        /// <summary>
        /// Gets a Client for the provided Id, including its related Address objects
        /// </summary>
        /// <param name="id">int - The Client Id</param>
        /// <returns>Task<ActionResult<ClientWithAddressesDTO>> - A Client object with its related Address objects</returns>
        //GET: api/clients/addresses/1
        [HttpGet("addresses/{id:int}")]
        [ServiceFilter(typeof(IdValidator))]
        public async Task<ActionResult<ClientWithAddressesDTO>> GetClientByIdWithAddressesAsync([FromRoute] int id)
        {
            Client clientWithDetails = await _genericRepository.GetOneByWithRelatedDataAsync(cl => cl.Id == id,
                                                                                   cl => cl.Addresses);

            if (clientWithDetails is null)
            {
                return NotFound("No data was found");
            }

            ClientWithAddressesDTO clientWithAddressesDTO = _mapper.Map<ClientWithAddressesDTO>(clientWithDetails);

            return Ok(clientWithAddressesDTO);
        }

        /// <summary>
        /// Gets a Client for the provided Id, including its related Contact objects
        /// </summary>
        /// <param name="id">int - The Client Id</param>
        /// <returns>Task<ActionResult<ClientWithContactsDTO>> - A Client object with its related Contact objects</returns>
        //GET: api/clients/contacts/1
        [HttpGet("contacts/{id:int}")]
        [ServiceFilter(typeof(IdValidator))]
        public async Task<ActionResult<ClientWithContactsDTO>> GetClientByIdWithContactsAsync([FromRoute] int id)
        {
            Client clientWithDetails = await _genericRepository.GetOneByWithRelatedDataAsync(cl => cl.Id == id,
                                                                                   cl => cl.Contacts);

            if (clientWithDetails is null)
            {
                return NotFound("No data was found");
            }

            ClientWithContactsDTO clientWithContactsDTO = _mapper.Map<ClientWithContactsDTO>(clientWithDetails);

            return Ok(clientWithContactsDTO);
        }

        /// <summary>
        /// Creates a Client
        /// </summary>
        /// <param name="client">Client - The Client object to create</param>
        /// <returns>Task<ActionResult<ClientDTO>> - The Client created</returns>
        // POST api/clients
        [HttpPost]
        [ServiceFilter(typeof(ClientValidationFilter))]
        public async Task<ActionResult<ClientDTO>> AddClientAsync([FromBody] Client client)
        {
            int created = await _genericRepository.AddTAsync(client);

            if (created == 0)
            {
                return NotFound("No Client was created");
            }

            var newClient = await _genericRepository.GetOneByAsync(cl =>
                                                                        cl.Name == client.Name &&
                                                                        cl.Description == client.Description && 
                                                                        cl.Website == client.Website);

            ClientDTO clientDTO = _mapper.Map<ClientDTO>(newClient);

            return Created("", clientDTO);
        }

        /// <summary>
        /// Updates a Client for a provided Id
        /// </summary>
        /// <param name="id">int - the Client Id</param>
        /// <param name="client">Client - The Client object to modify</param>
        /// <returns>Task<ActionResult<ClientDTO>> - The Client object updated</returns>
        // PUT api/clients/5
        // PATCH api/clients/5
        [HttpPut("{id:int}")]
        [HttpPatch("{id:int}")]
        [ServiceFilter(typeof(ClientValidationFilter))]
        public async Task<ActionResult<ClientDTO>> UpdateClientAsync([FromRoute] int id, [FromBody] Client client)
        {
            Client clientResult = await _genericRepository.GetOneByAsync(cl => cl.Id == client.Id);

            if (clientResult is null)
            {
                return NotFound("No Client was updated");
            }

            int updated = await _genericRepository.UpdateTAsync(client);

            ClientDTO clientDTO = _mapper.Map<ClientDTO>(client);

            return Ok(clientDTO);
        }

        /// <summary>
        /// Deletes a Client object for a provided Id
        /// </summary>
        /// <param name="id">int id - the Client id</param>
        /// <returns>Task<ActionResult<int>> - The number of Client objects deleted</returns>
        // DELETE api/clients/5
        [HttpDelete("{id:int}")]
        [ServiceFilter(typeof(IdValidator))]
        public async Task<ActionResult<int>> DeleteClientAsync([FromRoute] int id)
        {
            Client clientResult = await _genericRepository.GetOneByAsync(cl => cl.Id == id);

            if (clientResult is null)
            {
                return NotFound("No Client was found");
            }

            int deletedClient = await _genericRepository.DeleteTAsync(clientResult);

            return Ok(deletedClient);
        }
    }
}
