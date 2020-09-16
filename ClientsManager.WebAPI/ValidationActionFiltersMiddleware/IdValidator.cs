using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.ValidationActionFiltersMiddleware
{
    public class IdValidator : IActionFilter
    {
        private readonly ILogger _logger;

        public IdValidator(ILogger<IdValidator> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //get the action Id parameter
            var queryStringId = context.ActionArguments["id"] as int?;

            //validate that the Id is not null/zero
            if (queryStringId == 0 || queryStringId == null)
            {
                _logger.LogError($"IdValidator: Id is mandatory. Value received: {queryStringId}");
                context.Result = new BadRequestObjectResult("Id is mandatory");
                return;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
                
    }
}
