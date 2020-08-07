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
using ClientsManager.WebAPI.JWTAuthentication;

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
            //add cors handler
            services.AddCors(options => {
                options.AddPolicy("CorsPolicy", builder => builder
                                                                .AllowAnyOrigin()
                                                                .AllowAnyMethod()
                                                                .AllowAnyHeader()
                                                                .AllowCredentials());
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
                options.UseSqlServer(_configuration["connectionstring"])
            );

            //Add Repositories DI dependencies 
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


            //JWT Token authentication
            services.AddJWTokenAuthentication(_configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureExceptionHandler();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //error middleware
                app.UseExceptionHandler("/error");
            }

            //add Exception Handler Middleware
            //app.ConfigureExceptionHandler(logger);
            app.ConfigureExceptionHandler();

            app.UseHttpsRedirection();

            app.UseRouting();

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
