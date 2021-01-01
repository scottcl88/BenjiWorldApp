using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;

namespace BenjiWorldApp.Shared
{
    public class RelativeTimeBase : ComponentBase
    {
        public MarkupString RelativeTimeA = new MarkupString("");

        private System.Timers.Timer _timer;

        [Inject]
        protected IJSRuntime JsRunTime { get; set; }

        [Parameter]
        public DateTime RelativeDateTime { get; set; }

        [Parameter]
        public double Interval { get; set; }

        protected override void OnInitialized()
        {
            GetRelativeTime();
            _timer = new System.Timers.Timer(1 * 1000 * 60);

            _timer.Elapsed += async (s, e) =>
            {
                GetRelativeTime();
                await InvokeAsync(StateHasChanged);
            };
            _timer.Enabled = true;
        }

        private async void GetRelativeTime()
        {
            var dateTimeStr = await JsRunTime.InvokeAsync<string>("MyLib.GetRelativeTime", RelativeDateTime);

            RelativeTimeA = new MarkupString(dateTimeStr);

            StateHasChanged();
        }
    }
}