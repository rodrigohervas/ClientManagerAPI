using ClientsManager.Data;
using ClientsManager.WebAPI;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ClientsManager.Tests.IntegrationTests
{
    public class TestHttpClient<T> : HttpClient where T: class
    {
        //create a factory representing an in-memory API
        private readonly WebApplicationFactory<Startup> _factory;
        //create an HttpClient to send requests to in-memory API
        private readonly HttpClient _testClient;

        public TestHttpClient()
        {
            //create a WebApplicationfactory object
            _factory = new WebApplicationFactory<Startup>();

            //create and Configure an HttpClient
            _testClient = _factory
                                .WithWebHostBuilder(builder =>
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
                                                options.UseInMemoryDatabase("TestDB")
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
                                            var context = scopedServices.GetRequiredService<TestDBContext>();

                                            //Get the Logger service from the service collection
                                            var logger = scopedServices.GetRequiredService<ILogger<Startup>>();

                                            //ensure DB is deleted and newly created
                                            context.Database.EnsureCreated();

                                            try
                                            {
                                                //Seed db for tests
                                                TestSeeder<T>.SeedDB(context);
                                            }
                                            catch (Exception ex)
                                            {
                                                //log any seeding errors
                                                logger.LogError(ex, "Error seeding the Test DB. Error: {Message}", ex.Message);
                                            }
                                        }
                                    });
                                })
                                .CreateClient();

            //set HttpClient.BaseAddress
            this.BaseAddress = _testClient.BaseAddress;
            
            //set the Test Authorization for the HttpClient
            //Test Authorization is defined in TestAuthorizationHandler.cs
            _testClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");
        }

    }
}
