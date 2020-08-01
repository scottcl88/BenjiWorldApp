using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenjiWorldApp.Pages
{
    public class HealthBase : ComponentBase
    {
        public HealthBase()
        {
            CreatedDate = DateTime.Now;
        }
       
        public DateTime CreatedDate { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Height { get; set; }
        public decimal? Length { get; set; }
        public bool FromVet { get; set; }

    }
}
