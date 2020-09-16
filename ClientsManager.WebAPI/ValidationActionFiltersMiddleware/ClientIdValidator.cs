using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.ValidationActionFiltersMiddleware
{
    public class ClientIdValidator : IActionFilter
    {
        private readonly ILogger _logger;

        public ClientIdValidator(ILogger<ClientIdValidator> logger)
        {
            _logger = logger;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //get the action client_id parameter
            var queryStringId = context.ActionArguments["client_id"] as int?;

            //validate that the client_id is not null/zero
            if (queryStringId == 0 || queryStringId == null)
            {
                _logger.LogError($"ClientIdValidator: Client_id is mandatory. Value received: {queryStringId}");
                context.Result = new BadRequestObjectResult("Client_id is mandatory");
                return;
            }
        }


        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        
    }
}
