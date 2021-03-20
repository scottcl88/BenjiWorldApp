using DataExtensions;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Models.Shared;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<DateTime> GetLastBackup()
        {
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            return await client.GetFromJsonAsync<DateTime>($"/Backup/GetLastBackup");
        }
        public async Task<bool> AddBackup()
        {
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            return await client.GetFromJsonAsync<bool>($"/Backup/AddBackup");
        }
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

        public async Task<List<BoardingModel>> GetAllBoarding()
        {
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            return await client.GetFromJsonAsync<List<BoardingModel>>($"/boarding/GetAll");
        }

        public async Task<List<VaccineModel>> GetAllVaccines()
        {
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            return await client.GetFromJsonAsync<List<VaccineModel>>($"/vaccine/GetAll");
        }
        public async Task<List<VetVisitModel>> GetAllVetVisits()
        {
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            return await client.GetFromJsonAsync<List<VetVisitModel>>($"/VetVisit/GetAll");
        }
        public async Task<List<TreatmentModel>> GetAllTreatments()
        {
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            return await client.GetFromJsonAsync<List<TreatmentModel>>($"/Treatment/GetAll");
        }
        public async Task<List<InsuranceModel>> GetAllInsurance()
        {
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            return await client.GetFromJsonAsync<List<InsuranceModel>>($"/Insurance/GetAll");
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

        public async Task<InsuranceModel> GetDefaultInsurance()
        {
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            return await client.GetFromJsonAsync<InsuranceModel>($"/Insurance/get");
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
            var result = await client.PostAsync($"/dog/update", stringContent);
            var postContent = await result.Content.ReadAsStringAsync();
            Logger.LogInformation("Updating Dog got result: " + postContent);
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

        public async Task<HttpResponseMessage> DeleteIncident(IncidentDeleteRequest request)
        {
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            var serialized = System.Text.Json.JsonSerializer.Serialize(request);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            var result = await client.PostAsync($"/Incident/delete", stringContent);
            return result;
        }

        public async Task<HttpResponseMessage> CreateBoarding(BoardingCreateRequest request)
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
            var result = await client.PostAsync($"/Boarding/add", stringContent);
            var postContent = await result.Content.ReadAsStringAsync();
            Logger.LogInformation("Creating Health got result: " + postContent);
            Logger.LogInformation("Creating Health is success: " + result.IsSuccessStatusCode);

            return result;
        }

        public async Task<HttpResponseMessage> UpdateBoarding(BoardingUpdateRequest request)
        {
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            var serialized = System.Text.Json.JsonSerializer.Serialize(request);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            var result = await client.PostAsync($"/Boarding/update", stringContent);
            return result;
        }

        public async Task<HttpResponseMessage> DeleteBoarding(BoardingDeleteRequest request)
        {
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            var serialized = System.Text.Json.JsonSerializer.Serialize(request);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            var result = await client.PostAsync($"/Boarding/delete", stringContent);
            return result;
        }

        public async Task<HttpResponseMessage> CreateVaccine(VaccineCreateRequest request)
        {
            Logger.LogInformation("Creating Vaccine with request");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            //var postRequest = JsonSerializer.Serialize<DogUpdateRequest>(request);
            var serialized = System.Text.Json.JsonSerializer.Serialize(request); //JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");

            //var addItem = new { Name = "Test" };
            Logger.LogInformation("Creating Vaccine with request 1");
            var result = await client.PostAsync($"/Vaccine/add", stringContent);
            var postContent = await result.Content.ReadAsStringAsync();
            Logger.LogInformation("Creating Vaccine got result: " + postContent);
            Logger.LogInformation("Creating Vaccine is success: " + result.IsSuccessStatusCode);

            return result;
        }

        public async Task<HttpResponseMessage> UpdateVaccine(VaccineUpdateRequest request)
        {
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            var serialized = System.Text.Json.JsonSerializer.Serialize(request);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            var result = await client.PostAsync($"/Vaccine/update", stringContent);
            return result;
        }

        public async Task<HttpResponseMessage> DeleteVaccine(VaccineDeleteRequest request)
        {
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            var serialized = System.Text.Json.JsonSerializer.Serialize(request);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            var result = await client.PostAsync($"/Vaccine/delete", stringContent);
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

        public async Task<HttpResponseMessage> UpdateDocument(DocumentUpdateRequest request)
        {
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            var serialized = System.Text.Json.JsonSerializer.Serialize(request);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            var result = await client.PostAsync($"/Document/update", stringContent);
            return result;
        }

        public async Task<HttpResponseMessage> DeleteDocument(DocumentDeleteRequest request)
        {
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            var serialized = System.Text.Json.JsonSerializer.Serialize(request);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            var result = await client.PostAsync($"/Document/delete", stringContent);
            return result;
        }

        public async Task<HttpResponseMessage> CreateVetVisit(VetVisitCreateRequest request)
        {
            Logger.LogInformation("Creating VetVisit with request");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            //var postRequest = JsonSerializer.Serialize<DogUpdateRequest>(request);
            var serialized = System.Text.Json.JsonSerializer.Serialize(request); //JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");

            //var addItem = new { Name = "Test" };
            Logger.LogInformation("Creating VetVisit with request 1");
            var result = await client.PostAsync($"/VetVisit/add", stringContent);
            var postContent = await result.Content.ReadAsStringAsync();
            Logger.LogInformation("Creating VetVisit got result: " + postContent);
            Logger.LogInformation("Creating VetVisit is success: " + result.IsSuccessStatusCode);

            return result;
        }

        public async Task<HttpResponseMessage> UpdateVetVisit(VetVisitUpdateRequest request)
        {
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            var serialized = System.Text.Json.JsonSerializer.Serialize(request);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            var result = await client.PostAsync($"/VetVisit/update", stringContent);
            return result;
        }

        public async Task<HttpResponseMessage> DeleteVetVisit(VetVisitDeleteRequest request)
        {
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            var serialized = System.Text.Json.JsonSerializer.Serialize(request);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            var result = await client.PostAsync($"/VetVisit/delete", stringContent);
            return result;
        }
        public async Task<HttpResponseMessage> CreateTreatment(TreatmentCreateRequest request)
        {
            Logger.LogInformation("Creating Treatment with request");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            //var postRequest = JsonSerializer.Serialize<DogUpdateRequest>(request);
            var serialized = System.Text.Json.JsonSerializer.Serialize(request); //JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");

            //var addItem = new { Name = "Test" };
            Logger.LogInformation("Creating Treatment with request 1");
            var result = await client.PostAsync($"/Treatment/add", stringContent);
            var postContent = await result.Content.ReadAsStringAsync();
            Logger.LogInformation("Creating Treatment got result: " + postContent);
            Logger.LogInformation("Creating Treatment is success: " + result.IsSuccessStatusCode);

            return result;
        }

        public async Task<HttpResponseMessage> UpdateTreatment(TreatmentUpdateRequest request)
        {
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            var serialized = System.Text.Json.JsonSerializer.Serialize(request);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            var result = await client.PostAsync($"/Treatment/update", stringContent);
            return result;
        }

        public async Task<HttpResponseMessage> DeleteTreatment(TreatmentDeleteRequest request)
        {
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            var serialized = System.Text.Json.JsonSerializer.Serialize(request);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            var result = await client.PostAsync($"/Treatment/delete", stringContent);
            return result;
        }
        public async Task<HttpResponseMessage> CreateInsurance(InsuranceCreateRequest request)
        {
            Logger.LogInformation("Creating Insurance with request");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            //var postRequest = JsonSerializer.Serialize<DogUpdateRequest>(request);
            var serialized = System.Text.Json.JsonSerializer.Serialize(request); //JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");

            //var addItem = new { Name = "Test" };
            Logger.LogInformation("Creating Insurance with request 1");
            var result = await client.PostAsync($"/Insurance/add", stringContent);
            var postContent = await result.Content.ReadAsStringAsync();
            Logger.LogInformation("Creating Insurance got result: " + postContent);
            Logger.LogInformation("Creating Insurance is success: " + result.IsSuccessStatusCode);

            return result;
        }

        public async Task<HttpResponseMessage> UpdateInsurance(InsuranceUpdateRequest request)
        {
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            var serialized = System.Text.Json.JsonSerializer.Serialize(request);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            var result = await client.PostAsync($"/Insurance/update", stringContent);
            return result;
        }

        public async Task<HttpResponseMessage> DeleteInsurance(InsuranceDeleteRequest request)
        {
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
            client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
            var serialized = System.Text.Json.JsonSerializer.Serialize(request);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            var result = await client.PostAsync($"/Insurance/delete", stringContent);
            return result;
        }
    }
}