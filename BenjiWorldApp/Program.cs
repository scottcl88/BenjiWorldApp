using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BenjiWorldApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");


            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            //builder.Services.A.AddScoped("BenjiAPI", sp => new HttpClient { BaseAddress = new Uri("http://localhost:59006") });

            //builder.Services.AddHttpClient("BenjiAPI", client => client.BaseAddress = new Uri("http://localhost:59006"));

            builder.Services.AddHttpClient<BenjiAPIClient>(client => client.BaseAddress = new Uri("http://localhost:59006"));
            await builder.Build().RunAsync();
        }
    }
}
