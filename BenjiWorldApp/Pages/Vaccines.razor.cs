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
    public class VaccinesBase : ComponentBase
    {
        public VaccinesBase()
        {
            Model = new VaccineModel
            {
                Created = DateTime.UtcNow
            };
            ShowEditData = false;
            VaccineModels = new List<VaccineModel>();
            IncidentTypes = Enum.GetValues(typeof(IncidentType)).Cast<IncidentType>().Select(x => new IncidentTypeModel() { Name = x.ToString(), Value = (int)x });
        }

        public IEnumerable<IncidentTypeModel> IncidentTypes { get; set; }
        public List<VaccineModel> VaccineModels { get; set; }
        public int IncidentTypeValue { get; set; }

        [Inject]
        public BenjiAPIClient Client { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        public VaccineModel Model { get; set; }
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
            VaccineModels = await Client.GetAllVaccines();
            DialogService.OnClose += (res) => Close(res);
        }

        public async Task HandleValidSubmit()
        {
            HttpResponseMessage result = null;
            if (Model.VaccineId == null || Model.VaccineId.Value == 0)
            {
                var request = new VaccineCreateRequest();
                request.Vaccine.Created = Model.Created;
                request.Vaccine.Modified = DateTime.UtcNow;
                request.Vaccine.Dog = DogModel;
                request.Vaccine.Title = Model.Title;
                request.Vaccine.Doctor = Model.Doctor;
                request.Vaccine.Received = Model.Received;
                request.Vaccine.Expiration = Model.Expiration;
                request.Vaccine.Comments = Model.Comments;
                request.Vaccine.Company = Model.Company;
                request.Vaccine.Address = Model.Address;
                result = await Client.CreateVaccine(request);
            }
            else
            {
                var request = new VaccineUpdateRequest();
                request.Vaccine.VaccineId = Model.VaccineId;
                request.Vaccine.Title = Model.Title;
                request.Vaccine.Doctor = Model.Doctor;
                request.Vaccine.Received = Model.Received;
                request.Vaccine.Expiration = Model.Expiration;
                request.Vaccine.Comments = Model.Comments;
                request.Vaccine.Company = Model.Company;
                request.Vaccine.Address = Model.Address;
                request.Vaccine.Deleted = Model.Deleted;
                request.Vaccine.Created = Model.Created;
                request.Vaccine.Modified = Model.Modified;
                request.Vaccine.Dog = DogModel;
                result = await Client.UpdateVaccine(request);
            }
            if (result.IsSuccessStatusCode)
            {
                NotificationService.Notify(NotificationSeverity.Success, "Saved successfully");
                ShowEditData = false;
                VaccineModels = await Client.GetAllVaccines();
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
            Model = new VaccineModel
            {
                Created = DateTime.UtcNow
            };
            StateHasChanged();
        }

        public void EditData(MouseEventArgs e, VaccineModel model)
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
            var result = await Client.DeleteVaccine(new VaccineDeleteRequest() { VaccineId = Model.VaccineId });
            if (result.IsSuccessStatusCode)
            {
                NotificationService.Notify(NotificationSeverity.Success, "Deleted successfully");
                ShowEditData = false;
                VaccineModels = await Client.GetAllVaccines();
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