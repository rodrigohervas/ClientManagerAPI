using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.SwaggerMiddleware
{
    public static class SwaggerGenHandler
    {
        /// <summary>
        /// Registers the Swagger Documentation Service to allow for documentation customization
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="_configuration">IConfiguration</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddSwaggerGenExtension(this IServiceCollection services, IConfiguration _configuration)
        {
            //Register Swagger Documentation Service
            services.AddSwaggerGen(swg =>
            {
                swg.SwaggerDoc(_configuration["Swagger:ApiVersion"],
                                new OpenApiInfo
                                {
                                    Title = _configuration["Swagger:ApiTitle"],
                                    Version = _configuration["Swagger:ApiVersion"], 
                                    Description = _configuration["Swagger:Description"],
                                    TermsOfService = new Uri(_configuration["Swagger:TermsOfService"]),
                                    Contact = new OpenApiContact
                                    {
                                        Name = _configuration["Swagger:Contact:Name"],
                                        Email = _configuration["Swagger:Contact:Email"], 
                                        Url = new Uri(_configuration["Swagger:Contact:Url"]),
                                    }, 
                                    License = new OpenApiLicense
                                    {
                                        Name = _configuration["Swagger:License:Name"],
                                        Url = new Uri(_configuration["Swagger:License:Url"])
                                    }
                                });

                //Set the comments path for the Swagger JSON and UI, to use methods XML Comments
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                swg.IncludeXmlComments(xmlPath);
            
            });

            return services;
        }
    }
}
