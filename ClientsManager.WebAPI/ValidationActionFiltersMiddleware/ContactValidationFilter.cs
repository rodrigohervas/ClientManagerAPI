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
    public class ContactValidationFilter : IActionFilter
    {
        private readonly ILogger _logger;

        public ContactValidationFilter(ILogger<ContactValidationFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //get the Contact object from the context body
            Contact _contact = context.ActionArguments["contact"] as Contact;

            //validate that Contact is not null
            if (_contact == null)
            {
                _logger.LogError($"ContactValidationFilter: Contact is mandatory. Value Received: {_contact}");
                context.Result = new BadRequestObjectResult("Contact is mandatory");
                return;
            }

            //Validate Id exists if Request Method is not "POST" 
            var httpPostMethod = context.HttpContext.Request.Method;
            if (_contact.Id == 0 && httpPostMethod != "POST")
            {
                _logger.LogError($"ContactValidationFilter: Id is mandatory. Value Received: {_contact.Id}");
                context.Result = new BadRequestObjectResult("Id is mandatory");
                return;
            }

            //validate Client_id
            if (_contact.Client_Id == 0)
            {
                _logger.LogError($"ContactValidationFilter: Client_Id is mandatory. Value Received: {_contact.Client_Id}");
                context.Result = new BadRequestObjectResult("Client_Id is mandatory");
                return;
            }

            if (_contact.Client_Id.GetTypeCode() != TypeCode.Int32)
            {
                _logger.LogError($"ContactValidationFilter: Client_Id must be a valid number. Value Received: {_contact.Client_Id}");
                context.Result = new BadRequestObjectResult("Client_Id must be a valid number");
                return;
            }

            //validate Address_id
            if (_contact.Address_Id == 0)
            {
                _logger.LogError($"ContactValidationFilter: Address_Id is mandatory. Value Received: {_contact.Address_Id}");
                context.Result = new BadRequestObjectResult("Address_Id is mandatory");
                return;
            }

            if (_contact.Address_Id.GetTypeCode() != TypeCode.Int32)
            {
                _logger.LogError($"ContactValidationFilter: Address_Id must be a valid number. Value Received: {_contact.Address_Id}");
                context.Result = new BadRequestObjectResult("Address_Id must be a valid number");
                return;
            }

            //Validate Name
            if (String.IsNullOrEmpty(_contact.Name))
            {
                _logger.LogError($"ContactValidationFilter: Name is mandatory. Value Received: {_contact.Name}");
                context.Result = new BadRequestObjectResult("Name is mandatory");
                return;
            }

            //Validate Position
            if (String.IsNullOrEmpty(_contact.Position))
            {
                _logger.LogError($"ContactValidationFilter: Position is mandatory. Value Received: {_contact.Position}");
                context.Result = new BadRequestObjectResult("Position is mandatory");
                return;
            }

            //Validate Telephone
            if (String.IsNullOrEmpty(_contact.Telephone))
            {
                _logger.LogError($"ContactValidationFilter: Telephone is mandatory. Value Received: {_contact.Telephone}");
                context.Result = new BadRequestObjectResult("Telephone is mandatory");
                return;
            }

            //Validate Cellphone
            if (String.IsNullOrEmpty(_contact.Cellphone))
            {
                _logger.LogError($"ContactValidationFilter: Cellphone is mandatory. Value Received: {_contact.Cellphone}");
                context.Result = new BadRequestObjectResult("Cellphone is mandatory");
                return;
            }
            //Validate Email
            if (String.IsNullOrEmpty(_contact.Email))
            {
                _logger.LogError($"ContactValidationFilter: Email is mandatory. Value Received: {_contact.Email}");
                context.Result = new BadRequestObjectResult("Email is mandatory");
                return;
            }

            //validate the ModelState
            if (!context.ModelState.IsValid)
            {
                _logger.LogError($"ContactValidationFilter: ModelState is Invalid");
                context.Result = new BadRequestObjectResult(context.ModelState);
                return;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}
