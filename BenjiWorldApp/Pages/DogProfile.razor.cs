using Microsoft.AspNetCore.Components;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace BenjiWorldApp.Pages
{
    public class DogProfileBase : ComponentBase
    {
        public DogProfileBase()
        {
            CreatedDate = DateTime.Now;
            Model = new DogModel();
        }

        public DogModel Model { get; set; }
       
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public string Name { get; set; }
        [Inject]
        public BenjiAPIClient Client { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var someDog = await Client.GetDog(1);
            Name = someDog.Name;
        }
        public async Task HandleValidSubmit()
        {
        }

    }
}
