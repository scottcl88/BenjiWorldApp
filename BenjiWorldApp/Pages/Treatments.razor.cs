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
    public class TreatmentsBase : ComponentBase
    {
        public TreatmentsBase()
        {
            Model = new TreatmentModel
            {
                Created = DateTime.UtcNow
            };
            ShowEditData = false;
            TreatmentModels = new List<TreatmentModel>();
            IncidentTypes = Enum.GetValues(typeof(IncidentType)).Cast<IncidentType>().Select(x => new IncidentTypeModel() { Name = x.ToString(), Value = (int)x });
        }

        public IEnumerable<IncidentTypeModel> IncidentTypes { get; set; }
        public List<TreatmentModel> TreatmentModels { get; set; }
        public int IncidentTypeValue { get; set; }

        [Inject]
        public BenjiAPIClient Client { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        public TreatmentModel Model { get; set; }
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
            TreatmentModels = await Client.GetAllTreatments();
            DialogService.OnClose += (res) => Close(res);
        }

        public async Task HandleValidSubmit()
        {
            HttpResponseMessage result = null;
            if (Model.TreatmentId == null || Model.TreatmentId.Value == 0)
            {
                var request = new TreatmentCreateRequest();
                request.Treatment.Created = Model.Created;
                request.Treatment.Modified = DateTime.UtcNow;
                request.Treatment.Dog = DogModel;
                request.Treatment.Title = Model.Title;
                request.Treatment.Doctor = Model.Doctor;
                request.Treatment.ReceivedDateTime = Model.ReceivedDateTime;
                request.Treatment.Comments = Model.Comments;
                request.Treatment.Amount = Model.Amount;
                request.Treatment.Duration = Model.Duration;
                result = await Client.CreateTreatment(request);
            }
            else
            {
                var request = new TreatmentUpdateRequest();
                request.Treatment.TreatmentId = Model.TreatmentId;
                request.Treatment.Title = Model.Title;
                request.Treatment.Doctor = Model.Doctor;
                request.Treatment.ReceivedDateTime = Model.ReceivedDateTime;
                request.Treatment.Comments = Model.Comments;
                request.Treatment.Amount = Model.Amount;
                request.Treatment.Duration = Model.Duration;
                request.Treatment.Deleted = Model.Deleted;
                request.Treatment.Created = Model.Created;
                request.Treatment.Modified = Model.Modified;
                request.Treatment.Dog = DogModel;
                result = await Client.UpdateTreatment(request);
            }
            if (result.IsSuccessStatusCode)
            {
                NotificationService.Notify(NotificationSeverity.Success, "Saved successfully");
                ShowEditData = false;
                TreatmentModels = await Client.GetAllTreatments();
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
            Model = new TreatmentModel
            {
                Created = DateTime.UtcNow
            };
            StateHasChanged();
        }

        public void EditData(MouseEventArgs e, TreatmentModel model)
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
            var result = await Client.DeleteTreatment(new TreatmentDeleteRequest() { TreatmentId = Model.TreatmentId });
            if (result.IsSuccessStatusCode)
            {
                NotificationService.Notify(NotificationSeverity.Success, "Deleted successfully");
                ShowEditData = false;
                TreatmentModels = await Client.GetAllTreatments();
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