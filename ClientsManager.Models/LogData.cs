using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientsManager.Models
{
    public class LogData
    {
        /*
         * NewBillableActivity = billableActivity,
                    Action = ControllerContext.ActionDescriptor.DisplayName,
                    Verb = HttpContext.Request.Method,
                    EndpointPath = HttpContext.Request.Path.Value,
                    User = HttpContext.User.Claims.First(usr => usr.Type == "preferred_username").Value
         */
        public object Entity { get; set; }
        public string Action { get; set; }
        public string Verb { get; set; }
        public string EndpointPath { get; set; }
        public string User { get; set; }

        public LogData()
        {

        }

        public LogData(ControllerContext controllerContext, HttpContext httpContext)
        {
            Entity = new { test = "value" };
            Action = controllerContext.ActionDescriptor.DisplayName;
            Verb = httpContext.Request.Method;
            EndpointPath = httpContext.Request.Path.Value;
            User = httpContext.User.Claims.First(usr => usr.Type == "preferred_username").Value;
        }
    }
}
