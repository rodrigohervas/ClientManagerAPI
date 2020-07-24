using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.ValidationActionFiltersMiddleware
{
    public class ClientIdValidator : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //get the action client_id parameter
            var queryStringId = context.ActionArguments["client_id"] as int?;

            //validate that the client_id is not null/zero
            if (queryStringId == 0 || queryStringId == null)
            {
                context.Result = new BadRequestObjectResult("Client_id is mandatory");
                return;
            }
        }


        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        
    }
}
