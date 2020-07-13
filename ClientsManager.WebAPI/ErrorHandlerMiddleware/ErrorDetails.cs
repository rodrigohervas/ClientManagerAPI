using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.ErrorHandlerMiddleware
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }

        public object Message { get; set; }
    }
}
