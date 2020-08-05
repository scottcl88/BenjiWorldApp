using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Models;
using System.Net.Http.Json;
using DataExtensions;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace BenjiWorldApp
{
    public class BenjiAPIClient
    {
        private readonly HttpClient client;

        public BenjiAPIClient(HttpClient client, ILogger<BenjiAPIClient> logger)
        {
            this.client = client;
            Logger = logger;
        }

        [Inject]
        protected ILogger<BenjiAPIClient> Logger { get; set; }
        //[Inject]
        //protected ILoggerFactory LoggerFactory { get; set; }


        public async Task<DogModel> GetDog(int dogId)
        {
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            return await client.GetFromJsonAsync<DogModel>($"/dog/get/{dogId}");
        }
        public async Task<HttpResponseMessage> UpdateDog(DogUpdateRequest request)
        {
            Logger.LogInformation("Updating Dog with request");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            //var postRequest = JsonSerializer.Serialize<DogUpdateRequest>(request);
            var serialized = System.Text.Json.JsonSerializer.Serialize(request); //JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");

            //var addItem = new { Name = "Test" };
            Logger.LogInformation("Updating Dog with request 1");
            var result =  await client.PostAsync($"/dog/update", stringContent); 
            var postContent = await result.Content.ReadAsStringAsync();
            Logger.LogInformation("Updating Dog got result: "+ postContent);
            Logger.LogInformation("Updating Dog is success: " + result.IsSuccessStatusCode);

            return result;
        }
    }
}
