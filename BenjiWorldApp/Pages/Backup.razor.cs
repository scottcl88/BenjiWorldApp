using DataExtensions;
using Microsoft.AspNetCore.Components;
using Models.Shared;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BenjiWorldApp.Pages
{

    public class BackupBase : ComponentBase
    {
        public BackupBase()
        {
        }

        [Inject]
        public BenjiAPIClient Client { get; set; }
        public DateTime LastBackup { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            LastBackup = await Client.GetLastBackup();
        }
        public async Task AddBackup()
        {
            var result = await Client.AddBackup();
            if (result)
            {
                NotificationService.Notify(NotificationSeverity.Success, "Succesfully added backup");
                LastBackup = await Client.GetLastBackup();
            }
            else
            {
                NotificationService.Notify(NotificationSeverity.Error, "Backup failed");
            }
        }
    }
}