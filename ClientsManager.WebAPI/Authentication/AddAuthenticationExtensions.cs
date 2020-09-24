using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.Authentication
{
    public static class AddAuthenticationExtensions
    {
        /// <summary>
        /// Configures JWT Bearer authentication for Azure Active Directory authentication scheme
        /// </summary>
        /// <param name="services">IServiceCollection services collection object</param>
        /// <param name="configuration">IConfiguration object</param>
        /// <returns> IServiceCollection services collection object</returns>
        public static IServiceCollection AddAzureADAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            //AD Authentication with Jwt Bearer token
            services.AddAuthentication(AzureADDefaults.JwtBearerAuthenticationScheme)
                    .AddAzureADBearer(options => configuration.Bind("AzureAd", options));

            var APIApplicationIdUri = configuration["AzureAd:APIApplicationIdUri"];
            var ClientAudience = configuration["AzureAd:ClientAudience"];
            var APIAudience = configuration["AzureAd:APIAudience"];
            
            services.Configure<JwtBearerOptions>(AzureADDefaults.JwtBearerAuthenticationScheme, options =>
            {
                options.Authority += "/v2.0";

                options.Audience = APIApplicationIdUri;

                options.TokenValidationParameters.ValidateIssuer = true;
                options.TokenValidationParameters.ValidIssuer = APIApplicationIdUri;

                options.TokenValidationParameters.ValidateAudience = true;
                options.TokenValidationParameters.ValidAudiences = new List<string>()
                {
                    ClientAudience,
                    APIAudience
                };

                options.TokenValidationParameters.ValidateLifetime = true;

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context => {
                        var logData = new
                        {
                            Verb = context.Request.Method,
                            EndpointPath = context.Request.Path.Value,
                            exception = context.Exception
                        };
                        Log.Error($"Authentication Failed: {context.Exception.Message}. Data: {@logData}", context.Exception.Message, logData);
                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }

        /// <summary>
        /// Configures JWT Bearer authentication for Default JWT Bearer authentication scheme
        /// </summary>
        /// <param name="services">IServiceCollection services collection object</param>
        /// <param name="configuration">IConfiguration object</param>
        /// <returns> IServiceCollection services collection object</returns>
        public static IServiceCollection AddJWTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var secretKey = configuration["Jwt:SecretKey"];
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Issuer"];

            //Default JWT Bearer Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = issuer,
                            
                            ValidateAudience = true,
                            ValidAudience = audience,

                            ValidateLifetime = true,
                            
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)), 
                                                        
                            RequireExpirationTime = true
                        };

                        options.Events = new JwtBearerEvents
                        {
                            OnAuthenticationFailed = context => {
                                var logData = new
                                {
                                    Verb = context.Request.Method,
                                    EndpointPath = context.Request.Path.Value,
                                    exception = context.Exception
                                };
                                Log.Error($"Authentication Failed: {context.Exception.Message}. Data: {@logData}", context.Exception.Message, logData);
                                return Task.CompletedTask;
                            }
                        };
                    });

            return services;
        }
    }
}
