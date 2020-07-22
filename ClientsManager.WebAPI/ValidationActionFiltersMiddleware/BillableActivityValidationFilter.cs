using ClientsManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.ValidationActionFiltersMiddleware
{
    public class BillableActivityValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //get the action BillableActivity parameter, the input of the action method
            var billableActivity = context.ActionArguments["billableActivity"] as BillableActivity;
            
            //validate that BillableActivity is not null
            if (billableActivity == null)
            {
                context.Result = new BadRequestObjectResult("BillableActivity is mandatory");
                return;
            }

            //Validate Id exists if Request Method is not "POST" 
            var httpPostMethod = context.HttpContext.Request.Method;
            if (billableActivity.Id == 0 && httpPostMethod != "POST")
            {
                context.Result = new BadRequestObjectResult("Id is mandatory");
                return;
            }

            //validate Case_id
            if (billableActivity.Case_Id == 0)
            {
                context.Result = new BadRequestObjectResult("Case_Id is mandatory");
                return;
            }

            if (billableActivity.Case_Id.GetTypeCode() != TypeCode.Int32)
            {
                context.Result = new BadRequestObjectResult("Case_Id must be a valid number");
                return;
            }

            //validate Employee_id
            if (billableActivity.Employee_Id == 0)
            {
                context.Result = new BadRequestObjectResult("Employee_Id is mandatory");
                return;
            }

            if (billableActivity.Employee_Id.GetTypeCode() != TypeCode.Int32)
            {
                context.Result = new BadRequestObjectResult("Employee_Id must be a valid number");
                return;
            }

            //Validate Title
            if (String.IsNullOrEmpty(billableActivity.Title))
            {
                context.Result = new BadRequestObjectResult("Title is mandatory");
                return;
            }

            //Validate Description
            if (String.IsNullOrEmpty(billableActivity.Description))
            {
                context.Result = new BadRequestObjectResult("Description is mandatory");
                return;
            }

            //validate Price
            if (billableActivity.Price == 0.0M)
            {
                context.Result = new BadRequestObjectResult("Price is mandatory");
                return;
            }

            //validate Price Range
            if (billableActivity.Price < 0.1M)
            {
                context.Result = new BadRequestObjectResult("Price must be more than zero");
                return;
            }

            //validate Start_DateTime
            if (String.IsNullOrEmpty(billableActivity.Start_DateTime.ToString()))
            {
                context.Result = new BadRequestObjectResult("Start_DateTime is mandatory");
                return;
            }

            if (billableActivity.Start_DateTime.Year < 2000)
            {
                context.Result = new BadRequestObjectResult("Start_DateTime must be after year 2000");
                return;
            }

            //validate Finish_DateTime
            if (String.IsNullOrEmpty(billableActivity.Finish_DateTime.ToString()))
            {
                context.Result = new BadRequestObjectResult("Finish_DateTime is mandatory");
                return;
            }

            if (billableActivity.Finish_DateTime.Year < 2000)
            {
                context.Result = new BadRequestObjectResult("Finish_DateTime must be after year 2000");
                return;
            }

            //validate the ModelState
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult("from Validation Filter: " + context.ModelState);
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
