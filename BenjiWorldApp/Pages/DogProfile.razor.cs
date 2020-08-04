using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BenjiWorldApp.Pages
{
    public class DogProfileBase : ComponentBase
    {
        public DogProfileBase()
        {
            CreatedDate = DateTime.Now;
        }

        //public DogModel Model { get; set; }
       
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public string Name { get; set; }

        public void HandleValidSubmit()
        {

        }

    }
}
