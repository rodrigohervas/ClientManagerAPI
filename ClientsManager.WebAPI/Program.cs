using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ClientsManager.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //read config from appsettings.json
            var serilogConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            //create Serilog Logger object
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(serilogConfiguration)
                .CreateLogger();

            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));

            //TODO: delete for deployment
            var conf = serilogConfiguration.GetSection("Serilog:WriteTo:3:Args:connectionString").Value;
            Log.Information($"CONFIG for SQL Server:::: {@conf}", conf);

            try
            {
                Log.Information("ClientsManager API is Starting up");
                CreateHostBuilder(args).Build().Run();
                Log.Information("ClientsManager API has Started up successfully");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "ClientsManager API start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog() //log all events using Serilog
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
