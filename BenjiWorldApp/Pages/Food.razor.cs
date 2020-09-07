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
    public class FoodTypeModel
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
    public class FoodBase : ComponentBase
    {
        public FoodBase()
        {
            Model = new FoodModel();
            Model.Created = DateTime.UtcNow;
            ShowEditData = false;
            FoodModels = new List<FoodModel>();
        }
        public IEnumerable<FoodTypeModel> FoodTypes;
        public List<FoodModel> FoodModels { get; set; }
        public int FoodTypeValue { get; set; }
        [Inject]
        public BenjiAPIClient Client { get; set; }
        [Inject]
        protected NotificationService NotificationService { get; set; }
        public FoodModel Model { get; set; }
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
            Model.Dog = DogModel;
            FoodModels = await Client.GetAllFood();
        }
        public async Task HandleValidSubmit()
        {
            HttpResponseMessage result = null;
            if (Model.FoodId == null || Model.FoodId.Value == 0)
            {
                var request = new FoodCreateRequest();
                request.Food.Created = Model.Created;
                request.Food.Modified = DateTime.UtcNow;
                request.Food.Dog = DogModel;
                request.Food.AmountInOunces = Model.AmountInOunces;
                request.Food.FrequencyPerDay = Model.FrequencyPerDay;
                result = await Client.CreateFood(request);
            }
            else
            {
                var request = new FoodUpdateRequest();
                request.Food.FoodId = Model.FoodId;
                request.Food.Deleted = Model.Deleted;
                request.Food.Created = Model.Created;
                request.Food.Modified = Model.Modified;
                request.Food.Dog = DogModel;
                request.Food.AmountInOunces = Model.AmountInOunces;
                request.Food.FrequencyPerDay = Model.FrequencyPerDay;
                result = await Client.UpdateFood(request);
            }
            if (result.IsSuccessStatusCode)
            {
                NotificationService.Notify(NotificationSeverity.Success, "Saved successfully");
                ShowEditData = false;
                StateHasChanged();
                FoodModels = await Client.GetAllFood();
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
