using ClientsManager.Tests.IntegrationTests.Configuration;
using ClientsManager.WebAPI;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.Tests.IntegrationTests
{
    public static class TestConfigurationHelper
    {
        private static IConfiguration getConfiguration()
        {
            return new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: true)
                            //.AddUserSecrets("78a329d0-4c82-4430-9169-d4894aa7a79e") //UserSecretsId in ClientsManager.WebAPI.csproj
                            .AddUserSecrets(typeof(Program).Assembly) //UserSecretsId in ClientsManager.WebAPI.csproj
                            .AddEnvironmentVariables()
                            .Build();
        }

        public static TestsConfiguration getTestConfiguration()
        {
            var configuration = new TestsConfiguration();

            var iConfiguration = getConfiguration();

            iConfiguration
                .GetSection("ConnectionStrings:TestsDBConnectionString")
                .Bind(configuration);

            return configuration;
        }

        public static string getTestConnectionString()
        {
            var iConfiguration = getConfiguration();

            return iConfiguration.GetConnectionString("TestsDBConnectionString");
        }
    }
}
