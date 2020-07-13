using ClientsManager.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace ClientsManager.Tests.IntegrationTests
{
    /// <summary>
    /// Method to configure a Test Web Host for testing
    /// </summary>
    /// <typeparam name="TStartup">TStartup is the entry point class of the SUT, usually the Startup class.</typeparam>
    public class TestWebHost<TStartup>: WebApplicationFactory<TStartup> where TStartup: class
    {
        /// <summary>
        /// Configuration method
        /// </summary>
        /// <param name="builder"></param>
        protected override void ConfigureWebHost(IWebHostBuilder builder) 
        {
            //Add configuration for db connection string
            builder.ConfigureAppConfiguration((hostingContext, configurationBuilder) =>
            {
                var path = Directory.GetCurrentDirectory();
                
                configurationBuilder.AddJsonFile($"{path}\\appsettings.json", optional: true, reloadOnChange: true);
                configurationBuilder.AddEnvironmentVariables();
            });


            builder.ConfigureServices(services =>
            {
                //Remove the app's DbContext that was registered in Startup.ConfigureServices(), 
                //to be able to use a different DB for testing
                var descriptor = services.SingleOrDefault(d => 
                        d.ServiceType == typeof(DbContextOptions<ClientsManagerDBContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                //Add testing configured (InMemory) DbContext
                services.AddDbContext<ClientsManagerDBContext>(options =>
                        options.UseInMemoryDatabase("TestDB")
                        //options.UseSqlServer(config.ConnectionString["ConnectionString"])
                );

                //Add Repos?
                services.AddScoped<ITimeFrameRepository, TimeFrameRepository>();

                //Build service provider
                var sp = services.BuildServiceProvider();

                //Get a reference to the DbContext and seed its DB with Test data
                using (var scope = sp.CreateScope()) 
                {
                    //Get the Test DbContext from the service collection
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<ClientsManagerDBContext>();

                    //Get the Logger service from the service collection
                    var logger = scopedServices.GetRequiredService<ILogger<TestWebHost<TStartup>>>();

                    //ensure db is created
                    context.Database.EnsureCreated();

                    try {
                        //Seed db for tests
                        TestDbSeeder.SeedDB(context);
                    }
                    catch (Exception ex) {
                        //log any seeding errors
                        logger.LogError(ex, "Error seeding the Test DB. Error: {Message}", ex.Message);
                    }
                }
            });

        }
    }
}
