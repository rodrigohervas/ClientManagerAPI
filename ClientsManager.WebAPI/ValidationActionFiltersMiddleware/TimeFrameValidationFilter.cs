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
    public class TimeFrameValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //get the action TimeFrame parameter, the input of the action method
            var timeFrame = context.ActionArguments["timeFrame"] as TimeFrame;
            
            //validate that TimeFrame is not null
            if (timeFrame == null)
            {
                context.Result = new BadRequestObjectResult("TimeFrame is mandatory");
                return;
            }

            //Validate Id exists if Request Method is "POST" 
            var httpPostMethod = context.HttpContext.Request.Method;
            if (timeFrame.Id == 0 && httpPostMethod != "POST")
            {
                context.Result = new BadRequestObjectResult("Id is mandatory");
                return;
            }

            //validate Employee_id
            if (timeFrame.Employee_Id == 0)
            {
                context.Result = new BadRequestObjectResult("Employee_Id is mandatory");
                return;
            }

            if (timeFrame.Employee_Id.GetTypeCode() != TypeCode.Int32)
            {
                context.Result = new BadRequestObjectResult("Employee_Id must be a valid number");
                return;
            }

            //Validate Title
            if (String.IsNullOrEmpty(timeFrame.Title))
            {
                context.Result = new BadRequestObjectResult("Title is mandatory");
                return;
            }

            //Validate Description
            if (String.IsNullOrEmpty(timeFrame.Description))
            {
                context.Result = new BadRequestObjectResult("Description is mandatory");
                return;
            }

            //validate Price
            if (timeFrame.Price == 0.0M)
            {
                context.Result = new BadRequestObjectResult("Price is mandatory");
                return;
            }

            //validate Price Range
            if (timeFrame.Price < 0.1M)
            {
                context.Result = new BadRequestObjectResult("Price must be more than zero");
                return;
            }

            //validate Start_DateTime
            if (String.IsNullOrEmpty(timeFrame.Start_DateTime.ToString()))
            {
                context.Result = new BadRequestObjectResult("Start_DateTime is mandatory");
                return;
            }

            if (timeFrame.Start_DateTime.Year < 2000)
            {
                context.Result = new BadRequestObjectResult("Start_DateTime must not be before year 2000");
                return;
            }

            //validate Finish_DateTime
            if (String.IsNullOrEmpty(timeFrame.Finish_DateTime.ToString()))
            {
                context.Result = new BadRequestObjectResult("Finish_DateTime is mandatory");
                return;
            }

            if (timeFrame.Finish_DateTime.Year < 2000)
            {
                context.Result = new BadRequestObjectResult("Finish_DateTime must not be before year 2000  - from Validation Filter");
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
