using ClientsManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.ValidationActionFiltersMiddleware
{
    public class EmployeeTypeValidationFilter : IActionFilter
    {
        private readonly ILogger _logger;

        public EmployeeTypeValidationFilter(ILogger<EmployeeTypeValidationFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //get the action EmployeeType parameter, the input of the action method
            var employeeType = context.ActionArguments["employeeType"] as EmployeeType;
            
            //validate that EmployeeType is not null
            if (employeeType == null)
            {
                _logger.LogError($"EmployeeTypeValidationFilter: EmployeeType is mandatory. Value Received: {employeeType}");
                context.Result = new BadRequestObjectResult("EmployeeType is mandatory");
                return;
            }

            //Validate Id exists if Request Method is not "POST" 
            var httpPostMethod = context.HttpContext.Request.Method;
            if (employeeType.Id == 0 && httpPostMethod != "POST")
            {
                _logger.LogError($"EmployeeTypeValidationFilter: Id is mandatory. Value Received: {employeeType.Id}");
                context.Result = new BadRequestObjectResult("Id is mandatory");
                return;
            }

            //Validate Description
            if (String.IsNullOrEmpty(employeeType.Description))
            {
                _logger.LogError($"EmployeeTypeValidationFilter: Description is mandatory. Value Received: {employeeType.Description}");
                context.Result = new BadRequestObjectResult("Description is mandatory");
                return;
            }

            //validate the ModelState
            if (!context.ModelState.IsValid)
            {
                _logger.LogError($"EmployeeTypeValidationFilter: ModelState is invalid");
                context.Result = new BadRequestObjectResult(context.ModelState);
                return;
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
