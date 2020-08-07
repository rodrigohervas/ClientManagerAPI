using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.ErrorHandlerMiddleware
{
    /// <summary>
    /// Class that extends UseExceptionHandler middleware for custom error handling before returning the Http Response.
    /// After access to the error this class implements two error handling features:
    /// 1. Error logging - PENDING TODO
    /// 2. Custom Http Response error message
    /// </summary>
    public static class ExceptionHandlerExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app">IApplicationBuilder object</param>
        /// <param name="logger">Ilogger object</param>
        //public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            //register the UseExceptionHandler middleware, and provide a lambda expression as a parameter
            //the Error Handling configuration will be done with the lambda expression
            app.UseExceptionHandler(errorApp => 
            {
                errorApp.Run(async context => 
                {
                    //set the StatusCode and Content Type of the Http Response object
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json"; //json for Web API response

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        //log the error
                        //logger.LogError($"An error happened: {contextFeature.Error}");

                        //write the Http Response body, using our ErrorDetails class
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
