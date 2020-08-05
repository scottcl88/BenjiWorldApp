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

namespace BenjiWorldApp
{
    public class BenjiAPIClient
    {
        private readonly HttpClient client;

        public BenjiAPIClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<DogModel> GetDog(int dogId)
        {
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            return await client.GetFromJsonAsync<DogModel>($"/dog/get/{dogId}");
        }
        public async Task<HttpResponseMessage> UpdateDog(DogUpdateRequest request)
        {
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            return await client.PostAsJsonAsync<DogUpdateRequest>($"/dog/update", request);
        }
    }
}
