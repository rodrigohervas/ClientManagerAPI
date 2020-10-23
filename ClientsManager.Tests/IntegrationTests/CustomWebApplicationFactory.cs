using ClientsManager.Data;
using ClientsManager.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClientsManager.Tests.IntegrationTests
{
    /// <summary>
    /// Bootstrapps an in-memory application (TestServer) for integration tests
    /// </summary>
    /// <typeparam name="TStartup">Startup class</typeparam>
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {

        //uses the generic IHostBuilder, for apps using Host.CreateDefaultBuilder
        protected override IHostBuilder CreateHostBuilder()
        {
            return base.CreateHostBuilder();
        }

        //uses the IWebHostBuilder, for apps using the legacy WebHost.CreateDefaultBuilder
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config =>
            {
                config.AddUserSecrets<TStartup>(true, reloadOnChange: true);
            });

            builder.ConfigureServices(services =>
            {
                //set the test Authentication Scheme using the TestAuthorizationHandler class
                services.AddAuthentication("TestScheme")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthorizationHandler>("TestScheme", options => { });

                //remove DBContext registered in Startup, to set a new test one
                services.RemoveAll(typeof(ClientsManagerDbContext));

                //Register TestDbContext Middleware (In-Memory DB)
                services.AddDbContext<CMTestsDbContext>(options =>
                                options.UseInMemoryDatabase("ClientsManagerTestDB")
                                       .EnableSensitiveDataLogging()
                );

                //Register TestDbContext Middleware (SQL Server Test DB)
                //var testDBConnectionString = TestConfigurationHelper.getTestConnectionString();
                //services.AddDbContext<CMTestsDbContext>(options =>
                //    options.UseSqlServer(testDBConnectionString)
                //);

                //Add Generic Repo
                services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

                //Build service provider
                var sp = services.BuildServiceProvider();

                //Get a reference to the DbContext and seed its DB with Test data
                using (var scope = sp.CreateScope())
                {
                    //Get the Test DbContext from the service collection
                    var scopedServices = scope.ServiceProvider;
                    var dbContext = scopedServices.GetRequiredService<CMTestsDbContext>();

                    //Get the Logger service from the service collection
                    var logger = scopedServices.GetRequiredService<ILogger<TStartup>>();

                    try
                    {
                        //Seed DataBase for tests
                        TestSeeder.SeedDB(dbContext);
                    }
                    catch (Exception ex)
                    {
                        //log any seeding errors
                        logger.LogError(ex, "Error seeding the Test DB. Error: {Message}", ex.Message);
                    }
                }
            });
        }
    }
}
