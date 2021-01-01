using DataExtensions;
using Microsoft.AspNetCore.Components;
using Models;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BenjiWorldApp.Pages
{
    public class GenderModel
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }

    public class DogProfileBase : ComponentBase
    {
        public DogProfileBase()
        {
            Model = new DogModel();
            //NotificationService = new NotificationService();
            Genders = Enum.GetValues(typeof(Gender)).Cast<Gender>().Select(x => new GenderModel() { Name = x.ToString(), Value = (int)x });
        }

        public DogModel Model { get; set; }
        public int GenderValue { get; set; }

        [Inject]
        public BenjiAPIClient Client { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        public IEnumerable<GenderModel> Genders;

        public void Change(object value)
        {
            try
            {
                //NotificationService.Notify(NotificationSeverity.Info, "Working", value.ToString());
                Model.Gender = (Gender)value;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Failed", ex.Message, 6000);
            }
            //var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;

            //events.Add(DateTime.Now, $"{name} value changed to {str}");
        }

        protected override async Task OnInitializedAsync()
        {
            var myDog = await Client.GetDefaultDog();
            Model = new DogModel(myDog);
            GenderValue = (int)myDog.Gender;
        }

        public async Task HandleValidSubmit()
        {
            HttpResponseMessage result = null;
            if (Model.DogId.Value == 0)
            {
                var request = new DogCreateRequest();
                request.Dog.DogId = Model.DogId;
                request.Dog.Name = Model.Name;
                request.Dog.AdoptedDate = Model.AdoptedDate;
                request.Dog.Birthdate = Model.Birthdate;
                request.Dog.Gender = Model.Gender;
                request.Dog.Created = Model.Created;
                request.Dog.Modified = Model.Modified;
                request.Dog.Deleted = Model.Deleted;
                result = await Client.CreateDog(request);
            }
            else
            {
                var request = new DogUpdateRequest();
                request.Dog.DogId = Model.DogId;
                request.Dog.Name = Model.Name;
                request.Dog.AdoptedDate = Model.AdoptedDate;
                request.Dog.Birthdate = Model.Birthdate;
                request.Dog.Gender = Model.Gender;
                request.Dog.Created = Model.Created;
                request.Dog.Modified = Model.Modified;
                request.Dog.Deleted = Model.Deleted;
                result = await Client.UpdateDog(request);
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
    }
}