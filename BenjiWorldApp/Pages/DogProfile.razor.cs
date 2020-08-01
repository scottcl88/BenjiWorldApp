using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
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
       
        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }

    }
}
