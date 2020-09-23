using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.Authentication
{
    public static class AddAuthenticationExtension
    {
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
            });

            return services;
        }
    }
}
