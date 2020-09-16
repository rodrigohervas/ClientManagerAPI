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
    public class ClientValidationFilter : IActionFilter
    {
        private readonly ILogger _logger;

        public ClientValidationFilter(ILogger<ClientValidationFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //get the Client object from the context body
            Client _client = context.ActionArguments["client"] as Client;

            //validate that Client is not null
            if (_client == null)
            {
                _logger.LogError($"ClientValidationFilter: Client is mandatory. Value Received: {_client}");
                context.Result = new BadRequestObjectResult("Client is mandatory");
                return;
            }

            //Validate Id exists if Request Method is not "POST" 
            var httpPostMethod = context.HttpContext.Request.Method;
            if (_client.Id == 0 && httpPostMethod != "POST")
            {
                _logger.LogError($"ClientValidationFilter: Id is mandatory. Value Received: {_client.Id}");
                context.Result = new BadRequestObjectResult("Id is mandatory");
                return;
            }

            //Validate Name
            if (String.IsNullOrEmpty(_client.Name))
            {
                _logger.LogError($"ClientValidationFilter: Name is mandatory. Value Received: {_client.Name}");
                context.Result = new BadRequestObjectResult("Name is mandatory");
                return;
            }

            //Validate Description
            if (String.IsNullOrEmpty(_client.Description))
            {
                _logger.LogError($"ClientValidationFilter: Description is mandatory. Value Received: {_client.Description}");
                context.Result = new BadRequestObjectResult("Description is mandatory");
                return;
            }

            //Validate Website
            if (String.IsNullOrEmpty(_client.Website))
            {
                _logger.LogError($"ClientValidationFilter: Website is mandatory. Value Received: {_client.Website}");
                context.Result = new BadRequestObjectResult("Website is mandatory");
                return;
            }

            //validate the ModelState
            if (!context.ModelState.IsValid)
            {
                _logger.LogError($"ClientValidationFilter: ModelState is invalid");
                context.Result = new BadRequestObjectResult(context.ModelState);
                return;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}
