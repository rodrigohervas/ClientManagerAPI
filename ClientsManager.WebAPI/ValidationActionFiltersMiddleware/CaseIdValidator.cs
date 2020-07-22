using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.ValidationActionFiltersMiddleware
{
    public class CaseIdValidator : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //get the action employee_id parameter
            var queryStringId = context.ActionArguments["case_id"] as int?;

            //validate that the case_id is not null/zero
            if (queryStringId == 0 || queryStringId == null)
            {
                context.Result = new BadRequestObjectResult("Case_id is mandatory");
                return;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
                
    }
}
