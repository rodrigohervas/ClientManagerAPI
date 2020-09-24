using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.ErrorHandlingMiddleware
{
    /// <summary>
    /// Class that extends UseExceptionHandler middleware for custom error handling before returning the Http Response, 
    /// adding a middleware to the pipeline that will catch exceptions, log them, and re-execute the request in an alternate pipeline.
    /// After access to the error this class implements the following error handling features:
    /// 1. Error logging
    /// 2. Custom Http Response error message (using ErrorDetails class).
    /// </summary>
    public static class ExceptionHandlerExtension
    {
        /// <summary>
        /// Method that catches the exception, logs it, generates a custom Response and sends the response back to the client.
        /// </summary>
        /// <param name="app">IApplicationBuilder object</param>
        /// <param name="logger">ILogger object</param>
        public static void UseExceptionHandlerExtension(this IApplicationBuilder app, ILogger _logger)
        {
            //register the UseExceptionHandler middleware, and provide a lambda expression as a parameter
            //the Error Handling configuration will be done with the lambda expression
            app.UseExceptionHandler(errorApp => 
            {
                errorApp.Run(async context => 
                {
                    //set the StatusCode and Content Type of the Http Response object
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json"; //sets the Web API response content-type header as json

                    //access the exception
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var error = contextFeature.Error;
                        _logger.LogError("ExceptionHandler - There was an exception: {error}", error);

                        //write the Http Response body, using ErrorDetails class
                        await context.Response.WriteAsync(
                            new ErrorDetails()
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = "Internal Server Error",
                                InternalErrorMessage = contextFeature.Error.Message
                            }.ToString()
                            );
                    }
                });
            });
        }
    }
}
