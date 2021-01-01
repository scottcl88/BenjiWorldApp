using DataExtensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Models;
using Radzen;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BenjiWorldApp.Pages
{
    public class HealthBase : ComponentBase
    {
        public HealthBase()
        {
            Model = new HealthModel();
            Model.Created = DateTime.UtcNow;
            ShowEditData = false;
            HealthModels = new List<HealthModel>();
        }

        public List<HealthModel> HealthModels { get; set; }

        [Inject]
        public BenjiAPIClient Client { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        public HealthModel Model { get; set; }
        public DogModel DogModel { get; set; }
        public bool ShowEditData { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var myDog = await Client.GetDefaultDog();
            DogModel = new DogModel(myDog);
            var healthData = await Client.GetAllHealth();
            HealthModels = healthData;
        }

        public async Task HandleValidSubmit()
        {
            HttpResponseMessage result = null;
            if (Model.HealthId == null || Model.HealthId.Value == 0)
            {
                var request = new HealthCreateRequest();
                request.Health.Created = Model.Created;
                request.Health.Modified = DateTime.UtcNow;
                request.Health.FromVet = Model.FromVet;
                request.Health.Height = Model.Height;
                request.Health.MouthCircumference = Model.MouthCircumference;
                request.Health.NoseEyeLength = Model.NoseEyeLength;
                request.Health.TailLength = Model.TailLength;
                request.Health.Waist = Model.Waist;
                request.Health.Weight = Model.Weight;
                request.Health.Dog = DogModel;
                result = await Client.CreateHealth(request);
            }
            else
            {
                var request = new HealthUpdateRequest();
                request.Health.HealthId = Model.HealthId;
                request.Health.Deleted = Model.Deleted;
                request.Health.Created = Model.Created;
                request.Health.Modified = Model.Modified;
                request.Health.FromVet = Model.FromVet;
                request.Health.Height = Model.Height;
                request.Health.MouthCircumference = Model.MouthCircumference;
                request.Health.NoseEyeLength = Model.NoseEyeLength;
                request.Health.TailLength = Model.TailLength;
                request.Health.Waist = Model.Waist;
                request.Health.Weight = Model.Weight;
                request.Health.Dog = DogModel;
                result = await Client.UpdateHealth(request);
            }
            if (result.IsSuccessStatusCode)
            {
                NotificationService.Notify(NotificationSeverity.Success, "Saved successfully");
            }
            else
            {
                NotificationService.Notify(NotificationSeverity.Error, "Failed", result.ReasonPhrase, 6000);
            }
        }

        public void AddData(MouseEventArgs e)
        {
            ShowEditData = true;
            StateHasChanged();
        }

        public void EditData(MouseEventArgs e)
        {
            ShowEditData = true;
            StateHasChanged();
        }

        public void CancelEditData(MouseEventArgs e)
        {
            ShowEditData = false;
            StateHasChanged();
        }

        //////////////////////////
        ///
        public bool smooth = true;

        public class DataItem
        {
            public DateTime Date { get; set; }
            public decimal Weight { get; set; }
        }
    }
}