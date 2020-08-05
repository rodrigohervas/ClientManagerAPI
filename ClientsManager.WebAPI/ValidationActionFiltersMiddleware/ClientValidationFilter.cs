using ClientsManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.ValidationActionFiltersMiddleware
{
    public class ClientValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //get the Client object from the context body
            Client _client = context.ActionArguments["client"] as Client;

            //validate that Client is not null
            if (_client == null)
            {
                context.Result = new BadRequestObjectResult("Client is mandatory");
                return;
            }

            //Validate Id exists if Request Method is not "POST" 
            var httpPostMethod = context.HttpContext.Request.Method;
            if (_client.Id == 0 && httpPostMethod != "POST")
            {
                context.Result = new BadRequestObjectResult("Id is mandatory");
                return;
            }

            //Validate Name
            if (String.IsNullOrEmpty(_client.Name))
            {
                context.Result = new BadRequestObjectResult("Name is mandatory");
                return;
            }

            //Validate Description
            if (String.IsNullOrEmpty(_client.Description))
            {
                context.Result = new BadRequestObjectResult("Description is mandatory");
                return;
            }

            //Validate Website
            if (String.IsNullOrEmpty(_client.Website))
            {
                context.Result = new BadRequestObjectResult("Website is mandatory");
                return;
            }

            //validate the ModelState
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
                return;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}
