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

namespace ClientsManager.WebAPI
{
    public class Startup
    {
        public IConfiguration _configuration { get; }
        private string _apiKey = null;
        
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
               

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //register the custom action Validation Filters Middleware
            services.AddScoped<TimeFrameValidationFilter>();
            services.AddScoped<IdValidator>();
            services.AddScoped<EmployeeIdValidator>();

            services.AddControllers();

            //Get API Key from Secrets Manager
            _apiKey = _configuration["ServiceApiKey"];

            //Add DbContext Middleware
            services.AddDbContext<ClientsManagerDBContext>(options =>
                options.UseSqlServer(_configuration["connectionstring"])
            );

            //Add Repositories DI dependencies 
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ITimeFrameRepository, TimeFrameRepository>();

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else 
            {
                //error middleware to catch and route errors when not in dev env
                app.UseExceptionHandler("/error");
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
