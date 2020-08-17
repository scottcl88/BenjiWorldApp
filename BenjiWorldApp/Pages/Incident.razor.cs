using DataExtensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Models;
using Models.Shared;
using Radzen;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BenjiWorldApp.Pages
{
    public class IncidentTypeModel
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
    public class IncidentBase : ComponentBase
    {
        public IncidentBase()
        {
            Model = new IncidentModel();
            Model.Created = DateTime.UtcNow;
            Model.IncidentDate = DateTime.UtcNow;
            ShowEditData = false;
            IncidentModels = new List<IncidentModel>();
            IncidentTypes = Enum.GetValues(typeof(IncidentType)).Cast<IncidentType>().Select(x => new IncidentTypeModel() { Name = x.ToString(), Value = (int)x });
        }
        public IEnumerable<IncidentTypeModel> IncidentTypes;
        public List<IncidentModel> IncidentModels { get; set; }
        public int IncidentTypeValue { get; set; }
        [Inject]
        public BenjiAPIClient Client { get; set; }
        [Inject]
        protected NotificationService NotificationService { get; set; }
        public IncidentModel Model { get; set; }
        public DogModel DogModel { get; set; }
        public bool ShowEditData { get; set; }
        public void Change(object value)
        {
            try
            {
                Model.IncidentType = (IncidentType)value;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Failed", ex.Message, 6000);
            }
        }
        protected override async Task OnInitializedAsync()
        {
            var myDog = await Client.GetDefaultDog();
            DogModel = new DogModel(myDog);
            IncidentModels = await Client.GetAllIncident();
        }
        public async Task HandleValidSubmit()
        {
            HttpResponseMessage result = null;
            if (Model.IncidentId == null || Model.IncidentId.Value == 0)
            {
                var request = new IncidentCreateRequest();
                request.Incident.Created = Model.Created;
                request.Incident.Modified = DateTime.UtcNow;
                request.Incident.Dog = DogModel;
                request.Incident.Title = Model.Title;
                request.Incident.Description = Model.Description;
                request.Incident.IncidentDate = Model.IncidentDate;
                request.Incident.IncidentType = Model.IncidentType;
                result = await Client.CreateIncident(request);
            }
            else
            {
                var request = new IncidentUpdateRequest();
                request.Incident.IncidentId = Model.IncidentId;
                request.Incident.Deleted = Model.Deleted;
                request.Incident.Created = Model.Created;
                request.Incident.Modified = Model.Modified;
                request.Incident.Dog = DogModel;
                request.Incident.Title = Model.Title;
                request.Incident.Description = Model.Description;
                request.Incident.IncidentDate = Model.IncidentDate;
                request.Incident.IncidentType = Model.IncidentType;
                result = await Client.UpdateIncident(request);
            }
            if (result.IsSuccessStatusCode)
            {
                NotificationService.Notify(NotificationSeverity.Success, "Saved successfully");
                ShowEditData = false;
                StateHasChanged();
                IncidentModels = await Client.GetAllIncident();
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
