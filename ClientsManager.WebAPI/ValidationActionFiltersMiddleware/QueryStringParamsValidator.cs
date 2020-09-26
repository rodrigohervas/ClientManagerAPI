using ClientsManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.ValidationActionFiltersMiddleware
{
    public class QueryStringParamsValidator : IActionFilter
    {
        private readonly ILogger _logger;

        public QueryStringParamsValidator(ILogger<QueryStringParamsValidator> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //get the QueryStringParameters object from the context body
            var queryStringParams = context.ActionArguments["parameters"] as QueryStringParameters;
            var pageNumber = queryStringParams.pageNumber;
            var pageSize = queryStringParams.pageSize;

            //validate that pageNumber is not zero
            if (pageNumber == 0)
            {
                _logger.LogInformation($"QueryStringParamsValidator: pageNumber is mandatory. Value received: {pageNumber}");
                context.Result = new BadRequestObjectResult("pageNumber is mandatory");
                return;
            }

            //validate that pageSize is not zero
            if (pageSize == 0)
            {
                _logger.LogInformation($"QueryStringParamsValidator: pageSize is mandatory. Value received: {pageSize}");
                context.Result = new BadRequestObjectResult("pageSize is mandatory");
                return;
            }

            //validate the ModelState
            if (!context.ModelState.IsValid)
            {
                _logger.LogInformation($"QueryStringParamsValidator: modelState is not valid");
                context.Result = new BadRequestObjectResult(context.ModelState);
                return;
            }
        }
        
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}
