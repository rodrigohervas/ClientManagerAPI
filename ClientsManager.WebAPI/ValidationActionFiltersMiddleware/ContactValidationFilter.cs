using ClientsManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.ValidationActionFiltersMiddleware
{
    public class ContactValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //get the LegalCase object from the context body
            Contact _contact = context.ActionArguments["contact"] as Contact;

            //validate that Contact is not null
            if (_contact == null)
            {
                context.Result = new BadRequestObjectResult("Contact is mandatory");
                return;
            }

            //Validate Id exists if Request Method is not "POST" 
            var httpPostMethod = context.HttpContext.Request.Method;
            if (_contact.Id == 0 && httpPostMethod != "POST")
            {
                context.Result = new BadRequestObjectResult("Id is mandatory");
                return;
            }

            //validate Client_id
            if (_contact.Client_Id == 0)
            {
                context.Result = new BadRequestObjectResult("Client_Id is mandatory");
                return;
            }

            if (_contact.Client_Id.GetTypeCode() != TypeCode.Int32)
            {
                context.Result = new BadRequestObjectResult("Client_Id must be a valid number");
                return;
            }

            //validate Address_id
            if (_contact.Address_Id == 0)
            {
                context.Result = new BadRequestObjectResult("Address_Id is mandatory");
                return;
            }

            if (_contact.Address_Id.GetTypeCode() != TypeCode.Int32)
            {
                context.Result = new BadRequestObjectResult("Address_Id must be a valid number");
                return;
            }

            //Validate Name
            if (String.IsNullOrEmpty(_contact.Name))
            {
                context.Result = new BadRequestObjectResult("Name is mandatory");
                return;
            }

            //Validate Position
            if (String.IsNullOrEmpty(_contact.Position))
            {
                context.Result = new BadRequestObjectResult("Position is mandatory");
                return;
            }

            //Validate Telephone
            if (String.IsNullOrEmpty(_contact.Telephone))
            {
                context.Result = new BadRequestObjectResult("Telephone is mandatory");
                return;
            }

            //Validate Cellphone
            if (String.IsNullOrEmpty(_contact.Cellphone))
            {
                context.Result = new BadRequestObjectResult("Cellphone is mandatory");
                return;
            }
            //Validate Email
            if (String.IsNullOrEmpty(_contact.Email))
            {
                context.Result = new BadRequestObjectResult("Email is mandatory");
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
