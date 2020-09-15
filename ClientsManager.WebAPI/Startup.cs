using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ClientsManager.Data;
using ClientsManager.WebAPI.ValidationActionFiltersMiddleware;
using ClientsManager.WebAPI.ErrorHandlerMiddleware;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ClientsManager.WebAPI.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.IdentityModel.Logging;

namespace ClientsManager.WebAPI
{
    public class Startup
    {
        public IConfiguration _configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
               

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Make details available only for devlopment
            IdentityModelEventSource.ShowPII = true;

            //add cors handler
            services.AddCors(options => {
            options.AddPolicy("CorsPolicy", builder => builder
                                                            .AllowAnyOrigin()
                                                            .AllowAnyMethod()
                                                            .AllowAnyHeader());
                                                                //.AllowCredentials());
            });

            //register the custom action Validation Filters Middleware
            services.AddScoped<GenericValidationFilter>();
            services.AddScoped<IdValidator>();
            services.AddScoped<LegalCaseIdValidator>();
            services.AddScoped<EmployeeIdValidator>();
            services.AddScoped<EmployeeTypeIdValidator>();
            services.AddScoped<EmployeeValidationFilter>();
            services.AddScoped<BillableActivityValidationFilter>();
            services.AddScoped<EmployeeTypeValidationFilter>();
            services.AddScoped<ClientIdValidator>();
            services.AddScoped<LegalCaseIdValidator>();
            services.AddScoped<LegalCaseValidationFilter>();
            services.AddScoped<ContactValidationFilter>();
            services.AddScoped<AddressValidationFilter>();
            services.AddScoped<ClientValidationFilter>();
            //register QueryStringParamsValidator for Paging
            services.AddScoped<QueryStringParamsValidator>();

            services.AddControllers();

            //register AutoMapper for POCO to DTO mapping
            services.AddAutoMapper(typeof(Startup));

            //Add DbContext Middleware
            services.AddDbContext<ClientsManagerDBContext>(options =>
                options.UseSqlServer(_configuration["ConnectionStrings:ClientsManagerDBConnectionString"])
            );

            //Add Repositories DI dependencies 
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


            //JWT Token authentication configuration
            //services.AddJWTokenAuthentication(_configuration);

            //Azure AD authentication configuration
            services.AddAzureADAuthentication(_configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> _logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                _logger.LogInformation("Development Environment");
            }
            else 
            {
                //hook Exception Handler middleware extension to the pipeline
                app.UseExceptionHandlerExtension(_logger);
            }
                        

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Welcome to ClientsManager API");
                });

                endpoints.MapGet("/api", async context =>
                {
                    await context.Response.WriteAsync("Welcome to ClientsManager API");
                });

                endpoints.MapControllers();
            });
        }
    }
}
