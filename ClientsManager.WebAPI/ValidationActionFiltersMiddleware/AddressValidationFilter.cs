using ClientsManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.ValidationActionFiltersMiddleware
{
    public class AddressValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //get the Address object from the context body
            Address _address = context.ActionArguments["address"] as Address;

            //validate that Address is not null
            if (_address == null)
            {
                context.Result = new BadRequestObjectResult("Address is mandatory");
                return;
            }

            //Validate Id exists if Request Method is not "POST" 
            var httpPostMethod = context.HttpContext.Request.Method;
            if (_address.Id == 0 && httpPostMethod != "POST")
            {
                context.Result = new BadRequestObjectResult("Id is mandatory");
                return;
            }

            //validate Client_id
            if (_address.Client_Id == 0)
            {
                context.Result = new BadRequestObjectResult("Client_Id is mandatory");
                return;
            }

            if (_address.Client_Id.GetTypeCode() != TypeCode.Int32)
            {
                context.Result = new BadRequestObjectResult("Client_Id must be a valid number");
                return;
            }
            
            //Validate StreetNumber
            if (String.IsNullOrEmpty(_address.StreetNumber))
            {
                context.Result = new BadRequestObjectResult("Street and Number are mandatory");
                return;
            }

            if (String.IsNullOrEmpty(_address.City))
            {
                context.Result = new BadRequestObjectResult("City is mandatory");
                return;
            }
            
            //Validate City
            if (String.IsNullOrEmpty(_address.City))
            {
                context.Result = new BadRequestObjectResult("City is mandatory");
                return;
            }

            //Validate StateProvince
            if (String.IsNullOrEmpty(_address.StateProvince))
            {
                context.Result = new BadRequestObjectResult("State/Province is mandatory");
                return;
            }

            //Validate ZipCode
            if (String.IsNullOrEmpty(_address.ZipCode))
            {
                context.Result = new BadRequestObjectResult("ZipCode is mandatory");
                return;
            }

            //Validate Country
            if (String.IsNullOrEmpty(_address.Country))
            {
                context.Result = new BadRequestObjectResult("Country is mandatory");
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
