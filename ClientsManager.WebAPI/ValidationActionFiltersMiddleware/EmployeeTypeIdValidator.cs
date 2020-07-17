using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.ValidationActionFiltersMiddleware
{
    public class EmployeeTypeIdValidator : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //get the action employeeType_id parameter
            var queryStringId = context.ActionArguments["employeeType_id"] as int?;

            //validate that the employeeType_id is not null/zero
            if (queryStringId == 0 || queryStringId == null)
            {
                context.Result = new BadRequestObjectResult("EmployeeType_id is mandatory");
                return;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
                
    }
}
