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

namespace ClientsManager.Tests.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        //create a factory representing an in-memory API
        //private readonly WebApplicationFactory<TStartup> _factory;
        //create an HttpClient to send requests to in-memory API
        //private readonly HttpClient _testClient;

        //public CustomWebApplicationFactory()
        //{
        //    //create a WebApplicationfactory object
        //    _factory = new WebApplicationFactory<TStartup>();

        //    //create and Configure an HttpClient
        //    _testClient = _factory
        //                        .WithWebHostBuilder(builder =>
        //                        {
                                    
        //                        })
        //                        .CreateClient();

        //    //set HttpClient.BaseAddress
        //    this.BaseAddress = _testClient.BaseAddress;

        //    //set the Test Authorization for the HttpClient
        //    //Test Authorization is defined in TestAuthorizationHandler.cs
        //    _testClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");
        //}

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                //set the test Authentication Scheme using the TestAuthorizationHandler class
                services.AddAuthentication("TestScheme")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthorizationHandler>("TestScheme", options => { });

                //remove DBContext registered in Startup, to set a new test one
                services.RemoveAll(typeof(ClientsManagerDBContext));

                //Add testing configured (InMemory) DbContext
                services.AddDbContext<TestDBContext>(options =>
                        options.UseInMemoryDatabase("ClientsManagerTestDB")
                               .EnableSensitiveDataLogging()
                );

                //Add Generic Repo
                //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

                //Build service provider
                var sp = services.BuildServiceProvider();

                //Get a reference to the DbContext and seed its DB with Test data
                using (var scope = sp.CreateScope())
                {
                    //Get the Test DbContext from the service collection
                    var scopedServices = scope.ServiceProvider;
                    var dbContext = scopedServices.GetRequiredService<TestDBContext>();

                    //Get the Logger service from the service collection
                    var logger = scopedServices.GetRequiredService<ILogger<TStartup>>();

                    //ensure DB created
                    dbContext.Database.EnsureCreated();

                    try
                    {
                        //Seed db for tests
                        TestSeeder<Client>.SeedDB(dbContext);
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
