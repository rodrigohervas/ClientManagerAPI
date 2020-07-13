using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ClientsManager.WebAPI.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        /// <summary>
        /// Error method to catch exceptions and send a response with the exception details
        /// </summary>
        /// <returns>IActionResult: a ProblemDetails response with the exception information </returns>
        [Route("/error")]
        public IActionResult Error()
        {
            return Problem();
        }
    }
}
