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
using System.Threading;
using Models.Shared;

namespace BenjiWorldApp
{
    //protected override void OnInitialized()
    //{
    //    GetRelativeTime();
    //    var cancellationTokenSource = new CancellationTokenSource();
    //    var task = Repeat.Interval(
    //            TimeSpan.FromSeconds(15), () => GetRelativeTime(), cancellationTokenSource.Token);
    //}
    static class CancellationTokenExtensions
    {
        public static bool WaitCancellationRequested(
            this CancellationToken token,
            TimeSpan timeout)
        {
            return token.WaitHandle.WaitOne(timeout);
        }
    }
    internal static class Repeat
    {
        public static Task Interval(
            TimeSpan pollInterval,
            Action action,
            CancellationToken token)
        {
            // We don't use Observable.Interval:
            // If we block, the values start bunching up behind each other.
            return Task.Factory.StartNew(
                () =>
                {
                    for (; ; )
                    {
                        if (token.WaitCancellationRequested(pollInterval))
                            break;

                        action();
                    }
                }, token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }
    }
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

        public async Task<List<FoodModel>> GetAllFood()
        {
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            return await client.GetFromJsonAsync<List<FoodModel>>($"/food/GetAll");
        }
        public async Task<List<IncidentModel>> GetAllIncident()
        {
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            return await client.GetFromJsonAsync<List<IncidentModel>>($"/incident/GetAll");
        }
        public async Task<List<HealthModel>> GetAllHealth()
        {
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            return await client.GetFromJsonAsync<List<HealthModel>>($"/health/GetAll");
        }
        public async Task<List<FolderModel>> GetAllFolders()
        {
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            return await client.GetFromJsonAsync<List<FolderModel>>($"/folder/GetAll");
        }
        public async Task<List<DocumentModel>> GetAllDocuments()
        {
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            return await client.GetFromJsonAsync<List<DocumentModel>>($"/document/GetAll");
        }
        public async Task<DogModel> GetDefaultDog()
        {
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            return await client.GetFromJsonAsync<DogModel>($"/dog/get");
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
        public async Task<HttpResponseMessage> CreateDog(DogCreateRequest request)
        {
            Logger.LogInformation("Creating Dog with request");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            //var postRequest = JsonSerializer.Serialize<DogUpdateRequest>(request);
            var serialized = System.Text.Json.JsonSerializer.Serialize(request); //JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");

            //var addItem = new { Name = "Test" };
            Logger.LogInformation("Creating Dog with request 1");
            var result = await client.PostAsync($"/dog/add", stringContent);
            var postContent = await result.Content.ReadAsStringAsync();
            Logger.LogInformation("Creating Dog got result: " + postContent);
            Logger.LogInformation("Creating Dog is success: " + result.IsSuccessStatusCode);

            return result;
        }

        public async Task<HttpResponseMessage> CreateFolder(FolderCreateRequest request)
        {
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            var serialized = System.Text.Json.JsonSerializer.Serialize(request);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            var result = await client.PostAsync($"/folder/add", stringContent);
            return result;
        }
        public async Task<HttpResponseMessage> UpdateFolder(FolderUpdateRequest request)
        {
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            var serialized = System.Text.Json.JsonSerializer.Serialize(request);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            var result = await client.PostAsync($"/folder/update", stringContent);
            return result;
        }

        public async Task<HttpResponseMessage> CreateHealth(HealthCreateRequest request)
        {
            Logger.LogInformation("Creating Health with request");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            //var postRequest = JsonSerializer.Serialize<DogUpdateRequest>(request);
            var serialized = System.Text.Json.JsonSerializer.Serialize(request); //JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");

            //var addItem = new { Name = "Test" };
            Logger.LogInformation("Creating Health with request 1");
            var result = await client.PostAsync($"/health/add", stringContent);
            var postContent = await result.Content.ReadAsStringAsync();
            Logger.LogInformation("Creating Health got result: " + postContent);
            Logger.LogInformation("Creating Health is success: " + result.IsSuccessStatusCode);

            return result;
        }
        public async Task<HttpResponseMessage> UpdateHealth(HealthUpdateRequest request)
        {
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            var serialized = System.Text.Json.JsonSerializer.Serialize(request);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            var result = await client.PostAsync($"/health/update", stringContent);
            return result;
        }
        public async Task<HttpResponseMessage> CreateIncident(IncidentCreateRequest request)
        {
            Logger.LogInformation("Creating Health with request");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            //var postRequest = JsonSerializer.Serialize<DogUpdateRequest>(request);
            var serialized = System.Text.Json.JsonSerializer.Serialize(request); //JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");

            //var addItem = new { Name = "Test" };
            Logger.LogInformation("Creating Health with request 1");
            var result = await client.PostAsync($"/Incident/add", stringContent);
            var postContent = await result.Content.ReadAsStringAsync();
            Logger.LogInformation("Creating Health got result: " + postContent);
            Logger.LogInformation("Creating Health is success: " + result.IsSuccessStatusCode);

            return result;
        }
        public async Task<HttpResponseMessage> UpdateIncident(IncidentUpdateRequest request)
        {
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            var serialized = System.Text.Json.JsonSerializer.Serialize(request);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            var result = await client.PostAsync($"/Incident/update", stringContent);
            return result;
        }
        public async Task<HttpResponseMessage> CreateFood(FoodCreateRequest request)
        {
            Logger.LogInformation("Creating food with request");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            //var postRequest = JsonSerializer.Serialize<DogUpdateRequest>(request);
            var serialized = System.Text.Json.JsonSerializer.Serialize(request); //JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");

            //var addItem = new { Name = "Test" };
            Logger.LogInformation("Creating food with request 1");
            var result = await client.PostAsync($"/food/add", stringContent);
            var postContent = await result.Content.ReadAsStringAsync();
            Logger.LogInformation("Creating food got result: " + postContent);
            Logger.LogInformation("Creating food is success: " + result.IsSuccessStatusCode);

            return result;
        }
        public async Task<HttpResponseMessage> UpdateFood(FoodUpdateRequest request)
        {
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            var serialized = System.Text.Json.JsonSerializer.Serialize(request);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            var result = await client.PostAsync($"/food/update", stringContent);
            return result;
        }
    }
}
