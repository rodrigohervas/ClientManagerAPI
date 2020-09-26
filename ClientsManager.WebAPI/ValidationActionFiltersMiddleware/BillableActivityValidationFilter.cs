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
    public class BillableActivityValidationFilter : IActionFilter
    {
        private readonly ILogger _logger;

        public BillableActivityValidationFilter(ILogger<BillableActivityValidationFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //get the action BillableActivity parameter, the input of the action method
            var billableActivity = context.ActionArguments["billableActivity"] as BillableActivity;
            
            //validate that BillableActivity is not null
            if (billableActivity == null)
            {
                _logger.LogInformation($"BillableActivityValidationFilter: BillableActivity is mandatory. Value Received: {billableActivity}");
                context.Result = new BadRequestObjectResult("BillableActivity is mandatory");
                return;
            }

            //Validate Id exists if Request Method is not "POST" 
            var httpPostMethod = context.HttpContext.Request.Method;
            if (billableActivity.Id == 0 && httpPostMethod != "POST")
            {
                _logger.LogInformation($"BillableActivityValidationFilter: Id is mandatory. Value Received: {billableActivity.Id}");
                context.Result = new BadRequestObjectResult("Id is mandatory");
                return;
            }

            //validate Case_id
            if (billableActivity.LegalCase_Id == 0)
            {
                _logger.LogInformation($"BillableActivityValidationFilter: LegalCase_Id is mandatory. Value Received: {billableActivity.LegalCase_Id}");
                context.Result = new BadRequestObjectResult("LegalCase_Id is mandatory");
                return;
            }

            if (billableActivity.LegalCase_Id.GetTypeCode() != TypeCode.Int32)
            {
                _logger.LogInformation($"BillableActivityValidationFilter: LegalCase_Id must be a valid number. Value Received: {billableActivity.LegalCase_Id}");
                context.Result = new BadRequestObjectResult("LegalCase_Id must be a valid number");
                return;
            }

            //validate Employee_id
            if (billableActivity.Employee_Id == 0)
            {
                _logger.LogInformation($"BillableActivityValidationFilter: Employee_Id is mandatory. Value Received: {billableActivity.Employee_Id}");
                context.Result = new BadRequestObjectResult("Employee_Id is mandatory");
                return;
            }

            if (billableActivity.Employee_Id.GetTypeCode() != TypeCode.Int32)
            {
                _logger.LogInformation($"BillableActivityValidationFilter: Employee_Id must be a valid number. Value Received: {billableActivity.Employee_Id}");
                context.Result = new BadRequestObjectResult("Employee_Id must be a valid number");
                return;
            }

            //Validate Title
            if (String.IsNullOrEmpty(billableActivity.Title))
            {
                _logger.LogInformation($"BillableActivityValidationFilter: Title is mandatory. Value Received: {billableActivity.Title}");
                context.Result = new BadRequestObjectResult("Title is mandatory");
                return;
            }

            //Validate Description
            if (String.IsNullOrEmpty(billableActivity.Description))
            {
                _logger.LogInformation($"BillableActivityValidationFilter: Description is mandatory. Value Received: {billableActivity.Description}");
                context.Result = new BadRequestObjectResult("Description is mandatory");
                return;
            }

            //validate Price
            if (billableActivity.Price == 0.0M)
            {
                _logger.LogInformation($"BillableActivityValidationFilter: Price is mandatory. Value Received: {billableActivity.Price}");
                context.Result = new BadRequestObjectResult("Price is mandatory");
                return;
            }

            //validate Price Range
            if (billableActivity.Price < 0.1M)
            {
                _logger.LogInformation($"BillableActivityValidationFilter: Price must be more than zero. Value Received: {billableActivity.Price}");
                context.Result = new BadRequestObjectResult("Price must be more than zero");
                return;
            }

            //validate Start_DateTime
            if (String.IsNullOrEmpty(billableActivity.Start_DateTime.ToString()))
            {
                _logger.LogInformation($"BillableActivityValidationFilter: Start_DateTime is mandatory. Value Received: {billableActivity.Start_DateTime}");
                context.Result = new BadRequestObjectResult("Start_DateTime is mandatory");
                return;
            }

            if (billableActivity.Start_DateTime.Year < 2000)
            {
                _logger.LogInformation($"BillableActivityValidationFilter: Start_DateTime must be after year 2000. Value Received: {billableActivity.Start_DateTime}");
                context.Result = new BadRequestObjectResult("Start_DateTime must be after year 2000");
                return;
            }

            //validate Finish_DateTime
            if (String.IsNullOrEmpty(billableActivity.Finish_DateTime.ToString()))
            {
                _logger.LogInformation($"BillableActivityValidationFilter: Finish_DateTime is mandatory. Value Received: {billableActivity.Finish_DateTime}");
                context.Result = new BadRequestObjectResult("Finish_DateTime is mandatory");
                return;
            }

            if (billableActivity.Finish_DateTime.Year < 2000)
            {
                _logger.LogInformation($"BillableActivityValidationFilter: Finish_DateTime must be after year 2000. Value Received: {billableActivity.Finish_DateTime}");
                context.Result = new BadRequestObjectResult("Finish_DateTime must be after year 2000");
                return;
            }

            //validate the ModelState
            if (!context.ModelState.IsValid)
            {
                _logger.LogInformation($"BillableActivityValidationFilter: ModelState is invalid");
                context.Result = new BadRequestObjectResult("from Validation Filter: " + context.ModelState);
                return;
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
