using ClientsManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.ValidationActionFiltersMiddleware
{
    public class QueryStringParamsValidator : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //get the QueryStringParameters object from the context body
            var queryStringParams = context.ActionArguments["parameters"] as QueryStringParameters;
            var pageNumber = queryStringParams.pageNumber;
            var pageSize = queryStringParams.pageSize;

            //validate that pageNumber is not zero
            if (pageNumber == 0)
            {
                context.Result = new BadRequestObjectResult("pageNumber is mandatory");
                return;
            }

            //validate that pageSize is not zero
            if (pageSize == 0)
            {
                context.Result = new BadRequestObjectResult("pageSize is mandatory");
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
