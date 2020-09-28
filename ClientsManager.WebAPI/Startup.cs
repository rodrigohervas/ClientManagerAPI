using AutoMapper;
using ClientsManager.Data;
using ClientsManager.WebAPI.Authentication;
using ClientsManager.WebAPI.ErrorHandlingMiddleware;
using ClientsManager.WebAPI.SwaggerMiddleware;
using ClientsManager.WebAPI.ValidationActionFiltersMiddleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using Serilog;

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
            services.AddCors(options =>
            {
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

            //Register DbContext Middleware
            services.AddDbContext<ClientsManagerDBContext>(options =>
                options.UseSqlServer(_configuration["ConnectionStrings:ClientsManagerDBConnectionString"])
            );

            //Add Repositories DI dependencies 
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            
            //Configure Default JWT Bearer Token authentication
            //services.AddJWTokenAuthentication(_configuration);

            //Configure Azure AD JWT Bearer Token authentication
            services.AddAzureADAuthentication(_configuration);

            //Register Swagger Documentation Service
            services.AddSwaggerGenExtension(_configuration);
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

            //Route logging through Serilog
            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseAuthorization();

            //Add Swagger middleware
            app.UseSwagger();

            //Add Swagger UI Middleware
            app.UseSwaggerUI( swg => {
                swg.SwaggerEndpoint(_configuration["Swagger:SwaggerUIEndpoint"],
                                    $"{(_configuration["Swagger:ApiTitle"])} {(_configuration["Swagger:ApiVersion"])}");
            });

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
