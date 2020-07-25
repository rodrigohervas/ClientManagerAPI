using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ClientsManager.Data;
using ClientsManager.Models;
using ClientsManager.WebAPI.DTOs;
using ClientsManager.WebAPI.ValidationActionFiltersMiddleware;
using Microsoft.AspNetCore.Mvc;

namespace ClientsManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LegalCasesController : ControllerBase
    {
        private readonly IGenericRepository<LegalCase> _genericRepository;
        private readonly IMapper _mapper;

        public LegalCasesController(IGenericRepository<LegalCase> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all LegalCases for the paging parameters
        /// </summary>
        /// <param name="parameters">Paging parameters</param>
        /// <returns>Task<ActionResult<IEnumerable<Case>>> - A collection of LegalCase objects</returns>
        // GET: api/legalcases
        // GET: api/legalcases?pageNumber=2&pageSize=3
        [HttpGet]
        [ServiceFilter(typeof(QueryStringParamsValidator))]
        public async Task<ActionResult<IEnumerable<LegalCase>>> GetAllLegalCasesAsync([FromQuery] QueryStringParameters parameters)
        {
            var legalCases = await _genericRepository.GetAllPagedAsync(lc => lc.Client_Id, parameters);

            if (!legalCases.Any())
            {
                return NotFound("No Cases where found");
            }

            IEnumerable<LegalCaseDTO> legalCasesDTO = _mapper.Map<IEnumerable<LegalCaseDTO>>(legalCases);

            return Ok(legalCasesDTO);
        }

        /// <summary>
        /// Gets all LegalCases for a provided Client Id
        /// </summary>
        /// <param name="client_id">int - The Client_Id of the LegalCase objects</param>
        /// <returns>Task<ActionResult<IEnumerable<LegalCaseDTO>>> - A list of LegalCase objects</returns>
        //GET: api/legalcases/client/1
        [HttpGet("client/{client_id:int}")]
        [ServiceFilter(typeof(ClientIdValidator))]
        public async Task<ActionResult<IEnumerable<LegalCaseDTO>>> GetLegalCasesByClientIdAsync([FromRoute] int client_id)
        {
            var legalCases = await _genericRepository.GetByAsync(lc => lc.Client_Id == client_id);

            if (!legalCases.Any())
            {
                return NotFound("No data was found for the client");
            }

            IEnumerable<LegalCaseDTO> legalCasesDTO = _mapper.Map<IEnumerable<LegalCaseDTO>>(legalCases);

            return Ok(legalCasesDTO);
        }

        /// <summary>
        /// Gets the LegalCAse for the provided Id
        /// </summary>
        /// <param name="id">int - the LegalCase Id</param>
        /// <returns>Task<ActionResult<LegalCaseDTO>> - a LegalCase object</returns>
        //GET: api/legalcases/1
        [HttpGet("{id:int}")]
        [ServiceFilter(typeof(IdValidator))]
        public async Task<ActionResult<LegalCaseDTO>> GetLegalCaseByIdAsync([FromRoute] int id)
        {
            var legalCase = await _genericRepository.GetOneByAsync(lc => lc.Id == id);

            if (legalCase is null)
            {
                return NotFound("No data was found");
            }

            LegalCaseDTO legalCaseDTO = _mapper.Map<LegalCaseDTO>(legalCase);

            return Ok(legalCaseDTO);
        }

        /// <summary>
        /// Gets A LegalCase for the provided Id, including all its related BillableActivity objects
        /// </summary>
        /// <param name="id">int - The LegalCase Id</param>
        /// <returns>Task<ActionResult<LegalCaseWithBillableActivitiesDTO>> - A LegalCase object with its related BillableActivity objects</returns>
        //GET: api/legalcases/1
        [HttpGet("details/{id:int}")]
        [ServiceFilter(typeof(IdValidator))]
        public async Task<ActionResult<LegalCaseWithBillableActivitiesDTO>> GetLegalCaseByIdWithDetailsAsync([FromRoute] int id)
        {
            var legalCaseWithDetails = await _genericRepository.GetOneByWithRelatedDataAsync(lc => lc.Id == id, 
                                                                                   lc => lc.BillableActivities);

            if (legalCaseWithDetails is null)
            {
                return NotFound("No data was found");
            }

            LegalCaseWithBillableActivitiesDTO caseWithDetailsDTO = _mapper.Map<LegalCaseWithBillableActivitiesDTO>(legalCaseWithDetails);

            return Ok(caseWithDetailsDTO);
        }

        /// <summary>
        /// Creates a LegalCase
        /// </summary>
        /// <param name="legalCase">LegalCAse - The LegalCase object to create</param>
        /// <returns>Task<ActionResult<LegalCaseDTO>> - The LegalCase created</returns>
        // POST api/legalcases
        [HttpPost]
        [ServiceFilter(typeof(LegalCaseValidationFilter))]
        public async Task<ActionResult<LegalCaseDTO>> AddLegalCaseAsync([FromBody] LegalCase legalCase)
        {
            int created = await _genericRepository.AddTAsync(legalCase);

            if (created == 0)
            {
                return NotFound("No Legal Case was created");
            }

            var newLegalCase = await _genericRepository.GetOneByAsync(lc => 
                                                                        lc.Client_Id == legalCase.Client_Id && 
                                                                        lc.Title == legalCase.Title);

            LegalCaseDTO legalCaseDTO = _mapper.Map<LegalCaseDTO>(newLegalCase);

            return Created("", legalCaseDTO);
        }

        /// <summary>
        /// Updates a LegalCase for a provided Id
        /// </summary>
        /// <param name="id">int - the LegalCase Id</param>
        /// <param name="legalCase">LegalCase - The LegalCase object to modify</param>
        /// <returns>Task<ActionResult<LegalCaseDTO>> - The LegalCase object updated</returns>
        // PUT api/legalcases/5
        // PATCH api/legalcases/5
        [HttpPut("{id:int}")]
        [HttpPatch("{id:int}")]
        [ServiceFilter(typeof(LegalCaseValidationFilter))]
        public async Task<ActionResult<LegalCaseDTO>> UpdateLegalCaseAsync([FromRoute] int id, [FromBody] LegalCase legalCase)
        {
            var legalCaseResult = await _genericRepository.GetOneByAsync(lc => lc.Id == legalCase.Id);

            if (legalCaseResult is null)
            {
                return NotFound("No Legal Case was updated");
            }
            
            int updated = await _genericRepository.UpdateTAsync(legalCase);

            LegalCaseDTO legalCaseDTO = _mapper.Map<LegalCaseDTO>(legalCase);

            return Ok(legalCaseDTO);
        }

        /// <summary>
        /// Deletes a LegalCase object for a provided Id
        /// </summary>
        /// <param name="id">int id - the LegalCase id</param>
        /// <returns>Task<ActionResult<int>> - The number of LegalCase objects deleted</returns>
        // DELETE api/legalcases/5
        [HttpDelete("{id:int}")]
        [ServiceFilter(typeof(IdValidator))]
        public async Task<ActionResult<int>> DeleteLegalCaseAsync([FromRoute] int id)
        {
            LegalCase legalCase = await _genericRepository.GetOneByAsync(lc => lc.Id == id);

            if (legalCase is null)
            {
                return NotFound("No Legal Case was found");
            }

            int deletedLegalCases = await _genericRepository.DeleteTAsync(legalCase);

            return Ok(deletedLegalCases);
        }
    }
}
