using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenjiWorldApp.Shared
{
    public class MainLayoutBase: LayoutComponentBase
    {
        public MarkupString RelativeTimeA = new MarkupString("");

        private System.Timers.Timer _timer;

        public DateTime Birthdate { get; set; }
        [Inject]
        protected IJSRuntime JsRunTime { get; set; }
        [Inject]
        public BenjiAPIClient Client { get; set; }
        protected override void OnInitialized()
        {
            _timer = new System.Timers.Timer(1 * 1000 * 60);

            _timer.Elapsed += async (s, e) =>
            {
                GetRelativeTime();
                await InvokeAsync(StateHasChanged);
            };
            Task.Run(async () =>
            {
                var myDog = await Client.GetDefaultDog();
                Birthdate = myDog.Birthdate ?? DateTime.Now;
                GetRelativeTime();
                _timer.Enabled = true;
            });
        }
        async void GetRelativeTime()
        {
            var dateTimeStr = await JsRunTime.InvokeAsync<string>("MyLib.GetRelativeTime", Birthdate);

            RelativeTimeA = new MarkupString(dateTimeStr);

            StateHasChanged();
        }
    }
}
