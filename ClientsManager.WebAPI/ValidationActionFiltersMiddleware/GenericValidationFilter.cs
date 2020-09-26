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
    public class GenericValidationFilter : IActionFilter
    {
        private readonly ILogger _logger;

        public GenericValidationFilter(ILogger<GenericValidationFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var param = context.ActionArguments.FirstOrDefault(p => p.Value is object);

            if (param.Value == null)
            {
                _logger.LogInformation($"GenericValidationFilter: {param.Key} is null. Value received: {param.Value}");
                context.Result = new BadRequestObjectResult($"{param.Key} is null");
                return;
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
