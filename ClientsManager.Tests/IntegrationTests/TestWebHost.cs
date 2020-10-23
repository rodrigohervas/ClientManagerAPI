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
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
                //set the test Authentication Scheme using the TestAuthorizationHandler class
                services.AddAuthentication("TestScheme")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthorizationHandler>("TestScheme", options => { });

                //Remove the app's DbContext that was registered in Startup.ConfigureServices(), 
                //to be able to use a different DB for testing
                services.RemoveAll(typeof(ClientsManagerDbContext));

                //Add testing configured (InMemory) DbContext
                services.AddDbContext<CMTestsDbContext>(options =>
                                                options.UseInMemoryDatabase("InMemoryTestDB")
                                                       .EnableSensitiveDataLogging() );

                //ADD A TEST DB IN SQL SERVER
                //services.AddDbContext<TestDBContext>((options, context) =>
                //{
                //    context.UseSqlServer(
                //        Configuration.GetConnectionString("TestingDbConnectionString"));
                //});

                //Build service provider
                var sp = services.BuildServiceProvider();

                //Get a reference to the DbContext and seed its DB with Test data
                using (var scope = sp.CreateScope()) 
                {
                    //Get the Test DbContext from the service collection
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<CMTestsDbContext>();

                    //Get the Logger service from the service collection
                    var logger = scopedServices.GetRequiredService<ILogger<TestWebHost<TStartup>>>();

                    //ensure db is created
                    context.Database.EnsureCreated();

                    try {
                        //Seed db for tests
                        BATestDbSeeder.SeedDB(context);
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
