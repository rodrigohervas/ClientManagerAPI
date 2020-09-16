using ClientsManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.ValidationActionFiltersMiddleware
{
    public class LegalCaseValidationFilter : IActionFilter
    {
        private readonly ILogger _logger;

        public LegalCaseValidationFilter(ILogger<LegalCaseValidationFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //get the LegalCase object from the context body
            LegalCase _legalCase = context.ActionArguments["legalcase"] as LegalCase;

            //validate that LegalCase is not null
            if (_legalCase == null)
            {
                _logger.LogError($"LegalCaseValidationFilter: LegalCase is mandatory. Value Received: {_legalCase}");
                context.Result = new BadRequestObjectResult("LegalCase is mandatory");
                return;
            }

            //Validate Id exists if Request Method is not "POST" 
            var httpPostMethod = context.HttpContext.Request.Method;
            if (_legalCase.Id == 0 && httpPostMethod != "POST")
            {
                _logger.LogError($"LegalCaseValidationFilter: Id is mandatory. Value Received: {_legalCase.Id}");
                context.Result = new BadRequestObjectResult("Id is mandatory");
                return;
            }

            //Validate Title
            if (String.IsNullOrEmpty(_legalCase.Title))
            {
                _logger.LogError($"LegalCaseValidationFilter: Title is mandatory. Value Received: {_legalCase.Title}");
                context.Result = new BadRequestObjectResult("Title is mandatory");
                return;
            }

            //Validate Description
            if (String.IsNullOrEmpty(_legalCase.Description))
            {
                _logger.LogError($"LegalCaseValidationFilter: Description is mandatory. Value Received: {_legalCase.Description}");
                context.Result = new BadRequestObjectResult("Description is mandatory");
                return;
            }

            //validate Client_id
            if (_legalCase.Client_Id == 0)
            {
                _logger.LogError($"LegalCaseValidationFilter: Client_id is mandatory. Value Received: {_legalCase.Client_Id}");
                context.Result = new BadRequestObjectResult("Client_Id is mandatory");
                return;
            }

            if (_legalCase.Client_Id.GetTypeCode() != TypeCode.Int32)
            {
                _logger.LogError($"LegalCaseValidationFilter: Client_Id must be a valid number. Value Received: {_legalCase.Client_Id}");
                context.Result = new BadRequestObjectResult("Client_Id must be a valid number");
                return;
            }

            //Validate TrustFund
            if (_legalCase.TrustFund == 0.0M)
            {
                _logger.LogError($"LegalCaseValidationFilter: TrustFund is mandatory. Value Received: {_legalCase.TrustFund}");
                context.Result = new BadRequestObjectResult("TrustFund is mandatory");
                return;
            }

            //validate TrustFund Range
            if (_legalCase.TrustFund < 0.1M)
            {
                _logger.LogError($"LegalCaseValidationFilter: TrustFund must be more than zero. Value Received: {_legalCase.TrustFund}");
                context.Result = new BadRequestObjectResult("TrustFund must be more than zero");
                return;
            }

            //validate the ModelState
            if (!context.ModelState.IsValid)
            {
                _logger.LogError($"LegalCaseValidationFilter: Modelstate is invalid.");
                context.Result = new BadRequestObjectResult(context.ModelState);
                return;
            }
        }
        
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}
