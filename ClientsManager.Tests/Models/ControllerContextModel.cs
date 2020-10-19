using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace ClientsManager.Tests.Models
{
    public class ControllerContextModel: ControllerContext
    {
        public ControllerContextModel()
        {
            HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(
                        new ClaimsIdentity(new ClaimsIdentity(
                            new Claim[] { new Claim("preferred_username", "Test user") })
                            )
                        )
            };
            ActionDescriptor = new ControllerActionDescriptor() { DisplayName = "test action" };

            HttpContext.Request.Method = "POST";
            HttpContext.Request.Path = new PathString("/testPath");
        }
    }
}
