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
    public class AddressValidationFilter : IActionFilter
    {
        private readonly ILogger _logger;

        public AddressValidationFilter(ILogger<AddressValidationFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //get the Address object from the context body
            Address _address = context.ActionArguments["address"] as Address;

            //validate that Address is not null
            if (_address == null)
            {
                _logger.LogError($"AddressValidationFilter: Address is mandatory. Value Received: {_address}");
                context.Result = new BadRequestObjectResult("Address is mandatory");
                return;
            }

            //Validate Id exists if Request Method is not "POST" 
            var httpPostMethod = context.HttpContext.Request.Method;
            if (_address.Id == 0 && httpPostMethod != "POST")
            {
                _logger.LogError($"AddressValidationFilter: Id is mandatory. Value Received: {_address.Id}");
                context.Result = new BadRequestObjectResult("Id is mandatory");
                return;
            }

            //validate Client_id
            if (_address.Client_Id == 0)
            {
                _logger.LogError($"AddressValidationFilter: Client_Id is mandatory. Value Received: {_address.Client_Id}");
                context.Result = new BadRequestObjectResult("Client_Id is mandatory");
                return;
            }

            if (_address.Client_Id.GetTypeCode() != TypeCode.Int32)
            {
                _logger.LogError($"AddressValidationFilter: Client_Id must be a valid number. Value Received: {_address.Client_Id}");
                context.Result = new BadRequestObjectResult("Client_Id must be a valid number");
                return;
            }
            
            //Validate StreetNumber
            if (String.IsNullOrEmpty(_address.StreetNumber))
            {
                _logger.LogError($"AddressValidationFilter: Street and Number are mandatory. Value Received: {_address.StreetNumber}");
                context.Result = new BadRequestObjectResult("Street and Number are mandatory");
                return;
            }
            
            //Validate City
            if (String.IsNullOrEmpty(_address.City))
            {
                _logger.LogError($"AddressValidationFilter: City is mandatory. Value Received: {_address.City}");
                context.Result = new BadRequestObjectResult("City is mandatory");
                return;
            }

            //Validate StateProvince
            if (String.IsNullOrEmpty(_address.StateProvince))
            {
                _logger.LogError($"AddressValidationFilter: State/Province is mandatory. Value Received: {_address.StateProvince}");
                context.Result = new BadRequestObjectResult("State/Province is mandatory");
                return;
            }

            //Validate ZipCode
            if (String.IsNullOrEmpty(_address.ZipCode))
            {
                _logger.LogError($"AddressValidationFilter: ZipCode is mandatory. Value Received: {_address.ZipCode}");
                context.Result = new BadRequestObjectResult("ZipCode is mandatory");
                return;
            }

            //Validate Country
            if (String.IsNullOrEmpty(_address.Country))
            {
                _logger.LogError($"AddressValidationFilter: Country is mandatory. Value Received: {_address.Country}");
                context.Result = new BadRequestObjectResult("Country is mandatory");
                return;
            }

            //validate the ModelState
            if (!context.ModelState.IsValid)
            {
                _logger.LogError($"AddressValidationFilter: ModelState is invalid");
                context.Result = new BadRequestObjectResult(context.ModelState);
                return;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}
