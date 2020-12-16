﻿using DataExtensions;
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
            IncidentTypes = Enum.GetValues(typeof(IncidentType)).Cast<IncidentType>().Select(x => new IncidentTypeModel() { Name = x.ToString(), Value = (int)x });
        }
        public IEnumerable<IncidentTypeModel> IncidentTypes;
        public List<BoardingModel> BoardingModels { get; set; }
        public int IncidentTypeValue { get; set; }
        [Inject]
        public BenjiAPIClient Client { get; set; }
        [Inject]
        protected NotificationService NotificationService { get; set; }
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
                result = await Client.CreateBoarding(request);
            }
            else
            {
                var request = new BoardingUpdateRequest();
                request.Boarding.BoardingId = Model.BoardingId;
                request.Boarding.Deleted = Model.Deleted;
                request.Boarding.Created = Model.Created;
                request.Boarding.Modified = Model.Modified;
                request.Boarding.Dog = DogModel;
                result = await Client.UpdateBoarding(request);
            }
            if (result.IsSuccessStatusCode)
            {
                NotificationService.Notify(NotificationSeverity.Success, "Saved successfully");
                ShowEditData = false;
                StateHasChanged();
                BoardingModels = await Client.GetAllBoarding();
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
