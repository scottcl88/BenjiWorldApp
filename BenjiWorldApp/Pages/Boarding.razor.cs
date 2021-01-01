using DataExtensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Models;
using Models.Shared;
using Radzen;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BenjiWorldApp.Pages
{
    public class BoardingBase : ComponentBase
    {
        public BoardingBase()
        {
            Model = new BoardingModel
            {
                Created = DateTime.UtcNow
            };
            ShowEditData = false;
            BoardingModels = new List<BoardingModel>();
        }

        public List<BoardingModel> BoardingModels { get; set; }
        public int IncidentTypeValue { get; set; }

        [Inject]
        public BenjiAPIClient Client { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        public BoardingModel Model { get; set; }
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
            BoardingModels = await Client.GetAllBoarding();
            DialogService.OnClose += (res) => Close(res);
        }

        public async Task HandleValidSubmit()
        {
            HttpResponseMessage result = null;
            if (Model.BoardingId == null || Model.BoardingId.Value == 0)
            {
                var request = new BoardingCreateRequest();
                request.Boarding.Created = Model.Created;
                request.Boarding.Modified = DateTime.UtcNow;
                request.Boarding.Dog = DogModel;
                request.Boarding.PaymentAmount = Model.PaymentAmount;
                request.Boarding.Comments = Model.Comments;
                request.Boarding.Company = Model.Company;
                request.Boarding.Address = Model.Address;
                request.Boarding.Reason = Model.Reason;
                request.Boarding.StartDateTime = Model.StartDateTime;
                request.Boarding.EndDateTime = Model.EndDateTime;
                request.Boarding.Website = Model.Website;
                result = await Client.CreateBoarding(request);
            }
            else
            {
                var request = new BoardingUpdateRequest();
                request.Boarding.BoardingId = Model.BoardingId;
                request.Boarding.PaymentAmount = Model.PaymentAmount;
                request.Boarding.Comments = Model.Comments;
                request.Boarding.Company = Model.Company;
                request.Boarding.Address = Model.Address;
                request.Boarding.Reason = Model.Reason;
                request.Boarding.StartDateTime = Model.StartDateTime;
                request.Boarding.EndDateTime = Model.EndDateTime;
                request.Boarding.Deleted = Model.Deleted;
                request.Boarding.Created = Model.Created;
                request.Boarding.Modified = Model.Modified;
                request.Boarding.Dog = DogModel;
                request.Boarding.Website = Model.Website;
                result = await Client.UpdateBoarding(request);
            }
            if (result.IsSuccessStatusCode)
            {
                NotificationService.Notify(NotificationSeverity.Success, "Saved successfully");
                ShowEditData = false;
                BoardingModels = await Client.GetAllBoarding();
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
            Model = new BoardingModel
            {
                Created = DateTime.UtcNow
            };
            StateHasChanged();
        }

        public void EditData(MouseEventArgs e, BoardingModel model)
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
            var result = await Client.DeleteBoarding(new BoardingDeleteRequest() { BoardingId = Model.BoardingId });
            if (result.IsSuccessStatusCode)
            {
                NotificationService.Notify(NotificationSeverity.Success, "Deleted successfully");
                ShowEditData = false;
                BoardingModels = await Client.GetAllBoarding();
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