using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.ValidationActionFiltersMiddleware
{
    public class EmployeeIdValidator : IActionFilter
    {
        private readonly ILogger _logger;

        public EmployeeIdValidator(ILogger<EmployeeIdValidator> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //get the action employee_id parameter
            var queryStringId = context.ActionArguments["employee_id"] as int?;

            //validate that the employee_id is not null/zero
            if (queryStringId == 0 || queryStringId == null)
            {
                _logger.LogInformation($"EmployeeIdValidator: Employee_id is mandatory. Value Received: {queryStringId}");
                context.Result = new BadRequestObjectResult("Employee_id is mandatory");
                return;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
                
    }
}
