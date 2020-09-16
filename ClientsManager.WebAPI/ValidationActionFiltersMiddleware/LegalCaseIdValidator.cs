using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.ValidationActionFiltersMiddleware
{
    public class LegalCaseIdValidator : IActionFilter
    {
        private readonly ILogger _logger;

        public LegalCaseIdValidator(ILogger<LegalCaseIdValidator> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //get the action case_id parameter
            var queryStringId = context.ActionArguments["legalCase_id"] as int?;

            //validate that the case_id is not null/zero
            if (queryStringId == 0 || queryStringId == null)
            {
                _logger.LogError($"LegalCaseIdValidator: Case_id is mandatory. Value received: {queryStringId}");
                context.Result = new BadRequestObjectResult("Case_id is mandatory");
                return;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
                
    }
}
