﻿using DataExtensions;
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
    public class InsuranceBase : ComponentBase
    {
        public InsuranceBase()
        {
            Model = new InsuranceModel
            {
                Created = DateTime.UtcNow
            };
            ShowEditData = false;
            InsuranceModels = new List<InsuranceModel>();
            IncidentTypes = Enum.GetValues(typeof(IncidentType)).Cast<IncidentType>().Select(x => new IncidentTypeModel() { Name = x.ToString(), Value = (int)x });
        }

        public IEnumerable<IncidentTypeModel> IncidentTypes { get; set; }
        public List<InsuranceModel> InsuranceModels { get; set; }
        public int IncidentTypeValue { get; set; }

        [Inject]
        public BenjiAPIClient Client { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        public InsuranceModel Model { get; set; }
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
            InsuranceModels = await Client.GetAllInsurance();
            DialogService.OnClose += (res) => Close(res);
        }

        public async Task HandleValidSubmit()
        {
            HttpResponseMessage result = null;
            if (Model.InsuranceId == null || Model.InsuranceId.Value == 0)
            {
                var request = new InsuranceCreateRequest();
                request.Insurance.Created = Model.Created;
                request.Insurance.Modified = DateTime.UtcNow;
                request.Insurance.Dog = DogModel;
                request.Insurance.AnnualCoverageLimit = Model.AnnualCoverageLimit;
                request.Insurance.DeductibleAmount = Model.DeductibleAmount;
                request.Insurance.EndDateTime = Model.EndDateTime;
                request.Insurance.PaymentAmount = Model.PaymentAmount;
                request.Insurance.PaymentFrequency = Model.PaymentFrequency;
                request.Insurance.Company = Model.Company;
                request.Insurance.PolicyId = Model.PolicyId;
                request.Insurance.ReimbursementPercentage = Model.ReimbursementPercentage;
                request.Insurance.RenewalDateTime = Model.RenewalDateTime;
                request.Insurance.StartDateTime = Model.StartDateTime;
                request.Insurance.Website = Model.Website;
                result = await Client.CreateInsurance(request);
            }
            else
            {
                var request = new InsuranceUpdateRequest();
                request.Insurance.InsuranceId = Model.InsuranceId;
                request.Insurance.AnnualCoverageLimit = Model.AnnualCoverageLimit;
                request.Insurance.DeductibleAmount = Model.DeductibleAmount;
                request.Insurance.EndDateTime = Model.EndDateTime;
                request.Insurance.PaymentAmount = Model.PaymentAmount;
                request.Insurance.PaymentFrequency = Model.PaymentFrequency;
                request.Insurance.Company = Model.Company;
                request.Insurance.PolicyId = Model.PolicyId;
                request.Insurance.ReimbursementPercentage = Model.ReimbursementPercentage;
                request.Insurance.RenewalDateTime = Model.RenewalDateTime;
                request.Insurance.StartDateTime = Model.StartDateTime;
                request.Insurance.Website = Model.Website;
                request.Insurance.Deleted = Model.Deleted;
                request.Insurance.Created = Model.Created;
                request.Insurance.Modified = Model.Modified;
                request.Insurance.Dog = DogModel;
                result = await Client.UpdateInsurance(request);
            }
            if (result.IsSuccessStatusCode)
            {
                NotificationService.Notify(NotificationSeverity.Success, "Saved successfully");
                ShowEditData = false;
                InsuranceModels = await Client.GetAllInsurance();
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
            Model = new InsuranceModel
            {
                Created = DateTime.UtcNow
            };
            StateHasChanged();
        }

        public void EditData(MouseEventArgs e, InsuranceModel model)
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
            var result = await Client.DeleteInsurance(new InsuranceDeleteRequest() { InsuranceId = Model.InsuranceId });
            if (result.IsSuccessStatusCode)
            {
                NotificationService.Notify(NotificationSeverity.Success, "Deleted successfully");
                ShowEditData = false;
                InsuranceModels = await Client.GetAllInsurance();
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