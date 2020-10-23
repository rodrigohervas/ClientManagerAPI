using ClientsManager.Tests.IntegrationTests.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.Tests.IntegrationTests
{
    public static class TestConfigurationHelper
    {
        private static IConfiguration getConfiguration(string path)
        {
            return new ConfigurationBuilder()
                            .SetBasePath(path)
                            .AddJsonFile("appsettings.json", optional: true)
                            .AddUserSecrets("78a329d0-4c82-4430-9169-d4894aa7a79e") //UserSecretsId in ClientsManager.WebAPI.csproj
                            .AddEnvironmentVariables()
                            .Build();
        }

        public static TestsConfiguration getTestConfiguration(string path)
        {
            var configuration = new TestsConfiguration();

            var iConfiguration = getConfiguration(path);

            iConfiguration
                .GetSection("ConnectionStrings:TestsDBConnectionString")
                .Bind(configuration);

            return configuration;
        }

        public static string getTestConnectionString(string path)
        {
            var iConfiguration = getConfiguration(path);

            return iConfiguration.GetConnectionString("TestsDBConnectionString");
        }
    }
}
