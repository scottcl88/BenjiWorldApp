using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Radzen;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BenjiWorldApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new NotificationService());
            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            //builder.Services.A.AddScoped("BenjiAPI", sp => new HttpClient { BaseAddress = new Uri("http://localhost:59006") });

            //builder.Services.AddHttpClient("BenjiAPI", client => client.BaseAddress = new Uri("http://localhost:59006"));

            string apiUrl = builder.Configuration["APIUrl"];
            //builder.Services.AddHttpClient<BenjiAPIClient>(client => client.BaseAddress = new Uri("http://localhost:59006"));
            builder.Services.AddHttpClient<BenjiAPIClient>(client => client.BaseAddress = new Uri(apiUrl));

            builder.Logging.SetMinimumLevel(LogLevel.Debug);
            //builder.Logging.AddProvider(new CustomLoggingProvider());

            builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
            await builder.Build().RunAsync();
        }
    }

    internal class CustomLoggingProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}