using DataExtensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Models.Shared;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BenjiWorldApp.Pages
{
    public class VetVisitsBase : ComponentBase
    {
        public VetVisitsBase()
        {
            Model = new VetVisitModel
            {
                Created = DateTime.UtcNow
            };
            ShowEditData = false;
            VetVisitModels = new List<VetVisitModel>();
            IncidentTypes = Enum.GetValues(typeof(IncidentType)).Cast<IncidentType>().Select(x => new IncidentTypeModel() { Name = x.ToString(), Value = (int)x });
        }

        public IEnumerable<IncidentTypeModel> IncidentTypes { get; set; }
        public List<VetVisitModel> VetVisitModels { get; set; }
        public int IncidentTypeValue { get; set; }

        [Inject]
        public BenjiAPIClient Client { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        public VetVisitModel Model { get; set; }
        public DogModel DogModel { get; set; }
        public bool ShowEditData { get; set; }

        public void Change(object value)
        {
            try
            {
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
            VetVisitModels = await Client.GetAllVetVisits();
            DialogService.OnClose += (res) => Close(res);
        }

        public async Task HandleValidSubmit()
        {
            HttpResponseMessage result = null;
            if (Model.VetVisitId == null || Model.VetVisitId.Value == 0)
            {
                var request = new VetVisitCreateRequest();
                request.VetVisit.Created = Model.Created;
                request.VetVisit.Modified = DateTime.UtcNow;
                request.VetVisit.Dog = DogModel;
                request.VetVisit.Title = Model.Title;
                request.VetVisit.Doctor = Model.Doctor;
                request.VetVisit.VisitDateTime = Model.VisitDateTime;
                request.VetVisit.Comments = Model.Comments;
                request.VetVisit.Company = Model.Company;
                request.VetVisit.Address = Model.Address;
                result = await Client.CreateVetVisit(request);
            }
            else
            {
                var request = new VetVisitUpdateRequest();
                request.VetVisit.VetVisitId = Model.VetVisitId;
                request.VetVisit.Title = Model.Title;
                request.VetVisit.Doctor = Model.Doctor;
                request.VetVisit.VisitDateTime = Model.VisitDateTime;
                request.VetVisit.Comments = Model.Comments;
                request.VetVisit.Company = Model.Company;
                request.VetVisit.Address = Model.Address;
                request.VetVisit.Deleted = Model.Deleted;
                request.VetVisit.Created = Model.Created;
                request.VetVisit.Modified = Model.Modified;
                request.VetVisit.Dog = DogModel;
                result = await Client.UpdateVetVisit(request);
            }
            if (result.IsSuccessStatusCode)
            {
                NotificationService.Notify(NotificationSeverity.Success, "Saved successfully");
                ShowEditData = false;
                VetVisitModels = await Client.GetAllVetVisits();
                StateHasChanged();
            }
            else
            {
                NotificationService.Notify(NotificationSeverity.Error, "Failed", result.ReasonPhrase, 6000);
            }
        }

        public void AddData(MouseEventArgs e)
        {
            ShowEditData = true;
            Model = new VetVisitModel
            {
                Created = DateTime.UtcNow
            };
            StateHasChanged();
        }

        public void EditData(MouseEventArgs e, VetVisitModel model)
        {
            ShowEditData = true;
            Model = model;
            StateHasChanged();
        }

        public void CancelEditData(MouseEventArgs e)
        {
            ShowEditData = false;
            StateHasChanged();
        }

        public async Task DeleteData()
        {
            var result = await Client.DeleteVetVisit(new VetVisitDeleteRequest() { VetVisitId = Model.VetVisitId });
            if (result.IsSuccessStatusCode)
            {
                NotificationService.Notify(NotificationSeverity.Success, "Deleted successfully");
                ShowEditData = false;
                VetVisitModels = await Client.GetAllVetVisits();
                StateHasChanged();
            }
            else
            {
                NotificationService.Notify(NotificationSeverity.Error, "Failed", result.ReasonPhrase, 6000);
            }
        }

        public async Task Close(dynamic result)
        {
            if (result)
            {
                await DeleteData();
            }
        }
    }
}