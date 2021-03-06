﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.ErrorHandlingMiddleware
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }

        public object Message { get; set; }

        public object InternalErrorMessage { get; set; }

        /// <summary>
        /// Serialize to JSON
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
