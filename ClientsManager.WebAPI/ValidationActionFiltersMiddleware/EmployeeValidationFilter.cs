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
    public class EmployeeValidationFilter : IActionFilter
    {
        private readonly ILogger _logger;

        public EmployeeValidationFilter(ILogger<EmployeeValidationFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //get the action Employee parameter, the input of the action method
            Employee employee = context.ActionArguments["employee"] as Employee;
            
            //validate that Employee is not null
            if (employee == null)
            {
                _logger.LogInformation($"EmployeeValidationFilter: Employee is mandatory. Value Received: {employee}");
                context.Result = new BadRequestObjectResult("Employee is mandatory");
                return;
            }

            //Validate Id exists if Request Method is not "POST" 
            var httpPostMethod = context.HttpContext.Request.Method;
            if (employee.Id == 0 && httpPostMethod != "POST")
            {
                _logger.LogInformation($"EmployeeValidationFilter: Id is mandatory. Value Received: {employee.Id}");
                context.Result = new BadRequestObjectResult("Id is mandatory");
                return;
            }

            //Validate Name
            if (String.IsNullOrEmpty(employee.Name))
            {
                _logger.LogInformation($"EmployeeValidationFilter: Name is mandatory. Value Received: {employee.Name}");
                context.Result = new BadRequestObjectResult("Name is mandatory");
                return;
            }

            //validate EmployeeType_id
            if (employee.EmployeeType_Id == 0)
            {
                _logger.LogInformation($"EmployeeValidationFilter: EmployeeType_Id is mandatory. Value Received: {employee.EmployeeType_Id}");
                context.Result = new BadRequestObjectResult("EmployeeType_Id is mandatory");
                return;
            }

            if (employee.EmployeeType_Id.GetTypeCode() != TypeCode.Int32)
            {
                _logger.LogInformation($"EmployeeValidationFilter: EmployeeType_Id must be a valid number. Value Received: {employee.EmployeeType_Id}");
                context.Result = new BadRequestObjectResult("EmployeeType_Id must be a valid number");
                return;
            }

            //Validate Position
            if (String.IsNullOrEmpty(employee.Position))
            {
                _logger.LogInformation($"EmployeeValidationFilter: Position is mandatory. Value Received: {employee.Position}");
                context.Result = new BadRequestObjectResult("Position is mandatory");
                return;
            }

            //validate the ModelState
            if (!context.ModelState.IsValid)
            {
                _logger.LogInformation($"EmployeeValidationFilter: ModelState is invalid");
                context.Result = new BadRequestObjectResult(context.ModelState);
                return;
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
